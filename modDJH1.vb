Imports System.Xml
Module modDJH1
    Public Class clsDJHTrack
        Public id As String
        Public path As String
        Public bpm As Decimal
        Public name As String
        Public levels(4) As String
        Public twoPlayer As Boolean = False

        Public Sub New(n As Xml.XmlNode, _path As String)
            Dim IDTag As String = n.SelectSingleNode("IDTag").InnerText
            bpm = n.SelectSingleNode("BPM").InnerText
            Dim pos As Integer = IDTag.IndexOf(" ")
            id = IDTag.Substring(0, pos)
            name = IDTag.Substring(pos + 1)
            path = IO.Path.GetDirectoryName(_path) & "\" & id & "\SinglePlayer\Medium\"
            If IO.Directory.Exists(path) Then
                For i As Integer = 0 To 4
                    If IO.File.Exists(path & "Markup_Main_P1_" & i & ".fsgmub") Then levels(i) = path & "Markup_Main_P1_" & i & ".fsgmub"
                Next
            End If

            path = IO.Path.GetDirectoryName(_path) & "\" & id & "\TwoPlayer\Medium\"
            If IO.Directory.Exists(path) Then
                For i As Integer = 0 To 4
                    If IO.File.Exists(path & "Markup_Main_P2_" & i & ".fsgmub") Then levels(i) = path & "Markup_Main_P2_" & i & ".fsgmub"
                    If IO.File.Exists(path & "Markup_Main_P1_" & i & ".fsgmub") Then levels(i) = path & "Markup_Main_P1_" & i & ".fsgmub"
                Next
            End If
        End Sub
    End Class

    Public Sub createDJH1Scripts(path As String)
        Dim doc As New Xml.XmlDocument
        Dim tracks As New List(Of clsDJHTrack)
        doc.Load(path)
        Dim nodes As Xml.XmlNodeList = doc.SelectNodes("/TrackList/Track")
        For Each n As Xml.XmlNode In nodes
            tracks.Add(New clsDJHTrack(n, path))
        Next

        For Each track As clsDJHTrack In tracks
            For Each level As String In track.levels
                If level <> vbNullString Then convertFSGMUB(level, track.bpm, track.name)
            Next
        Next
    End Sub

    Public Class clsDJHNote
        Implements IComparable(Of clsDJHNote)
        Public measure As Single
        Public type As Integer
        Public length As Single

        Public Sub New(data() As Byte, index As Integer)
            measure = BitConverter.ToSingle(revBytes(data, index * 16), 0)
            type = BitConverter.ToInt32(revBytes(data, index * 16 + 4), 0)
            length = BitConverter.ToSingle(revBytes(data, index * 16 + 8), 0)
        End Sub

        Function CompareTo(other As AutoGH.modDJH1.clsDJHNote) As Integer Implements IComparable(Of clsDJHNote).CompareTo
            Dim result As Integer = measure.CompareTo(other.measure)
            If result <> 0 Then Return result
            result = length.CompareTo(other.length)
            If result <> 0 Then Return result
            Return type.CompareTo(other.type)
        End Function

    End Class

    'the crossfades seem to be a continuous stream (i.e. one picks up immediately where the last one left off, so the time isn't really meaningful for us, we just change the CF whenever we hit one and leave it that way
    'effectors, we can spin the knob when we hit them, but it seems to be independent of everything else, we can probably just toggle between one and the other every time we hit one. There's no real meaning to R vs G vs B for our purposes

    'euphoria: no real gameplay need for us, but I think we can either do a tap of Y at the end of each euphoria section or else leave that one to be manually triggered at the user's discretion
    'G/B scratchzone: i think we just hold the apporpriate button down for the length of each scratchzone.
    'That way the scratches are we doing will take effect.
    'Sample Zone: keep tapping red and switching effector constantly during zone
    'So that's all the gameplay stuff, see if that makes sense to you (since i've never played)
    'Here I'm getting into implementation just for my reference (i'm probably gonna just copy/paste this to go over while coding)
    'As we go through the note list i'll make a list of timestamps and events, so something like a tap/scratch zone will generate two events, green down & green up.
    'scratch up/down would generate two, scratch CW/CCW start & stop
    'effector would just be an effector change (we will make a second pass to make sure each one is different from last)
    'so then we take that list sort it by timestamp, and go through and make each effector unique
    'finally we start with a state of r/g/b/CF/Eff/Scratch all at zero, then we go through our timestamp event list and at each time stamp we update our state (press/release buttons, set new CF, etc), once me make all the changes for that state we add a new AutoGH wait instruction (with however much time there was from the prior state and a hold instruction with the appropriate state

    Public Class clsDJHAction
        Implements IComparable(Of clsDJHAction)
        Public toTarget As Integer
        Public toMinimum As Integer
        Public toMaximum As Integer
        Public rsY As Integer
        Public lsY As Integer
        Public rsX As Integer
        Public buttonOn As clsSimpleAction.saButtons
        Public buttonOff As clsSimpleAction.saButtons
        Public priority As Byte = 0

        Public Sub New(_toTarget As Integer, _toMinimum As Integer, _toMaximum As Integer, _button As clsSimpleAction.saButtons, _value As Integer, _priority As Byte)
            toTarget = _toTarget
            toMinimum = _toMinimum
            toMaximum = _toMaximum
            If toMinimum > toMaximum Then Stop
            Select Case _button
                Case clsSimpleAction.saButtons.btnLSY
                    lsY = _value
                    buttonOn = _button
                Case clsSimpleAction.saButtons.btnRSY
                    rsY = _value
                    buttonOn = _button
                Case clsSimpleAction.saButtons.btnRSX
                    rsX = _value
                    buttonOn = _button
                Case Else
                    If _value = 0 Then buttonOff = _button Else buttonOn = _button
            End Select
            priority = _priority
        End Sub

        Function compareTo(other As clsDJHAction) As Integer Implements IComparable(Of clsDJHAction).CompareTo
            If toTarget < other.toTarget Then Return -1
            If toTarget > other.toTarget Then Return 1
            Return 0
        End Function

        Sub merge(other As clsDJHAction)
            If Math.Abs(rsY) < Math.Abs(other.rsY) Then rsY = other.rsY
            If Math.Abs(lsY) < Math.Abs(other.lsY) Then lsY = other.lsY
            If Math.Abs(rsX) < Math.Abs(other.rsX) Then rsX = other.rsX
            If Math.Max(toMinimum, other.toMinimum) > Math.Min(toMaximum, other.toMaximum) Then Stop
            toMinimum = Math.Max(toMinimum, other.toMinimum)
            toMaximum = Math.Min(toMaximum, other.toMaximum)
            buttonOn = buttonOn Or other.buttonOn
            buttonOff = buttonOff Or other.buttonOff
            priority = Math.Min(priority, other.priority)
        End Sub
    End Class

    Public Sub convertFSGMUB(path As String, bpm As Decimal, title As String)
        Debug.Print(path)
        Dim factor As Decimal = 240000 / bpm
        Dim DJHActions As New List(Of clsDJHAction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(path & "," & bpm)
        Dim data() As Byte = IO.File.ReadAllBytes(path)
        If BitConverter.ToInt32(revBytes(data, 0), 0) <> 2 Then Exit Sub
        Dim noteCount As Integer = BitConverter.ToInt32(revBytes(data, 8), 0)
        Dim notes As New List(Of clsDJHNote)
        For i As Integer = 1 To noteCount
            Dim note As New clsDJHNote(data, i)
            Dim toMid As Integer = factor * (note.measure + note.length / 2)
            Dim toBegin As Integer = factor * note.measure
            Dim toEnd As Integer = factor * (note.measure + note.length)
            Dim duration As Integer
            Select Case note.type
                Case 0 'tap Green
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnA, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnA, 0, 2))
                Case 1 'tap Blue
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnX, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnX, 0, 2))
                Case 2 'tap Red
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnB, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnB, 0, 2))
                Case 3, 7 'scratch up/any green
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnLSY, -12, 1))
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnA, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnLSY, 0, 2))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnA, 0, 2))
                Case 5 'scratch down green
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnLSY, 12, 1))
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnA, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnLSY, 0, 2))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnA, 0, 2))
                Case 4, 8 'scratch up/any blue
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnLSY, -12, 1))
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnX, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnLSY, 0, 2))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnX, 0, 2))
                Case 6 'scratch down blue
                    duration = 24
                    If (toEnd - toBegin) < duration Then
                        toBegin = toMid - duration \ 2
                        toEnd = toMid + duration \ 2
                    End If
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnLSY, 12, 1))
                    DJHActions.Add(New clsDJHAction(toMid - duration \ 2, toBegin, toEnd - duration, clsSimpleAction.saButtons.btnX, 1, 1))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnLSY, 0, 2))
                    DJHActions.Add(New clsDJHAction(toMid + duration \ 2, toBegin + duration, toEnd, clsSimpleAction.saButtons.btnX, 0, 2))
                Case 9 'XF Right
                    DJHActions.Add(New clsDJHAction(toBegin, toBegin, toBegin + 25, clsSimpleAction.saButtons.btnRSY, -32767, 1))
                Case 10 'XF Center
                    DJHActions.Add(New clsDJHAction(toBegin, toBegin, toBegin + 25, clsSimpleAction.saButtons.btnRSY, 0, 1))
                Case 11 'XF Left
                    DJHActions.Add(New clsDJHAction(toBegin, toBegin, toBegin + 25, clsSimpleAction.saButtons.btnRSY, 32767, 1))
                Case 12, 13, 14, 50 'Effector Green/Blue/Red
                    'Dim pos As Integer = 0
                    'Dim value As Integer
                    'For ts As Integer = toBegin To toEnd Step 50
                    '    Select Case pos
                    '        Case 0
                    '            value = -127
                    '        Case 1
                    '            value = 0
                    '        Case 2
                    '            value = 127
                    '        Case 3
                    '            value = 0
                    '    End Select
                    '    DJHActions.Add(New clsDJHAction(ts, ts - 20, ts + 20, clsSimpleAction.saButtons.btnRSX, value, 3))
                    '    pos += 1
                    '    If pos = 4 Then pos = 0
                    'Next
                Case 16 'Sample Zone
                    '    Dim pos As Integer = 0
                    '    Dim value As Integer
                    '    Dim bState As TriState = TriState.UseDefault
                    '    For ts As Integer = toBegin To toEnd Step 50
                    '        Select Case pos
                    '            Case 1, 4
                    '                bState = TriState.True
                    '            Case 2, 5
                    '                bState = TriState.False
                    '        End Select
                    '        If ts > toEnd - 50 Then bState = TriState.False
                    '        Select Case pos
                    '            Case 0
                    '                value = -127
                    '            Case 2
                    '                value = 0
                    '            Case 3
                    '                value = 127
                    '            Case 5
                    '                value = 0
                    '            Case Else
                    '                value = -129
                    '        End Select
                    '        If value > -129 Then DJHActions.Add(New clsDJHAction(ts, ts - 20, ts + 20, clsSimpleAction.saButtons.btnRSX, value, 3))
                    '        If bState = TriState.True Then DJHActions.Add(New clsDJHAction(ts, ts - 20, ts + 20, clsSimpleAction.saButtons.btnB, 1, 3))
                    '        If bState = TriState.False Then DJHActions.Add(New clsDJHAction(ts, ts - 20, ts + 20, clsSimpleAction.saButtons.btnB, 0, 3))
                    '        pos += 1
                    '        If pos = 6 Then pos = 0
                    '    Next
                Case 48 'Green Scratchzone
                    'actions.Add(New clsDJHAction(toBegin, toBegin, toBegin + 10, clsSimpleAction.saButtons.btnA, 1, 2))
                    'actions.Add(New clsDJHAction(toEnd, toEnd - 10, toEnd, clsSimpleAction.saButtons.btnA, 0, 3))
                Case 49 'Blue Scratchzone
                    'actions.Add(New clsDJHAction(toBegin, toBegin, toBegin + 10, clsSimpleAction.saButtons.btnX, 1, 2))
                    'actions.Add(New clsDJHAction(toEnd, toEnd - 10, toEnd, clsSimpleAction.saButtons.btnX, 0, 3))
            End Select
            sb.AppendLine(note.measure & "," & note.length & "," & note.type)
        Next
        DJHActions.Sort()

        'merge dupes

        Dim remove As New List(Of clsDJHAction)

        Dim last As clsDJHAction = Nothing
        For Each sa As clsDJHAction In DJHActions
            If Not last Is Nothing Then
                If sa.toTarget = last.toTarget Then
                    last.merge(sa)
                    remove.Add(sa)
                Else
                    last = sa
                End If
            Else
                last = sa
            End If
        Next

        For Each sa As clsDJHAction In remove
            DJHActions.Remove(sa)
        Next
        remove.Clear()

        last = Nothing
        For Each sa As clsDJHAction In DJHActions
            If Not last Is Nothing Then
                If Math.Abs(sa.toTarget - last.toTarget) < 12 Then
                    Dim overlapMin As Integer = Math.Max(sa.toMinimum, last.toMinimum)
                    Dim overlapMax As Integer = Math.Min(sa.toMaximum, last.toMaximum)
                    Dim newTarget As Integer = 0
                    If overlapMin <= overlapMax Then
                        If sa.priority < last.priority Then
                            sa.merge(last)
                            remove.Add(last)
                            last = sa
                        End If
                        If sa.priority > last.priority Then
                            last.merge(sa)
                            remove.Add(sa)
                        End If
                        If sa.priority = last.priority Then
                            newTarget = (sa.toTarget + last.toTarget) \ 2
                            newTarget = Math.Min(newTarget, overlapMax)
                            newTarget = Math.Max(newTarget, overlapMin)
                            sa.merge(last)
                            remove.Add(last)
                            sa.toTarget = newTarget
                            last = sa
                        End If
                    Else
                        Stop
                    End If

                    'if we can't, shift them apart temporally
                Else
                    last = sa
                End If
            Else
                last = sa
            End If
        Next

        For Each sa As clsDJHAction In remove
            DJHActions.Remove(sa)
        Next
        remove.Clear()

        last = Nothing
        For Each sa As clsDJHAction In DJHActions
            If Not last Is Nothing Then
                If sa.toTarget = last.toTarget Or Math.Abs(sa.toTarget - last.toTarget) < 10 Then Stop
            End If
            last = sa
        Next

        Dim g As New clsActionGroup("[Main]")
        Dim actions As New List(Of clsAction)
        Dim curTime As Integer = 0
        Dim prevAXBMask As Integer = 0
        Dim curAXBMask As Integer = 0
        Dim maskOn As Integer = 0
        Dim maskOff As Integer = 0
        Dim onRS As Boolean = False
        Dim onLS As Boolean = False
        Dim offRS As Boolean = False
        Dim offLS As Boolean = False
        Dim hasRT As Boolean = False
        Dim lastXF As Integer = 0
        For Each sa As clsDJHAction In DJHActions
            If sa.toTarget > curTime Then
                actions.Add(New clsActionWait(sa.toTarget - curTime, g))
                curTime = sa.toTarget
            End If
            curAXBMask = curAXBMask Or (sa.buttonOn And (clsSimpleAction.saButtons.btnA Or clsSimpleAction.saButtons.btnB Or clsSimpleAction.saButtons.btnX))
            curAXBMask = curAXBMask And Not (sa.buttonOff And (clsSimpleAction.saButtons.btnA Or clsSimpleAction.saButtons.btnB Or clsSimpleAction.saButtons.btnX))
            maskOn = sa.buttonOn And Not (clsSimpleAction.saButtons.btnA Or clsSimpleAction.saButtons.btnB Or clsSimpleAction.saButtons.btnX)
            maskOff = sa.buttonOn And Not (clsSimpleAction.saButtons.btnA Or clsSimpleAction.saButtons.btnB Or clsSimpleAction.saButtons.btnX)
            hasRT = curAXBMask <> prevAXBMask
            prevAXBMask = curAXBMask
            If sa.buttonOn And clsSimpleAction.saButtons.btnRSY Then lastXF = sa.rsY
            If sa.buttonOff And clsSimpleAction.saButtons.btnRSY Then lastXF = 0
            onRS = sa.buttonOn And (clsSimpleAction.saButtons.btnRSX Or clsSimpleAction.saButtons.btnRSY)
            onLS = sa.buttonOn And (clsSimpleAction.saButtons.btnLSX Or clsSimpleAction.saButtons.btnLSY)
            offRS = sa.buttonOff And (clsSimpleAction.saButtons.btnRSX Or clsSimpleAction.saButtons.btnRSY)
            offLS = sa.buttonOff And (clsSimpleAction.saButtons.btnLSX Or clsSimpleAction.saButtons.btnLSY)
            If sa.buttonOn > 0 Or onRS Or onLS Or hasRT Then
                Dim ls As Point = New Point(-32768, -32768)
                Dim rs As Point = New Point(-32768, -32768)
                If onRS Then rs = New Point(sa.rsX, lastXF)
                If onLS Then ls = New Point(0, sa.lsY)
                'If hasRT Then maskOn = maskOn Or clsSimpleAction.saButtons.btnRT
                actions.Add(New clsActionHold(1, maskOn, -1, curAXBMask \ 16, ls, rs, g))
            End If
            If sa.buttonOff > 0 Then
                actions.Add(New clsActionRelease(1, sa.buttonOff, False, curAXBMask = 0, offLS, offRS, g))
            End If
        Next

        saveActionsXML(actions, IO.Path.ChangeExtension(path, ".axb"), title)

        'Stop
        '0=a
        '1=X
        '2=b
        '3=a(hold)+ly -1
        '4=x(hold)+ly -1
        '5=b(hold)+ly -1
        '6=a(hold)+ly 1
        '7=x(hold)+ly 1
        '8=b(hold)+ly 1
        '9=ry+127
        '10=ry 0
        '11=ry-127
        '12=a+"spin" rx
        '13=x+"spin" rx
        '14=b+"spin" rx
        '15=y
        '16=b,spin rx,b,spin rx
    End Sub

    Private Sub saveActionsXML(actions As List(Of clsAction), path As String, title As String)
        Dim doc As New XmlDocument
        Dim root As XmlElement = doc.CreateElement("XBScript")
        doc.AppendChild(root)
        Dim desc As XmlElement = doc.CreateElement("Information")
        root.AppendChild(desc)
        desc.AppendChild(doc.CreateElement("Game")).InnerText = "DJ Hero 1"
        desc.AppendChild(doc.CreateElement("Title")).InnerText = title
        desc.AppendChild(doc.CreateElement("Description")).InnerText = ""
        desc.AppendChild(doc.CreateElement("Version")).InnerText = 2
        Dim agsNode As XmlElement = doc.CreateElement("ActionGroups")
        root.AppendChild(agsNode)
        Dim agNode As XmlElement = doc.CreateElement("ActionGroup")
        agsNode.AppendChild(agNode)
        agNode.AppendChild(doc.CreateElement("Name")).InnerText = "[Main]"
        For Each action As clsAction In actions
            agNode.AppendChild(action.toXML(doc))
        Next
        IO.File.WriteAllText(path, doc.OuterXml)
    End Sub

    Private Function revBytes(src() As Byte, index As Integer, Optional length As Integer = 4) As Byte()
        Dim tmp(length - 1) As Byte
        Array.Copy(src, index, tmp, 0, length)
        Array.Reverse(tmp)
        Return tmp
    End Function
End Module
