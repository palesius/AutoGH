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
        Me.gbParts = New System.Windows.Forms.GroupBox()
        Me.lblStrum3 = New System.Windows.Forms.Label()
        Me.cmbStrum3 = New System.Windows.Forms.ComboBox()
        Me.lblStrum2 = New System.Windows.Forms.Label()
        Me.cmbStrum2 = New System.Windows.Forms.ComboBox()
        Me.lblStrum1 = New System.Windows.Forms.Label()
        Me.cmbStrum1 = New System.Windows.Forms.ComboBox()
        Me.lblStrum0 = New System.Windows.Forms.Label()
        Me.cmbStrum0 = New System.Windows.Forms.ComboBox()
        Me.chkHOPO3 = New System.Windows.Forms.CheckBox()
        Me.chkLF3 = New System.Windows.Forms.CheckBox()
        Me.chkHOPO2 = New System.Windows.Forms.CheckBox()
        Me.chkLF2 = New System.Windows.Forms.CheckBox()
        Me.chkHOPO1 = New System.Windows.Forms.CheckBox()
        Me.chkLF1 = New System.Windows.Forms.CheckBox()
        Me.chkHOPO0 = New System.Windows.Forms.CheckBox()
        Me.chkLF0 = New System.Windows.Forms.CheckBox()
        Me.cbTrack3 = New System.Windows.Forms.ComboBox()
        Me.cbLevel3 = New System.Windows.Forms.ComboBox()
        Me.cbTrack2 = New System.Windows.Forms.ComboBox()
        Me.cbLevel2 = New System.Windows.Forms.ComboBox()
        Me.cbTrack1 = New System.Windows.Forms.ComboBox()
        Me.cbLevel1 = New System.Windows.Forms.ComboBox()
        Me.cbManual = New System.Windows.Forms.CheckBox()
        Me.btnMakeVocals = New System.Windows.Forms.Button()
        Me.gbSong.SuspendLayout()
        Me.gbParts.SuspendLayout()
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
        Me.cbSong.Size = New System.Drawing.Size(353, 21)
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
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(510, 200)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(77, 21)
        Me.btnOk.TabIndex = 25
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(12, 200)
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
        'gbParts
        '
        Me.gbParts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbParts.Controls.Add(Me.lblStrum3)
        Me.gbParts.Controls.Add(Me.cmbStrum3)
        Me.gbParts.Controls.Add(Me.lblStrum2)
        Me.gbParts.Controls.Add(Me.cmbStrum2)
        Me.gbParts.Controls.Add(Me.lblStrum1)
        Me.gbParts.Controls.Add(Me.cmbStrum1)
        Me.gbParts.Controls.Add(Me.lblStrum0)
        Me.gbParts.Controls.Add(Me.cmbStrum0)
        Me.gbParts.Controls.Add(Me.chkHOPO3)
        Me.gbParts.Controls.Add(Me.chkLF3)
        Me.gbParts.Controls.Add(Me.chkHOPO2)
        Me.gbParts.Controls.Add(Me.chkLF2)
        Me.gbParts.Controls.Add(Me.chkHOPO1)
        Me.gbParts.Controls.Add(Me.chkLF1)
        Me.gbParts.Controls.Add(Me.chkHOPO0)
        Me.gbParts.Controls.Add(Me.chkLF0)
        Me.gbParts.Controls.Add(Me.cbTrack3)
        Me.gbParts.Controls.Add(Me.cbLevel3)
        Me.gbParts.Controls.Add(Me.cbTrack2)
        Me.gbParts.Controls.Add(Me.cbLevel2)
        Me.gbParts.Controls.Add(Me.cbTrack1)
        Me.gbParts.Controls.Add(Me.cbLevel1)
        Me.gbParts.Controls.Add(Me.lblTrack)
        Me.gbParts.Controls.Add(Me.lblLevel)
        Me.gbParts.Controls.Add(Me.cbTrack0)
        Me.gbParts.Controls.Add(Me.cbLevel0)
        Me.gbParts.Location = New System.Drawing.Point(12, 68)
        Me.gbParts.Name = "gbParts"
        Me.gbParts.Size = New System.Drawing.Size(575, 126)
        Me.gbParts.TabIndex = 29
        Me.gbParts.TabStop = False
        Me.gbParts.Text = "Parts"
        '
        'lblStrum3
        '
        Me.lblStrum3.AutoSize = True
        Me.lblStrum3.Location = New System.Drawing.Point(437, 99)
        Me.lblStrum3.Name = "lblStrum3"
        Me.lblStrum3.Size = New System.Drawing.Size(34, 13)
        Me.lblStrum3.TabIndex = 48
        Me.lblStrum3.Text = "Strum"
        '
        'cmbStrum3
        '
        Me.cmbStrum3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStrum3.FormattingEnabled = True
        Me.cmbStrum3.Items.AddRange(New Object() {"Default", "Down", "Up", "Alternate"})
        Me.cmbStrum3.Location = New System.Drawing.Point(477, 96)
        Me.cmbStrum3.Name = "cmbStrum3"
        Me.cmbStrum3.Size = New System.Drawing.Size(88, 21)
        Me.cmbStrum3.TabIndex = 47
        '
        'lblStrum2
        '
        Me.lblStrum2.AutoSize = True
        Me.lblStrum2.Location = New System.Drawing.Point(306, 99)
        Me.lblStrum2.Name = "lblStrum2"
        Me.lblStrum2.Size = New System.Drawing.Size(34, 13)
        Me.lblStrum2.TabIndex = 46
        Me.lblStrum2.Text = "Strum"
        '
        'cmbStrum2
        '
        Me.cmbStrum2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStrum2.FormattingEnabled = True
        Me.cmbStrum2.Items.AddRange(New Object() {"Default", "Down", "Up", "Alternate"})
        Me.cmbStrum2.Location = New System.Drawing.Point(346, 96)
        Me.cmbStrum2.Name = "cmbStrum2"
        Me.cmbStrum2.Size = New System.Drawing.Size(88, 21)
        Me.cmbStrum2.TabIndex = 45
        '
        'lblStrum1
        '
        Me.lblStrum1.AutoSize = True
        Me.lblStrum1.Location = New System.Drawing.Point(175, 99)
        Me.lblStrum1.Name = "lblStrum1"
        Me.lblStrum1.Size = New System.Drawing.Size(34, 13)
        Me.lblStrum1.TabIndex = 44
        Me.lblStrum1.Text = "Strum"
        '
        'cmbStrum1
        '
        Me.cmbStrum1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStrum1.FormattingEnabled = True
        Me.cmbStrum1.Items.AddRange(New Object() {"Default", "Down", "Up", "Alternate"})
        Me.cmbStrum1.Location = New System.Drawing.Point(215, 96)
        Me.cmbStrum1.Name = "cmbStrum1"
        Me.cmbStrum1.Size = New System.Drawing.Size(88, 21)
        Me.cmbStrum1.TabIndex = 43
        '
        'lblStrum0
        '
        Me.lblStrum0.AutoSize = True
        Me.lblStrum0.Location = New System.Drawing.Point(44, 100)
        Me.lblStrum0.Name = "lblStrum0"
        Me.lblStrum0.Size = New System.Drawing.Size(34, 13)
        Me.lblStrum0.TabIndex = 42
        Me.lblStrum0.Text = "Strum"
        '
        'cmbStrum0
        '
        Me.cmbStrum0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStrum0.FormattingEnabled = True
        Me.cmbStrum0.Items.AddRange(New Object() {"Default", "Down", "Up", "Alternate"})
        Me.cmbStrum0.Location = New System.Drawing.Point(84, 97)
        Me.cmbStrum0.Name = "cmbStrum0"
        Me.cmbStrum0.Size = New System.Drawing.Size(88, 21)
        Me.cmbStrum0.TabIndex = 41
        '
        'chkHOPO3
        '
        Me.chkHOPO3.AutoSize = True
        Me.chkHOPO3.Location = New System.Drawing.Point(508, 73)
        Me.chkHOPO3.Name = "chkHOPO3"
        Me.chkHOPO3.Size = New System.Drawing.Size(57, 17)
        Me.chkHOPO3.TabIndex = 40
        Me.chkHOPO3.Text = "HOPO"
        Me.chkHOPO3.UseVisualStyleBackColor = True
        '
        'chkLF3
        '
        Me.chkLF3.AutoSize = True
        Me.chkLF3.Location = New System.Drawing.Point(440, 73)
        Me.chkLF3.Name = "chkLF3"
        Me.chkLF3.Size = New System.Drawing.Size(38, 17)
        Me.chkLF3.TabIndex = 39
        Me.chkLF3.Text = "LF"
        Me.chkLF3.UseVisualStyleBackColor = True
        '
        'chkHOPO2
        '
        Me.chkHOPO2.AutoSize = True
        Me.chkHOPO2.Location = New System.Drawing.Point(377, 73)
        Me.chkHOPO2.Name = "chkHOPO2"
        Me.chkHOPO2.Size = New System.Drawing.Size(57, 17)
        Me.chkHOPO2.TabIndex = 38
        Me.chkHOPO2.Text = "HOPO"
        Me.chkHOPO2.UseVisualStyleBackColor = True
        '
        'chkLF2
        '
        Me.chkLF2.AutoSize = True
        Me.chkLF2.Location = New System.Drawing.Point(309, 73)
        Me.chkLF2.Name = "chkLF2"
        Me.chkLF2.Size = New System.Drawing.Size(38, 17)
        Me.chkLF2.TabIndex = 37
        Me.chkLF2.Text = "LF"
        Me.chkLF2.UseVisualStyleBackColor = True
        '
        'chkHOPO1
        '
        Me.chkHOPO1.AutoSize = True
        Me.chkHOPO1.Location = New System.Drawing.Point(246, 73)
        Me.chkHOPO1.Name = "chkHOPO1"
        Me.chkHOPO1.Size = New System.Drawing.Size(57, 17)
        Me.chkHOPO1.TabIndex = 36
        Me.chkHOPO1.Text = "HOPO"
        Me.chkHOPO1.UseVisualStyleBackColor = True
        '
        'chkLF1
        '
        Me.chkLF1.AutoSize = True
        Me.chkLF1.Location = New System.Drawing.Point(178, 73)
        Me.chkLF1.Name = "chkLF1"
        Me.chkLF1.Size = New System.Drawing.Size(38, 17)
        Me.chkLF1.TabIndex = 35
        Me.chkLF1.Text = "LF"
        Me.chkLF1.UseVisualStyleBackColor = True
        '
        'chkHOPO0
        '
        Me.chkHOPO0.AutoSize = True
        Me.chkHOPO0.Location = New System.Drawing.Point(115, 73)
        Me.chkHOPO0.Name = "chkHOPO0"
        Me.chkHOPO0.Size = New System.Drawing.Size(57, 17)
        Me.chkHOPO0.TabIndex = 34
        Me.chkHOPO0.Text = "HOPO"
        Me.chkHOPO0.UseVisualStyleBackColor = True
        '
        'chkLF0
        '
        Me.chkLF0.AutoSize = True
        Me.chkLF0.Location = New System.Drawing.Point(47, 73)
        Me.chkLF0.Name = "chkLF0"
        Me.chkLF0.Size = New System.Drawing.Size(38, 17)
        Me.chkLF0.TabIndex = 33
        Me.chkLF0.Text = "LF"
        Me.chkLF0.UseVisualStyleBackColor = True
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
        Me.cbManual.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbManual.AutoSize = True
        Me.cbManual.Location = New System.Drawing.Point(418, 203)
        Me.cbManual.Name = "cbManual"
        Me.cbManual.Size = New System.Drawing.Size(86, 17)
        Me.cbManual.TabIndex = 31
        Me.cbManual.Text = "Manual Start"
        Me.cbManual.UseVisualStyleBackColor = True
        '
        'btnMakeVocals
        '
        Me.btnMakeVocals.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMakeVocals.Location = New System.Drawing.Point(95, 199)
        Me.btnMakeVocals.Name = "btnMakeVocals"
        Me.btnMakeVocals.Size = New System.Drawing.Size(100, 23)
        Me.btnMakeVocals.TabIndex = 32
        Me.btnMakeVocals.Text = "Make Vocals"
        Me.btnMakeVocals.UseVisualStyleBackColor = True
        '
        'frmMusic
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(599, 227)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnMakeVocals)
        Me.Controls.Add(Me.cbManual)
        Me.Controls.Add(Me.gbSong)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.gbParts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMusic"
        Me.Text = "Song Selection"
        Me.gbSong.ResumeLayout(False)
        Me.gbSong.PerformLayout()
        Me.gbParts.ResumeLayout(False)
        Me.gbParts.PerformLayout()
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
    Friend WithEvents gbParts As System.Windows.Forms.GroupBox
    Friend WithEvents cbTrack3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbTrack2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbTrack1 As System.Windows.Forms.ComboBox
    Friend WithEvents cbLevel1 As System.Windows.Forms.ComboBox
    Friend WithEvents cbManual As System.Windows.Forms.CheckBox
    Friend WithEvents btnMakeVocals As System.Windows.Forms.Button
    Friend WithEvents chkHOPO3 As CheckBox
    Friend WithEvents chkLF3 As CheckBox
    Friend WithEvents chkHOPO2 As CheckBox
    Friend WithEvents chkLF2 As CheckBox
    Friend WithEvents chkHOPO1 As CheckBox
    Friend WithEvents chkLF1 As CheckBox
    Friend WithEvents chkHOPO0 As CheckBox
    Friend WithEvents chkLF0 As CheckBox
    Friend WithEvents lblStrum3 As Label
    Friend WithEvents cmbStrum3 As ComboBox
    Friend WithEvents lblStrum2 As Label
    Friend WithEvents cmbStrum2 As ComboBox
    Friend WithEvents lblStrum1 As Label
    Friend WithEvents cmbStrum1 As ComboBox
    Friend WithEvents lblStrum0 As Label
    Friend WithEvents cmbStrum0 As ComboBox
End Class
