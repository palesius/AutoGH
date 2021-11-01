Imports System.Xml
Public Enum ActionType
    actWait
    actLoop
    actHold
    actRelease
    actPress
    actGroup
    actInputVideo
    actInputAudio
    actInputRumble
End Enum

Public Class clsActionGroup
    Public name As String
    Public actions As List(Of clsAction)

    Public Sub New(_name As String)
        name = _name
        actions = New List(Of clsAction)
    End Sub

    Public Function containsGroup(g As clsActionGroup) As Boolean
        For Each a As clsAction In actions
            If a.getActType = ActionType.actGroup Then
                Dim ag As clsActionAGroup = a
                If ag.target Is g Then Return True
                If ag.target.containsGroup(g) Then Return True
            End If
        Next
        Return False
    End Function

    Public Function getActions() As clsAction()
        Dim tmp As New List(Of clsAction)
        For Each action As clsAction In actions
            tmp.Add(action.clone)
        Next
        For Each action As clsAction In tmp
            If action.getActType = ActionType.actLoop Then
                Dim agLoop As clsActionLoop = action
                agLoop.linkTarget(tmp)
            End If
        Next
        Dim final As New List(Of clsAction)
        For Each action As clsAction In tmp
            final.Add(action)
            If action.getActType = ActionType.actGroup Then
                Dim agAction As clsActionAGroup = action
                For i As Integer = 1 To agAction.repeat
                    final.AddRange(agAction.target.getActions())
                Next
            End If
        Next
        Return final.ToArray
    End Function

    Public Overrides Function ToString() As String
        Return name
    End Function
End Class

Public MustInherit Class clsAction
    Public index As Integer
    Public referrers As New List(Of clsActionLoop)
    Public controllerNumber As Byte
    Public group As clsActionGroup
    Public comment As String
    Public original As clsAction

    Public MustOverride Function getDescription() As String
    Public MustOverride Function getActType() As ActionType
    Public MustOverride Function Clone() As clsAction

    Public Overridable Function isInput() As Boolean
        Return False
    End Function

    Public Sub baseClone(orig As clsAction)
        index = orig.index
        referrers.Clear()
        controllerNumber = orig.controllerNumber
        group = orig.group
        original = orig
    End Sub

    Public Sub refresh(lb As RefreshingListBox)
        lb.RefreshItem(index)
        For Each action In referrers
            action.refresh(lb)
        Next
    End Sub

    Public Overridable Sub delete()
        For Each action In referrers
            action.target = Nothing
        Next
    End Sub

    Public Overridable Function serialize() As String
        Return Nothing
    End Function

    Public MustOverride Function toXML(doc As XmlDocument) As XmlElement

    Public Shared Function deSerialize(src As String, group As clsActionGroup) As clsAction
        Dim type As String = Left(src, InStr(1, src, ",") - 1)
        Select Case type
            Case "Wait"
                Return New clsActionWait(src, group)
            Case "Loop"
                Return New clsActionLoop(src, group)
            Case "Hold"
                Return New clsActionHold(src, group)
            Case "Release"
                Return New clsActionRelease(src, group)
            Case "Press"
                Return New clsActionPress(src, group)
            Case Else
                Return Nothing
        End Select
    End Function

    Public Shared Function fromXML(node As XmlElement, version As Integer, group As clsActionGroup)
        Dim a As clsAction
        Select Case node.Name
            Case "Wait"
                a = New clsActionWait(node, version, group)
            Case "Loop"
                a = New clsActionLoop(node, version, group)
            Case "Hold"
                a = New clsActionHold(node, version, group)
            Case "Release"
                a = New clsActionRelease(node, version, group)
            Case "Press"
                a = New clsActionPress(node, version, group)
            Case "Group"
                a = New clsActionAGroup(node, version, group)
            Case "VideoInput"
                a = New clsActionInputVideo(node, version, group)
            Case Else
                Return Nothing
                Stop
        End Select
        Dim comAtt As XmlAttribute = node.Attributes("Comment")
        If Not comAtt Is Nothing Then a.comment = comAtt.Value
        Return a
    End Function

    Public Overrides Function toString() As String
        Return (index + 1) & ": " & getDescription() & IIf(comment <> vbNullString, " {" & comment & "}", "")
    End Function
End Class

Public MustInherit Class clsActionInput
    Inherits clsAction

    Public interval As Integer
    Public duration As Integer
    Public startTime As Date
    Public nextTest As Date
    Public lastTest As Date

    Protected MustOverride Function inputTest() As Boolean

    Public Function test() As Boolean
        If Now > lastTest OrElse inputTest() Then
            Return True
        Else
            nextTest = nextTest.AddMilliseconds(interval)
            Return False
        End If
    End Function

    Public Sub start()
        startTime = Now
        nextTest = startTime.AddMilliseconds(interval)
        lastTest = startTime.AddMilliseconds(duration)
    End Sub

    Public Overrides Function isInput() As Boolean
        Return True
    End Function
End Class

Public Class clsActionWait
    Inherits clsAction

    Public delay As Integer

    Public Sub New(_delay As Integer, _group As clsActionGroup)
        controllerNumber = 0
        delay = _delay
        group = _group
    End Sub

    Public Overrides Function getDescription() As String
        Return "Wait for " & formatMS(delay)
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actWait
    End Function

    Public Overrides Function serialize() As String
        Return "Wait," & delay
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Wait")
        tmp.SetAttribute("Delay", delay)
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub New(serial As String, _group As clsActionGroup)
        Dim toks() As String = serial.Split(",")
        delay = toks(1)
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        delay = node.Attributes("Delay").Value
        group = _group
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionWait(delay, group)
        tmp.baseClone(Me)
        Return tmp
    End Function
End Class

Public Class clsActionLoop
    Inherits clsAction

    Public repeat As Integer
    Public repeatLeft As Integer
    Public target As clsAction
    Private tgtIndex As Integer

    Public Sub New(_target As clsAction, _repeat As Integer, _group As clsActionGroup)
        controllerNumber = 0
        repeat = _repeat
        target = _target
        target.referrers.Add(Me)
        group = _group
    End Sub

    Public Overrides Sub delete()
        MyBase.delete()
        If Not target Is Nothing Then target.referrers.Remove(Me)
    End Sub

    Public Overrides Function getDescription() As String
        If target Is Nothing Then
            Return "Loop to N/A, " & repeat & " times"
        Else
            Return "Loop to " & (target.index + 1) & ", " & repeat & " times"
        End If
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actLoop
    End Function

    Public Overrides Function serialize() As String
        Return "Loop," & target.index & "," & repeat
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Loop")
        tmp.SetAttribute("Target", target.index)
        tmp.SetAttribute("Repeat", repeat)
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub linkTarget(l As List(Of clsAction))
        target = l(Me.tgtIndex)
        target.referrers.Add(Me)
    End Sub

    Public Sub New(serial As String, _group As clsActionGroup)
        Dim toks() As String = serial.Split(",")
        target = Nothing
        tgtIndex = toks(1)
        repeat = toks(2)
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        target = Nothing
        tgtIndex = node.Attributes("Target").Value
        repeat = node.Attributes("Repeat").Value
        group = _group
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionLoop(target, repeat, group)
        tmp.baseClone(Me)
        Return tmp
    End Function
End Class

Public Class clsActionAGroup
    Inherits clsAction

    Public repeat As Integer
    Public repeatLeft As Integer
    Public target As clsActionGroup
    Private tgtName As String

    Public Sub New(_target As clsActionGroup, _repeat As Integer, _group As clsActionGroup)
        controllerNumber = 0
        repeat = _repeat
        target = _target
        tgtName = target.name
        group = _group
    End Sub

    Public Overrides Sub delete()
        MyBase.delete()
    End Sub

    Public Overrides Function getDescription() As String
        If target Is Nothing Then
            Return "Do {Nothing}"
        Else
            If repeat = 1 Then
                Return "Do {" & tgtName & "}"
            Else
                Return "Do {" & tgtName & "}, " & repeat & " times"
            End If
        End If
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actGroup
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Group")
        tmp.SetAttribute("Name", target.name)
        tmp.SetAttribute("Repeat", repeat)
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub linkTarget(l As Dictionary(Of String, clsActionGroup))
        target = l(Me.tgtName)
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        group = _group
        target = Nothing
        tgtName = node.Attributes("Name").Value
        repeat = node.Attributes("Repeat").Value
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionAGroup(target, repeat, group)
        tmp.baseClone(Me)
        Return tmp
    End Function

End Class

Public Class clsActionHold
    Inherits clsAction

    Public buttonMask As Integer
    Public LT As Integer
    Public RT As Integer
    Public LS As New Point
    Public RS As New Point

    Public ReadOnly Property LTDefined As Boolean
        Get
            Return LT >= 0
        End Get
    End Property

    Public ReadOnly Property RTDefined As Boolean
        Get
            Return RT >= 0
        End Get
    End Property

    Public ReadOnly Property LSDefined As Boolean
        Get
            Return LS.X > -32768 And LS.Y > -32768
        End Get
    End Property

    Public ReadOnly Property RSDefined As Boolean
        Get
            Return RS.X > -32768 And RS.Y > -32768
        End Get
    End Property

    Public Sub New(_controllerNumber As Byte, _buttonMask As Integer, _LT As Integer, _RT As Integer, _LS As Point, _RS As Point, _group As clsActionGroup)
        controllerNumber = _controllerNumber
        buttonMask = _buttonMask
        LT = _LT
        RT = _RT
        If _LS.X > -32768 And _LS.Y > -32768 Then
            LS.X = _LS.X
            LS.Y = _LS.Y
        Else
            LS.X = -32768
            LS.Y = -32768
        End If
        If _RS.X > -32768 And _RS.Y > -32768 Then
            RS.X = _RS.X
            RS.Y = _RS.Y
        Else
            RS.X = -32768
            RS.Y = -32768
        End If
        group = _group
    End Sub

    Public Overrides Function getDescription() As String
        Dim sb As New System.Text.StringBuilder("[" & controllerNumber & "] Hold ")
        If buttonMask And clsController.XBButtons.btnGuide Then sb.Append("Guide,")
        If buttonMask And clsController.XBButtons.btnStart Then sb.Append("Start,")
        If buttonMask And clsController.XBButtons.btnBack Then sb.Append("Back,")
        If buttonMask And clsController.XBButtons.btnUp Then sb.Append("Up,")
        If buttonMask And clsController.XBButtons.btnDown Then sb.Append("Down,")
        If buttonMask And clsController.XBButtons.btnLeft Then sb.Append("Left,")
        If buttonMask And clsController.XBButtons.btnRight Then sb.Append("Right,")
        If LTDefined Then sb.Append("LT(" & LT & "),")
        If buttonMask And clsController.XBButtons.btnLB Then sb.Append("LB,")
        If buttonMask And clsController.XBButtons.btnL3 Then sb.Append("LS,")
        If LS.X > -32768 And LS.Y > -32768 Then sb.Append("LJ(" & LS.X / 256 & "," & LS.Y / 256 & "),")
        If RTDefined Then sb.Append("RT(" & RT & "),")
        If buttonMask And clsController.XBButtons.btnRB Then sb.Append("RB,")
        If buttonMask And clsController.XBButtons.btnR3 Then sb.Append("RS,")
        If RS.X > -32768 And RS.Y > -32768 Then sb.Append("RJ(" & RS.X / 256 & "," & RS.Y / 256 & "),")
        If buttonMask And clsController.XBButtons.btnA Then sb.Append("A,")
        If buttonMask And clsController.XBButtons.btnB Then sb.Append("B,")
        If buttonMask And clsController.XBButtons.btnX Then sb.Append("X,")
        If buttonMask And clsController.XBButtons.btnY Then sb.Append("Y,")
        Dim tmp As String = sb.ToString
        Return tmp.Substring(0, tmp.Length - 1)
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actHold
    End Function

    Public Overrides Function serialize() As String
        Return "Hold," & controllerNumber & "," & buttonMask & "," & IIf(LTDefined, LT, -1) & "," & LS.X & "," & LS.Y & "," & IIf(RTDefined, RT, -1) & "," & RS.X & "," & RS.Y
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Hold")
        tmp.SetAttribute("Controller", controllerNumber)
        If buttonMask > 0 Then tmp.SetAttribute("ButtonMask", buttonMask)
        If LTDefined Then tmp.SetAttribute("LT", LT)
        If RTDefined Then tmp.SetAttribute("RT", RT)
        If LS.X > -32768 And LS.Y > -32768 Then
            tmp.SetAttribute("LSX", LS.X)
            tmp.SetAttribute("LSY", LS.Y)
        End If
        If RS.X > -32768 And RS.Y > -32768 Then
            tmp.SetAttribute("RSX", RS.X)
            tmp.SetAttribute("RSY", RS.Y)
        End If
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub New(serial As String, _group As clsActionGroup)
        Dim toks() As String = serial.Split(",")
        Select Case toks(1)
            Case "10.100.8.51"
                controllerNumber = 1
            Case "10.100.8.52"
                controllerNumber = 2
            Case Else
                Stop
        End Select
        buttonMask = toks(2)
        LT = toks(3)
        LS.X = toks(4)
        LS.Y = toks(5)
        RT = toks(6)
        RS.X = toks(7)
        RS.Y = toks(8)
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        group = _group
        controllerNumber = node.Attributes("Controller").Value
        Dim att As XmlAttribute = node.Attributes("ButtonMask")
        If att Is Nothing Then buttonMask = 0 Else buttonMask = att.Value
        att = node.Attributes("LT")
        If att Is Nothing Then LT = -1 Else LT = att.Value
        att = node.Attributes("RT")
        If att Is Nothing Then RT = -1 Else RT = att.Value
        Select Case version
            Case 1
                att = node.Attributes("LSX")
                If att Is Nothing Then LS.X = -32768 Else LS.X = att.Value * 256
                att = node.Attributes("LSY")
                If att Is Nothing Then LS.Y = -32768 Else LS.Y = att.Value * 256
                att = node.Attributes("RSX")
                If att Is Nothing Then RS.X = -32768 Else RS.X = att.Value * 256
                att = node.Attributes("RSY")
                If att Is Nothing Then RS.Y = -32768 Else RS.Y = att.Value * 256
            Case 2
                att = node.Attributes("LSX")
                If att Is Nothing Then LS.X = -32768 Else LS.X = att.Value
                att = node.Attributes("LSY")
                If att Is Nothing Then LS.Y = -32768 Else LS.Y = att.Value
                att = node.Attributes("RSX")
                If att Is Nothing Then RS.X = -32768 Else RS.X = att.Value
                att = node.Attributes("RSY")
                If att Is Nothing Then RS.Y = -32768 Else RS.Y = att.Value
        End Select
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionHold(controllerNumber, buttonMask, LT, RT, LS, RS, group)
        tmp.baseClone(Me)
        Return tmp
    End Function
End Class

Public Class clsActionRelease
    Inherits clsAction

    Public buttonMask As Integer
    Public LTDefined As Boolean
    Public RTDefined As Boolean
    Public LSDefined As Boolean
    Public RSDefined As Boolean

    Public Sub New(_controllerNumber As Byte, _buttonMask As Integer, _LT As Integer, _RT As Integer, _LS As Point, _RS As Point, _group As clsActionGroup)
        controllerNumber = _controllerNumber
        buttonMask = _buttonMask
        LTDefined = _LT >= 0
        RTDefined = _RT >= 0
        LSDefined = _LS.X > -32768 And _LS.Y > -32768
        RSDefined = _RS.X > -32768 And _RS.Y > -32768
        group = _group
    End Sub

    Public Sub New(_controllerNumber As Byte, _buttonMask As Integer, _LTDefined As Boolean, _RTDefined As Boolean, _LSDefined As Boolean, _RSDefined As Boolean, _group As clsActionGroup)
        controllerNumber = _controllerNumber
        buttonMask = _buttonMask
        LTDefined = _LTDefined
        RTDefined = _RTDefined
        LSDefined = _LSDefined
        RSDefined = _RSDefined
        group = _group
    End Sub

    Public Overrides Function getDescription() As String
        Dim sb As New System.Text.StringBuilder("[" & controllerNumber & "] Release ")
        If buttonMask And clsController.XBButtons.btnGuide Then sb.Append("Guide,")
        If buttonMask And clsController.XBButtons.btnStart Then sb.Append("Start,")
        If buttonMask And clsController.XBButtons.btnBack Then sb.Append("Back,")
        If buttonMask And clsController.XBButtons.btnUp Then sb.Append("Up,")
        If buttonMask And clsController.XBButtons.btnDown Then sb.Append("Down,")
        If buttonMask And clsController.XBButtons.btnLeft Then sb.Append("Left,")
        If buttonMask And clsController.XBButtons.btnRight Then sb.Append("Right,")
        If buttonMask And clsController.XBButtons.btnLB Then sb.Append("LB,")
        If buttonMask And clsController.XBButtons.btnL3 Then sb.Append("LS,")
        If buttonMask And clsController.XBButtons.btnRB Then sb.Append("RB,")
        If buttonMask And clsController.XBButtons.btnR3 Then sb.Append("RS,")
        If buttonMask And clsController.XBButtons.btnA Then sb.Append("A,")
        If buttonMask And clsController.XBButtons.btnB Then sb.Append("B,")
        If buttonMask And clsController.XBButtons.btnX Then sb.Append("X,")
        If buttonMask And clsController.XBButtons.btnY Then sb.Append("Y,")
        If LTDefined Then sb.Append("LT(0),")
        If RTDefined Then sb.Append("RT(0),")
        Dim tmp As String = sb.ToString
        Return tmp.Substring(0, tmp.Length - 1)
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actRelease
    End Function

    Public Overrides Function serialize() As String
        Return "Release," & controllerNumber & "," & buttonMask & "," & (IIf(LTDefined, &H1, 0) Or IIf(RTDefined, &H2, 0) Or IIf(LSDefined, &H4, 0) Or IIf(RSDefined, &H8, 0))
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Release")
        tmp.SetAttribute("Controller", controllerNumber)
        If buttonMask > 0 Then tmp.SetAttribute("ButtonMask", buttonMask)
        If LTDefined Or RTDefined Or LSDefined Or RSDefined Then tmp.SetAttribute("AnalogMask", (IIf(LTDefined, &H1, 0) Or IIf(RTDefined, &H2, 0) Or IIf(LSDefined, &H4, 0) Or IIf(RSDefined, &H8, 0)))
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub New(serial As String, _group As clsActionGroup)
        Dim toks() As String = serial.Split(",")
        Select Case toks(1)
            Case "10.100.8.51"
                controllerNumber = 1
            Case "10.100.8.52"
                controllerNumber = 2
            Case Else
                Stop
        End Select
        buttonMask = toks(2)
        Dim mask2 As Integer = toks(3)
        LTDefined = mask2 And &H1
        RTDefined = mask2 And &H2
        LSDefined = mask2 And &H4
        RSDefined = mask2 And &H8
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        group = _group
        controllerNumber = node.Attributes("Controller").Value
        Dim att As XmlAttribute = node.Attributes("ButtonMask")
        If att Is Nothing Then buttonMask = 0 Else buttonMask = att.Value
        att = node.Attributes("AnalogMask")
        If att Is Nothing Then Exit Sub
        Dim mask2 As Integer = att.Value
        LTDefined = mask2 And &H1
        RTDefined = mask2 And &H2
        LSDefined = mask2 And &H4
        RSDefined = mask2 And &H8
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionRelease(controllerNumber, buttonMask, LTDefined, RTDefined, LSDefined, RSDefined, group)
        tmp.baseClone(Me)
        Return tmp
    End Function
End Class

Public Class clsActionPress
    Inherits clsAction

    Public holdTime As Integer
    Public waitTime As Integer
    Public repeat As Integer

    Public buttonMask As Integer
    Public LT As Integer
    Public RT As Integer
    Public LS As New Point
    Public RS As New Point

    Public ReadOnly Property LTDefined As Boolean
        Get
            Return LT >= 0
        End Get
    End Property

    Public ReadOnly Property RTDefined As Boolean
        Get
            Return RT >= 0
        End Get
    End Property

    Public ReadOnly Property LSDefined As Boolean
        Get
            Return LS.X > -32768 And LS.Y > -32768
        End Get
    End Property

    Public ReadOnly Property RSDefined As Boolean
        Get
            Return RS.X > -32768 And RS.Y > -32768
        End Get
    End Property

    Public Sub New(_controllerNumber As String, _buttonMask As Integer, _LT As Integer, _RT As Integer, _LS As Point, _RS As Point, _holdTime As Integer, _waitTime As Integer, _repeat As Integer, _group As clsActionGroup)
        controllerNumber = _controllerNumber
        buttonMask = _buttonMask
        LT = _LT
        RT = _RT
        If _LS.X > -32768 And _LS.Y > -32768 Then
            LS.X = _LS.X
            LS.Y = _LS.Y
        Else
            LS.X = -32768
            LS.Y = -32768
        End If
        If _RS.X > -32768 And _RS.Y > -32768 Then
            RS.X = _RS.X
            RS.Y = _RS.Y
        Else
            RS.X = -32768
            RS.Y = -32768
        End If
        holdTime = _holdTime
        waitTime = _waitTime
        repeat = _repeat
        group = _group
    End Sub

    Public Overrides Function getDescription() As String
        Dim sb As New System.Text.StringBuilder("[" & controllerNumber & "] Press ")
        If buttonMask And clsController.XBButtons.btnGuide Then sb.Append("Guide,")
        If buttonMask And clsController.XBButtons.btnStart Then sb.Append("Start,")
        If buttonMask And clsController.XBButtons.btnBack Then sb.Append("Back,")
        If buttonMask And clsController.XBButtons.btnUp Then sb.Append("Up,")
        If buttonMask And clsController.XBButtons.btnDown Then sb.Append("Down,")
        If buttonMask And clsController.XBButtons.btnLeft Then sb.Append("Left,")
        If buttonMask And clsController.XBButtons.btnRight Then sb.Append("Right,")
        If LTDefined Then sb.Append("LT(" & LT & "),")
        If buttonMask And clsController.XBButtons.btnLB Then sb.Append("LB,")
        If buttonMask And clsController.XBButtons.btnL3 Then sb.Append("LS,")
        If LS.X > -32768 And LS.Y > -32768 Then sb.Append("LJ(" & LS.X / 256 & "," & LS.Y / 256 & "),")
        If RTDefined Then sb.Append("RT(" & RT & "),")
        If buttonMask And clsController.XBButtons.btnRB Then sb.Append("RB,")
        If buttonMask And clsController.XBButtons.btnR3 Then sb.Append("RS,")
        If RS.X > -32768 And RS.Y > -32768 Then sb.Append("RJ(" & RS.X / 256 & "," & RS.Y / 256 & "),")
        If buttonMask And clsController.XBButtons.btnA Then sb.Append("A,")
        If buttonMask And clsController.XBButtons.btnB Then sb.Append("B,")
        If buttonMask And clsController.XBButtons.btnX Then sb.Append("X,")
        If buttonMask And clsController.XBButtons.btnY Then sb.Append("Y,")
        Dim tmp As String = sb.ToString
        tmp = tmp.Substring(0, tmp.Length - 1) & " for " & formatMS(holdTime) & ", wait " & formatMS(waitTime) & IIf(repeat > 1, ", " & repeat & " times", vbNullString)
        Return tmp
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actPress
    End Function

    Public Overrides Function serialize() As String
        Return "Press," & controllerNumber & "," & buttonMask & "," & IIf(LTDefined, LT, -1) & "," & LS.X & "," & LS.Y & "," & IIf(RTDefined, RT, -1) & "," & RS.X & "," & RS.Y & "," & holdTime & "," & waitTime & "," & repeat
    End Function

    Public Overrides Function toXML(doc As XmlDocument) As XmlElement
        Dim tmp As XmlElement = doc.CreateElement("Press")
        tmp.SetAttribute("Controller", controllerNumber)
        If buttonMask > 0 Then tmp.SetAttribute("ButtonMask", buttonMask)
        If LTDefined Then tmp.SetAttribute("LT", LT)
        If RTDefined Then tmp.SetAttribute("RT", RT)
        If LS.X > -32768 And LS.Y > -32768 Then
            tmp.SetAttribute("LSX", LS.X)
            tmp.SetAttribute("LSY", LS.Y)
        End If
        If RS.X > -32768 And RS.Y > -32768 Then
            tmp.SetAttribute("RSX", RS.X)
            tmp.SetAttribute("RSY", RS.Y)
        End If
        tmp.SetAttribute("Hold", holdTime)
        tmp.SetAttribute("Wait", waitTime)
        tmp.SetAttribute("Repeat", repeat)
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function

    Public Sub New(serial As String, _group As clsActionGroup)
        Dim toks() As String = serial.Split(",")
        Select Case toks(1)
            Case "10.100.8.51"
                controllerNumber = 1
            Case "10.100.8.52"
                controllerNumber = 2
            Case Else
                Stop
        End Select
        buttonMask = toks(2)
        LT = toks(3)
        LS.X = toks(4)
        LS.Y = toks(5)
        RT = toks(6)
        RS.X = toks(7)
        RS.Y = toks(8)
        holdTime = toks(9)
        waitTime = toks(10)
        repeat = toks(11)
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        group = _group
        controllerNumber = node.Attributes("Controller").Value
        Dim att As XmlAttribute = node.Attributes("ButtonMask")
        If att Is Nothing Then buttonMask = 0 Else buttonMask = att.Value
        att = node.Attributes("LT")
        If att Is Nothing Then LT = -1 Else LT = att.Value
        att = node.Attributes("RT")
        If att Is Nothing Then RT = -1 Else RT = att.Value
        Select Case version
            Case 1
                att = node.Attributes("LSX")
                If att Is Nothing Then LS.X = -32768 Else LS.X = att.Value * 256
                att = node.Attributes("LSY")
                If att Is Nothing Then LS.Y = -32768 Else LS.Y = att.Value * 256
                att = node.Attributes("RSX")
                If att Is Nothing Then RS.X = -32768 Else RS.X = att.Value * 256
                att = node.Attributes("RSY")
                If att Is Nothing Then RS.Y = -32768 Else RS.Y = att.Value * 256
            Case 2
                att = node.Attributes("LSX")
                If att Is Nothing Then LS.X = -32768 Else LS.X = att.Value
                att = node.Attributes("LSY")
                If att Is Nothing Then LS.Y = -32768 Else LS.Y = att.Value
                att = node.Attributes("RSX")
                If att Is Nothing Then RS.X = -32768 Else RS.X = att.Value
                att = node.Attributes("RSY")
                If att Is Nothing Then RS.Y = -32768 Else RS.Y = att.Value
        End Select
        holdTime = node.Attributes("Hold").Value
        waitTime = node.Attributes("Wait").Value
        repeat = node.Attributes("Repeat").Value
    End Sub

    Public Overrides Function clone() As clsAction
        Dim tmp As New clsActionPress(controllerNumber, buttonMask, LT, RT, LS, RS, holdTime, waitTime, repeat, group)
        tmp.baseClone(Me)
        Return tmp
    End Function
End Class

Public Class clsActionInputVideo
    Inherits clsActionInput

    Public pixel As Point
    Public minColor As Color
    Public maxColor As Color
    Public capture As clsSnapshot

    Public Sub New(_interval As Integer, _duration As Integer, _pixel As Point, _minColor As Color, _maxColor As Color, _group As clsActionGroup)
        interval = _interval
        duration = _duration
        pixel = _pixel
        minColor = _minColor
        maxColor = _maxColor
        group = _group
    End Sub

    Public Sub New(node As XmlNode, version As Integer, _group As clsActionGroup)
        group = _group
        interval = node.Attributes("Interval").Value
        duration = node.Attributes("Duration").Value
        pixel = New Point(node.Attributes("X").Value, node.Attributes("Y").Value)
        minColor = Color.FromArgb(node.Attributes("minColor").Value)
        maxColor = Color.FromArgb(node.Attributes("maxColor").Value)
    End Sub

    Public Overrides Function Clone() As clsAction
        Dim tmp As New clsActionInputVideo(interval, duration, pixel, minColor, maxColor, group)
        tmp.baseClone(Me)
        Return tmp
    End Function

    Public Overrides Function getActType() As ActionType
        Return ActionType.actInputVideo
    End Function

    Public Overrides Function getDescription() As String
        Return String.Format("Check [{0},{1}] for colors between {2} and {3} every {4} stop after {5}", pixel.X, pixel.Y, System.Drawing.ColorTranslator.ToHtml(minColor), System.Drawing.ColorTranslator.ToHtml(maxColor), formatMS(interval), formatMS(duration))
    End Function

    Protected Overrides Function inputTest() As Boolean
        Dim bmp As Bitmap = capture.capture
        Dim c As Color = bmp.GetPixel(pixel.X, pixel.Y)
        bmp.Dispose()
        If c.R >= minColor.R AndAlso c.R <= maxColor.R AndAlso c.G >= minColor.G AndAlso c.G <= maxColor.G AndAlso c.B >= minColor.B AndAlso c.B <= maxColor.B Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function toXML(doc As System.Xml.XmlDocument) As System.Xml.XmlElement
        Dim tmp As XmlElement = doc.CreateElement("VideoInput")
        tmp.SetAttribute("Interval", interval)
        tmp.SetAttribute("Duration", duration)
        tmp.SetAttribute("X", pixel.X)
        tmp.SetAttribute("Y", pixel.Y)
        tmp.SetAttribute("minColor", minColor.ToArgb)
        tmp.SetAttribute("maxColor", maxColor.ToArgb)
        If comment <> vbNullString Then tmp.SetAttribute("Comment", comment)
        Return tmp
    End Function
End Class

Public Class clsSimpleAction
    Implements IComparable(Of clsSimpleAction)

    Public Enum saButtons
        btnUp = &H100
        btnDown = &H200
        btnLeft = &H400
        btnRight = &H800
        btnStart = &H1000
        btnBack = &H2000
        btnL3 = &H4000
        btnR3 = &H8000
        btnLB = &H1
        btnRB = &H2
        btnGuide = &H4
        btnA = &H10
        btnB = &H20
        btnX = &H40
        btnY = &H80
        btnLT = &H10000
        btnRT = &H20000
        btnLSX = &H40000
        btnLSY = &H80000
        btnRSX = &H100000
        btnRSY = &H200000
    End Enum

    Public controllerNumber As Byte
    Public timeoffset As Integer
    Public value As Integer
    Public button As saButtons
    Public parent As clsAction
    Public wait As Boolean
    Public input As Boolean

    Public Sub New(_timeoffset As Integer, _parent As clsAction)
        timeoffset = _timeoffset
        parent = _parent
        wait = True
        input = _parent.isInput
    End Sub

    Public Sub New(_controllerNumber As Byte, _timeoffset As Integer, _button As saButtons, _value As Integer, _parent As clsAction)
        controllerNumber = _controllerNumber
        timeoffset = _timeoffset
        button = _button
        value = _value
        parent = _parent
        wait = False
        If Not _parent Is Nothing Then input = _parent.isInput
    End Sub

    Function compareTo(other As clsSimpleAction) As Integer Implements IComparable(Of clsSimpleAction).CompareTo
        If timeoffset < other.timeoffset Then Return -1
        If timeoffset > other.timeoffset Then Return 1
        If controllerNumber.CompareTo(other.controllerNumber) <> 0 Then Return controllerNumber.CompareTo(other.controllerNumber)
        If parent Is Nothing Then Return 0
        If parent.index < other.parent.index Then Return -1
        If parent.index > other.parent.index Then Return 1
        Return 0
    End Function

End Class

Public Class clsStatelessAction
    Public timeoffset As Integer
    Public report() As Byte
    Public parent As clsAction
    Public controller As clsController
    Public wait As Boolean
    Public input As Boolean

    Public Sub New(_timeoffset As Integer, _parent As clsAction)
        timeoffset = _timeoffset
        parent = _parent
        wait = True
        input = _parent.isInput
    End Sub

    Public Sub New(_controller As clsController, _report() As Byte, _timeoffset As Integer, _parent As clsAction)
        controller = _controller
        ReDim report(_report.Length - 1)
        Array.Copy(_report, report, report.Length)
        timeoffset = _timeoffset
        parent = _parent
        wait = False
        input = _parent.isInput
    End Sub

    Public Overrides Function ToString() As String
        If TypeOf controller Is clsBBBController Or TypeOf controller Is clsPS3Controller Then
            Return timeoffset & "," & CType(controller, clsBBBController).IP & "," & System.BitConverter.ToString(report).Replace("-", "")
        Else
            Return MyBase.ToString
        End If
    End Function

End Class