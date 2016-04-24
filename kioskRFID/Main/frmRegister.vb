Imports System.Data
Imports System.Data.SqlClient
Imports kioskRFID.Org.Mentalis.Files
Imports System.Drawing.Printing
Imports GenCode128
Imports System.Globalization
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class frmRegister
    Dim tmpIDCard As String
    Dim conn As New SqlConnection
    Dim _id As String

    Public Property ID() As String
        Get
            ID = _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Private Sub frmRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Set Sub Delegate
            myUpdateBoxThaiID = AddressOf WriteBoxThaiID


            'Open SerialPort
            Dim iniReader1 As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\kiosk.ini")
            Dim port As String
            port = iniReader1.ReadString("setting", "port")

            If Not SerialPort1.IsOpen Then
                SerialPort1.PortName = port
                SerialPort1.BaudRate = 115200
                SerialPort1.Open()
                SerialPort1.DiscardInBuffer()
            Else
                SerialPort1.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Function GetInfo(ByVal Number As String) As DataTable
        Dim iniReader1 As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\kiosk.ini")
        Dim servername, Fromdatabasename, dbUserid, dbUserPassword, strconn As String
        servername = iniReader1.ReadString("Database", "ServerName")
        dbUserid = iniReader1.ReadString("Database", "Username")
        dbUserPassword = iniReader1.ReadString("Database", "Password")
        Fromdatabasename = iniReader1.ReadString("Database", "Database")
        strconn = "Data Source=" & servername & _
                      ";Persist Security Info=True;Password=" & dbUserPassword & _
                      ";User ID=" & dbUserid & ";Initial Catalog=" & Fromdatabasename
        conn = New SqlConnection(strconn)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim sql As String = "Select ID,contact_mobile_no,isregister_contact,invite_mobile_no1," & _
            " isregister_invite1,invite_mobile_no2,isregister_invite2,invite_idcard1,invite_idcard2 " & _
            " from TS_REGISTER Where replace(replace(company_telno,'-',''),' ','') ='" & Number & "' or replace(replace(invite_mobile_no1,'-',''),' ' ,'') ='" & Number & "' or replace(replace(invite_mobile_no2,'-',''),' ','') ='" & Number & _
            "' or invite_idcard1='" & Number & "' or invite_idcard2='" & Number & "' or replace(replace(contact_mobile_no,'-',''),' ','')  ='" & Number & "'"
        Dim myadapter As New SqlDataAdapter(sql, conn)
        Dim dt As New DataTable
        myadapter.Fill(dt)
        If dt.Rows.Count > 0 Then
            _id = dt.Rows(0).Item("ID").ToString
        Else
            _id = 0
        End If
        conn.Close()
        Return dt
    End Function

    Function UpdateStatusRegister(id As String, fieldupdate As String) As Boolean
        Dim iniReader1 As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\kiosk.ini")
        Dim servername, Fromdatabasename, dbUserid, dbUserPassword, strconn As String
        servername = iniReader1.ReadString("Database", "ServerName")
        dbUserid = iniReader1.ReadString("Database", "Username")
        dbUserPassword = iniReader1.ReadString("Database", "Password")
        Fromdatabasename = iniReader1.ReadString("Database", "Database")
        strconn = "Data Source=" & servername & _
                      ";Persist Security Info=True;Password=" & dbUserPassword & _
                      ";User ID=" & dbUserid & ";Initial Catalog=" & Fromdatabasename
        Dim objConn As New SqlConnection(strconn)
        Try
            If objConn.State = ConnectionState.Closed Then objConn.Open()
            Dim sql As String = "Update TS_REGISTER set " & fieldupdate & "='Y' where id='" & id & "'"
            Dim cmd As New SqlCommand(sql, objConn)
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            objConn.Close()
            Return False
        End Try

    End Function

    Sub OK()
        Try

            If txtNumber.Text.Trim = "" Then
                frmDialogMsg.lblText.Text = "กรุณาระบุหมายเลขที่ต้องการลงทะเบียนค่ะ"
                frmDialogMsg.ShowDialog()
                Exit Sub
            End If

            Dim dt As New DataTable
            dt = GetInfo(Replace(txtNumber.Text, "-", ""))
            If dt.Rows.Count > 0 Then
                If Replace(txtNumber.Text, "-", "") = Replace(Replace(dt.Rows(0)("contact_mobile_no").ToString, "-", ""), " ", "") Then
                    If dt.Rows(0)("isregister_contact").ToString = "Y" Then
                        frmDialogMsg.lblText.Text = "ขออภัยค่ะ ท่านได้ลงทะเบียนในระบบเรียบร้อยแล้ว"
                        frmDialogMsg.ShowDialog()
                        txtNumber.Text = ""
                        Exit Sub
                    End If
                End If

                If Replace(txtNumber.Text, "-", "") = Replace(Replace(dt.Rows(0)("invite_mobile_no1").ToString, "-", ""), " ", "") Then
                    If dt.Rows(0)("isregister_invite1").ToString = "Y" Then
                        frmDialogMsg.lblText.Text = "ขออภัยค่ะ ท่านได้ลงทะเบียนในระบบเรียบร้อยแล้ว"
                        frmDialogMsg.ShowDialog()
                        txtNumber.Text = ""
                        Exit Sub
                    End If
                End If

                If Replace(txtNumber.Text, "-", "") = Replace(Replace(dt.Rows(0)("invite_mobile_no2").ToString, "-", ""), " ", "") Then
                    If dt.Rows(0)("isregister_invite2").ToString = "Y" Then
                        frmDialogMsg.lblText.Text = "ขออภัยค่ะ ท่านได้ลงทะเบียนในระบบเรียบร้อยแล้ว"
                        frmDialogMsg.ShowDialog()
                        txtNumber.Text = ""
                        Exit Sub
                    End If
                End If

                If Replace(txtNumber.Text, "-", "") = Replace(Replace(dt.Rows(0)("invite_idcard1").ToString, "-", ""), " ", "") Then
                    If dt.Rows(0)("isregister_invite1").ToString = "Y" Then
                        frmDialogMsg.lblText.Text = "ขออภัยค่ะ ท่านได้ลงทะเบียนในระบบเรียบร้อยแล้ว"
                        frmDialogMsg.ShowDialog()
                        txtNumber.Text = ""
                        Exit Sub
                    End If
                End If

                If Replace(txtNumber.Text, "-", "") = Replace(Replace(dt.Rows(0)("invite_idcard2").ToString, "-", ""), " ", "") Then
                    If dt.Rows(0)("isregister_invite2").ToString = "Y" Then
                        frmDialogMsg.lblText.Text = "ขออภัยค่ะ ท่านได้ลงทะเบียนในระบบเรียบร้อยแล้ว"
                        frmDialogMsg.ShowDialog()
                        txtNumber.Text = ""
                        Exit Sub
                    End If
                End If

                If PrintInterest() Then
                    frmDialogMsg.lblText.Text = " ลงทะเบียนเรียบร้อย ขอบคุณค่ะ"
                    frmDialogMsg.ShowDialog()
                    txtNumber.Text = ""
                End If

            Else
                frmDialogMsg.lblText.Text = "ไม่พบข้อมูล กรุณาลงทะเบียนก่อนค่ะ"
                frmDialogMsg.ShowDialog()
                txtNumber.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            frmDialogMsg.lblText.Text = ex.ToString
            frmDialogMsg.ShowDialog()
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OK()
    End Sub


#Region "Print"

    Structure cPrint
        Shared intIndex As Integer = 0
        Shared DsData As DataSet
    End Structure

    Function GetDataForPrint() As DataSet
        Dim Ds As New DataSet
        Dim iniReader1 As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\kiosk.ini")
        Dim servername, Fromdatabasename, dbUserid, dbUserPassword, strconn As String
        servername = iniReader1.ReadString("Database", "ServerName")
        dbUserid = iniReader1.ReadString("Database", "Username")
        dbUserPassword = iniReader1.ReadString("Database", "Password")
        Fromdatabasename = iniReader1.ReadString("Database", "Database")
        strconn = "Data Source=" & servername & _
                      ";Persist Security Info=True;Password=" & dbUserPassword & _
                      ";User ID=" & dbUserid & ";Initial Catalog=" & Fromdatabasename
        Dim objConn As New SqlConnection(strconn)
        Try
            If objConn.State = ConnectionState.Closed Then objConn.Open()
            Dim strSql As String = "Select * from TS_REGISTER where ID ='" & Me.ID & "'   "
            Dim objAdapter As New SqlDataAdapter(strSql, objConn)
            objAdapter.Fill(Ds, "REGISTER")
        Catch ex As Exception
        End Try
        objConn.Close()
        Return Ds
    End Function

    Dim printHeight As Integer

    Function PrintInterest() As Boolean
        Try
            Dim p As New PrintDocument
            p.PrintController = New Printing.StandardPrintController
            Dim ini As New IniReader(INIFName)
            ini.Section = "SETTING"
            p.PrinterSettings.PrinterName = ini.ReadString("PrinterName")
            'Dim IsPrintContact As String = ini.ReadString("printcontact")
            Dim countPrint As Integer = 2
            'If IsPrintContact = "Y" Then
            '    countPrint = 3
            'End If

            cPrint.DsData = GetDataForPrint()

            Dim displayname As String = ""
            Dim displaymobileno As String = ""





            For i As Integer = 1 To countPrint
                cPrint.intIndex = i

                displayname = ""
                displaymobileno = ""
                For Each Drow As DataRow In cPrint.DsData.Tables("REGISTER").Rows
                    Dim contact_person As String = Drow.Item("contact_person").ToString
                    Dim contact_mobile_no As String = Drow.Item("contact_mobile_no").ToString
                    Dim invite_person1 As String = Drow.Item("invite_person1").ToString
                    Dim invite_mobile_no1 As String = Drow.Item("invite_mobile_no1").ToString
                    Dim invite_person2 As String = Drow.Item("invite_person2").ToString
                    Dim invite_mobile_no2 As String = Drow.Item("invite_mobile_no2").ToString
                    Dim invite_idcard1 As String = Drow.Item("invite_idcard1").ToString
                    Dim invite_idcard2 As String = Drow.Item("invite_idcard2").ToString
                    Dim isRegisInvite1 As String = Drow.Item("isregister_invite1").ToString
                    Dim isRegisInvite2 As String = Drow.Item("isregister_invite2").ToString

                    Select Case cPrint.intIndex
                        Case 1
                            If isRegisInvite1 = "N" Then
                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_mobile_no1, "-", ""), " ", "") Then
                                    displayname = invite_person1
                                    displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                End If

                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_idcard1, "-", ""), " ", "") Then
                                    displayname = invite_person1
                                    displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                End If

                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                                    displayname = invite_person1
                                    displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                End If
                            End If
                        Case 2
                            If isRegisInvite2 = "N" Then
                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_mobile_no2, "-", ""), " ", "") Then
                                    displayname = invite_person2
                                    displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                End If

                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_idcard2, "-", ""), " ", "") Then
                                    displayname = invite_person2
                                    displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                End If

                                If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                                    displayname = invite_person2
                                    displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                End If
                            End If
                            'Case 3

                            '    If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                            '        displayname = contact_person
                            '        displaymobileno = Replace(Replace(contact_mobile_no, "-", ""), " ", "")
                            '    End If
                    End Select
                Next

                If displayname <> "" Then
                    AddHandler p.PrintPage, AddressOf PrintDocument1_PrintPage
                    p.Print()
                End If

                lastPrintY = 0
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function GenQRCode(glncode As String) As String
        Try
            Dim a As New WolfSoftware.Library_NET.BarcodeControl
            a.Unlock("Phantom 2008", "WSFCX-0100-100883561")
            a.CurrentCode = 1014
            a.DataToEncode = glncode
            Dim pic As New Bitmap(a.GetCode(300)) 'The bitmap you created
            'pic.SetResolution(50, 50)
            Dim path As String = Application.StartupPath() & "\temp"
            If Directory.Exists(path) = False Then
                Directory.CreateDirectory(path)
            End If

            If File.Exists(path & "\QRCode.jpg") Then
                File.Delete(path & "\QRCode.jpg")
            End If

            pic.Save(path & "\QRCode.jpg", Imaging.ImageFormat.Jpeg)

            Return path & "\QRCode.jpg"
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        SetUpFont()
        lastPrintY = 0

        Try

            Dim Ds As New DataSet
            Ds = cPrint.DsData

            For Each Drow As DataRow In Ds.Tables("REGISTER").Rows
                Dim gln_code As String = Drow.Item("gln_code").ToString
                Dim company_name As String = Drow.Item("company_name").ToString

                Dim contact_person As String = Drow.Item("contact_person").ToString
                Dim contact_mobile_no As String = Drow.Item("contact_mobile_no").ToString
                Dim invite_person1 As String = Drow.Item("invite_person1").ToString
                Dim invite_mobile_no1 As String = Drow.Item("invite_mobile_no1").ToString
                Dim invite_person2 As String = Drow.Item("invite_person2").ToString
                Dim invite_mobile_no2 As String = Drow.Item("invite_mobile_no2").ToString
                Dim invite_idcard1 As String = Drow.Item("invite_idcard1").ToString
                Dim invite_idcard2 As String = Drow.Item("invite_idcard2").ToString
                Dim isRegisInvite1 As String = Drow.Item("isregister_invite1").ToString
                Dim isRegisInvite2 As String = Drow.Item("isregister_invite2").ToString

                Dim type_a As String = Drow.Item("type_a").ToString
                If type_a = "" Then
                    type_a = "B"
                Else
                    type_a = "A"
                End If


                Dim displayname As String = ""
                Dim displaymobileno As String = ""
                Select Case cPrint.intIndex

                    Case 1
                        If isRegisInvite1 = "N" Then
                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_mobile_no1, "-", ""), " ", "") Then
                                displayname = invite_person1
                                displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite1")

                                If Replace(Replace(contact_mobile_no, "-", ""), " ", "") = Replace(Replace(invite_mobile_no1, "-", ""), " ", "") Then
                                    UpdateStatusRegister(Me.ID, "isregister_contact")
                                End If
                                Exit Select
                            End If

                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_idcard1, "-", ""), " ", "") Then
                                displayname = invite_person1
                                displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite1")

                                If Replace(Replace(contact_mobile_no, "-", ""), " ", "") = Replace(Replace(invite_mobile_no1, "-", ""), " ", "") Then
                                    UpdateStatusRegister(Me.ID, "isregister_contact")
                                End If
                                Exit Select
                            End If

                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                                displayname = invite_person1
                                displaymobileno = Replace(Replace(invite_mobile_no1, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite1")
                                UpdateStatusRegister(Me.ID, "isregister_contact")
                            End If
                        End If
                    Case 2
                        If isRegisInvite2 = "N" Then
                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_mobile_no2, "-", ""), " ", "") Then
                                displayname = invite_person2
                                displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite2")

                                If Replace(Replace(contact_mobile_no, "-", ""), " ", "") = Replace(Replace(invite_mobile_no2, "-", ""), " ", "") Then
                                    UpdateStatusRegister(Me.ID, "isregister_contact")
                                End If
                                Exit Select

                            End If

                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(invite_idcard2, "-", ""), " ", "") Then
                                displayname = invite_person2
                                displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite2")

                                If Replace(Replace(contact_mobile_no, "-", ""), " ", "") = Replace(Replace(invite_mobile_no2, "-", ""), " ", "") Then
                                    UpdateStatusRegister(Me.ID, "isregister_contact")
                                End If
                                Exit Select
                            End If

                            If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                                displayname = invite_person2
                                displaymobileno = Replace(Replace(invite_mobile_no2, "-", ""), " ", "")
                                PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                                UpdateStatusRegister(Me.ID, "isregister_invite2")
                                UpdateStatusRegister(Me.ID, "isregister_contact")

                            End If
                        End If
                        'Case 3

                        'If Replace(txtNumber.Text, "-", "") = Replace(Replace(contact_mobile_no, "-", ""), " ", "") Then
                        '    displayname = contact_person
                        '    displaymobileno = Replace(Replace(contact_mobile_no, "-", ""), " ", "")
                        '    PrintText(gln_code, company_name, displayname, displaymobileno, type_a, e)
                        '    UpdateStatusRegister(Me.ID, "isregister_contact")
                        'End If
                End Select

            Next
            Ds.Dispose()
        Catch ex As Exception
        Finally
            'objConn.Close()
        End Try


        With PrintDocument1.DefaultPageSettings
            printHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
        End With

        e.HasMorePages = False

    End Sub

    Sub PrintText(gln_code As String, company_name As String, displayname As String, displaymobileno As String, type_a As String, e As System.Drawing.Printing.PrintPageEventArgs)
        '## QRCode
        Dim obj As New WolfSoftware.Library_NET.BarcodeControl
        obj.Unlock("Phantom 2008", "WSFCX-0100-100883561")
        obj.CurrentCode = 1014
        obj.DataToEncode = gln_code
        Dim pic As New Bitmap(obj.GetCode(300)) 'The bitmap you created
        pic.SetResolution(1080, 1080)
        Dim path As String = Application.StartupPath() & "\temp"
        If Directory.Exists(path) = False Then
            Directory.CreateDirectory(path)
        End If

        If File.Exists(path & "\QRCode.jpg") Then
            File.Delete(path & "\QRCode.jpg")
        End If
        'pic.Save(path & "\QRCode.jpg", Imaging.ImageFormat.Jpeg)
        PictureBox2.Image = pic
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        txtCompany.Text = company_name
        txtGLN.Text = gln_code
        txtName.Text = displayname
        txtTel.Text = displaymobileno
        lblType.Text = type_a

        Dim g As Graphics = Graphics.FromImage(pic)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

        Dim pic2 As New Bitmap(gbTag.ClientRectangle.Width, gbTag.ClientRectangle.Height) 'The bitmap you created
        gbTag.DrawToBitmap(pic2, gbTag.ClientRectangle)
        'pic2.SetResolution(1080, 1080)

        Dim QRCodePath As String = GenQRCode(gln_code)
        PrintImage(pic2, Align.Center, e)
        '## QRCode

    End Sub
#End Region
#Region "btnclick"
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Dim k_len As Integer = 13

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "1"
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "2"
    End Sub


    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "3"
    End Sub

    Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "4"
    End Sub

    Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "5"
    End Sub

    Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "6"
    End Sub

    Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "7"
    End Sub

    Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "8"
    End Sub

    Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "9"
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
        OK()
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click
        If txtNumber.Text.Length = k_len Then Exit Sub
        txtNumber.Text = txtNumber.Text & "0"
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If txtNumber.Text <> "" Then txtNumber.Text = txtNumber.Text.Substring(0, (txtNumber.Text.Length - 1))
    End Sub
#End Region
#Region "IDCard Reader"

    Dim LoopRec As Integer = 0
    Public BuffComPort As Byte() = New Byte(9999) {}
    Public DataCom As String() = New String(8) {}
    Public BuffCom As String = ""
    Public BuffLength As Integer = 0
    Public TimeOut As Integer

    'Create Delegate
    Private Delegate Sub myDelegateOne(ByVal data As String)
    'Create Sub Delegate
    Private myUpdateBoxThaiID As myDelegateOne
    Private Sub timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

            'Open SerialPort
            Dim iniReader1 As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\kiosk.ini")
            Dim port As String
            port = iniReader1.ReadString("setting", "port")

            If Not SerialPort1.IsOpen Then
                SerialPort1.PortName = port
                SerialPort1.BaudRate = 115200
                SerialPort1.Open()
                SerialPort1.DiscardInBuffer()

            End If

            If TimeOut <> 0 Then
                TimeOut += 1
                If TimeOut >= 5 Then
                    If SerialPort1.IsOpen Then
                        BuffLength = 0
                        TimeOut = 0
                        LoopRec = 0
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub serialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try
            If SerialPort1.IsOpen Then
                'Get Length Data Receive
                Dim Len As Integer = SerialPort1.BytesToRead
                TimeOut = 1
                If Len >= 10000 Then
                    Len = 0
                End If
                If BuffLength >= 10000 Then
                    ' Check length data for limit  
                    ' Initial Buffer length 
                    BuffLength = 0
                End If
                SerialPort1.Read(BuffComPort, BuffLength, Len)
                BuffLength += Len
                If BuffLength >= 1 Then
                    'Check Enter Key To Encode Data
                    If Chr(BuffComPort(BuffLength - 1)) = Chr(13) AndAlso LoopRec < 9 Then
                        'Me.Invoke(myUpdateBoxThaiID, "")
                        'Set CodePage To Encode Thai Language
                        BuffCom = Encoding.GetEncoding(&H36A).GetString(BuffComPort, 0, BuffLength)
                        BuffLength = 0
                        SerialPort1.DiscardInBuffer()

                        If BuffCom = "REMOVE" & vbCr Then
                            LoopRec = 0
                            Me.Invoke(myUpdateBoxThaiID, "")
                            txtNumber.Text = ""
                            'pictureBoxPhoto.Image = Nothing
                        Else
                            'Keep Data To Array
                            DataCom(LoopRec) = BuffCom
                            LoopRec += 1
                        End If
                    End If

                    Dim str As String = ""
                    For i As Integer = 0 To DataCom.Length - 1
                        Try
                            'Loop Get Thai ID To Delegate TextBox ThaiID
                            If DataCom(i).Length = 14 Then
                                Me.Invoke(myUpdateBoxThaiID, DataCom(i))
                                SerialPort1.DiscardInBuffer()
                                BuffLength = 0
                                LoopRec = 0

                                str &= DataCom(i)
                            End If
                        Catch ex As Exception
                        End Try

                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Sub Delegate Cotrol TextBoxThaiNAtionID
    Private Sub WriteBoxThaiID(ByVal msg As String)
        txtNumber.Text = msg.Substring(0, 13)

        If msg.Length = 14 Then
            If tmpIDCard <> msg Then
                OK()
                tmpIDCard = msg
            Else
                tmpIDCard = msg
                txtNumber.Text = ""
            End If

        End If
    End Sub
#End Region

End Class