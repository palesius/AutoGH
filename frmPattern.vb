Imports System.ComponentModel
Public Class frmPattern
    Public actions As List(Of clsAction)
    Public Class patternData
        Public _button As String
        Public mask As Integer
        Public _interval As Integer
        Public _offset As Integer
        Public _count As Integer

        Public Sub New(__button As String, _mask As Integer)
            _button = __button
            mask = _mask
        End Sub

        Public Property Button As String
            Get
                Return _button
            End Get
            Set(value As String)
                _button = value
            End Set
        End Property

        Public Property Interval As String
            Get
                Return IIf(_interval > 0, _interval, vbNullString)
            End Get
            Set(value As String)
                _interval = IIf(value = vbNullString, 0, value)
            End Set
        End Property

        Public Property Offset As String
            Get
                Return IIf(_offset > 0, _offset, vbNullString)
            End Get
            Set(value As String)
                _offset = IIf(value = vbNullString, 0, value)
            End Set
        End Property

        Public Property Count As String
            Get
                Return IIf(_count > 0, _count, vbNullString)
            End Get
            Set(value As String)
                _count = IIf(value = vbNullString, 0, value)
            End Set
        End Property
    End Class

    Private buttons As List(Of patternData)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        buttons = New List(Of patternData)
        buttons.Add(New patternData("A", &H10))
        buttons.Add(New patternData("B", &H20))
        buttons.Add(New patternData("X", &H40))
        buttons.Add(New patternData("Y", &H80))
        buttons.Add(New patternData("LB", &H1))
        dgvPattern.DataSource = buttons
        dgvPattern.AutoResizeColumns()
    End Sub

    Private Sub dgvPattern_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvPattern.CellValidating
        Select Case e.ColumnIndex
            Case 0
            Case 1, 2
                Dim a As Integer
                If e.FormattedValue = vbNullString Then Exit Sub
                e.Cancel = Not Integer.TryParse(e.FormattedValue, a)
                If e.Cancel Then dgvPattern.CancelEdit()
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        actions = Nothing
        Me.Close()
    End Sub

    Private Class patternAction
        Implements IComparable(Of patternAction)
        Public time As Integer
        Public mask As Integer

        Public Sub New(_time As Integer, _mask As Integer)
            time = _time
            mask=_mask
        End Sub

        Function CompareTo(other As patternAction) As Integer Implements IComparable(Of patternAction).CompareTo
            If time < other.time Then Return -1
            If time > other.time Then Return 1
            Return 0
        End Function
    End Class

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        actions = Nothing
        Dim patternButtons As New List(Of patternData)
        Dim period As Integer = 0
        For Each pd In buttons
            If pd.Interval > 0 Then
                patternButtons.Add(pd)
                If pd.Count > 0 Then
                    period = Math.Max(period, pd.Interval * pd.Count + pd.Offset)
                End If
            End If
        Next
        Dim pressDict As New Dictionary(Of Integer, Integer)
        For Each pd In patternButtons
            For i As Integer = pd.Offset To period - 1 Step pd.Interval
                If pressDict.ContainsKey(i) Then
                    pressDict(i) = pressDict(i) Or pd.mask
                Else
                    pressDict.Add(i, pd.mask)
                End If
            Next
        Next
        Dim pressList As New List(Of patternAction)
        For Each p As KeyValuePair(Of Integer, Integer) In pressDict
            pressList.Add(New patternAction(p.Key, p.Value))
        Next
        pressDict = Nothing
        pressList.Sort()

        If pressList(0).time > 0 Then actions.Add(New clsActionWait(pressList(0).time, Nothing))

        actions = New List(Of clsAction)

        For i = 0 To pressList.Count - 1
            Dim delay As Integer
            If i = pressList.Count - 1 Then
                delay = period - pressList(i).time
            Else
                delay = pressList(i + 1).time - pressList(i).time
            End If
            actions.Add(New clsActionPress(1, pressList(i).mask, -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), 100, delay, 1, Nothing))
            actions(i).index = i
        Next


        Me.Close()
    End Sub

    Private Sub dgvPattern_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPattern.CellContentClick

    End Sub
End Class