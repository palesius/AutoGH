<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCMIdentify
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCMIdentify))
        Me.btnDetect = New System.Windows.Forms.Button()
        Me.btnIdentify = New System.Windows.Forms.Button()
        Me.cbDevices = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnIdentifyAll = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnDetect
        '
        Me.btnDetect.Location = New System.Drawing.Point(32, 224)
        Me.btnDetect.Name = "btnDetect"
        Me.btnDetect.Size = New System.Drawing.Size(75, 23)
        Me.btnDetect.TabIndex = 0
        Me.btnDetect.Text = "Detect"
        Me.btnDetect.UseVisualStyleBackColor = True
        '
        'btnIdentify
        '
        Me.btnIdentify.Enabled = False
        Me.btnIdentify.Location = New System.Drawing.Point(173, 224)
        Me.btnIdentify.Name = "btnIdentify"
        Me.btnIdentify.Size = New System.Drawing.Size(97, 23)
        Me.btnIdentify.TabIndex = 1
        Me.btnIdentify.Text = "Identify Selected"
        Me.btnIdentify.UseVisualStyleBackColor = True
        '
        'cbDevices
        '
        Me.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDevices.FormattingEnabled = True
        Me.cbDevices.Location = New System.Drawing.Point(110, 226)
        Me.cbDevices.Name = "cbDevices"
        Me.cbDevices.Size = New System.Drawing.Size(57, 21)
        Me.cbDevices.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(300, 208)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(176, 253)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnIdentifyAll
        '
        Me.btnIdentifyAll.Enabled = False
        Me.btnIdentifyAll.Location = New System.Drawing.Point(32, 253)
        Me.btnIdentifyAll.Name = "btnIdentifyAll"
        Me.btnIdentifyAll.Size = New System.Drawing.Size(138, 23)
        Me.btnIdentifyAll.TabIndex = 5
        Me.btnIdentifyAll.Text = "Identify All"
        Me.btnIdentifyAll.UseVisualStyleBackColor = True
        '
        'frmCMIdentify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 285)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnIdentifyAll)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbDevices)
        Me.Controls.Add(Me.btnIdentify)
        Me.Controls.Add(Me.btnDetect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCMIdentify"
        Me.Text = "Cronus Identification"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDetect As System.Windows.Forms.Button
    Friend WithEvents btnIdentify As System.Windows.Forms.Button
    Friend WithEvents cbDevices As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnIdentifyAll As System.Windows.Forms.Button
End Class
