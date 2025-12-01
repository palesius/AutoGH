Public Class frmMLInput
    Public response As String

    Public Shared Function MLInputBox(title As String, prompt As String, Optional defValue As String = vbNullString) As String
        Dim frmMLI As New frmMLInput(title, prompt, defValue)
        frmMLI.ShowDialog()
        Return frmMLI.response
    End Function

    Public Sub New(title As String, prompt As String, defValue As String)
        InitializeComponent()
        Me.Text = title
        lblPrompt.Text = prompt
        txtResponse.Text = defValue
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        response = txtResponse.Text
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        response = vbNullString
        Me.Close()
    End Sub
End Class