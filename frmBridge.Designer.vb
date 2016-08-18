<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBridge
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
        Me.lblController = New System.Windows.Forms.Label()
        Me.cmbController = New System.Windows.Forms.ComboBox()
        Me.btnStartBridge = New System.Windows.Forms.Button()
        Me.btnStopBridge = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblController
        '
        Me.lblController.AutoSize = True
        Me.lblController.Location = New System.Drawing.Point(12, 15)
        Me.lblController.Name = "lblController"
        Me.lblController.Size = New System.Drawing.Size(54, 13)
        Me.lblController.TabIndex = 0
        Me.lblController.Text = "Controller:"
        '
        'cmbController
        '
        Me.cmbController.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbController.FormattingEnabled = True
        Me.cmbController.Location = New System.Drawing.Point(72, 12)
        Me.cmbController.Name = "cmbController"
        Me.cmbController.Size = New System.Drawing.Size(121, 21)
        Me.cmbController.TabIndex = 1
        '
        'btnStartBridge
        '
        Me.btnStartBridge.Location = New System.Drawing.Point(12, 39)
        Me.btnStartBridge.Name = "btnStartBridge"
        Me.btnStartBridge.Size = New System.Drawing.Size(51, 23)
        Me.btnStartBridge.TabIndex = 2
        Me.btnStartBridge.Text = "Start"
        Me.btnStartBridge.UseVisualStyleBackColor = True
        '
        'btnStopBridge
        '
        Me.btnStopBridge.Enabled = False
        Me.btnStopBridge.Location = New System.Drawing.Point(69, 39)
        Me.btnStopBridge.Name = "btnStopBridge"
        Me.btnStopBridge.Size = New System.Drawing.Size(51, 23)
        Me.btnStopBridge.TabIndex = 3
        Me.btnStopBridge.Text = "Stop"
        Me.btnStopBridge.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(142, 39)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(51, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmBridge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(202, 68)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnStopBridge)
        Me.Controls.Add(Me.btnStartBridge)
        Me.Controls.Add(Me.cmbController)
        Me.Controls.Add(Me.lblController)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmBridge"
        Me.Text = "Bridge Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblController As System.Windows.Forms.Label
    Friend WithEvents cmbController As System.Windows.Forms.ComboBox
    Friend WithEvents btnStartBridge As System.Windows.Forms.Button
    Friend WithEvents btnStopBridge As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
