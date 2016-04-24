Imports System.IO
Imports System.Data
Imports AMS
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports Facebook
Imports Newtonsoft.Json.Linq
Imports System.Configuration

Public Class frmKiosCapture
    Private ConfigINI As Profile.Ini
    Private webcam As WebCam
    Dim _Number As String
    Dim _IsCapture As Boolean = False
    Dim CamIndex As Integer = 0
    Private _accessToken As String
    Public Property accessToken() As String
        Get
            Return _accessToken
        End Get
        Set(ByVal value As String)
            _accessToken = value
        End Set
    End Property

    Public Sub SetAccessToken(ByVal accessToken As String)
        Me.accessToken = accessToken
    End Sub

    Private waitingForm As frmWaiting
    Private Delegate Sub CloseWaitingFormCallback()
    Private Delegate Sub ShowMessageBoxCallback(ByVal str As String, ByVal caption As String, ByVal button As MessageBoxButtons, ByVal icon As MessageBoxIcon)


    Public WriteOnly Property Number() As String
        Set(ByVal value As String)
            _Number = value
        End Set
    End Property

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConfigINI = New Profile.Ini(Application.StartupPath & "\kiosk.ini")
        pcImage.BringToFront()
        ' pcBG.Parent = pcImage
        Try
            webcam = New WebCam()
            webcam.InitializeWebCam(pcImage)
            webcam.Start(CamIndex)

            Dim FormWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim FormHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            Dim CaptureHeight As Integer = ConfigINI.GetValue("CaptureImage", "CaptureHeight", "")
            Dim CaptureWidth As Integer = ConfigINI.GetValue("CaptureImage", "CaptureWidth", "")
            Me.Size = New Size(FormWidth, FormHeight)
            pbReCapture.Location = New Point(pbCapture.Location)
            pbClose.Location = New Point(FormWidth - 40, 12)

            webcam.CaptureHeight(CaptureHeight)
            webcam.CaptureWidth(CaptureWidth)

            pbCapture.BringToFront()

        Catch ex As Exception
            Me.Close()
        End Try

    End Sub

    Private Sub pbReCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbReCapture.Click
        webcam.Start(CamIndex)
        pbCapture.Visible = True
        pbReCapture.Visible = False
        pbSave.Visible = False

        pcImage.BringToFront()
        pbCapture.BringToFront()

        Dim FileName As String = frmRegister.ID & ".jpg"
        Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
        File.Delete(Path & FileName)
    End Sub

    Private Sub pbCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCapture.Click
        pbReCapture.Visible = True
        pbSave.Visible = True
        pbCapture.Visible = False

        Panel1.BringToFront()
        CaptureImage(frmRegister.ID)
        webcam.Stop()

        CombineImage(frmRegister.ID)

        _IsCapture = True
    End Sub

    Sub CaptureImage(ByVal id As String)

        Dim FileName As String = id & ".jpg"
        Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
        If Directory.Exists(Path) = False Then
            Directory.CreateDirectory(Path)
        End If
        Dim Path1 As String = Path & FileName
        ' pcImage.Image = CaptureScreen()
        pcImage.Image.Save(Path1, System.Drawing.Imaging.ImageFormat.Jpeg)


        Threading.Thread.Sleep(1000)
        Dim bipimag As New MemoryStream(File.ReadAllBytes(Path1))
        Dim imag As Image = New Bitmap(bipimag)
        pbDisplay.BackgroundImageLayout = ImageLayout.Stretch
        pbDisplay.Image = imag
    End Sub


    Private Sub pbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbSave.Click
        If _IsCapture = False Then

            Exit Sub
        End If

        Try
            '\\192.168.1.63\D\_TestDesImage
            Dim FileName As String = frmRegister.ID & ".jpg"
            Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
            Dim toPath As String = ConfigINI.GetValue("CaptureImage", "topath", "")

            If Directory.Exists(toPath) = False Then
                Directory.CreateDirectory(toPath)
            End If

            If File.Exists(toPath & FileName) Then
                File.Delete(toPath & FileName)
            End If

            File.Copy(Path & FileName, toPath & FileName)
            File.Delete(Path & FileName)

            _IsCapture = False
            pbReCapture.Visible = False
            pbCapture.Visible = True
            pbSave.Visible = False

            Try
                Dim t As New System.Threading.Thread(AddressOf PostImageToFB)
                t.Start()
            Catch ex As Exception

            End Try


        Catch ex As Exception
            'frmDialogMsg.lblText.Text = "บันทึกรูปไม่สำเร็จ"
            'frmDialogMsg.ShowDialog()
        End Try
        frmLoading.Show()
        Me.Close()

    End Sub

    Private Sub pbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbClose.Click
        _IsCapture = False

        pbReCapture.Visible = False
        pbCapture.Visible = True
        pbSave.Visible = False

        webcam.Stop()
        System.Environment.Exit(0)
    End Sub

    Private Sub frmKiosCapture_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub CombineImage(ByVal id As String)
        Dim FileName As String = id & ".jpg"
        ' Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")

        Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
        Using character As Image = pbDisplay.Image
            Using bg As Image = New Bitmap(pcBG.Image)

                Using newImage As New Bitmap(640, 480)
                    Using canvas As System.Drawing.Graphics = Graphics.FromImage(newImage)
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic
                        canvas.DrawImage(character, New Rectangle(0, 0, 800, pbDisplay.Size.Height), New Rectangle(0, 0, 800, character.Height), GraphicsUnit.Pixel)


                        canvas.DrawImage(bg, New Rectangle(300, 340, pcBG.Size.Width, pcBG.Size.Height), New Rectangle(0, 0, pcBG.Width, pcBG.Height), GraphicsUnit.Pixel)
                        canvas.Save()
                        If Not System.IO.Directory.Exists(Path) Then
                            System.IO.Directory.CreateDirectory(Path)
                        End If

                        newImage.Save(Path & FileName, ImageFormat.Png)
                        'canvas.Dispose()
                    End Using
                End Using
            End Using
        End Using

        Dim bipimag As New MemoryStream(File.ReadAllBytes(Path & FileName))
        Dim imag2 As Image = New Bitmap(bipimag)
        pbDisplay.BackgroundImageLayout = ImageLayout.Stretch
        pbDisplay.Image = imag2


    End Sub

    Public Function CaptureScreen() As Bitmap

        Dim Width As Integer = 640
        Dim Height As Integer = 480
        Dim x As Integer = 74
        Dim y As Integer = 418

        Dim tempBmp As New Bitmap(Width, _
                          Height, _
                          System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        Dim grp As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(tempBmp)
        grp.CopyFromScreen(x, y, _
                           0, 0, Screen.PrimaryScreen.Bounds.Size, _
                           System.Drawing.CopyPixelOperation.SourceCopy)

        Return tempBmp
    End Function



#Region "FaceBook"
    Private Sub PostImageToFB()
        Dim ms As New MemoryStream()
        pbDisplay.Image.Save(ms, ImageFormat.Jpeg)
        Dim mediaObject As New FacebookMediaObject()
        mediaObject.ContentType = "image/jpeg"
        mediaObject.FileName = "RFID.jpg"
        mediaObject.SetValue(ms.ToArray())
        ms.Dispose()
        Dim fbParams As New Dictionary(Of String, Object)
        fbParams("req_perms") = "publish_stream"
        fbParams("scope") = "publish_stream"
        fbParams("source") = mediaObject
        Dim fbClient As New FacebookClient(GetPageAccessToken(Me.accessToken))
        fbClient.PostAsync("/" + ConfigurationManager.AppSettings("pageId") + "/photos", fbParams)
    End Sub


    Public Shared Function GetPageAccessToken(ByVal userAccessToken As String) As String
        Dim fbClient As New FacebookClient(userAccessToken)
        fbClient.AppId = ConfigurationManager.AppSettings("appId").ToString()
        Dim fbParams As New Dictionary(Of String, Object)
        Dim publishedResponse As JsonObject = DirectCast(fbClient.Get("/me/accounts", fbParams), JsonObject)
        Dim data As JArray = JArray.Parse(publishedResponse("data").ToString())
        For Each account In data
            If (account("id") = ConfigurationManager.AppSettings("pageId")) Then
                Return account("access_token")
            End If
        Next
        Return String.Empty
    End Function
#End Region



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        pbReCapture.Visible = True
        pbSave.Visible = True
        pbCapture.Visible = False

        Panel1.BringToFront()
        CaptureImage(frmRegister.ID)
        webcam.Stop()

        CombineImage(frmRegister.ID)

        _IsCapture = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If _IsCapture = False Then

            Exit Sub
        End If

        Try
            '\\192.168.1.63\D\_TestDesImage
            Dim FileName As String = frmRegister.ID & ".jpg"
            Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
            Dim toPath As String = ConfigINI.GetValue("CaptureImage", "topath", "")

            If Directory.Exists(toPath) = False Then
                Directory.CreateDirectory(toPath)
            End If

            If File.Exists(toPath & FileName) Then
                File.Delete(toPath & FileName)
            End If

            File.Copy(Path & FileName, toPath & FileName)
            File.Delete(Path & FileName)

            _IsCapture = False
            pbReCapture.Visible = False
            pbCapture.Visible = True
            pbSave.Visible = False

            'Try
            '    Dim t As New System.Threading.Thread(AddressOf PostImageToFB)
            '    t.Start()
            'Catch ex As Exception

            'End Try


        Catch ex As Exception
            'frmDialogMsg.lblText.Text = "บันทึกรูปไม่สำเร็จ"
            'frmDialogMsg.ShowDialog()
        End Try
        frmLoading.Show()
        Me.Close()


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        webcam.Start(CamIndex)
        pbCapture.Visible = True
        pbReCapture.Visible = False
        pbSave.Visible = False

        pcImage.BringToFront()
        pbCapture.BringToFront()

        Dim FileName As String = frmRegister.ID & ".jpg"
        Dim Path As String = ConfigINI.GetValue("CaptureImage", "path", "")
        File.Delete(Path & FileName)
    End Sub
End Class
