<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKiosCapture
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKiosCapture))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pcBG = New System.Windows.Forms.PictureBox()
        Me.pbDisplay = New System.Windows.Forms.PictureBox()
        Me.pcImage = New System.Windows.Forms.PictureBox()
        Me.pbClose = New System.Windows.Forms.PictureBox()
        Me.pbSave = New System.Windows.Forms.PictureBox()
        Me.pbCapture = New System.Windows.Forms.PictureBox()
        Me.pbReCapture = New System.Windows.Forms.PictureBox()
        Me.pb = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.pcBG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbReCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(464, 69)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "cap"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(560, 69)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 55
        Me.Button2.Text = "save"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(655, 69)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 56
        Me.Button3.Text = "<"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(464, 98)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 57
        Me.Button4.Text = "recap"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.pcBG)
        Me.Panel1.Controls.Add(Me.pbDisplay)
        Me.Panel1.Location = New System.Drawing.Point(74, 418)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 480)
        Me.Panel1.TabIndex = 58
        '
        'pcBG
        '
        Me.pcBG.BackColor = System.Drawing.Color.Transparent
        Me.pcBG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pcBG.Image = Global.kioskRFID.My.Resources.Resources.ss_rfid_logo_01
        Me.pcBG.Location = New System.Drawing.Point(296, 3)
        Me.pcBG.Name = "pcBG"
        Me.pcBG.Size = New System.Drawing.Size(341, 259)
        Me.pcBG.TabIndex = 54
        Me.pcBG.TabStop = False
        Me.pcBG.Visible = False
        '
        'pbDisplay
        '
        Me.pbDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbDisplay.BackColor = System.Drawing.Color.Transparent
        Me.pbDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbDisplay.Location = New System.Drawing.Point(0, 0)
        Me.pbDisplay.Name = "pbDisplay"
        Me.pbDisplay.Size = New System.Drawing.Size(640, 480)
        Me.pbDisplay.TabIndex = 53
        Me.pbDisplay.TabStop = False
        '
        'pcImage
        '
        Me.pcImage.BackColor = System.Drawing.Color.White
        Me.pcImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pcImage.Location = New System.Drawing.Point(74, 418)
        Me.pcImage.Name = "pcImage"
        Me.pcImage.Size = New System.Drawing.Size(640, 480)
        Me.pcImage.TabIndex = 47
        Me.pcImage.TabStop = False
        '
        'pbClose
        '
        Me.pbClose.BackgroundImage = CType(resources.GetObject("pbClose.BackgroundImage"), System.Drawing.Image)
        Me.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbClose.Location = New System.Drawing.Point(732, 12)
        Me.pbClose.Name = "pbClose"
        Me.pbClose.Size = New System.Drawing.Size(40, 35)
        Me.pbClose.TabIndex = 37
        Me.pbClose.TabStop = False
        '
        'pbSave
        '
        Me.pbSave.BackColor = System.Drawing.Color.Transparent
        Me.pbSave.BackgroundImage = Global.kioskRFID.My.Resources.Resources.OK_share_btn_011
        Me.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbSave.Location = New System.Drawing.Point(438, 922)
        Me.pbSave.Name = "pbSave"
        Me.pbSave.Size = New System.Drawing.Size(140, 50)
        Me.pbSave.TabIndex = 36
        Me.pbSave.TabStop = False
        Me.pbSave.Visible = False
        '
        'pbCapture
        '
        Me.pbCapture.BackColor = System.Drawing.Color.Transparent
        Me.pbCapture.BackgroundImage = Global.kioskRFID.My.Resources.Resources.capture_011
        Me.pbCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbCapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbCapture.Location = New System.Drawing.Point(280, 922)
        Me.pbCapture.Name = "pbCapture"
        Me.pbCapture.Size = New System.Drawing.Size(140, 50)
        Me.pbCapture.TabIndex = 35
        Me.pbCapture.TabStop = False
        '
        'pbReCapture
        '
        Me.pbReCapture.BackColor = System.Drawing.Color.Transparent
        Me.pbReCapture.BackgroundImage = Global.kioskRFID.My.Resources.Resources.Recapture_btn_01
        Me.pbReCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbReCapture.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbReCapture.Location = New System.Drawing.Point(280, 922)
        Me.pbReCapture.Name = "pbReCapture"
        Me.pbReCapture.Size = New System.Drawing.Size(140, 50)
        Me.pbReCapture.TabIndex = 34
        Me.pbReCapture.TabStop = False
        Me.pbReCapture.Visible = False
        '
        'pb
        '
        Me.pb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb.BackgroundImage = Global.kioskRFID.My.Resources.Resources.BG_04
        Me.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb.Location = New System.Drawing.Point(-4, 0)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(777, 1024)
        Me.pb.TabIndex = 31
        Me.pb.TabStop = False
        '
        'frmKiosCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 780)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pcImage)
        Me.Controls.Add(Me.pbClose)
        Me.Controls.Add(Me.pbSave)
        Me.Controls.Add(Me.pbCapture)
        Me.Controls.Add(Me.pbReCapture)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmKiosCapture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Capture"
        Me.TransparencyKey = System.Drawing.Color.Red
        Me.Panel1.ResumeLayout(False)
        CType(Me.pcBG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbReCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pb As System.Windows.Forms.PictureBox
    Friend WithEvents pbSave As System.Windows.Forms.PictureBox
    Friend WithEvents pbCapture As System.Windows.Forms.PictureBox
    Friend WithEvents pbReCapture As System.Windows.Forms.PictureBox
    Friend WithEvents pbClose As System.Windows.Forms.PictureBox
    Friend WithEvents pcImage As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pcBG As System.Windows.Forms.PictureBox
    Friend WithEvents pbDisplay As System.Windows.Forms.PictureBox

End Class
