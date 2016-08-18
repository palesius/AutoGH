<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMusic
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
        Me.lblSong = New System.Windows.Forms.Label()
        Me.lblGame = New System.Windows.Forms.Label()
        Me.lblTrack = New System.Windows.Forms.Label()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.cbGame = New System.Windows.Forms.ComboBox()
        Me.cbSong = New System.Windows.Forms.ComboBox()
        Me.cbTrack0 = New System.Windows.Forms.ComboBox()
        Me.cbLevel0 = New System.Windows.Forms.ComboBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbSong = New System.Windows.Forms.GroupBox()
        Me.btnStar = New System.Windows.Forms.Button()
        Me.btnVocal = New System.Windows.Forms.Button()
        Me.dbParts = New System.Windows.Forms.GroupBox()
        Me.cbTrack3 = New System.Windows.Forms.ComboBox()
        Me.cbLevel3 = New System.Windows.Forms.ComboBox()
        Me.cbTrack2 = New System.Windows.Forms.ComboBox()
        Me.cbLevel2 = New System.Windows.Forms.ComboBox()
        Me.cbTrack1 = New System.Windows.Forms.ComboBox()
        Me.cbLevel1 = New System.Windows.Forms.ComboBox()
        Me.cbManual = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.gbSong.SuspendLayout()
        Me.dbParts.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSong
        '
        Me.lblSong.AutoSize = True
        Me.lblSong.Location = New System.Drawing.Point(178, 22)
        Me.lblSong.Name = "lblSong"
        Me.lblSong.Size = New System.Drawing.Size(32, 13)
        Me.lblSong.TabIndex = 1
        Me.lblSong.Text = "Song"
        '
        'lblGame
        '
        Me.lblGame.AutoSize = True
        Me.lblGame.Location = New System.Drawing.Point(6, 22)
        Me.lblGame.Name = "lblGame"
        Me.lblGame.Size = New System.Drawing.Size(35, 13)
        Me.lblGame.TabIndex = 2
        Me.lblGame.Text = "Game"
        '
        'lblTrack
        '
        Me.lblTrack.AutoSize = True
        Me.lblTrack.Location = New System.Drawing.Point(6, 22)
        Me.lblTrack.Name = "lblTrack"
        Me.lblTrack.Size = New System.Drawing.Size(35, 13)
        Me.lblTrack.TabIndex = 3
        Me.lblTrack.Text = "Track"
        '
        'lblLevel
        '
        Me.lblLevel.AutoSize = True
        Me.lblLevel.Location = New System.Drawing.Point(6, 49)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(33, 13)
        Me.lblLevel.TabIndex = 4
        Me.lblLevel.Text = "Level"
        '
        'cbGame
        '
        Me.cbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGame.FormattingEnabled = True
        Me.cbGame.Location = New System.Drawing.Point(47, 19)
        Me.cbGame.Name = "cbGame"
        Me.cbGame.Size = New System.Drawing.Size(125, 21)
        Me.cbGame.TabIndex = 10
        '
        'cbSong
        '
        Me.cbSong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cbSong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSong.FormattingEnabled = True
        Me.cbSong.Location = New System.Drawing.Point(216, 19)
        Me.cbSong.Name = "cbSong"
        Me.cbSong.Size = New System.Drawing.Size(218, 21)
        Me.cbSong.TabIndex = 12
        '
        'cbTrack0
        '
        Me.cbTrack0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTrack0.FormattingEnabled = True
        Me.cbTrack0.Location = New System.Drawing.Point(47, 19)
        Me.cbTrack0.Name = "cbTrack0"
        Me.cbTrack0.Size = New System.Drawing.Size(125, 21)
        Me.cbTrack0.TabIndex = 14
        '
        'cbLevel0
        '
        Me.cbLevel0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLevel0.FormattingEnabled = True
        Me.cbLevel0.Location = New System.Drawing.Point(47, 46)
        Me.cbLevel0.Name = "cbLevel0"
        Me.cbLevel0.Size = New System.Drawing.Size(125, 21)
        Me.cbLevel0.TabIndex = 16
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(510, 149)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(77, 21)
        Me.btnOk.TabIndex = 25
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(12, 149)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 21)
        Me.btnCancel.TabIndex = 26
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbSong
        '
        Me.gbSong.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSong.Controls.Add(Me.btnStar)
        Me.gbSong.Controls.Add(Me.btnVocal)
        Me.gbSong.Controls.Add(Me.lblGame)
        Me.gbSong.Controls.Add(Me.lblSong)
        Me.gbSong.Controls.Add(Me.cbGame)
        Me.gbSong.Controls.Add(Me.cbSong)
        Me.gbSong.Location = New System.Drawing.Point(12, 12)
        Me.gbSong.Name = "gbSong"
        Me.gbSong.Size = New System.Drawing.Size(575, 50)
        Me.gbSong.TabIndex = 28
        Me.gbSong.TabStop = False
        Me.gbSong.Text = "Song"
        '
        'btnStar
        '
        Me.btnStar.Location = New System.Drawing.Point(523, 18)
        Me.btnStar.Name = "btnStar"
        Me.btnStar.Size = New System.Drawing.Size(42, 21)
        Me.btnStar.TabIndex = 31
        Me.btnStar.Text = "SP"
        Me.btnStar.UseVisualStyleBackColor = True
        '
        'btnVocal
        '
        Me.btnVocal.Location = New System.Drawing.Point(440, 19)
        Me.btnVocal.Name = "btnVocal"
        Me.btnVocal.Size = New System.Drawing.Size(77, 21)
        Me.btnVocal.TabIndex = 30
        Me.btnVocal.Text = "Play Vocal"
        Me.btnVocal.UseVisualStyleBackColor = True
        '
        'dbParts
        '
        Me.dbParts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dbParts.Controls.Add(Me.cbTrack3)
        Me.dbParts.Controls.Add(Me.cbLevel3)
        Me.dbParts.Controls.Add(Me.cbTrack2)
        Me.dbParts.Controls.Add(Me.cbLevel2)
        Me.dbParts.Controls.Add(Me.cbTrack1)
        Me.dbParts.Controls.Add(Me.cbLevel1)
        Me.dbParts.Controls.Add(Me.lblTrack)
        Me.dbParts.Controls.Add(Me.lblLevel)
        Me.dbParts.Controls.Add(Me.cbTrack0)
        Me.dbParts.Controls.Add(Me.cbLevel0)
        Me.dbParts.Location = New System.Drawing.Point(12, 68)
        Me.dbParts.Name = "dbParts"
        Me.dbParts.Size = New System.Drawing.Size(575, 75)
        Me.dbParts.TabIndex = 29
        Me.dbParts.TabStop = False
        Me.dbParts.Text = "Parts"
        '
        'cbTrack3
        '
        Me.cbTrack3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTrack3.FormattingEnabled = True
        Me.cbTrack3.Location = New System.Drawing.Point(440, 19)
        Me.cbTrack3.Name = "cbTrack3"
        Me.cbTrack3.Size = New System.Drawing.Size(125, 21)
        Me.cbTrack3.TabIndex = 31
        '
        'cbLevel3
        '
        Me.cbLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLevel3.FormattingEnabled = True
        Me.cbLevel3.Location = New System.Drawing.Point(440, 46)
        Me.cbLevel3.Name = "cbLevel3"
        Me.cbLevel3.Size = New System.Drawing.Size(125, 21)
        Me.cbLevel3.TabIndex = 32
        '
        'cbTrack2
        '
        Me.cbTrack2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTrack2.FormattingEnabled = True
        Me.cbTrack2.Location = New System.Drawing.Point(309, 19)
        Me.cbTrack2.Name = "cbTrack2"
        Me.cbTrack2.Size = New System.Drawing.Size(125, 21)
        Me.cbTrack2.TabIndex = 28
        '
        'cbLevel2
        '
        Me.cbLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLevel2.FormattingEnabled = True
        Me.cbLevel2.Location = New System.Drawing.Point(309, 46)
        Me.cbLevel2.Name = "cbLevel2"
        Me.cbLevel2.Size = New System.Drawing.Size(125, 21)
        Me.cbLevel2.TabIndex = 29
        '
        'cbTrack1
        '
        Me.cbTrack1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTrack1.FormattingEnabled = True
        Me.cbTrack1.Location = New System.Drawing.Point(178, 19)
        Me.cbTrack1.Name = "cbTrack1"
        Me.cbTrack1.Size = New System.Drawing.Size(125, 21)
        Me.cbTrack1.TabIndex = 25
        '
        'cbLevel1
        '
        Me.cbLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLevel1.FormattingEnabled = True
        Me.cbLevel1.Location = New System.Drawing.Point(178, 46)
        Me.cbLevel1.Name = "cbLevel1"
        Me.cbLevel1.Size = New System.Drawing.Size(125, 21)
        Me.cbLevel1.TabIndex = 26
        '
        'cbManual
        '
        Me.cbManual.AutoSize = True
        Me.cbManual.Location = New System.Drawing.Point(418, 152)
        Me.cbManual.Name = "cbManual"
        Me.cbManual.Size = New System.Drawing.Size(86, 17)
        Me.cbManual.TabIndex = 31
        Me.cbManual.Text = "Manual Start"
        Me.cbManual.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(146, 149)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 32
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMusic
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(599, 176)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cbManual)
        Me.Controls.Add(Me.gbSong)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.dbParts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMusic"
        Me.Text = "Song Selection"
        Me.gbSong.ResumeLayout(False)
        Me.gbSong.PerformLayout()
        Me.dbParts.ResumeLayout(False)
        Me.dbParts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSong As System.Windows.Forms.Label
    Friend WithEvents lblGame As System.Windows.Forms.Label
    Friend WithEvents lblTrack As System.Windows.Forms.Label
    Friend WithEvents lblLevel As System.Windows.Forms.Label
    Friend WithEvents cbGame As System.Windows.Forms.ComboBox
    Friend WithEvents cbSong As System.Windows.Forms.ComboBox
    Friend WithEvents cbTrack0 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel0 As System.Windows.Forms.ComboBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbSong As System.Windows.Forms.GroupBox
    Friend WithEvents dbParts As System.Windows.Forms.GroupBox
    Friend WithEvents cbTrack3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbTrack2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbTrack1 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnVocal As System.Windows.Forms.Button
    Friend WithEvents cbManual As System.Windows.Forms.CheckBox
    Friend WithEvents btnStar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
