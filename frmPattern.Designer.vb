<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPattern
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
        Me.dgvPattern = New System.Windows.Forms.DataGridView()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Button = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Interval = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Offset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvPattern, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPattern
        '
        Me.dgvPattern.AllowUserToAddRows = False
        Me.dgvPattern.AllowUserToDeleteRows = False
        Me.dgvPattern.AllowUserToResizeColumns = False
        Me.dgvPattern.AllowUserToResizeRows = False
        Me.dgvPattern.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPattern.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Button, Me.Interval, Me.Offset, Me.Count})
        Me.dgvPattern.Location = New System.Drawing.Point(17, 12)
        Me.dgvPattern.MultiSelect = False
        Me.dgvPattern.Name = "dgvPattern"
        Me.dgvPattern.RowHeadersVisible = False
        Me.dgvPattern.Size = New System.Drawing.Size(207, 237)
        Me.dgvPattern.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(149, 255)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(68, 255)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Button
        '
        Me.Button.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Button.DataPropertyName = "button"
        Me.Button.FillWeight = 10.0!
        Me.Button.HeaderText = "Button"
        Me.Button.Name = "Button"
        Me.Button.ReadOnly = True
        Me.Button.Width = 50
        '
        'Interval
        '
        Me.Interval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Interval.DataPropertyName = "Interval"
        Me.Interval.HeaderText = "Interval"
        Me.Interval.Name = "Interval"
        Me.Interval.Width = 50
        '
        'Offset
        '
        Me.Offset.DataPropertyName = "offset"
        Me.Offset.HeaderText = "Start@"
        Me.Offset.Name = "Offset"
        Me.Offset.Width = 50
        '
        'Count
        '
        Me.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Count.DataPropertyName = "count"
        Me.Count.HeaderText = "Count"
        Me.Count.Name = "Count"
        '
        'frmPattern
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.dgvPattern)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPattern"
        Me.Text = "Create Pattern"
        CType(Me.dgvPattern, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvPattern As System.Windows.Forms.DataGridView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Button As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Interval As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Offset As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Count As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
