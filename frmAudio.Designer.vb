<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAudio
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
        Me.cmbDriver = New System.Windows.Forms.ComboBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtChannels = New System.Windows.Forms.TextBox()
        Me.lblChannels = New System.Windows.Forms.Label()
        Me.btnTestAll = New System.Windows.Forms.Button()
        Me.gbMappings = New System.Windows.Forms.GroupBox()
        Me.cmbOMPart = New System.Windows.Forms.ComboBox()
        Me.btnOMTone = New System.Windows.Forms.Button()
        Me.lblOMChannel = New System.Windows.Forms.Label()
        Me.gbDriver = New System.Windows.Forms.GroupBox()
        Me.tmrASIO = New System.Windows.Forms.Timer(Me.components)
        Me.gbMappings.SuspendLayout()
        Me.gbDriver.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbDriver
        '
        Me.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDriver.FormattingEnabled = True
        Me.cmbDriver.Location = New System.Drawing.Point(6, 19)
        Me.cmbDriver.Name = "cmbDriver"
        Me.cmbDriver.Size = New System.Drawing.Size(188, 21)
        Me.cmbDriver.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(140, 152)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(59, 152)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtChannels
        '
        Me.txtChannels.Location = New System.Drawing.Point(80, 48)
        Me.txtChannels.Name = "txtChannels"
        Me.txtChannels.ReadOnly = True
        Me.txtChannels.Size = New System.Drawing.Size(33, 20)
        Me.txtChannels.TabIndex = 4
        '
        'lblChannels
        '
        Me.lblChannels.AutoSize = True
        Me.lblChannels.Location = New System.Drawing.Point(8, 51)
        Me.lblChannels.Name = "lblChannels"
        Me.lblChannels.Size = New System.Drawing.Size(54, 13)
        Me.lblChannels.TabIndex = 5
        Me.lblChannels.Text = "Channels:"
        '
        'btnTestAll
        '
        Me.btnTestAll.Enabled = False
        Me.btnTestAll.Location = New System.Drawing.Point(119, 46)
        Me.btnTestAll.Name = "btnTestAll"
        Me.btnTestAll.Size = New System.Drawing.Size(75, 23)
        Me.btnTestAll.TabIndex = 6
        Me.btnTestAll.Text = "Test All"
        Me.btnTestAll.UseVisualStyleBackColor = True
        '
        'gbMappings
        '
        Me.gbMappings.Controls.Add(Me.cmbOMPart)
        Me.gbMappings.Controls.Add(Me.btnOMTone)
        Me.gbMappings.Controls.Add(Me.lblOMChannel)
        Me.gbMappings.Location = New System.Drawing.Point(15, 95)
        Me.gbMappings.Name = "gbMappings"
        Me.gbMappings.Size = New System.Drawing.Size(200, 51)
        Me.gbMappings.TabIndex = 10
        Me.gbMappings.TabStop = False
        Me.gbMappings.Text = "Output Mappings"
        '
        'cmbOMPart
        '
        Me.cmbOMPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOMPart.FormattingEnabled = True
        Me.cmbOMPart.Items.AddRange(New Object() {"None", "1", "2", "3"})
        Me.cmbOMPart.Location = New System.Drawing.Point(129, 21)
        Me.cmbOMPart.Name = "cmbOMPart"
        Me.cmbOMPart.Size = New System.Drawing.Size(65, 21)
        Me.cmbOMPart.TabIndex = 2
        '
        'btnOMTone
        '
        Me.btnOMTone.Location = New System.Drawing.Point(25, 19)
        Me.btnOMTone.Name = "btnOMTone"
        Me.btnOMTone.Size = New System.Drawing.Size(98, 23)
        Me.btnOMTone.TabIndex = 1
        Me.btnOMTone.Text = "Start Tone"
        Me.btnOMTone.UseVisualStyleBackColor = True
        '
        'lblOMChannel
        '
        Me.lblOMChannel.AutoSize = True
        Me.lblOMChannel.Location = New System.Drawing.Point(6, 24)
        Me.lblOMChannel.Name = "lblOMChannel"
        Me.lblOMChannel.Size = New System.Drawing.Size(13, 13)
        Me.lblOMChannel.TabIndex = 0
        Me.lblOMChannel.Text = "1"
        '
        'gbDriver
        '
        Me.gbDriver.Controls.Add(Me.cmbDriver)
        Me.gbDriver.Controls.Add(Me.btnTestAll)
        Me.gbDriver.Controls.Add(Me.txtChannels)
        Me.gbDriver.Controls.Add(Me.lblChannels)
        Me.gbDriver.Location = New System.Drawing.Point(15, 12)
        Me.gbDriver.Name = "gbDriver"
        Me.gbDriver.Size = New System.Drawing.Size(200, 77)
        Me.gbDriver.TabIndex = 11
        Me.gbDriver.TabStop = False
        Me.gbDriver.Text = "Asio Driver"
        '
        'tmrASIO
        '
        '
        'frmAudio
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(230, 189)
        Me.ControlBox = False
        Me.Controls.Add(Me.gbDriver)
        Me.Controls.Add(Me.gbMappings)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmAudio"
        Me.Text = "Audio Settings"
        Me.gbMappings.ResumeLayout(False)
        Me.gbMappings.PerformLayout()
        Me.gbDriver.ResumeLayout(False)
        Me.gbDriver.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbDriver As ComboBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtChannels As TextBox
    Friend WithEvents lblChannels As Label
    Friend WithEvents btnTestAll As Button
    Friend WithEvents gbMappings As GroupBox
    Friend WithEvents btnOMTone As Button
    Friend WithEvents lblOMChannel As Label
    Friend WithEvents cmbOMPart As ComboBox
    Friend WithEvents gbDriver As GroupBox
    Friend WithEvents tmrASIO As Timer
End Class
