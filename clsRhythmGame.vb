Public Class clsRhythmGame
    Public name As String
    Public code As String
    Public notes(4) As Integer
    Public drumNoteStr As String
    Public drumNotes(4) As Integer
    Public strum As Integer
    Public soloStrum As Integer
    Public dilation As Decimal
    Public loadTime As Integer
    Public truncation As Integer
    Public minimumDuration As Integer
    Public hopoTrigger As Dictionary(Of String, Integer)

    Public Sub New(bn As Xml.XmlNode, base As clsRhythmGame)
        Dim n As Xml.XmlNode
        name = bn.Attributes("Name").Value
        code = bn.Attributes("Code").Value

        n = bn.SelectSingleNode("Strum")
        If n Is Nothing Then strum = base.strum Else strum = getBtnValue(n.InnerText)
        n = bn.SelectSingleNode("SoloStrum")
        If n Is Nothing Then soloStrum = base.soloStrum Else soloStrum = getBtnValue(n.InnerText)
        Dim noteColors() As String = New String() {"Green", "Red", "Yellow", "Blue", "Orange"}
        For i As Integer = 0 To 4
            n = bn.SelectSingleNode("Notes/" & noteColors(i))
            If n Is Nothing Then notes(i) = base.notes(i) Else notes(i) = getBtnValue(n.InnerText)
        Next
        n = bn.SelectSingleNode("Dilation")
        If n Is Nothing Then dilation = base.dilation Else dilation = CDec(n.InnerText)
        n = bn.SelectSingleNode("LoadTime")
        If n Is Nothing Then loadTime = base.loadTime Else loadTime = CInt(n.InnerText)
        n = bn.SelectSingleNode("Truncation")
        If n Is Nothing Then truncation = base.truncation Else truncation = CInt(n.InnerText)
        n = bn.SelectSingleNode("MinimumDuration")
        If n Is Nothing Then minimumDuration = base.minimumDuration Else minimumDuration = CInt(n.InnerText)
        n = bn.SelectSingleNode("DrumNotes")
        If n Is Nothing Then drumNoteStr = base.drumNoteStr Else drumNoteStr = n.InnerText
        For i As Integer = 0 To 4
            drumNotes(i) = notes(CInt(drumNoteStr.Substring(i, 1)))
        Next

        n = bn.SelectSingleNode("Hopo")
        If n IsNot Nothing Then
            hopoTrigger = New Dictionary(Of String, Integer)
            Dim nodeList As Xml.XmlNodeList
            nodeList = n.SelectNodes("*")
            For Each node As Xml.XmlNode In n.SelectNodes("Song")
                hopoTrigger(node.Attributes("Title").Value) = CInt(node.InnerText)
            Next
        Else
            ' Nothing indicates the Guitar Hero default of a 1/12th node
            hopoTrigger = Nothing
        End If
    End Sub

    Private Function getBtnValue(name As String) As Integer
        If name = vbNullString Then Return 0
        Select Case name.ToUpper
            Case "RT"
                Return &H20000
            Case "LT"
                Return &H10000
            Case "RS"
                Return clsController.XBButtons.btnR3
            Case "LS"
                Return clsController.XBButtons.btnL3
            Case "BACK"
                Return clsController.XBButtons.btnBack
            Case "START"
                Return clsController.XBButtons.btnStart
            Case "RIGHT"
                Return clsController.XBButtons.btnRight
            Case "LEFT"
                Return clsController.XBButtons.btnLeft
            Case "DOWN"
                Return clsController.XBButtons.btnDown
            Case "UP"
                Return clsController.XBButtons.btnUp
            Case "Y"
                Return clsController.XBButtons.btnY
            Case "X"
                Return clsController.XBButtons.btnX
            Case "B"
                Return clsController.XBButtons.btnB
            Case "A"
                Return clsController.XBButtons.btnA
            Case "RB"
                Return clsController.XBButtons.btnRB
            Case "LB"
                Return clsController.XBButtons.btnLB
            Case Else
                Return 0
        End Select
    End Function

    Public Overrides Function toString() As String
        Return name
    End Function
End Class