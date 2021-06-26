<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMirror
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.cbTarget1 = New System.Windows.Forms.CheckBox()
        Me.cbTarget2 = New System.Windows.Forms.CheckBox()
        Me.cbTarget3 = New System.Windows.Forms.CheckBox()
        Me.cbTarget4 = New System.Windows.Forms.CheckBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(12, 107)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(131, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 12)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(44, 23)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'cbTarget1
        '
        Me.cbTarget1.AutoSize = True
        Me.cbTarget1.Location = New System.Drawing.Point(62, 12)
        Me.cbTarget1.Name = "cbTarget1"
        Me.cbTarget1.Size = New System.Drawing.Size(81, 17)
        Me.cbTarget1.TabIndex = 4
        Me.cbTarget1.Text = "CheckBox1"
        Me.cbTarget1.UseVisualStyleBackColor = True
        '
        'cbTarget2
        '
        Me.cbTarget2.AutoSize = True
        Me.cbTarget2.Location = New System.Drawing.Point(62, 36)
        Me.cbTarget2.Name = "cbTarget2"
        Me.cbTarget2.Size = New System.Drawing.Size(81, 17)
        Me.cbTarget2.TabIndex = 5
        Me.cbTarget2.Text = "CheckBox2"
        Me.cbTarget2.UseVisualStyleBackColor = True
        '
        'cbTarget3
        '
        Me.cbTarget3.AutoSize = True
        Me.cbTarget3.Location = New System.Drawing.Point(62, 60)
        Me.cbTarget3.Name = "cbTarget3"
        Me.cbTarget3.Size = New System.Drawing.Size(81, 17)
        Me.cbTarget3.TabIndex = 6
        Me.cbTarget3.Text = "CheckBox3"
        Me.cbTarget3.UseVisualStyleBackColor = True
        '
        'cbTarget4
        '
        Me.cbTarget4.AutoSize = True
        Me.cbTarget4.Location = New System.Drawing.Point(62, 84)
        Me.cbTarget4.Name = "cbTarget4"
        Me.cbTarget4.Size = New System.Drawing.Size(81, 17)
        Me.cbTarget4.TabIndex = 7
        Me.cbTarget4.Text = "CheckBox4"
        Me.cbTarget4.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(12, 41)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(44, 23)
        Me.btnStop.TabIndex = 8
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'frmMirror
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(154, 137)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.cbTarget4)
        Me.Controls.Add(Me.cbTarget3)
        Me.Controls.Add(Me.cbTarget2)
        Me.Controls.Add(Me.cbTarget1)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMirror"
        Me.Text = "Input Mirroring"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents cbTarget1 As System.Windows.Forms.CheckBox
    Friend WithEvents cbTarget2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbTarget3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbTarget4 As System.Windows.Forms.CheckBox
    Friend WithEvents btnStop As System.Windows.Forms.Button
End Class
