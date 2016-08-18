<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcaptureWizard
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
        Me.components = New System.ComponentModel.Container()
        Me.pbStart = New System.Windows.Forms.PictureBox()
        Me.pbStop = New System.Windows.Forms.PictureBox()
        Me.pbZoom = New System.Windows.Forms.PictureBox()
        Me.btnRecord = New System.Windows.Forms.Button()
        Me.lblStart = New System.Windows.Forms.Label()
        Me.lblStop = New System.Windows.Forms.Label()
        Me.lblZoom = New System.Windows.Forms.Label()
        Me.lvCaptures = New System.Windows.Forms.ListView()
        Me.ch1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ilCaptures = New System.Windows.Forms.ImageList(Me.components)
        Me.pbMain = New System.Windows.Forms.PictureBox()
        Me.nudRMin = New System.Windows.Forms.NumericUpDown()
        Me.nudRMax = New System.Windows.Forms.NumericUpDown()
        Me.nudGMin = New System.Windows.Forms.NumericUpDown()
        Me.nudGMax = New System.Windows.Forms.NumericUpDown()
        Me.nudBMin = New System.Windows.Forms.NumericUpDown()
        Me.nudBMax = New System.Windows.Forms.NumericUpDown()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.lblRed = New System.Windows.Forms.Label()
        Me.lblGreen = New System.Windows.Forms.Label()
        Me.lblBlue = New System.Windows.Forms.Label()
        Me.tmrPreview = New System.Windows.Forms.Timer(Me.components)
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbColors = New System.Windows.Forms.PictureBox()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.tmrRecord = New System.Windows.Forms.Timer(Me.components)
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        CType(Me.pbStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudGMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudGMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudBMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudBMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbColors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbStart
        '
        Me.pbStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbStart.Location = New System.Drawing.Point(12, 33)
        Me.pbStart.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.pbStart.Name = "pbStart"
        Me.pbStart.Size = New System.Drawing.Size(242, 132)
        Me.pbStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbStart.TabIndex = 4
        Me.pbStart.TabStop = False
        '
        'pbStop
        '
        Me.pbStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbStop.Location = New System.Drawing.Point(258, 33)
        Me.pbStop.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.pbStop.Name = "pbStop"
        Me.pbStop.Size = New System.Drawing.Size(242, 132)
        Me.pbStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbStop.TabIndex = 5
        Me.pbStop.TabStop = False
        '
        'pbZoom
        '
        Me.pbZoom.Cursor = System.Windows.Forms.Cursors.Cross
        Me.pbZoom.Location = New System.Drawing.Point(504, 33)
        Me.pbZoom.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.pbZoom.Name = "pbZoom"
        Me.pbZoom.Size = New System.Drawing.Size(132, 132)
        Me.pbZoom.TabIndex = 6
        Me.pbZoom.TabStop = False
        '
        'btnRecord
        '
        Me.btnRecord.Font = New System.Drawing.Font("Webdings", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRecord.ForeColor = System.Drawing.Color.Red
        Me.btnRecord.Location = New System.Drawing.Point(746, 169)
        Me.btnRecord.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnRecord.Name = "btnRecord"
        Me.btnRecord.Size = New System.Drawing.Size(42, 39)
        Me.btnRecord.TabIndex = 7
        Me.btnRecord.Text = "="
        Me.btnRecord.UseVisualStyleBackColor = True
        '
        'lblStart
        '
        Me.lblStart.AutoSize = True
        Me.lblStart.Location = New System.Drawing.Point(12, 13)
        Me.lblStart.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(68, 13)
        Me.lblStart.TabIndex = 9
        Me.lblStart.Text = "Section Start"
        '
        'lblStop
        '
        Me.lblStop.AutoSize = True
        Me.lblStop.Location = New System.Drawing.Point(252, 13)
        Me.lblStop.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblStop.Name = "lblStop"
        Me.lblStop.Size = New System.Drawing.Size(73, 13)
        Me.lblStop.TabIndex = 10
        Me.lblStop.Text = "Section Finish"
        '
        'lblZoom
        '
        Me.lblZoom.AutoSize = True
        Me.lblZoom.Location = New System.Drawing.Point(504, 13)
        Me.lblZoom.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(68, 13)
        Me.lblZoom.TabIndex = 11
        Me.lblZoom.Text = "Target Detail"
        '
        'lvCaptures
        '
        Me.lvCaptures.Activation = System.Windows.Forms.ItemActivation.TwoClick
        Me.lvCaptures.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ch1})
        Me.lvCaptures.Font = New System.Drawing.Font("Microsoft Sans Serif", 1.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvCaptures.LargeImageList = Me.ilCaptures
        Me.lvCaptures.Location = New System.Drawing.Point(847, 9)
        Me.lvCaptures.Margin = New System.Windows.Forms.Padding(0)
        Me.lvCaptures.MultiSelect = False
        Me.lvCaptures.Name = "lvCaptures"
        Me.lvCaptures.ShowGroups = False
        Me.lvCaptures.Size = New System.Drawing.Size(166, 563)
        Me.lvCaptures.SmallImageList = Me.ilCaptures
        Me.lvCaptures.TabIndex = 12
        Me.lvCaptures.UseCompatibleStateImageBehavior = False
        '
        'ch1
        '
        Me.ch1.Text = ""
        Me.ch1.Width = 0
        '
        'ilCaptures
        '
        Me.ilCaptures.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
        Me.ilCaptures.ImageSize = New System.Drawing.Size(128, 72)
        Me.ilCaptures.TransparentColor = System.Drawing.Color.Transparent
        '
        'pbMain
        '
        Me.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMain.Cursor = System.Windows.Forms.Cursors.Cross
        Me.pbMain.Location = New System.Drawing.Point(12, 169)
        Me.pbMain.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.pbMain.Name = "pbMain"
        Me.pbMain.Size = New System.Drawing.Size(722, 405)
        Me.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMain.TabIndex = 13
        Me.pbMain.TabStop = False
        '
        'nudRMin
        '
        Me.nudRMin.Location = New System.Drawing.Point(690, 65)
        Me.nudRMin.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudRMin.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudRMin.Name = "nudRMin"
        Me.nudRMin.Size = New System.Drawing.Size(42, 20)
        Me.nudRMin.TabIndex = 14
        '
        'nudRMax
        '
        Me.nudRMax.Location = New System.Drawing.Point(738, 65)
        Me.nudRMax.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudRMax.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudRMax.Name = "nudRMax"
        Me.nudRMax.Size = New System.Drawing.Size(42, 20)
        Me.nudRMax.TabIndex = 15
        '
        'nudGMin
        '
        Me.nudGMin.Location = New System.Drawing.Point(690, 87)
        Me.nudGMin.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudGMin.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudGMin.Name = "nudGMin"
        Me.nudGMin.Size = New System.Drawing.Size(42, 20)
        Me.nudGMin.TabIndex = 16
        '
        'nudGMax
        '
        Me.nudGMax.Location = New System.Drawing.Point(738, 87)
        Me.nudGMax.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudGMax.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudGMax.Name = "nudGMax"
        Me.nudGMax.Size = New System.Drawing.Size(42, 20)
        Me.nudGMax.TabIndex = 17
        '
        'nudBMin
        '
        Me.nudBMin.Location = New System.Drawing.Point(690, 110)
        Me.nudBMin.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudBMin.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudBMin.Name = "nudBMin"
        Me.nudBMin.Size = New System.Drawing.Size(42, 20)
        Me.nudBMin.TabIndex = 18
        '
        'nudBMax
        '
        Me.nudBMax.Location = New System.Drawing.Point(738, 110)
        Me.nudBMax.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.nudBMax.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudBMax.Name = "nudBMax"
        Me.nudBMax.Size = New System.Drawing.Size(42, 20)
        Me.nudBMax.TabIndex = 19
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(678, 39)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(64, 20)
        Me.txtLocation.TabIndex = 20
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(648, 39)
        Me.lblLocation.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(28, 13)
        Me.lblLocation.TabIndex = 21
        Me.lblLocation.Text = "Loc:"
        '
        'btnLeft
        '
        Me.btnLeft.Enabled = False
        Me.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnLeft.Font = New System.Drawing.Font("Webdings", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnLeft.Location = New System.Drawing.Point(744, 39)
        Me.btnLeft.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(18, 13)
        Me.btnLeft.TabIndex = 22
        Me.btnLeft.Text = "3"
        Me.btnLeft.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Enabled = False
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnUp.Font = New System.Drawing.Font("Webdings", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnUp.Location = New System.Drawing.Point(762, 32)
        Me.btnUp.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(18, 13)
        Me.btnUp.TabIndex = 23
        Me.btnUp.Text = "5"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Enabled = False
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDown.Font = New System.Drawing.Font("Webdings", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnDown.Location = New System.Drawing.Point(762, 46)
        Me.btnDown.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(18, 13)
        Me.btnDown.TabIndex = 24
        Me.btnDown.Text = "6"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        Me.btnRight.Enabled = False
        Me.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnRight.Font = New System.Drawing.Font("Webdings", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRight.Location = New System.Drawing.Point(780, 39)
        Me.btnRight.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(18, 13)
        Me.btnRight.TabIndex = 25
        Me.btnRight.Text = "4"
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'lblRed
        '
        Me.lblRed.AutoSize = True
        Me.lblRed.Location = New System.Drawing.Point(648, 72)
        Me.lblRed.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblRed.Name = "lblRed"
        Me.lblRed.Size = New System.Drawing.Size(30, 13)
        Me.lblRed.TabIndex = 26
        Me.lblRed.Text = "Red:"
        '
        'lblGreen
        '
        Me.lblGreen.AutoSize = True
        Me.lblGreen.Location = New System.Drawing.Point(648, 91)
        Me.lblGreen.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblGreen.Name = "lblGreen"
        Me.lblGreen.Size = New System.Drawing.Size(39, 13)
        Me.lblGreen.TabIndex = 27
        Me.lblGreen.Text = "Green:"
        '
        'lblBlue
        '
        Me.lblBlue.AutoSize = True
        Me.lblBlue.Location = New System.Drawing.Point(648, 117)
        Me.lblBlue.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblBlue.Name = "lblBlue"
        Me.lblBlue.Size = New System.Drawing.Size(31, 13)
        Me.lblBlue.TabIndex = 28
        Me.lblBlue.Text = "Blue:"
        '
        'tmrPreview
        '
        Me.tmrPreview.Interval = 250
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(746, 546)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(54, 26)
        Me.btnOK.TabIndex = 29
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(746, 520)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 26)
        Me.btnCancel.TabIndex = 30
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pbColors
        '
        Me.pbColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbColors.Location = New System.Drawing.Point(803, 41)
        Me.pbColors.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.pbColors.Name = "pbColors"
        Me.pbColors.Size = New System.Drawing.Size(38, 119)
        Me.pbColors.TabIndex = 31
        Me.pbColors.TabStop = False
        '
        'btnPlay
        '
        Me.btnPlay.Font = New System.Drawing.Font("Webdings", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnPlay.ForeColor = System.Drawing.Color.Green
        Me.btnPlay.Location = New System.Drawing.Point(789, 169)
        Me.btnPlay.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(42, 39)
        Me.btnPlay.TabIndex = 32
        Me.btnPlay.Text = "4"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'tmrRecord
        '
        Me.tmrRecord.Interval = 250
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(92, 6)
        Me.btnFirst.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(54, 26)
        Me.btnFirst.TabIndex = 33
        Me.btnFirst.Text = "Set"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(337, 6)
        Me.btnLast.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(54, 26)
        Me.btnLast.TabIndex = 34
        Me.btnLast.Text = "Set"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnScan
        '
        Me.btnScan.Enabled = False
        Me.btnScan.Location = New System.Drawing.Point(690, 134)
        Me.btnScan.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(89, 26)
        Me.btnScan.TabIndex = 35
        Me.btnScan.Text = "Scan"
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'frmcaptureWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1031, 585)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnScan)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.pbColors)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblBlue)
        Me.Controls.Add(Me.lblGreen)
        Me.Controls.Add(Me.lblRed)
        Me.Controls.Add(Me.btnRight)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.nudBMax)
        Me.Controls.Add(Me.nudBMin)
        Me.Controls.Add(Me.nudGMax)
        Me.Controls.Add(Me.nudGMin)
        Me.Controls.Add(Me.nudRMax)
        Me.Controls.Add(Me.nudRMin)
        Me.Controls.Add(Me.pbMain)
        Me.Controls.Add(Me.lvCaptures)
        Me.Controls.Add(Me.lblZoom)
        Me.Controls.Add(Me.lblStop)
        Me.Controls.Add(Me.lblStart)
        Me.Controls.Add(Me.btnRecord)
        Me.Controls.Add(Me.pbZoom)
        Me.Controls.Add(Me.pbStop)
        Me.Controls.Add(Me.pbStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Name = "frmcaptureWizard"
        Me.Text = "Pixel Watch Wizard"
        CType(Me.pbStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudGMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudGMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudBMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudBMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbColors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbStart As System.Windows.Forms.PictureBox
    Friend WithEvents pbStop As System.Windows.Forms.PictureBox
    Friend WithEvents pbZoom As System.Windows.Forms.PictureBox
    Friend WithEvents btnRecord As System.Windows.Forms.Button
    Friend WithEvents lblStart As System.Windows.Forms.Label
    Friend WithEvents lblStop As System.Windows.Forms.Label
    Friend WithEvents lblZoom As System.Windows.Forms.Label
    Friend WithEvents lvCaptures As System.Windows.Forms.ListView
    Friend WithEvents pbMain As System.Windows.Forms.PictureBox
    Friend WithEvents nudRMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudRMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudGMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudGMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudBMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudBMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents lblRed As System.Windows.Forms.Label
    Friend WithEvents lblGreen As System.Windows.Forms.Label
    Friend WithEvents lblBlue As System.Windows.Forms.Label
    Friend WithEvents ilCaptures As System.Windows.Forms.ImageList
    Friend WithEvents tmrPreview As System.Windows.Forms.Timer
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pbColors As System.Windows.Forms.PictureBox
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents tmrRecord As System.Windows.Forms.Timer
    Friend WithEvents ch1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
End Class
