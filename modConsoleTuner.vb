Imports HtmlAgilityPack
Imports System.IO
Imports System.Reflection.Emit
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Module modConsoleTuner
    Private Function getCombos(gpc As String) As Dictionary(Of String, String)
        Dim braceLoc As New List(Of Integer)
        Dim braceLevel As New List(Of Integer)
        Dim curLevel As Integer = 0
        braceLoc.Add(0)
        braceLevel.Add(curLevel)

        Dim posStart As Integer = gpc.IndexOfAny(New Char() {"{"c, "}"c})
        While posStart > 0
            Select Case gpc.Chars(posStart)
                Case "{"
                    curLevel += 1
                Case "}"
                    curLevel -= 1
            End Select
            braceLoc.Add(posStart)
            braceLevel.Add(curLevel)
            posStart = gpc.IndexOfAny(New Char() {"{"c, "}"c}, posStart + 1)
        End While

        If braceLevel(braceLevel.Count - 1) <> 0 Then
            MsgBox("Brace Mismatch")
            Return Nothing
        End If

        Dim i As Integer = 0
        Dim rootStart As New List(Of Integer)
        Dim rootEnd As New List(Of Integer)
        Dim curStart As Integer = -1
        While i < braceLoc.Count - 1
            If curStart < 0 Then
                If braceLevel(i) = 0 And braceLevel(i + 1) = 1 Then curStart = i + 1
            Else
                If braceLevel(i) = 1 And braceLevel(i + 1) = 0 Then
                    rootStart.Add(braceLoc(curStart) + 1)
                    rootEnd.Add(braceLoc(i + 1))
                    curStart = -1
                End If
            End If
            i += 1
        End While
        If curStart <> -1 Then
            MsgBox("Last Section Didn't end.")
            Return Nothing
        End If

        Dim comboDict As New Dictionary(Of String, String)
        Dim reCombo As New Regex("^\s*combo\s(?<comboname>\S+)\s*$")
        For i = 0 To rootStart.Count - 1
            Dim lineStart = gpc.LastIndexOfAny(New Char() {vbCr, vbLf}, rootStart(i) - 1)
            lineStart += 1
            Dim prefix As String = gpc.Substring(lineStart, rootStart(i) - lineStart - 1)
            Dim m As Match = reCombo.Match(prefix)
            If m.Success Then
                Dim contents As String = gpc.Substring(rootStart(i), rootEnd(i) - rootStart(i))
                comboDict.Add(m.Groups("comboname").Value, contents)
            End If
        Next
        Return comboDict
    End Function

    'https://www.consoletuner.com/gpclib/?s=850
    Public Function convertCTScript(url As String) As clsScriptFile
        Dim htmlText As String = fetchPageText(url)
        Dim htmlDoc As New HtmlDocument
        htmlDoc.LoadHtml(htmlText)
        Dim baseNode As HtmlNode = htmlDoc.DocumentNode

        Dim sf As New clsScriptFile()
        Dim titleNode As HtmlNode = baseNode.SelectSingleNode("/html/head/title")
        If titleNode Is Nothing Then
            MsgBox("Can't find title.")
            Return Nothing
        End If
        sf.game = "Unknown"
        sf.title = titleNode.InnerText
        Dim noteNode As HtmlNode = baseNode.SelectSingleNode("//table[@class='gpclib_tw100']/tr/td/div")
        If noteNode Is Nothing Then
            MsgBox("Can't find notes.")
            Return Nothing
        End If
        sf.description = noteNode.InnerText & vbCrLf & New String("-", 20) & vbCrLf & "Imported from: " & vbCrLf & url
        sf.version = 2

        Dim gpcNode As HtmlNode = baseNode.SelectSingleNode("//pre[@class='gpc']")
        If gpcNode Is Nothing Then
            MsgBox("Can't find gpc code.")
            Return Nothing
        End If

        Dim idNode As HtmlNode = baseNode.SelectSingleNode("//input[@name='s']")
        If idNode Is Nothing Then
            MsgBox("Can't find ID")
            Return Nothing
        End If
        Dim id As Integer = idNode.Attributes("value").Value
        Dim gpcCode As String = fetchPageText("https://www.consoletuner.com/gpclib/api/download.php?s=" & id)

        Dim combos As Dictionary(Of String, String) = getCombos(gpcCode)
        If combos Is Nothing Then Return Nothing
        If combos.Count = 0 Then
            MsgBox("No combos found.")
            Return Nothing
        End If

        For Each kvp As KeyValuePair(Of String, String) In combos
            Dim ag As clsActionGroup = convertCTCombo(url, kvp.Key, kvp.Value)
            If combos.Count = 1 Then ag.name = mainGroup Else ag.name = kvp.Key
            sf.groups.Add(ag.name, ag)
        Next
        Return sf
    End Function

    Function convertCTCombo(url As String, comboName As String, gpcCode As String) As clsActionGroup
        Dim reGPC As New Regex("^\s*(|" &
                               "((?<cmd>wait)\((?<delay>\d+)\))|" &
                               "((?<cmd>set_val)\((?<button>XB360_XBOX|XB360_BACK|XB360_START|XB360_RB|XB360_RT|XB360_RS|XB360_LB|XB360_LT|XB360_LS|XB360_RX|XB360_RY|XB360_LX|XB360_LY|XB360_UP|XB360_DOWN|XB360_LEFT|XB360_RIGHT|XB360_Y|XB360_B|XB360_A|XB360_X)\s*,\s*(?<value>\d+)\)))\s*$", System.Text.RegularExpressions.RegexOptions.Compiled)
        Dim sbError As New Text.StringBuilder
        Dim lines() As String = gpcCode.Split(New String() {vbLf, vbCr, ";"}, StringSplitOptions.RemoveEmptyEntries)
        Dim buttonMask As Integer = 0
        Dim LS As clsController.joystickPosition
        Dim RS As clsController.joystickPosition
        Dim LT As Integer = -1
        Dim RT As Integer = -1
        LS.x = -32768
        LS.y = -32768
        RS.x = -32768
        RS.y = -32768

        Dim ag As New clsActionGroup(vbNullString)
        For i As Integer = 0 To lines.Length - 1
            Dim m As Match = reGPC.Match(lines(i))
            If Not m.Success Then
                sbError.Append(i + 1 & ": " & lines(i))
            Else
                Select Case m.Groups("cmd").Value
                    Case "wait"
                        Dim delay As Integer = m.Groups("delay").Value
                        ag.actions.Add(New clsActionPress(1, buttonMask, LT, RT, New Point(LS.x, LS.y), New Point(RS.x, RS.y), delay, delay, 1, ag))
                        'add the action
                        buttonMask = 0
                        LT = -1
                        RT = -1
                        LS.x = -32768
                        LS.y = -32768
                        RS.x = -32768
                        RS.y = -32768
                    Case "set_val"
                        Dim value As Integer = m.Groups("value").Value
                        Dim curButton As Integer
                        Select Case m.Groups("button").Value
                            Case "XB360_XBOX"
                                curButton = clsController.XBButtons.btnGuide
                            Case "XB360_BACK"
                                curButton = clsController.XBButtons.btnBack
                            Case "XB360_START"
                                curButton = clsController.XBButtons.btnStart
                            Case "XB360_RB"
                                curButton = clsController.XBButtons.btnRB
                            Case "XB360_RS"
                                curButton = clsController.XBButtons.btnR3
                            Case "XB360_LB"
                                curButton = clsController.XBButtons.btnLB
                            Case "XB360_LS"
                                curButton = clsController.XBButtons.btnL3
                            Case "XB360_UP"
                                curButton = clsController.XBButtons.btnUp
                            Case "XB360_DOWN"
                                curButton = clsController.XBButtons.btnDown
                            Case "XB360_LEFT"
                                curButton = clsController.XBButtons.btnLeft
                            Case "XB360_RIGHT"
                                curButton = clsController.XBButtons.btnRight
                            Case "XB360_Y"
                                curButton = clsController.XBButtons.btnY
                            Case "XB360_B"
                                curButton = clsController.XBButtons.btnB
                            Case "XB360_A"
                                curButton = clsController.XBButtons.btnA
                            Case "XB360_X"
                                curButton = clsController.XBButtons.btnX
                        End Select
                        Select Case m.Groups("button").Value
                            Case "XB360_RT"
                                RT = value * 256 / 100
                            Case "XB360_LT"
                                LT = value * 256 / 100
                            Case "XB360_RX"
                                RS.x = value * 32767 / 100
                            Case "XB360_RY"
                                RS.y = value * 32767 / 100
                            Case "XB360_LX"
                                LS.x = value * 32767 / 100
                            Case "XB360_LY"
                                LS.y = value * 32767 / 100
                            Case Else
                                If value > 0 Then buttonMask = buttonMask Or curButton Else buttonMask = buttonMask And Not curButton
                        End Select
                End Select
            End If
        Next
        If sbError.Length > 0 Then MsgBox("URL: " & url & vbCrLf & "Combo: " & comboName & vbCrLf & "The following lines were not understood and were skipped:" & vbCrLf & sbError.ToString())
        Return ag
    End Function

    'Public Sub loadCTScript()
    '    clearScript()
    '    loadScriptXML(Path)
    '    For Each g As clsActionGroup In groups.Values
    '        lbGroups.Items.Add(g)
    '    Next
    '    If groups.ContainsKey(mainGroup) Then activeGroup = groups(mainGroup) Else activeGroup = lbGroups.Items(0)
    '    lbGroups.SelectedItem = activeGroup
    '    linkActions()
    '    refreshGroup()
    'End Sub

End Module
