<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdit))
        Me.btnA = New System.Windows.Forms.Button()
        Me.btnB = New System.Windows.Forms.Button()
        Me.btnY = New System.Windows.Forms.Button()
        Me.btnX = New System.Windows.Forms.Button()
        Me.btnDD = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnLS = New System.Windows.Forms.Button()
        Me.btnDL = New System.Windows.Forms.Button()
        Me.btnLB = New System.Windows.Forms.Button()
        Me.btnRB = New System.Windows.Forms.Button()
        Me.btnDR = New System.Windows.Forms.Button()
        Me.btnLT = New System.Windows.Forms.Button()
        Me.btnRS = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnRT = New System.Windows.Forms.Button()
        Me.btnDU = New System.Windows.Forms.Button()
        Me.btnGuide = New System.Windows.Forms.Button()
        Me.pbLS = New System.Windows.Forms.PictureBox()
        Me.pbRS = New System.Windows.Forms.PictureBox()
        Me.txtRSY = New System.Windows.Forms.TextBox()
        Me.lblRSX = New System.Windows.Forms.Label()
        Me.lblRSY = New System.Windows.Forms.Label()
        Me.txtRSX = New System.Windows.Forms.TextBox()
        Me.txtLSX = New System.Windows.Forms.TextBox()
        Me.lblLSY = New System.Windows.Forms.Label()
        Me.lblLSX = New System.Windows.Forms.Label()
        Me.txtLSY = New System.Windows.Forms.TextBox()
        Me.rbPress = New System.Windows.Forms.RadioButton()
        Me.rbRelease = New System.Windows.Forms.RadioButton()
        Me.rbWait = New System.Windows.Forms.RadioButton()
        Me.rbLoop = New System.Windows.Forms.RadioButton()
        Me.pnlControls = New System.Windows.Forms.Panel()
        Me.txtLT = New System.Windows.Forms.TextBox()
        Me.lblLT = New System.Windows.Forms.Label()
        Me.txtRT = New System.Windows.Forms.TextBox()
        Me.lblRT = New System.Windows.Forms.Label()
        Me.rbHold = New System.Windows.Forms.RadioButton()
        Me.rbGroup = New System.Windows.Forms.RadioButton()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.cmbAdd = New System.Windows.Forms.ComboBox()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SongToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatternToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BridgeModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CronusIdentifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CaptureCardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblGame = New System.Windows.Forms.Label()
        Me.txtGame = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.btnPlayPause = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.fdOpen = New System.Windows.Forms.OpenFileDialog()
        Me.fdSave = New System.Windows.Forms.SaveFileDialog()
        Me.tmrScriptStatus = New System.Windows.Forms.Timer(Me.components)
        Me.cbPrecompile = New System.Windows.Forms.CheckBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.lblWaitTime = New System.Windows.Forms.Label()
        Me.lbActions = New AutoGH.RefreshingListBox()
        Me.tcActions = New System.Windows.Forms.TabControl()
        Me.tpController = New System.Windows.Forms.TabPage()
        Me.lblControllerIP = New System.Windows.Forms.Label()
        Me.lblControllerHold = New System.Windows.Forms.Label()
        Me.txtControllerRepeat = New System.Windows.Forms.TextBox()
        Me.lblControllerRepeat = New System.Windows.Forms.Label()
        Me.txtControllerHold = New System.Windows.Forms.TextBox()
        Me.txtControllerWait = New System.Windows.Forms.TextBox()
        Me.lblControllerWait = New System.Windows.Forms.Label()
        Me.cbControllerIP = New System.Windows.Forms.ComboBox()
        Me.tpFlow = New System.Windows.Forms.TabPage()
        Me.btnFlowTarget = New System.Windows.Forms.Button()
        Me.txtFlowTarget = New System.Windows.Forms.TextBox()
        Me.lblFlowTarget = New System.Windows.Forms.Label()
        Me.txtFlowRepeat = New System.Windows.Forms.TextBox()
        Me.lblFlowRepeat = New System.Windows.Forms.Label()
        Me.txtFlowWait = New System.Windows.Forms.TextBox()
        Me.lblFlowWait = New System.Windows.Forms.Label()
        Me.tpInput = New System.Windows.Forms.TabPage()
        Me.gbVideo = New System.Windows.Forms.GroupBox()
        Me.btnVideoWizard = New System.Windows.Forms.Button()
        Me.btnVideoColorMax = New System.Windows.Forms.Button()
        Me.btnVideoColorMin = New System.Windows.Forms.Button()
        Me.lblVideoColor = New System.Windows.Forms.Label()
        Me.txtVideoPixelY = New System.Windows.Forms.TextBox()
        Me.txtVideoPixelX = New System.Windows.Forms.TextBox()
        Me.lblVideoPixelY = New System.Windows.Forms.Label()
        Me.lblVideoPixelX = New System.Windows.Forms.Label()
        Me.gbTest = New System.Windows.Forms.GroupBox()
        Me.txtInputDuration = New System.Windows.Forms.TextBox()
        Me.txtInputInterval = New System.Windows.Forms.TextBox()
        Me.lblInputDuration = New System.Windows.Forms.Label()
        Me.lblInputInterval = New System.Windows.Forms.Label()
        Me.rbInputVideo = New System.Windows.Forms.RadioButton()
        Me.rbInputRumble = New System.Windows.Forms.RadioButton()
        Me.rbInputAudio = New System.Windows.Forms.RadioButton()
        Me.txtSync = New System.Windows.Forms.TextBox()
        Me.cbSync = New System.Windows.Forms.CheckBox()
        Me.cbSyncWait = New System.Windows.Forms.CheckBox()
        Me.cbTrace = New System.Windows.Forms.CheckBox()
        Me.gbGroups = New System.Windows.Forms.GroupBox()
        Me.btnRenameGroup = New System.Windows.Forms.Button()
        Me.btnAddGroup = New System.Windows.Forms.Button()
        Me.btnDeleteGroup = New System.Windows.Forms.Button()
        Me.lbGroups = New AutoGH.RefreshingListBox()
        Me.gbControllers = New System.Windows.Forms.GroupBox()
        Me.txtController4 = New System.Windows.Forms.TextBox()
        Me.lblController4 = New System.Windows.Forms.Label()
        Me.txtController3 = New System.Windows.Forms.TextBox()
        Me.lblController3 = New System.Windows.Forms.Label()
        Me.txtController2 = New System.Windows.Forms.TextBox()
        Me.lblController2 = New System.Windows.Forms.Label()
        Me.txtController1 = New System.Windows.Forms.TextBox()
        Me.lblController1 = New System.Windows.Forms.Label()
        Me.cdCapture = New System.Windows.Forms.ColorDialog()
        Me.USBDeviceFinderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.pbLS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbRS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlControls.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.tcActions.SuspendLayout()
        Me.tpController.SuspendLayout()
        Me.tpFlow.SuspendLayout()
        Me.tpInput.SuspendLayout()
        Me.gbVideo.SuspendLayout()
        Me.gbTest.SuspendLayout()
        Me.gbGroups.SuspendLayout()
        Me.gbControllers.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnA
        '
        Me.btnA.FlatAppearance.BorderSize = 0
        Me.btnA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnA.Image = CType(resources.GetObject("btnA.Image"), System.Drawing.Image)
        Me.btnA.Location = New System.Drawing.Point(234, 45)
        Me.btnA.Margin = New System.Windows.Forms.Padding(0)
        Me.btnA.Name = "btnA"
        Me.btnA.Size = New System.Drawing.Size(21, 21)
        Me.btnA.TabIndex = 25
        Me.btnA.UseVisualStyleBackColor = True
        '
        'btnB
        '
        Me.btnB.FlatAppearance.BorderSize = 0
        Me.btnB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnB.Image = CType(resources.GetObject("btnB.Image"), System.Drawing.Image)
        Me.btnB.Location = New System.Drawing.Point(245, 24)
        Me.btnB.Margin = New System.Windows.Forms.Padding(0)
        Me.btnB.Name = "btnB"
        Me.btnB.Size = New System.Drawing.Size(21, 21)
        Me.btnB.TabIndex = 24
        Me.btnB.UseVisualStyleBackColor = True
        '
        'btnY
        '
        Me.btnY.FlatAppearance.BorderSize = 0
        Me.btnY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnY.Image = CType(resources.GetObject("btnY.Image"), System.Drawing.Image)
        Me.btnY.Location = New System.Drawing.Point(234, 3)
        Me.btnY.Margin = New System.Windows.Forms.Padding(0)
        Me.btnY.Name = "btnY"
        Me.btnY.Size = New System.Drawing.Size(21, 21)
        Me.btnY.TabIndex = 22
        Me.btnY.UseVisualStyleBackColor = True
        '
        'btnX
        '
        Me.btnX.FlatAppearance.BorderSize = 0
        Me.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnX.Image = CType(resources.GetObject("btnX.Image"), System.Drawing.Image)
        Me.btnX.Location = New System.Drawing.Point(223, 24)
        Me.btnX.Margin = New System.Windows.Forms.Padding(0)
        Me.btnX.Name = "btnX"
        Me.btnX.Size = New System.Drawing.Size(21, 21)
        Me.btnX.TabIndex = 23
        Me.btnX.UseVisualStyleBackColor = True
        '
        'btnDD
        '
        Me.btnDD.FlatAppearance.BorderSize = 0
        Me.btnDD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDD.Image = CType(resources.GetObject("btnDD.Image"), System.Drawing.Image)
        Me.btnDD.Location = New System.Drawing.Point(156, 45)
        Me.btnDD.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDD.Name = "btnDD"
        Me.btnDD.Size = New System.Drawing.Size(21, 21)
        Me.btnDD.TabIndex = 18
        Me.btnDD.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(186, 3)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(0)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(21, 21)
        Me.btnBack.TabIndex = 19
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnLS
        '
        Me.btnLS.FlatAppearance.BorderSize = 0
        Me.btnLS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLS.Image = CType(resources.GetObject("btnLS.Image"), System.Drawing.Image)
        Me.btnLS.Location = New System.Drawing.Point(119, 45)
        Me.btnLS.Margin = New System.Windows.Forms.Padding(0)
        Me.btnLS.Name = "btnLS"
        Me.btnLS.Size = New System.Drawing.Size(21, 21)
        Me.btnLS.TabIndex = 14
        Me.btnLS.UseVisualStyleBackColor = True
        '
        'btnDL
        '
        Me.btnDL.FlatAppearance.BorderSize = 0
        Me.btnDL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDL.Image = CType(resources.GetObject("btnDL.Image"), System.Drawing.Image)
        Me.btnDL.Location = New System.Drawing.Point(145, 24)
        Me.btnDL.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDL.Name = "btnDL"
        Me.btnDL.Size = New System.Drawing.Size(21, 21)
        Me.btnDL.TabIndex = 16
        Me.btnDL.UseVisualStyleBackColor = True
        '
        'btnLB
        '
        Me.btnLB.FlatAppearance.BorderSize = 0
        Me.btnLB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLB.Image = CType(resources.GetObject("btnLB.Image"), System.Drawing.Image)
        Me.btnLB.Location = New System.Drawing.Point(119, 24)
        Me.btnLB.Margin = New System.Windows.Forms.Padding(0)
        Me.btnLB.Name = "btnLB"
        Me.btnLB.Size = New System.Drawing.Size(21, 21)
        Me.btnLB.TabIndex = 13
        Me.btnLB.UseVisualStyleBackColor = True
        '
        'btnRB
        '
        Me.btnRB.FlatAppearance.BorderSize = 0
        Me.btnRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRB.Image = CType(resources.GetObject("btnRB.Image"), System.Drawing.Image)
        Me.btnRB.Location = New System.Drawing.Point(271, 24)
        Me.btnRB.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRB.Name = "btnRB"
        Me.btnRB.Size = New System.Drawing.Size(21, 21)
        Me.btnRB.TabIndex = 27
        Me.btnRB.UseVisualStyleBackColor = True
        '
        'btnDR
        '
        Me.btnDR.FlatAppearance.BorderSize = 0
        Me.btnDR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDR.Image = CType(resources.GetObject("btnDR.Image"), System.Drawing.Image)
        Me.btnDR.Location = New System.Drawing.Point(167, 24)
        Me.btnDR.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDR.Name = "btnDR"
        Me.btnDR.Size = New System.Drawing.Size(21, 21)
        Me.btnDR.TabIndex = 17
        Me.btnDR.UseVisualStyleBackColor = True
        '
        'btnLT
        '
        Me.btnLT.FlatAppearance.BorderSize = 0
        Me.btnLT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLT.Image = CType(resources.GetObject("btnLT.Image"), System.Drawing.Image)
        Me.btnLT.Location = New System.Drawing.Point(119, 3)
        Me.btnLT.Margin = New System.Windows.Forms.Padding(0)
        Me.btnLT.Name = "btnLT"
        Me.btnLT.Size = New System.Drawing.Size(21, 21)
        Me.btnLT.TabIndex = 12
        Me.btnLT.UseVisualStyleBackColor = True
        '
        'btnRS
        '
        Me.btnRS.FlatAppearance.BorderSize = 0
        Me.btnRS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRS.Image = CType(resources.GetObject("btnRS.Image"), System.Drawing.Image)
        Me.btnRS.Location = New System.Drawing.Point(271, 45)
        Me.btnRS.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRS.Name = "btnRS"
        Me.btnRS.Size = New System.Drawing.Size(21, 21)
        Me.btnRS.TabIndex = 28
        Me.btnRS.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.FlatAppearance.BorderSize = 0
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Image = CType(resources.GetObject("btnStart.Image"), System.Drawing.Image)
        Me.btnStart.Location = New System.Drawing.Point(208, 3)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(0)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(21, 21)
        Me.btnStart.TabIndex = 20
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnRT
        '
        Me.btnRT.FlatAppearance.BorderSize = 0
        Me.btnRT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRT.Image = CType(resources.GetObject("btnRT.Image"), System.Drawing.Image)
        Me.btnRT.Location = New System.Drawing.Point(271, 3)
        Me.btnRT.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRT.Name = "btnRT"
        Me.btnRT.Size = New System.Drawing.Size(21, 21)
        Me.btnRT.TabIndex = 26
        Me.btnRT.UseVisualStyleBackColor = True
        '
        'btnDU
        '
        Me.btnDU.FlatAppearance.BorderSize = 0
        Me.btnDU.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDU.Image = CType(resources.GetObject("btnDU.Image"), System.Drawing.Image)
        Me.btnDU.Location = New System.Drawing.Point(156, 3)
        Me.btnDU.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDU.Name = "btnDU"
        Me.btnDU.Size = New System.Drawing.Size(21, 21)
        Me.btnDU.TabIndex = 15
        Me.btnDU.UseVisualStyleBackColor = True
        '
        'btnGuide
        '
        Me.btnGuide.FlatAppearance.BorderSize = 0
        Me.btnGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuide.Image = CType(resources.GetObject("btnGuide.Image"), System.Drawing.Image)
        Me.btnGuide.Location = New System.Drawing.Point(197, 24)
        Me.btnGuide.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGuide.Name = "btnGuide"
        Me.btnGuide.Size = New System.Drawing.Size(21, 21)
        Me.btnGuide.TabIndex = 21
        Me.btnGuide.UseVisualStyleBackColor = True
        '
        'pbLS
        '
        Me.pbLS.BackColor = System.Drawing.Color.Silver
        Me.pbLS.BackgroundImage = CType(resources.GetObject("pbLS.BackgroundImage"), System.Drawing.Image)
        Me.pbLS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pbLS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbLS.Location = New System.Drawing.Point(53, 3)
        Me.pbLS.Name = "pbLS"
        Me.pbLS.Size = New System.Drawing.Size(63, 63)
        Me.pbLS.TabIndex = 21
        Me.pbLS.TabStop = False
        '
        'pbRS
        '
        Me.pbRS.BackColor = System.Drawing.Color.Silver
        Me.pbRS.BackgroundImage = CType(resources.GetObject("pbRS.BackgroundImage"), System.Drawing.Image)
        Me.pbRS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pbRS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbRS.Location = New System.Drawing.Point(295, 3)
        Me.pbRS.Name = "pbRS"
        Me.pbRS.Size = New System.Drawing.Size(63, 63)
        Me.pbRS.TabIndex = 22
        Me.pbRS.TabStop = False
        '
        'txtRSY
        '
        Me.txtRSY.Location = New System.Drawing.Point(384, 25)
        Me.txtRSY.Name = "txtRSY"
        Me.txtRSY.Size = New System.Drawing.Size(27, 20)
        Me.txtRSY.TabIndex = 30
        Me.txtRSY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRSX
        '
        Me.lblRSX.AutoSize = True
        Me.lblRSX.Location = New System.Drawing.Point(362, 5)
        Me.lblRSX.Name = "lblRSX"
        Me.lblRSX.Size = New System.Drawing.Size(17, 13)
        Me.lblRSX.TabIndex = 24
        Me.lblRSX.Text = "X:"
        '
        'lblRSY
        '
        Me.lblRSY.AutoSize = True
        Me.lblRSY.Location = New System.Drawing.Point(362, 28)
        Me.lblRSY.Name = "lblRSY"
        Me.lblRSY.Size = New System.Drawing.Size(17, 13)
        Me.lblRSY.TabIndex = 25
        Me.lblRSY.Text = "Y:"
        '
        'txtRSX
        '
        Me.txtRSX.Location = New System.Drawing.Point(384, 2)
        Me.txtRSX.Name = "txtRSX"
        Me.txtRSX.Size = New System.Drawing.Size(27, 20)
        Me.txtRSX.TabIndex = 29
        Me.txtRSX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLSX
        '
        Me.txtLSX.Location = New System.Drawing.Point(22, 2)
        Me.txtLSX.Name = "txtLSX"
        Me.txtLSX.Size = New System.Drawing.Size(27, 20)
        Me.txtLSX.TabIndex = 9
        Me.txtLSX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLSY
        '
        Me.lblLSY.AutoSize = True
        Me.lblLSY.Location = New System.Drawing.Point(0, 28)
        Me.lblLSY.Name = "lblLSY"
        Me.lblLSY.Size = New System.Drawing.Size(17, 13)
        Me.lblLSY.TabIndex = 29
        Me.lblLSY.Text = "Y:"
        '
        'lblLSX
        '
        Me.lblLSX.AutoSize = True
        Me.lblLSX.Location = New System.Drawing.Point(0, 5)
        Me.lblLSX.Name = "lblLSX"
        Me.lblLSX.Size = New System.Drawing.Size(17, 13)
        Me.lblLSX.TabIndex = 28
        Me.lblLSX.Text = "X:"
        '
        'txtLSY
        '
        Me.txtLSY.Location = New System.Drawing.Point(22, 25)
        Me.txtLSY.Name = "txtLSY"
        Me.txtLSY.Size = New System.Drawing.Size(27, 20)
        Me.txtLSY.TabIndex = 10
        Me.txtLSY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rbPress
        '
        Me.rbPress.AutoSize = True
        Me.rbPress.Location = New System.Drawing.Point(3, 3)
        Me.rbPress.Name = "rbPress"
        Me.rbPress.Size = New System.Drawing.Size(51, 17)
        Me.rbPress.TabIndex = 4
        Me.rbPress.TabStop = True
        Me.rbPress.Text = "Press"
        Me.rbPress.UseVisualStyleBackColor = True
        '
        'rbRelease
        '
        Me.rbRelease.AutoSize = True
        Me.rbRelease.Location = New System.Drawing.Point(3, 35)
        Me.rbRelease.Name = "rbRelease"
        Me.rbRelease.Size = New System.Drawing.Size(64, 17)
        Me.rbRelease.TabIndex = 6
        Me.rbRelease.TabStop = True
        Me.rbRelease.Text = "Release"
        Me.rbRelease.UseVisualStyleBackColor = True
        '
        'rbWait
        '
        Me.rbWait.AutoSize = True
        Me.rbWait.Location = New System.Drawing.Point(5, 3)
        Me.rbWait.Name = "rbWait"
        Me.rbWait.Size = New System.Drawing.Size(47, 17)
        Me.rbWait.TabIndex = 7
        Me.rbWait.TabStop = True
        Me.rbWait.Text = "Wait"
        Me.rbWait.UseVisualStyleBackColor = True
        '
        'rbLoop
        '
        Me.rbLoop.AutoSize = True
        Me.rbLoop.Location = New System.Drawing.Point(5, 19)
        Me.rbLoop.Name = "rbLoop"
        Me.rbLoop.Size = New System.Drawing.Size(49, 17)
        Me.rbLoop.TabIndex = 8
        Me.rbLoop.TabStop = True
        Me.rbLoop.Text = "Loop"
        Me.rbLoop.UseVisualStyleBackColor = True
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.txtLT)
        Me.pnlControls.Controls.Add(Me.lblLT)
        Me.pnlControls.Controls.Add(Me.txtRT)
        Me.pnlControls.Controls.Add(Me.lblRT)
        Me.pnlControls.Controls.Add(Me.pbRS)
        Me.pnlControls.Controls.Add(Me.btnA)
        Me.pnlControls.Controls.Add(Me.btnB)
        Me.pnlControls.Controls.Add(Me.btnY)
        Me.pnlControls.Controls.Add(Me.btnX)
        Me.pnlControls.Controls.Add(Me.btnDD)
        Me.pnlControls.Controls.Add(Me.btnBack)
        Me.pnlControls.Controls.Add(Me.btnLS)
        Me.pnlControls.Controls.Add(Me.btnDL)
        Me.pnlControls.Controls.Add(Me.btnLB)
        Me.pnlControls.Controls.Add(Me.btnRB)
        Me.pnlControls.Controls.Add(Me.btnDR)
        Me.pnlControls.Controls.Add(Me.btnLT)
        Me.pnlControls.Controls.Add(Me.btnRS)
        Me.pnlControls.Controls.Add(Me.btnStart)
        Me.pnlControls.Controls.Add(Me.txtLSX)
        Me.pnlControls.Controls.Add(Me.btnRT)
        Me.pnlControls.Controls.Add(Me.lblLSY)
        Me.pnlControls.Controls.Add(Me.btnDU)
        Me.pnlControls.Controls.Add(Me.lblLSX)
        Me.pnlControls.Controls.Add(Me.btnGuide)
        Me.pnlControls.Controls.Add(Me.txtLSY)
        Me.pnlControls.Controls.Add(Me.pbLS)
        Me.pnlControls.Controls.Add(Me.txtRSX)
        Me.pnlControls.Controls.Add(Me.txtRSY)
        Me.pnlControls.Controls.Add(Me.lblRSY)
        Me.pnlControls.Controls.Add(Me.lblRSX)
        Me.pnlControls.Location = New System.Drawing.Point(74, 3)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(413, 69)
        Me.pnlControls.TabIndex = 46
        '
        'txtLT
        '
        Me.txtLT.Location = New System.Drawing.Point(22, 48)
        Me.txtLT.Name = "txtLT"
        Me.txtLT.Size = New System.Drawing.Size(27, 20)
        Me.txtLT.TabIndex = 11
        Me.txtLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLT
        '
        Me.lblLT.AutoSize = True
        Me.lblLT.Location = New System.Drawing.Point(0, 51)
        Me.lblLT.Name = "lblLT"
        Me.lblLT.Size = New System.Drawing.Size(23, 13)
        Me.lblLT.TabIndex = 34
        Me.lblLT.Text = "LT:"
        '
        'txtRT
        '
        Me.txtRT.Location = New System.Drawing.Point(384, 48)
        Me.txtRT.Name = "txtRT"
        Me.txtRT.Size = New System.Drawing.Size(27, 20)
        Me.txtRT.TabIndex = 31
        Me.txtRT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRT
        '
        Me.lblRT.AutoSize = True
        Me.lblRT.Location = New System.Drawing.Point(362, 51)
        Me.lblRT.Name = "lblRT"
        Me.lblRT.Size = New System.Drawing.Size(25, 13)
        Me.lblRT.TabIndex = 32
        Me.lblRT.Text = "RT:"
        '
        'rbHold
        '
        Me.rbHold.AutoSize = True
        Me.rbHold.Location = New System.Drawing.Point(3, 19)
        Me.rbHold.Name = "rbHold"
        Me.rbHold.Size = New System.Drawing.Size(47, 17)
        Me.rbHold.TabIndex = 5
        Me.rbHold.TabStop = True
        Me.rbHold.Text = "Hold"
        Me.rbHold.UseVisualStyleBackColor = True
        '
        'rbGroup
        '
        Me.rbGroup.AutoSize = True
        Me.rbGroup.Location = New System.Drawing.Point(5, 35)
        Me.rbGroup.Name = "rbGroup"
        Me.rbGroup.Size = New System.Drawing.Size(54, 17)
        Me.rbGroup.TabIndex = 65
        Me.rbGroup.TabStop = True
        Me.rbGroup.Text = "Group"
        Me.rbGroup.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Wingdings 2", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.Green
        Me.btnAdd.Location = New System.Drawing.Point(3, 265)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(30, 28)
        Me.btnAdd.TabIndex = 36
        Me.btnAdd.Text = "Ì"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Font = New System.Drawing.Font("Wingdings 2", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnApply.Location = New System.Drawing.Point(208, 265)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(30, 28)
        Me.btnApply.TabIndex = 41
        Me.btnApply.Text = "P"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.Red
        Me.btnDelete.Location = New System.Drawing.Point(100, 265)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnDelete.Size = New System.Drawing.Size(30, 28)
        Me.btnDelete.TabIndex = 38
        Me.btnDelete.Text = "r"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'cmbAdd
        '
        Me.cmbAdd.FormattingEnabled = True
        Me.cmbAdd.Items.AddRange(New Object() {"Last", "After", "Before", "First"})
        Me.cmbAdd.Location = New System.Drawing.Point(39, 269)
        Me.cmbAdd.Name = "cmbAdd"
        Me.cmbAdd.Size = New System.Drawing.Size(55, 21)
        Me.cmbAdd.TabIndex = 37
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Font = New System.Drawing.Font("Wingdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnMoveUp.Location = New System.Drawing.Point(136, 265)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(30, 28)
        Me.btnMoveUp.TabIndex = 39
        Me.btnMoveUp.Text = "á"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Font = New System.Drawing.Font("Wingdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnMoveDown.Location = New System.Drawing.Point(172, 265)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(30, 28)
        Me.btnMoveDown.TabIndex = 40
        Me.btnMoveDown.Text = "â"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(669, 24)
        Me.MenuStrip1.TabIndex = 59
        Me.MenuStrip1.Text = "mnu"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OpenToolStripMenuItem.Text = "Open..."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As..."
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.ExportToolStripMenuItem.Text = "Export..."
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SongToolStripMenuItem, Me.PatternToolStripMenuItem, Me.BridgeModeToolStripMenuItem, Me.CronusIdentifyToolStripMenuItem, Me.CaptureCardToolStripMenuItem, Me.USBDeviceFinderToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'SongToolStripMenuItem
        '
        Me.SongToolStripMenuItem.Name = "SongToolStripMenuItem"
        Me.SongToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SongToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.SongToolStripMenuItem.Text = "Song..."
        '
        'PatternToolStripMenuItem
        '
        Me.PatternToolStripMenuItem.Name = "PatternToolStripMenuItem"
        Me.PatternToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PatternToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.PatternToolStripMenuItem.Text = "Pattern..."
        '
        'BridgeModeToolStripMenuItem
        '
        Me.BridgeModeToolStripMenuItem.Name = "BridgeModeToolStripMenuItem"
        Me.BridgeModeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.BridgeModeToolStripMenuItem.Text = "Bridge Mode..."
        '
        'CronusIdentifyToolStripMenuItem
        '
        Me.CronusIdentifyToolStripMenuItem.Name = "CronusIdentifyToolStripMenuItem"
        Me.CronusIdentifyToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.CronusIdentifyToolStripMenuItem.Text = "Cronus Identify..."
        '
        'CaptureCardToolStripMenuItem
        '
        Me.CaptureCardToolStripMenuItem.Name = "CaptureCardToolStripMenuItem"
        Me.CaptureCardToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.CaptureCardToolStripMenuItem.Text = "Capture Card..."
        '
        'lblGame
        '
        Me.lblGame.AutoSize = True
        Me.lblGame.Location = New System.Drawing.Point(4, 7)
        Me.lblGame.Name = "lblGame"
        Me.lblGame.Size = New System.Drawing.Size(38, 13)
        Me.lblGame.TabIndex = 49
        Me.lblGame.Text = "Game:"
        '
        'txtGame
        '
        Me.txtGame.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGame.Location = New System.Drawing.Point(44, 4)
        Me.txtGame.Name = "txtGame"
        Me.txtGame.Size = New System.Drawing.Size(457, 20)
        Me.txtGame.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(4, 33)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(30, 13)
        Me.lblTitle.TabIndex = 60
        Me.lblTitle.Text = "Title:"
        '
        'txtTitle
        '
        Me.txtTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitle.Location = New System.Drawing.Point(44, 30)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(457, 20)
        Me.txtTitle.TabIndex = 2
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Location = New System.Drawing.Point(4, 59)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(35, 13)
        Me.lblDesc.TabIndex = 62
        Me.lblDesc.Text = "Desc:"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(44, 56)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(457, 64)
        Me.txtDesc.TabIndex = 3
        '
        'btnPlayPause
        '
        Me.btnPlayPause.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnPlayPause.ForeColor = System.Drawing.Color.Green
        Me.btnPlayPause.Location = New System.Drawing.Point(435, 265)
        Me.btnPlayPause.Name = "btnPlayPause"
        Me.btnPlayPause.Size = New System.Drawing.Size(30, 28)
        Me.btnPlayPause.TabIndex = 43
        Me.btnPlayPause.Text = "4"
        Me.btnPlayPause.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnStop.ForeColor = System.Drawing.Color.Red
        Me.btnStop.Location = New System.Drawing.Point(471, 265)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(30, 28)
        Me.btnStop.TabIndex = 44
        Me.btnStop.Text = "<"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'fdOpen
        '
        Me.fdOpen.Filter = "AutoXB Scripts|*.axb"
        '
        'fdSave
        '
        Me.fdSave.Filter = "AutoXB Scripts|*.axb"
        '
        'tmrScriptStatus
        '
        '
        'cbPrecompile
        '
        Me.cbPrecompile.AutoSize = True
        Me.cbPrecompile.Location = New System.Drawing.Point(11, 159)
        Me.cbPrecompile.Name = "cbPrecompile"
        Me.cbPrecompile.Size = New System.Drawing.Size(68, 17)
        Me.cbPrecompile.TabIndex = 63
        Me.cbPrecompile.Text = "Precomp"
        Me.cbPrecompile.UseVisualStyleBackColor = True
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.scMain.IsSplitterFixed = True
        Me.scMain.Location = New System.Drawing.Point(0, 22)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.lblWaitTime)
        Me.scMain.Panel1.Controls.Add(Me.lbActions)
        Me.scMain.Panel1.Controls.Add(Me.btnAdd)
        Me.scMain.Panel1.Controls.Add(Me.btnStop)
        Me.scMain.Panel1.Controls.Add(Me.btnApply)
        Me.scMain.Panel1.Controls.Add(Me.btnPlayPause)
        Me.scMain.Panel1.Controls.Add(Me.btnDelete)
        Me.scMain.Panel1.Controls.Add(Me.lblDesc)
        Me.scMain.Panel1.Controls.Add(Me.cmbAdd)
        Me.scMain.Panel1.Controls.Add(Me.txtDesc)
        Me.scMain.Panel1.Controls.Add(Me.btnMoveUp)
        Me.scMain.Panel1.Controls.Add(Me.lblTitle)
        Me.scMain.Panel1.Controls.Add(Me.txtGame)
        Me.scMain.Panel1.Controls.Add(Me.txtTitle)
        Me.scMain.Panel1.Controls.Add(Me.btnMoveDown)
        Me.scMain.Panel1.Controls.Add(Me.lblGame)
        Me.scMain.Panel1.Controls.Add(Me.tcActions)
        Me.scMain.Panel1MinSize = 505
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.txtSync)
        Me.scMain.Panel2.Controls.Add(Me.cbSync)
        Me.scMain.Panel2.Controls.Add(Me.cbSyncWait)
        Me.scMain.Panel2.Controls.Add(Me.cbTrace)
        Me.scMain.Panel2.Controls.Add(Me.gbGroups)
        Me.scMain.Panel2.Controls.Add(Me.gbControllers)
        Me.scMain.Panel2.Controls.Add(Me.cbPrecompile)
        Me.scMain.Size = New System.Drawing.Size(665, 491)
        Me.scMain.SplitterDistance = 505
        Me.scMain.TabIndex = 65
        '
        'lblWaitTime
        '
        Me.lblWaitTime.Location = New System.Drawing.Point(329, 274)
        Me.lblWaitTime.Name = "lblWaitTime"
        Me.lblWaitTime.Size = New System.Drawing.Size(100, 13)
        Me.lblWaitTime.TabIndex = 63
        Me.lblWaitTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbActions
        '
        Me.lbActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbActions.FormattingEnabled = True
        Me.lbActions.IntegralHeight = False
        Me.lbActions.Location = New System.Drawing.Point(3, 297)
        Me.lbActions.Name = "lbActions"
        Me.lbActions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbActions.Size = New System.Drawing.Size(498, 191)
        Me.lbActions.TabIndex = 45
        '
        'tcActions
        '
        Me.tcActions.Controls.Add(Me.tpController)
        Me.tcActions.Controls.Add(Me.tpFlow)
        Me.tcActions.Controls.Add(Me.tpInput)
        Me.tcActions.Location = New System.Drawing.Point(3, 126)
        Me.tcActions.Name = "tcActions"
        Me.tcActions.SelectedIndex = 0
        Me.tcActions.Size = New System.Drawing.Size(498, 133)
        Me.tcActions.TabIndex = 64
        '
        'tpController
        '
        Me.tpController.Controls.Add(Me.lblControllerIP)
        Me.tpController.Controls.Add(Me.lblControllerHold)
        Me.tpController.Controls.Add(Me.txtControllerRepeat)
        Me.tpController.Controls.Add(Me.lblControllerRepeat)
        Me.tpController.Controls.Add(Me.txtControllerHold)
        Me.tpController.Controls.Add(Me.txtControllerWait)
        Me.tpController.Controls.Add(Me.lblControllerWait)
        Me.tpController.Controls.Add(Me.cbControllerIP)
        Me.tpController.Controls.Add(Me.rbPress)
        Me.tpController.Controls.Add(Me.rbHold)
        Me.tpController.Controls.Add(Me.pnlControls)
        Me.tpController.Controls.Add(Me.rbRelease)
        Me.tpController.Location = New System.Drawing.Point(4, 22)
        Me.tpController.Name = "tpController"
        Me.tpController.Padding = New System.Windows.Forms.Padding(3)
        Me.tpController.Size = New System.Drawing.Size(490, 107)
        Me.tpController.TabIndex = 0
        Me.tpController.Text = "Controller"
        Me.tpController.UseVisualStyleBackColor = True
        '
        'lblControllerIP
        '
        Me.lblControllerIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblControllerIP.AutoSize = True
        Me.lblControllerIP.Location = New System.Drawing.Point(373, 79)
        Me.lblControllerIP.Name = "lblControllerIP"
        Me.lblControllerIP.Size = New System.Drawing.Size(20, 13)
        Me.lblControllerIP.TabIndex = 71
        Me.lblControllerIP.Text = "IP:"
        '
        'lblControllerHold
        '
        Me.lblControllerHold.AutoSize = True
        Me.lblControllerHold.Location = New System.Drawing.Point(74, 79)
        Me.lblControllerHold.Name = "lblControllerHold"
        Me.lblControllerHold.Size = New System.Drawing.Size(32, 13)
        Me.lblControllerHold.TabIndex = 68
        Me.lblControllerHold.Text = "Hold:"
        '
        'txtControllerRepeat
        '
        Me.txtControllerRepeat.Location = New System.Drawing.Point(328, 76)
        Me.txtControllerRepeat.Name = "txtControllerRepeat"
        Me.txtControllerRepeat.Size = New System.Drawing.Size(31, 20)
        Me.txtControllerRepeat.TabIndex = 67
        Me.txtControllerRepeat.Text = "1"
        Me.txtControllerRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblControllerRepeat
        '
        Me.lblControllerRepeat.AutoSize = True
        Me.lblControllerRepeat.Location = New System.Drawing.Point(301, 79)
        Me.lblControllerRepeat.Name = "lblControllerRepeat"
        Me.lblControllerRepeat.Size = New System.Drawing.Size(27, 13)
        Me.lblControllerRepeat.TabIndex = 70
        Me.lblControllerRepeat.Text = "Rpt:"
        '
        'txtControllerHold
        '
        Me.txtControllerHold.Location = New System.Drawing.Point(106, 76)
        Me.txtControllerHold.Name = "txtControllerHold"
        Me.txtControllerHold.Size = New System.Drawing.Size(75, 20)
        Me.txtControllerHold.TabIndex = 65
        Me.txtControllerHold.Text = "150ms"
        Me.txtControllerHold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtControllerWait
        '
        Me.txtControllerWait.Location = New System.Drawing.Point(219, 76)
        Me.txtControllerWait.Name = "txtControllerWait"
        Me.txtControllerWait.Size = New System.Drawing.Size(76, 20)
        Me.txtControllerWait.TabIndex = 66
        Me.txtControllerWait.Text = "500ms"
        Me.txtControllerWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblControllerWait
        '
        Me.lblControllerWait.AutoSize = True
        Me.lblControllerWait.Location = New System.Drawing.Point(187, 79)
        Me.lblControllerWait.Name = "lblControllerWait"
        Me.lblControllerWait.Size = New System.Drawing.Size(32, 13)
        Me.lblControllerWait.TabIndex = 69
        Me.lblControllerWait.Text = "Wait:"
        '
        'cbControllerIP
        '
        Me.cbControllerIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbControllerIP.FormattingEnabled = True
        Me.cbControllerIP.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cbControllerIP.Location = New System.Drawing.Point(399, 76)
        Me.cbControllerIP.Name = "cbControllerIP"
        Me.cbControllerIP.Size = New System.Drawing.Size(87, 21)
        Me.cbControllerIP.TabIndex = 72
        '
        'tpFlow
        '
        Me.tpFlow.Controls.Add(Me.btnFlowTarget)
        Me.tpFlow.Controls.Add(Me.txtFlowTarget)
        Me.tpFlow.Controls.Add(Me.lblFlowTarget)
        Me.tpFlow.Controls.Add(Me.txtFlowRepeat)
        Me.tpFlow.Controls.Add(Me.lblFlowRepeat)
        Me.tpFlow.Controls.Add(Me.txtFlowWait)
        Me.tpFlow.Controls.Add(Me.lblFlowWait)
        Me.tpFlow.Controls.Add(Me.rbWait)
        Me.tpFlow.Controls.Add(Me.rbLoop)
        Me.tpFlow.Controls.Add(Me.rbGroup)
        Me.tpFlow.Location = New System.Drawing.Point(4, 22)
        Me.tpFlow.Name = "tpFlow"
        Me.tpFlow.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFlow.Size = New System.Drawing.Size(490, 107)
        Me.tpFlow.TabIndex = 1
        Me.tpFlow.Text = "Flow"
        Me.tpFlow.UseVisualStyleBackColor = True
        '
        'btnFlowTarget
        '
        Me.btnFlowTarget.Location = New System.Drawing.Point(449, 6)
        Me.btnFlowTarget.Name = "btnFlowTarget"
        Me.btnFlowTarget.Size = New System.Drawing.Size(35, 22)
        Me.btnFlowTarget.TabIndex = 75
        Me.btnFlowTarget.Text = "Set"
        Me.btnFlowTarget.UseVisualStyleBackColor = True
        '
        'txtFlowTarget
        '
        Me.txtFlowTarget.Location = New System.Drawing.Point(418, 7)
        Me.txtFlowTarget.Name = "txtFlowTarget"
        Me.txtFlowTarget.ReadOnly = True
        Me.txtFlowTarget.Size = New System.Drawing.Size(31, 20)
        Me.txtFlowTarget.TabIndex = 77
        Me.txtFlowTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFlowTarget
        '
        Me.lblFlowTarget.AutoSize = True
        Me.lblFlowTarget.Location = New System.Drawing.Point(369, 10)
        Me.lblFlowTarget.Name = "lblFlowTarget"
        Me.lblFlowTarget.Size = New System.Drawing.Size(49, 13)
        Me.lblFlowTarget.TabIndex = 76
        Me.lblFlowTarget.Text = "Tgt Line:"
        '
        'txtFlowRepeat
        '
        Me.txtFlowRepeat.Location = New System.Drawing.Point(326, 6)
        Me.txtFlowRepeat.Name = "txtFlowRepeat"
        Me.txtFlowRepeat.Size = New System.Drawing.Size(31, 20)
        Me.txtFlowRepeat.TabIndex = 72
        Me.txtFlowRepeat.Text = "1"
        Me.txtFlowRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFlowRepeat
        '
        Me.lblFlowRepeat.AutoSize = True
        Me.lblFlowRepeat.Location = New System.Drawing.Point(299, 9)
        Me.lblFlowRepeat.Name = "lblFlowRepeat"
        Me.lblFlowRepeat.Size = New System.Drawing.Size(27, 13)
        Me.lblFlowRepeat.TabIndex = 74
        Me.lblFlowRepeat.Text = "Rpt:"
        '
        'txtFlowWait
        '
        Me.txtFlowWait.Location = New System.Drawing.Point(217, 6)
        Me.txtFlowWait.Name = "txtFlowWait"
        Me.txtFlowWait.Size = New System.Drawing.Size(76, 20)
        Me.txtFlowWait.TabIndex = 71
        Me.txtFlowWait.Text = "500ms"
        Me.txtFlowWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFlowWait
        '
        Me.lblFlowWait.AutoSize = True
        Me.lblFlowWait.Location = New System.Drawing.Point(185, 9)
        Me.lblFlowWait.Name = "lblFlowWait"
        Me.lblFlowWait.Size = New System.Drawing.Size(32, 13)
        Me.lblFlowWait.TabIndex = 73
        Me.lblFlowWait.Text = "Wait:"
        '
        'tpInput
        '
        Me.tpInput.Controls.Add(Me.gbVideo)
        Me.tpInput.Controls.Add(Me.gbTest)
        Me.tpInput.Controls.Add(Me.rbInputVideo)
        Me.tpInput.Controls.Add(Me.rbInputRumble)
        Me.tpInput.Controls.Add(Me.rbInputAudio)
        Me.tpInput.Location = New System.Drawing.Point(4, 22)
        Me.tpInput.Name = "tpInput"
        Me.tpInput.Size = New System.Drawing.Size(490, 107)
        Me.tpInput.TabIndex = 2
        Me.tpInput.Text = "Input"
        Me.tpInput.UseVisualStyleBackColor = True
        '
        'gbVideo
        '
        Me.gbVideo.Controls.Add(Me.btnVideoWizard)
        Me.gbVideo.Controls.Add(Me.btnVideoColorMax)
        Me.gbVideo.Controls.Add(Me.btnVideoColorMin)
        Me.gbVideo.Controls.Add(Me.lblVideoColor)
        Me.gbVideo.Controls.Add(Me.txtVideoPixelY)
        Me.gbVideo.Controls.Add(Me.txtVideoPixelX)
        Me.gbVideo.Controls.Add(Me.lblVideoPixelY)
        Me.gbVideo.Controls.Add(Me.lblVideoPixelX)
        Me.gbVideo.Location = New System.Drawing.Point(222, 11)
        Me.gbVideo.Name = "gbVideo"
        Me.gbVideo.Size = New System.Drawing.Size(236, 83)
        Me.gbVideo.TabIndex = 9
        Me.gbVideo.TabStop = False
        Me.gbVideo.Text = "Pixel"
        '
        'btnVideoWizard
        '
        Me.btnVideoWizard.BackColor = System.Drawing.Color.Transparent
        Me.btnVideoWizard.Location = New System.Drawing.Point(76, 45)
        Me.btnVideoWizard.Name = "btnVideoWizard"
        Me.btnVideoWizard.Size = New System.Drawing.Size(154, 23)
        Me.btnVideoWizard.TabIndex = 8
        Me.btnVideoWizard.Text = "Video Capture Wizard..."
        Me.btnVideoWizard.UseVisualStyleBackColor = False
        '
        'btnVideoColorMax
        '
        Me.btnVideoColorMax.BackColor = System.Drawing.Color.White
        Me.btnVideoColorMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVideoColorMax.Location = New System.Drawing.Point(177, 17)
        Me.btnVideoColorMax.Name = "btnVideoColorMax"
        Me.btnVideoColorMax.Size = New System.Drawing.Size(23, 23)
        Me.btnVideoColorMax.TabIndex = 7
        Me.btnVideoColorMax.UseVisualStyleBackColor = False
        '
        'btnVideoColorMin
        '
        Me.btnVideoColorMin.BackColor = System.Drawing.Color.White
        Me.btnVideoColorMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVideoColorMin.Location = New System.Drawing.Point(148, 17)
        Me.btnVideoColorMin.Name = "btnVideoColorMin"
        Me.btnVideoColorMin.Size = New System.Drawing.Size(23, 23)
        Me.btnVideoColorMin.TabIndex = 6
        Me.btnVideoColorMin.UseVisualStyleBackColor = False
        '
        'lblVideoColor
        '
        Me.lblVideoColor.AutoSize = True
        Me.lblVideoColor.Location = New System.Drawing.Point(73, 22)
        Me.lblVideoColor.Name = "lblVideoColor"
        Me.lblVideoColor.Size = New System.Drawing.Size(69, 13)
        Me.lblVideoColor.TabIndex = 5
        Me.lblVideoColor.Text = "Color Range:"
        '
        'txtVideoPixelY
        '
        Me.txtVideoPixelY.Location = New System.Drawing.Point(27, 46)
        Me.txtVideoPixelY.Name = "txtVideoPixelY"
        Me.txtVideoPixelY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVideoPixelY.Size = New System.Drawing.Size(40, 20)
        Me.txtVideoPixelY.TabIndex = 4
        Me.txtVideoPixelY.Text = "0"
        '
        'txtVideoPixelX
        '
        Me.txtVideoPixelX.Location = New System.Drawing.Point(27, 19)
        Me.txtVideoPixelX.Name = "txtVideoPixelX"
        Me.txtVideoPixelX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVideoPixelX.Size = New System.Drawing.Size(40, 20)
        Me.txtVideoPixelX.TabIndex = 3
        Me.txtVideoPixelX.Text = "0"
        '
        'lblVideoPixelY
        '
        Me.lblVideoPixelY.AutoSize = True
        Me.lblVideoPixelY.Location = New System.Drawing.Point(4, 48)
        Me.lblVideoPixelY.Name = "lblVideoPixelY"
        Me.lblVideoPixelY.Size = New System.Drawing.Size(17, 13)
        Me.lblVideoPixelY.TabIndex = 1
        Me.lblVideoPixelY.Text = "Y:"
        '
        'lblVideoPixelX
        '
        Me.lblVideoPixelX.AutoSize = True
        Me.lblVideoPixelX.Location = New System.Drawing.Point(4, 22)
        Me.lblVideoPixelX.Name = "lblVideoPixelX"
        Me.lblVideoPixelX.Size = New System.Drawing.Size(17, 13)
        Me.lblVideoPixelX.TabIndex = 0
        Me.lblVideoPixelX.Text = "X:"
        '
        'gbTest
        '
        Me.gbTest.Controls.Add(Me.txtInputDuration)
        Me.gbTest.Controls.Add(Me.txtInputInterval)
        Me.gbTest.Controls.Add(Me.lblInputDuration)
        Me.gbTest.Controls.Add(Me.lblInputInterval)
        Me.gbTest.Location = New System.Drawing.Point(72, 11)
        Me.gbTest.Name = "gbTest"
        Me.gbTest.Size = New System.Drawing.Size(143, 83)
        Me.gbTest.TabIndex = 8
        Me.gbTest.TabStop = False
        Me.gbTest.Text = "Testing"
        '
        'txtInputDuration
        '
        Me.txtInputDuration.Location = New System.Drawing.Point(60, 46)
        Me.txtInputDuration.Name = "txtInputDuration"
        Me.txtInputDuration.Size = New System.Drawing.Size(75, 20)
        Me.txtInputDuration.TabIndex = 3
        Me.txtInputDuration.Text = "1h"
        '
        'txtInputInterval
        '
        Me.txtInputInterval.Location = New System.Drawing.Point(60, 20)
        Me.txtInputInterval.Name = "txtInputInterval"
        Me.txtInputInterval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInputInterval.Size = New System.Drawing.Size(75, 20)
        Me.txtInputInterval.TabIndex = 2
        Me.txtInputInterval.Text = "500ms"
        '
        'lblInputDuration
        '
        Me.lblInputDuration.AutoSize = True
        Me.lblInputDuration.Location = New System.Drawing.Point(4, 49)
        Me.lblInputDuration.Name = "lblInputDuration"
        Me.lblInputDuration.Size = New System.Drawing.Size(50, 13)
        Me.lblInputDuration.TabIndex = 1
        Me.lblInputDuration.Text = "Duration:"
        '
        'lblInputInterval
        '
        Me.lblInputInterval.AutoSize = True
        Me.lblInputInterval.Location = New System.Drawing.Point(4, 23)
        Me.lblInputInterval.Name = "lblInputInterval"
        Me.lblInputInterval.Size = New System.Drawing.Size(45, 13)
        Me.lblInputInterval.TabIndex = 0
        Me.lblInputInterval.Text = "Interval:"
        '
        'rbInputVideo
        '
        Me.rbInputVideo.AutoSize = True
        Me.rbInputVideo.Checked = True
        Me.rbInputVideo.Location = New System.Drawing.Point(5, 3)
        Me.rbInputVideo.Name = "rbInputVideo"
        Me.rbInputVideo.Size = New System.Drawing.Size(52, 17)
        Me.rbInputVideo.TabIndex = 5
        Me.rbInputVideo.TabStop = True
        Me.rbInputVideo.Text = "Video"
        Me.rbInputVideo.UseVisualStyleBackColor = True
        '
        'rbInputRumble
        '
        Me.rbInputRumble.AutoSize = True
        Me.rbInputRumble.Location = New System.Drawing.Point(5, 35)
        Me.rbInputRumble.Name = "rbInputRumble"
        Me.rbInputRumble.Size = New System.Drawing.Size(61, 17)
        Me.rbInputRumble.TabIndex = 7
        Me.rbInputRumble.Text = "Rumble"
        Me.rbInputRumble.UseVisualStyleBackColor = True
        '
        'rbInputAudio
        '
        Me.rbInputAudio.AutoSize = True
        Me.rbInputAudio.Location = New System.Drawing.Point(5, 19)
        Me.rbInputAudio.Name = "rbInputAudio"
        Me.rbInputAudio.Size = New System.Drawing.Size(52, 17)
        Me.rbInputAudio.TabIndex = 6
        Me.rbInputAudio.Text = "Audio"
        Me.rbInputAudio.UseVisualStyleBackColor = True
        '
        'txtSync
        '
        Me.txtSync.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSync.Location = New System.Drawing.Point(64, 116)
        Me.txtSync.Name = "txtSync"
        Me.txtSync.Size = New System.Drawing.Size(80, 20)
        Me.txtSync.TabIndex = 2
        '
        'cbSync
        '
        Me.cbSync.AutoSize = True
        Me.cbSync.Location = New System.Drawing.Point(11, 118)
        Me.cbSync.Name = "cbSync"
        Me.cbSync.Size = New System.Drawing.Size(53, 17)
        Me.cbSync.TabIndex = 66
        Me.cbSync.Text = "Sync:"
        Me.cbSync.UseVisualStyleBackColor = True
        '
        'cbSyncWait
        '
        Me.cbSyncWait.AutoSize = True
        Me.cbSyncWait.Location = New System.Drawing.Point(11, 139)
        Me.cbSyncWait.Name = "cbSyncWait"
        Me.cbSyncWait.Size = New System.Drawing.Size(75, 17)
        Me.cbSyncWait.TabIndex = 65
        Me.cbSyncWait.Text = "Sync Wait"
        Me.cbSyncWait.UseVisualStyleBackColor = True
        '
        'cbTrace
        '
        Me.cbTrace.AutoSize = True
        Me.cbTrace.Location = New System.Drawing.Point(90, 159)
        Me.cbTrace.Name = "cbTrace"
        Me.cbTrace.Size = New System.Drawing.Size(54, 17)
        Me.cbTrace.TabIndex = 64
        Me.cbTrace.Text = "Trace"
        Me.cbTrace.UseVisualStyleBackColor = True
        '
        'gbGroups
        '
        Me.gbGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbGroups.Controls.Add(Me.btnRenameGroup)
        Me.gbGroups.Controls.Add(Me.btnAddGroup)
        Me.gbGroups.Controls.Add(Me.btnDeleteGroup)
        Me.gbGroups.Controls.Add(Me.lbGroups)
        Me.gbGroups.Location = New System.Drawing.Point(4, 179)
        Me.gbGroups.Name = "gbGroups"
        Me.gbGroups.Size = New System.Drawing.Size(150, 309)
        Me.gbGroups.TabIndex = 1
        Me.gbGroups.TabStop = False
        Me.gbGroups.Text = "Action Groups"
        '
        'btnRenameGroup
        '
        Me.btnRenameGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRenameGroup.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRenameGroup.Location = New System.Drawing.Point(60, 275)
        Me.btnRenameGroup.Name = "btnRenameGroup"
        Me.btnRenameGroup.Size = New System.Drawing.Size(30, 28)
        Me.btnRenameGroup.TabIndex = 41
        Me.btnRenameGroup.Text = "!"
        Me.btnRenameGroup.UseVisualStyleBackColor = True
        '
        'btnAddGroup
        '
        Me.btnAddGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddGroup.Font = New System.Drawing.Font("Wingdings 2", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnAddGroup.ForeColor = System.Drawing.Color.Green
        Me.btnAddGroup.Location = New System.Drawing.Point(7, 275)
        Me.btnAddGroup.Name = "btnAddGroup"
        Me.btnAddGroup.Size = New System.Drawing.Size(30, 28)
        Me.btnAddGroup.TabIndex = 39
        Me.btnAddGroup.Text = "Ì"
        Me.btnAddGroup.UseVisualStyleBackColor = True
        '
        'btnDeleteGroup
        '
        Me.btnDeleteGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteGroup.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnDeleteGroup.ForeColor = System.Drawing.Color.Red
        Me.btnDeleteGroup.Location = New System.Drawing.Point(110, 276)
        Me.btnDeleteGroup.Name = "btnDeleteGroup"
        Me.btnDeleteGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnDeleteGroup.Size = New System.Drawing.Size(30, 28)
        Me.btnDeleteGroup.TabIndex = 40
        Me.btnDeleteGroup.Text = "r"
        Me.btnDeleteGroup.UseVisualStyleBackColor = True
        '
        'lbGroups
        '
        Me.lbGroups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbGroups.FormattingEnabled = True
        Me.lbGroups.IntegralHeight = False
        Me.lbGroups.Location = New System.Drawing.Point(7, 20)
        Me.lbGroups.Name = "lbGroups"
        Me.lbGroups.Size = New System.Drawing.Size(133, 249)
        Me.lbGroups.TabIndex = 0
        '
        'gbControllers
        '
        Me.gbControllers.Controls.Add(Me.txtController4)
        Me.gbControllers.Controls.Add(Me.lblController4)
        Me.gbControllers.Controls.Add(Me.txtController3)
        Me.gbControllers.Controls.Add(Me.lblController3)
        Me.gbControllers.Controls.Add(Me.txtController2)
        Me.gbControllers.Controls.Add(Me.lblController2)
        Me.gbControllers.Controls.Add(Me.txtController1)
        Me.gbControllers.Controls.Add(Me.lblController1)
        Me.gbControllers.Location = New System.Drawing.Point(4, 2)
        Me.gbControllers.Name = "gbControllers"
        Me.gbControllers.Size = New System.Drawing.Size(150, 109)
        Me.gbControllers.TabIndex = 0
        Me.gbControllers.TabStop = False
        Me.gbControllers.Text = "Controllers"
        '
        'txtController4
        '
        Me.txtController4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtController4.Location = New System.Drawing.Point(25, 83)
        Me.txtController4.Name = "txtController4"
        Me.txtController4.Size = New System.Drawing.Size(115, 20)
        Me.txtController4.TabIndex = 1
        '
        'lblController4
        '
        Me.lblController4.AutoSize = True
        Me.lblController4.Location = New System.Drawing.Point(7, 86)
        Me.lblController4.Name = "lblController4"
        Me.lblController4.Size = New System.Drawing.Size(13, 13)
        Me.lblController4.TabIndex = 0
        Me.lblController4.Text = "4"
        '
        'txtController3
        '
        Me.txtController3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtController3.Location = New System.Drawing.Point(25, 61)
        Me.txtController3.Name = "txtController3"
        Me.txtController3.Size = New System.Drawing.Size(115, 20)
        Me.txtController3.TabIndex = 1
        Me.txtController3.Text = "10.100.8.53"
        '
        'lblController3
        '
        Me.lblController3.AutoSize = True
        Me.lblController3.Location = New System.Drawing.Point(7, 64)
        Me.lblController3.Name = "lblController3"
        Me.lblController3.Size = New System.Drawing.Size(13, 13)
        Me.lblController3.TabIndex = 0
        Me.lblController3.Text = "3"
        '
        'txtController2
        '
        Me.txtController2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtController2.Location = New System.Drawing.Point(25, 39)
        Me.txtController2.Name = "txtController2"
        Me.txtController2.Size = New System.Drawing.Size(115, 20)
        Me.txtController2.TabIndex = 1
        Me.txtController2.Text = "10.100.8.52"
        '
        'lblController2
        '
        Me.lblController2.AutoSize = True
        Me.lblController2.Location = New System.Drawing.Point(7, 42)
        Me.lblController2.Name = "lblController2"
        Me.lblController2.Size = New System.Drawing.Size(13, 13)
        Me.lblController2.TabIndex = 0
        Me.lblController2.Text = "2"
        '
        'txtController1
        '
        Me.txtController1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtController1.Location = New System.Drawing.Point(25, 17)
        Me.txtController1.Name = "txtController1"
        Me.txtController1.Size = New System.Drawing.Size(115, 20)
        Me.txtController1.TabIndex = 1
        Me.txtController1.Text = "10.100.8.51"
        '
        'lblController1
        '
        Me.lblController1.AutoSize = True
        Me.lblController1.Location = New System.Drawing.Point(7, 20)
        Me.lblController1.Name = "lblController1"
        Me.lblController1.Size = New System.Drawing.Size(13, 13)
        Me.lblController1.TabIndex = 0
        Me.lblController1.Text = "1"
        '
        'cdCapture
        '
        Me.cdCapture.FullOpen = True
        '
        'USBDeviceFinderToolStripMenuItem
        '
        Me.USBDeviceFinderToolStripMenuItem.Name = "USBDeviceFinderToolStripMenuItem"
        Me.USBDeviceFinderToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.USBDeviceFinderToolStripMenuItem.Text = "USB Device Finder"
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(669, 513)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(685, 446)
        Me.Name = "frmEdit"
        Me.Text = "Script Editor"
        CType(Me.pbLS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbRS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlControls.ResumeLayout(False)
        Me.pnlControls.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel1.PerformLayout()
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.tcActions.ResumeLayout(False)
        Me.tpController.ResumeLayout(False)
        Me.tpController.PerformLayout()
        Me.tpFlow.ResumeLayout(False)
        Me.tpFlow.PerformLayout()
        Me.tpInput.ResumeLayout(False)
        Me.tpInput.PerformLayout()
        Me.gbVideo.ResumeLayout(False)
        Me.gbVideo.PerformLayout()
        Me.gbTest.ResumeLayout(False)
        Me.gbTest.PerformLayout()
        Me.gbGroups.ResumeLayout(False)
        Me.gbControllers.ResumeLayout(False)
        Me.gbControllers.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnA As System.Windows.Forms.Button
    Friend WithEvents btnB As System.Windows.Forms.Button
    Friend WithEvents btnY As System.Windows.Forms.Button
    Friend WithEvents btnX As System.Windows.Forms.Button
    Friend WithEvents btnDD As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnLS As System.Windows.Forms.Button
    Friend WithEvents btnDL As System.Windows.Forms.Button
    Friend WithEvents btnLB As System.Windows.Forms.Button
    Friend WithEvents btnRB As System.Windows.Forms.Button
    Friend WithEvents btnDR As System.Windows.Forms.Button
    Friend WithEvents btnLT As System.Windows.Forms.Button
    Friend WithEvents btnRS As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnRT As System.Windows.Forms.Button
    Friend WithEvents btnDU As System.Windows.Forms.Button
    Friend WithEvents btnGuide As System.Windows.Forms.Button
    Friend WithEvents pbLS As System.Windows.Forms.PictureBox
    Friend WithEvents pbRS As System.Windows.Forms.PictureBox
    Friend WithEvents txtRSY As System.Windows.Forms.TextBox
    Friend WithEvents lblRSX As System.Windows.Forms.Label
    Friend WithEvents lblRSY As System.Windows.Forms.Label
    Friend WithEvents txtRSX As System.Windows.Forms.TextBox
    Friend WithEvents txtLSX As System.Windows.Forms.TextBox
    Friend WithEvents lblLSY As System.Windows.Forms.Label
    Friend WithEvents lblLSX As System.Windows.Forms.Label
    Friend WithEvents txtLSY As System.Windows.Forms.TextBox
    Friend WithEvents rbPress As System.Windows.Forms.RadioButton
    Friend WithEvents rbRelease As System.Windows.Forms.RadioButton
    Friend WithEvents rbWait As System.Windows.Forms.RadioButton
    Friend WithEvents rbLoop As System.Windows.Forms.RadioButton
    Friend WithEvents pnlControls As System.Windows.Forms.Panel
    Friend WithEvents rbHold As System.Windows.Forms.RadioButton
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lbActions As RefreshingListBox
    Friend WithEvents cmbAdd As System.Windows.Forms.ComboBox
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblGame As System.Windows.Forms.Label
    Friend WithEvents txtGame As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents btnPlayPause As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents txtLT As System.Windows.Forms.TextBox
    Friend WithEvents lblLT As System.Windows.Forms.Label
    Friend WithEvents txtRT As System.Windows.Forms.TextBox
    Friend WithEvents lblRT As System.Windows.Forms.Label
    Friend WithEvents fdOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents fdSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents tmrScriptStatus As System.Windows.Forms.Timer
    Friend WithEvents cbPrecompile As System.Windows.Forms.CheckBox
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents gbGroups As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddGroup As System.Windows.Forms.Button
    Friend WithEvents btnDeleteGroup As System.Windows.Forms.Button
    Friend WithEvents lbGroups As AutoGH.RefreshingListBox
    Friend WithEvents gbControllers As System.Windows.Forms.GroupBox
    Friend WithEvents txtController4 As System.Windows.Forms.TextBox
    Friend WithEvents lblController4 As System.Windows.Forms.Label
    Friend WithEvents txtController3 As System.Windows.Forms.TextBox
    Friend WithEvents lblController3 As System.Windows.Forms.Label
    Friend WithEvents txtController2 As System.Windows.Forms.TextBox
    Friend WithEvents lblController2 As System.Windows.Forms.Label
    Friend WithEvents txtController1 As System.Windows.Forms.TextBox
    Friend WithEvents lblController1 As System.Windows.Forms.Label
    Friend WithEvents btnRenameGroup As System.Windows.Forms.Button
    Friend WithEvents rbGroup As System.Windows.Forms.RadioButton
    Friend WithEvents cbTrace As System.Windows.Forms.CheckBox
    Friend WithEvents lblWaitTime As System.Windows.Forms.Label
    Friend WithEvents txtSync As System.Windows.Forms.TextBox
    Friend WithEvents cbSync As System.Windows.Forms.CheckBox
    Friend WithEvents cbSyncWait As System.Windows.Forms.CheckBox
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SongToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatternToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BridgeModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CronusIdentifyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CaptureCardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cdCapture As System.Windows.Forms.ColorDialog
    Friend WithEvents tcActions As System.Windows.Forms.TabControl
    Friend WithEvents tpController As System.Windows.Forms.TabPage
    Friend WithEvents lblControllerIP As System.Windows.Forms.Label
    Friend WithEvents lblControllerHold As System.Windows.Forms.Label
    Friend WithEvents txtControllerRepeat As System.Windows.Forms.TextBox
    Friend WithEvents lblControllerRepeat As System.Windows.Forms.Label
    Friend WithEvents txtControllerHold As System.Windows.Forms.TextBox
    Friend WithEvents txtControllerWait As System.Windows.Forms.TextBox
    Friend WithEvents lblControllerWait As System.Windows.Forms.Label
    Friend WithEvents cbControllerIP As System.Windows.Forms.ComboBox
    Friend WithEvents tpFlow As System.Windows.Forms.TabPage
    Friend WithEvents btnFlowTarget As System.Windows.Forms.Button
    Friend WithEvents txtFlowTarget As System.Windows.Forms.TextBox
    Friend WithEvents lblFlowTarget As System.Windows.Forms.Label
    Friend WithEvents txtFlowRepeat As System.Windows.Forms.TextBox
    Friend WithEvents lblFlowRepeat As System.Windows.Forms.Label
    Friend WithEvents txtFlowWait As System.Windows.Forms.TextBox
    Friend WithEvents lblFlowWait As System.Windows.Forms.Label
    Friend WithEvents tpInput As System.Windows.Forms.TabPage
    Friend WithEvents gbTest As System.Windows.Forms.GroupBox
    Friend WithEvents rbInputVideo As System.Windows.Forms.RadioButton
    Friend WithEvents rbInputRumble As System.Windows.Forms.RadioButton
    Friend WithEvents rbInputAudio As System.Windows.Forms.RadioButton
    Friend WithEvents txtInputDuration As System.Windows.Forms.TextBox
    Friend WithEvents txtInputInterval As System.Windows.Forms.TextBox
    Friend WithEvents lblInputDuration As System.Windows.Forms.Label
    Friend WithEvents lblInputInterval As System.Windows.Forms.Label
    Friend WithEvents gbVideo As System.Windows.Forms.GroupBox
    Friend WithEvents btnVideoWizard As System.Windows.Forms.Button
    Friend WithEvents btnVideoColorMax As System.Windows.Forms.Button
    Friend WithEvents btnVideoColorMin As System.Windows.Forms.Button
    Friend WithEvents lblVideoColor As System.Windows.Forms.Label
    Friend WithEvents txtVideoPixelY As System.Windows.Forms.TextBox
    Friend WithEvents txtVideoPixelX As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoPixelY As System.Windows.Forms.Label
    Friend WithEvents lblVideoPixelX As System.Windows.Forms.Label
    Friend WithEvents USBDeviceFinderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
