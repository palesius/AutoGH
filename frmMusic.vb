'editor with lines
'press <buttons>
'hold <buttons> of <n> ms
'release <buttons>
'goto <lineX>[,<repeatY>]
'ability to pause,stop,start from a certain line

Imports System.IO

Public Class frmMusic

    Private basepath As String

    Private dictGames As SortedDictionary(Of String, clsRhythmGame)
    Private lstSongs As New List(Of clsSong)
    Private lstTracks As New List(Of clsTrack)
    Private arrLevel(3) As clsLevel

    Private lastGame As clsRhythmGame
    Private lastSong As clsSong

    Private cbTrack(3) As ComboBox
    Private cbLevel(3) As ComboBox
    Private chkLF(3) As CheckBox
    Private chkHOPO(3) As CheckBox

    Public actions As List(Of clsAction)
    Public info As String

    Private Sub saveSettings()
        Dim val As String
        If lastGame Is Nothing Then val = "" Else val = lastGame.name
        SaveSetting(Application.ProductName, "Settings", "Game", val)
        If lastSong Is Nothing Then val = "" Else val = lastSong.name
        SaveSetting(Application.ProductName, "Settings", "Song", val)
        SaveSetting(Application.ProductName, "Settings", "Manual", IIf(cbManual.Checked, "True", "False"))
        For i As Integer = 0 To 3
            If cbTrack(i).SelectedItem Is Nothing Then val = "" Else val = cbTrack(i).SelectedItem.ToString
            SaveSetting(Application.ProductName, "Settings" & i, "Track", val)
            Select Case val
                Case "GUITAR", "BASS"
                    SaveSetting(Application.ProductName, "Settings" & i, "LF", IIf(chkLF(i).Checked, "T", ""))
                    SaveSetting(Application.ProductName, "Settings" & i, "HOPO", IIf(chkHOPO(i).Checked, "T", ""))
            End Select
            If cbLevel(i).SelectedItem Is Nothing Then val = "" Else val = cbLevel(i).SelectedItem.ToString
            SaveSetting(Application.ProductName, "Settings" & i, "Level", val)
        Next i
    End Sub

    Private Sub selectByName(cb As ComboBox, name As String)
        For i = 0 To cb.Items.Count - 1
            If cb.Items(i).ToString = name Then
                cb.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub frmMusic_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbSong.Focus()
    End Sub

    Private Sub frmMusic_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not vocalOut Is Nothing Then
            If vocalOut.PlaybackState = NAudio.Wave.PlaybackState.Playing Then vocalOut.Stop()
            vocalOut.Dispose()
            vocalOut = Nothing
            vocalProvider = Nothing
            For Each vi As NAudio.Wave.WaveFileReader In vocalInputs
                vi.Close()
                vi.Dispose()
            Next
            For Each vf As IO.FileInfo In vocalFiles
                vf.Delete()
            Next
            vocalInputs = Nothing
        End If
    End Sub

    Private Sub loadGames()
        dictGames = New SortedDictionary(Of String, clsRhythmGame)
        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(Path.Combine(AppContext.BaseDirectory, "RhythmGames.xml"))
        Dim bn As Xml.XmlNode = xmlDoc.SelectSingleNode("/Games/Game[@Name='Default']")
        If bn Is Nothing Then
            MsgBox("Error loading game configurations.")
            Exit Sub
        End If
        Dim base As New clsRhythmGame(bn, Nothing)
        For Each n As Xml.XmlNode In xmlDoc.SelectNodes("/Games/Game")
            If n.Attributes("Name").Value <> "Default" Then
                Dim game As New clsRhythmGame(n, base)
                dictGames.Add(game.code, game)
            End If
        Next
    End Sub

    Private Sub frmMusic_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        basepath = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\GH\"
        cbTrack(0) = cbTrack0
        cbTrack(1) = cbTrack1
        cbTrack(2) = cbTrack2
        cbTrack(3) = cbTrack3
        cbLevel(0) = cbLevel0
        cbLevel(1) = cbLevel1
        cbLevel(2) = cbLevel2
        cbLevel(3) = cbLevel3
        chkLF(0) = chkLF0
        chkLF(1) = chkLF1
        chkLF(2) = chkLF2
        chkLF(3) = chkLF3
        chkHOPO(0) = chkHOPO0
        chkHOPO(1) = chkHOPO1
        chkHOPO(2) = chkHOPO2
        chkHOPO(3) = chkHOPO3

        loadGames()

        For i As Integer = 0 To 3
            arrLevel(i) = New clsLevel(i)
        Next i

        Dim saved As String

        cbManual.Checked = GetSetting(Application.ProductName, "Settings", "Manual", "False") = "True"

        cbGame.Items.Clear()
        For Each game As clsRhythmGame In dictGames.Values
            cbGame.Items.Add(game)
        Next
        saved = GetSetting(Application.ProductName, "Settings", "Game", "")
        If saved <> "" Then
            selectByName(cbGame, saved)
            cbGame_SelectedIndexChanged(cbGame, Nothing)
        End If
        AddHandler cbGame.SelectedIndexChanged, AddressOf cbGame_SelectedIndexChanged

        saved = GetSetting(Application.ProductName, "Settings", "Song", "")
        If saved <> "" Then
            selectByName(cbSong, saved)
            cbSong_SelectedIndexChanged(cbSong, Nothing)
        End If
        AddHandler cbSong.SelectedIndexChanged, AddressOf cbSong_SelectedIndexChanged

        For i As Integer = 0 To 3
            saved = GetSetting(Application.ProductName, "Settings" & i, "Track", "")
            If saved <> "" Then
                selectByName(cbTrack(i), saved)
                cbTrack_SelectedIndexChanged(cbTrack(i), Nothing)
            End If
            AddHandler cbTrack(i).SelectedIndexChanged, AddressOf cbTrack_SelectedIndexChanged

            Select Case saved
                Case "GUITAR", "BASS"
                    chkLF(i).Checked = GetSetting(Application.ProductName, "Settings" & i, "LF") = "T"
                    chkHOPO(i).Checked = GetSetting(Application.ProductName, "Settings" & i, "HOPO") = "T"
                    chkLF(i).Enabled = True
                    chkHOPO(i).Enabled = True
                Case Else
                    chkLF(i).Checked = False
                    chkHOPO(i).Checked = False
                    chkLF(i).Enabled = False
                    chkHOPO(i).Enabled = False
            End Select

            For Each level As clsLevel In arrLevel
                cbLevel(i).Items.Add(level)
            Next
            saved = GetSetting(Application.ProductName, "Settings" & i, "Level", "")
            If saved <> "" Then selectByName(cbLevel(i), saved)
        Next

    End Sub

    Private Sub cbGame_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If CType(sender, ComboBox).SelectedItem Is lastGame Then Exit Sub
        lastGame = CType(sender, ComboBox).SelectedItem
        populateSongs(cbSong, lastGame)
    End Sub

    Private Sub populateSongs(cb As ComboBox, game As clsRhythmGame)
        cb.Items.Clear()
        lstSongs.Clear()
        Dim di As New IO.DirectoryInfo(basepath & game.code)
        If Not di.Exists Then di.Create()

        For Each fi As IO.FileInfo In New IO.DirectoryInfo(basepath & game.code).GetFiles("*.mid")
            Dim song As New clsSong(fi, game)
            lstSongs.Add(song)
            cb.Items.Add(song)
        Next
        cb.SelectedIndex = -1
    End Sub

    Private Sub cbSong_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If CType(sender, ComboBox).SelectedItem Is lastSong Then Exit Sub
        lastSong = CType(sender, ComboBox).SelectedItem
        For i As Integer = 0 To 3
            populateTracks(cbTrack(i), lastGame, lastSong)
        Next i
    End Sub

    Private Sub cbTrack_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim si As Object = CType(sender, ComboBox).SelectedItem
        Dim i As Integer
        For i = 0 To 3
            If cbTrack(i) Is sender Then Exit For
        Next
        If TypeOf si Is clsTrack Then
            Dim t As clsTrack = si

            Select Case t.name
                Case "GUITAR", "BASS"
                    chkLF(i).Enabled = True
                    chkHOPO(i).Enabled = True
                Case Else
                    chkLF(i).Enabled = False
                    chkHOPO(i).Enabled = False
            End Select
        Else
            chkLF(i).Enabled = False
            chkHOPO(i).Enabled = False
        End If
    End Sub

    Private Sub populateTracks(cb As ComboBox, game As clsRhythmGame, song As clsSong)
        Dim oldPart As String = vbNullString
        If Not cb.SelectedItem Is Nothing AndAlso Not cb.SelectedItem.ToString = "" Then
            oldPart = CType(cb.SelectedItem, clsTrack).name
        End If
        cb.Items.Clear()
        lstTracks.Clear()
        cb.Items.Add("")
        With song.mf
            For i As Integer = 0 To .Tracks - 1
                For j = 0 To .Events(i).Count - 1
                    If .Events(i)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                        If CType(.Events(i)(j), NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName Then
                            Dim name As String = CType(.Events(i)(j), NAudio.Midi.TextEvent).Text
                            Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(name, "^(PART (?<part>.*))|(?<part>HARM1)$")
                            If match.Success Then
                                Dim track As New clsTrack(song.mf, i, match.Groups("part").Value, song)
                                lstTracks.Add(track)
                                cb.Items.Add(track)
                            ElseIf i = 0 Then
                                ' The track title for track 0 is the title of the song
                                song.title = name
                            End If
                            Exit For
                        End If
                    End If
                Next j
            Next i
        End With
        cb.SelectedIndex = -1
        If oldPart <> vbNullString Then
            For i = 0 To cb.Items.Count - 1
                If cb.Items(i).ToString <> "" AndAlso CType(cb.Items(i), clsTrack).name = oldPart Then
                    cb.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        actions = Nothing
        Me.Close()
    End Sub

    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click
        If cbSong.SelectedItem Is Nothing OrElse cbSong.SelectedItem.ToString() = "" Then
            MsgBox("You must select a song.")
            Exit Sub
        End If
        saveSettings()

        Dim trackCount As Integer = 0
        Dim allNotes As New List(Of clsNoteEntry)
        Dim vocalPath() As String = New String() {vbNullString, vbNullString, vbNullString, vbNullString}
        Dim vocalPart() As String = New String() {vbNullString, vbNullString, vbNullString, vbNullString}
        Dim vocalStart() As Integer = New Integer() {0, 0, 0, 0}
        Dim vocalDuration() As Integer = New Integer() {0, 0, 0, 0}
        Dim vocalsStarted() As Boolean = New Boolean() {True, True, True, True}
        For i = 0 To 3
            If (Not cbTrack(i).SelectedItem Is Nothing) AndAlso (Not cbTrack(i).SelectedItem.ToString() = "") Then
                Dim trackName As String = cbTrack(i).SelectedItem.ToString()
                If chkHOPO(i).Checked Then CType(cbTrack(i).SelectedItem, clsTrack).hopo = True
                If chkLF(i).Checked Then CType(cbTrack(i).SelectedItem, clsTrack).lefty = True
                Select Case trackName
                    Case "VOCALS"
                        Dim midiPath As String = CType(cbSong.SelectedItem, clsSong).fi.FullName
                        vocalPath(i) = midiPath.Substring(0, InStrRev(midiPath, ".") - 1) & ".mp3"
                        If Not IO.File.Exists(vocalPath(i)) Then generateMP3(midiPath)
                        If Not IO.File.Exists(vocalPath(i)) Then
                            MsgBox("Couldn't generate vocal file.")
                            Exit Sub
                        End If
                        Dim tf As TagLib.File = TagLib.File.Create(vocalPath(i))
                        vocalPart(i) = trackName
                        vocalDuration(i) = tf.Properties.Duration.TotalMilliseconds
                        vocalStart(i) = tf.Tag.Comment + CType(cbGame.SelectedItem, clsRhythmGame).loadTime
                        vocalsStarted(i) = False
                        trackCount += 1
                    Case "HARM1", "HARM2", "HARM3"
                        Dim midiPath As String = CType(cbSong.SelectedItem, clsSong).fi.FullName
                        Dim harmonyPaths(2) As String
                        harmonyPaths(0) = midiPath.Substring(0, InStrRev(midiPath, ".") - 1) & "-HARM1.mp3"
                        harmonyPaths(1) = midiPath.Substring(0, InStrRev(midiPath, ".") - 1) & "-HARM2.mp3"
                        harmonyPaths(2) = midiPath.Substring(0, InStrRev(midiPath, ".") - 1) & "-HARM3.mp3"

                        vocalPath(i) = harmonyPaths(0)
                        If Not IO.File.Exists(vocalPath(i)) Then generateHarmMP3(midiPath)
                        If Not IO.File.Exists(vocalPath(i)) Then
                            MsgBox("Couldn't generate vocal file.")
                            Exit Sub
                        End If
                        vocalPart(i) = "HARM1"
                        vocalDuration(i) = 0
                        vocalPath(i) = vbNullString
                        For Each harmonyPath As String In harmonyPaths
                            If IO.File.Exists(harmonyPath) Then
                                Dim tf As TagLib.File = TagLib.File.Create(harmonyPath)
                                vocalDuration(i) = Math.Max(vocalDuration(i), tf.Properties.Duration.TotalMilliseconds)
                                vocalStart(i) = tf.Tag.Comment + CType(cbGame.SelectedItem, clsRhythmGame).loadTime
                                If vocalPath(i) <> vbNullString Then vocalPath(i) &= "|"
                                vocalPath(i) &= harmonyPath
                            End If
                        Next

                        vocalsStarted(i) = False
                        trackCount += 1
                    Case Else
                        If (Not cbLevel(i).SelectedItem Is Nothing) Then
                            Dim hopoThreshold As Integer
                            With CType(cbGame.SelectedItem, clsRhythmGame)
                                If .hopoTrigger Is Nothing Then
                                    hopoThreshold = CType(cbTrack(i).SelectedItem, clsTrack)._song.mf.DeltaTicksPerQuarterNote / 3
                                ElseIf Not .hopoTrigger.TryGetValue(CType(cbSong.SelectedItem, clsSong).title, hopoThreshold) Then
                                    hopoThreshold = CType(cbGame.SelectedItem, clsRhythmGame).hopoTrigger("_Default")
                                End If
                            End With
                            Dim trackNotes As List(Of clsNoteEntry) = getNotes(i + 1, cbTrack(i).SelectedItem, cbLevel(i).SelectedItem, hopoThreshold)
                            If trackNotes Is Nothing Then Exit Sub
                                allNotes.AddRange(trackNotes)
                                trackCount += 1
                            End If
                End Select
            End If
        Next
        If trackCount = 0 Then
            MsgBox("You must select at least one track to play.")
            Exit Sub
        End If

        allNotes.Sort()

        Dim mergedNotes As New List(Of clsNoteEntry)
        If allNotes.Count > 0 Then
            Dim lastNote As clsNoteEntry = allNotes(0)
            mergedNotes.Add(lastNote)
            For i = 1 To allNotes.Count - 1
                If allNotes(i).controller = lastNote.controller AndAlso allNotes(i).tickOffset = lastNote.tickOffset Then 'AndAlso allNotes(i).tickDuration = lastNote.tickDuration Then
                    lastNote.merge(allNotes(i))
                Else
                    mergedNotes.Add(allNotes(i))
                    lastNote = allNotes(i)
                End If
            Next
        End If

        'Dim manlines() As String = IO.File.ReadAllLines("g:\th\pony.hopoc")
        'For i = 0 To mergedNotes.Count - 1
        '    Dim line As String = vbNullString
        '    Dim track As clsTrack = cbTrack(0).SelectedItem
        '    With mergedNotes(i)
        '        If .noteMask And track.noteGreen(True) Then line = line & "A"
        '        If .noteMask And track.noteRed(True) Then line = line & "B"
        '        If .noteMask And track.noteYellow(True) Then line = line & "C"
        '        If .noteMask And track.noteBlue(True) Then line = line & "D"
        '        If .noteMask And track.noteOrange(True) Then line = line & "E"
        '        If .hopo Then line = line.ToLower
        '    End With
        '    Dim manline = manlines(i)
        '    If line.ToUpper <> manline.ToUpper Then Stop
        '    If line <> manline Then
        '        If manline.ToUpper = manline Then
        '            mergedNotes(i).noteMask = mergedNotes(i).noteMask Or track.strumButton
        '        Else
        '            mergedNotes(i).noteMask = mergedNotes(i).noteMask And Not track.strumButton
        '        End If
        '    End If
        '    Debug.Print(line)
        'Next i

        Dim noteActions As New List(Of clsNoteAction)
        For Each ne As clsNoteEntry In mergedNotes
            noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + lastGame.loadTime, True, ne.comment))
            noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + ne.msDuration + lastGame.loadTime, False, ne.comment))
        Next

        noteActions.Sort()

        actions = New List(Of clsAction)
        Dim curOffset As Integer
        Dim a As clsAction

        If cbManual.Checked Then
            Dim startMS As Integer = Integer.MaxValue
            Dim startController As String = vbNullString
            Dim endMS As Integer = 0
            If noteActions.Count > 0 Then
                startMS = noteActions(0).msOffset
                endMS = noteActions(noteActions.Count - 1).msOffset
                startController = noteActions(0).controller
            End If
            For c As Integer = 0 To 3
                If Not vocalsStarted(c) Then
                    If vocalStart(c) < startMS Then
                        startMS = vocalStart(c)
                        startController = vocalPart(c)
                    End If
                    If vocalStart(c) + vocalDuration(c) > endMS Then endMS = vocalStart(c) + vocalDuration(c)
                End If
            Next
            Dim endSpan As DateTime = (New DateTime).AddMilliseconds(endMS)
            info = "[" & startController & "] starts at " & ((startMS - CType(cbGame.SelectedItem, clsRhythmGame).loadTime) / 1000) & "s (of " & endSpan.ToString("m:ss") & ")"
        Else
            curOffset = 100
            a = New clsActionPress(1, clsController.XBButtons.btnA, -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), 100, 100, 1, Nothing)
            a.index = actions.Count
            actions.Add(a)
        End If
        Dim curmask As Integer = 0
        Dim vocalsToStart As List(Of Integer)
        For i As Integer = 0 To noteActions.Count - 1
            vocalsToStart = New List(Of Integer)
            For c As Integer = 0 To 3
                If Not vocalsStarted(c) AndAlso vocalStart(c) > curOffset AndAlso vocalStart(c) <= noteActions(i).msOffset Then vocalsToStart.Add(c)
            Next
            While vocalsToStart.Count > 0
                Dim firstStart As Integer = Integer.MaxValue
                Dim firstIdx As Integer = -1
                For Each vc As Integer In vocalsToStart
                    If vocalStart(vc) < firstStart Then
                        firstStart = vocalStart(vc)
                        firstIdx = vc
                    End If
                Next
                If firstStart > curOffset Then
                    If curOffset > 0 Then
                        a = New clsActionWait(firstStart - curOffset, Nothing)
                        a.index = actions.Count
                        actions.Add(a)
                    End If
                    curOffset = firstStart
                End If
                a = New clsActionOutputAudio(New List(Of String)(vocalPath(firstIdx).Split("|")), Nothing)
                a.index = actions.Count
                actions.Add(a)
                vocalsStarted(firstIdx) = True
                vocalsToStart.Remove(firstIdx)
            End While
            With noteActions(i)
                Dim notemask As Integer = .noteMask And &HFFFF
                Dim LT As Integer = IIf(.noteMask And &H10000, 255, -1)
                Dim RT As Integer = IIf(.noteMask And &H20000, 255, -1)
                If .msOffset > curOffset Then
                    If curOffset > 0 Then
                        a = New clsActionWait(.msOffset - curOffset, Nothing)
                        a.index = actions.Count
                        actions.Add(a)
                    End If
                    curOffset = .msOffset
                End If
                'If .press AndAlso curmask = 0 AndAlso i < noteActions.Count - 1 Then
                '    If (Not noteActions(i + 1).press) AndAlso noteActions(i + 1).noteMask = .noteMask Then
                '        Dim holdTime As Integer = noteActions(i + 1).msOffset - .msOffset
                '        Dim waitTime As Integer
                '        If i < noteActions.Count - 2 AndAlso noteActions(i + 2).press Then
                '            waitTime = noteActions(i + 2).msOffset - .msOffset
                '        Else
                '            waitTime = holdTime
                '        End If
                '        a = New clsActionPress(.controller, notemask, LT, RT, New Point(-129, -129), New Point(-129, -129), holdTime, waitTime, 1)
                '        a.index = actions.Count
                '        actions.Add(a)
                '        i = i + 1
                '        curOffset = curOffset + waitTime
                '    End If
                'Else
                If .press Then
                    curmask = curmask Or .noteMask
                    a = New clsActionHold(.controller, notemask, LT, RT, New Point(-32768, -32768), New Point(-32768, -32768), Nothing)
                    a.comment = .comment
                    a.index = actions.Count
                    actions.Add(a)
                Else
                    curmask = curmask And Not .noteMask
                    a = New clsActionRelease(.controller, notemask, LT, RT, New Point(-32768, -32768), New Point(-32768, -32768), Nothing)
                    a.comment = .comment
                    a.index = actions.Count
                    actions.Add(a)
                End If
                'End If
            End With
        Next

        vocalsToStart = New List(Of Integer)
        For c As Integer = 0 To 3
            If Not vocalsStarted(c) AndAlso vocalStart(c) > curOffset Then vocalsToStart.Add(c)
        Next
        While vocalsToStart.Count > 0
            Dim firstStart As Integer = Integer.MaxValue
            Dim firstIdx As Integer = -1
            For Each vc As Integer In vocalsToStart
                If vocalStart(vc) < firstStart Then
                    firstStart = vocalStart(vc)
                    firstIdx = vc
                End If
            Next
            If firstStart > curOffset Then
                If curOffset > 0 Then
                    a = New clsActionWait(firstStart - curOffset, Nothing)
                    a.index = actions.Count
                    actions.Add(a)
                End If
                curOffset = firstStart
            End If
            a = New clsActionOutputAudio(New List(Of String)(vocalPath(firstIdx).Split("|")), Nothing)
            a.index = actions.Count
            actions.Add(a)
            vocalsStarted(firstIdx) = True
            vocalsToStart.Remove(firstIdx)
        End While
        Dim maxVocal As Integer = 0
        For c As Integer = 0 To 3
            maxVocal = Math.Max(maxVocal, vocalStart(c) + vocalDuration(c))
        Next

        If curOffset < maxVocal Then
            a = New clsActionWait(maxVocal - curOffset, Nothing)
            a.index = actions.Count
            actions.Add(a)
        End If

        a = New clsActionWait(15000, Nothing)
        a.index = actions.Count
        actions.Add(a)

        a = New clsActionPress(1, clsController.XBButtons.btnGuide, -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), 500, 500, 1, Nothing)
        a.index = actions.Count
        actions.Add(a)

        Me.Close()
    End Sub

    Private vocalFiles As List(Of IO.FileInfo)
    Private vocalInputs As List(Of NAudio.Wave.WaveFileReader)
    Private vocalProvider As NAudio.Wave.MultiplexingWaveProvider
    Private vocalOut As NAudio.Wave.WasapiOut

    'Private Sub btnVocal_Click(sender As System.Object, e As System.EventArgs)
    '    If Not vocalOut Is Nothing Then
    '        Dim origState As NAudio.Wave.PlaybackState = vocalOut.PlaybackState
    '        If vocalOut.PlaybackState = NAudio.Wave.PlaybackState.Playing Then vocalOut.Stop()
    '        vocalOut.Dispose()
    '        vocalOut = Nothing
    '        vocalProvider = Nothing
    '        For Each vi As NAudio.Wave.WaveFileReader In vocalInputs
    '            vi.Close()
    '            vi.Dispose()
    '        Next
    '        For Each vf As IO.FileInfo In vocalFiles
    '            vf.Delete()
    '        Next
    '        vocalInputs = Nothing
    '        If origState = NAudio.Wave.PlaybackState.Playing Then Exit Sub
    '    End If
    '    Dim song As clsSong = cbSong.SelectedItem
    '    vocalFiles = modVocal.generateWAV(song.fi.FullName)
    '    vocalInputs = New List(Of NAudio.Wave.WaveFileReader)
    '    For Each vf As IO.FileInfo In vocalFiles
    '        vocalInputs.Add(New NAudio.Wave.WaveFileReader(vf.FullName))
    '    Next
    '    vocalProvider = New NAudio.Wave.MultiplexingWaveProvider(vocalInputs, vocalInputs.Count)
    '    For i As Integer = 0 To vocalInputs.Count - 1
    '        vocalProvider.ConnectInputToOutput(i, i)
    '    Next
    '    Dim devs As New NAudio.CoreAudioApi.MMDeviceEnumerator()
    '    Dim outdev As NAudio.CoreAudioApi.MMDevice = Nothing
    '    outdev = devs.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia)
    '    vocalOut = New NAudio.Wave.WasapiOut(outdev, NAudio.CoreAudioApi.AudioClientShareMode.Shared, True, 0)
    '    vocalOut.Init(vocalProvider)
    '    MsgBox("Vocals ready to go." & vbCrLf & "Hit enter to start playing!")
    '    vocalOut.Play()
    'End Sub

    'Private Sub btnStar_Click(sender As System.Object, e As System.EventArgs)
    '    Dim spInput(2) As NAudio.Wave.WaveFileReader
    '    For i As Integer = 0 To 2
    '        spInput(i) = New NAudio.Wave.WaveFileReader("g:\gh\noise.wav")
    '    Next
    '    Dim spProvider As New NAudio.Wave.MultiplexingWaveProvider(spInput, spInput.Length)
    '    For i As Integer = 0 To spInput.Length - 1
    '        spProvider.ConnectInputToOutput(i, i)
    '    Next
    '    Dim spOut As New NAudio.Wave.WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 0)
    '    spOut.Init(spProvider)
    '    spOut.Play()
    '    While spOut.PlaybackState = NAudio.Wave.PlaybackState.Playing
    '        System.Threading.Thread.Sleep(50)
    '    End While
    '    spOut.Dispose()
    '    spOut = Nothing
    '    spProvider = Nothing
    '    For i As Integer = 0 To 2
    '        spInput(i).Close()
    '        spInput(i).Dispose()
    '    Next
    'End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnMakeVocals.Click
        Dim rg As clsRhythmGame = CType(cbGame.SelectedItem, clsRhythmGame)
        For Each fi As IO.FileInfo In New IO.DirectoryInfo(basepath & rg.code).GetFiles("*.mid")
            If Not IO.File.Exists(fi.FullName.Substring(0, fi.FullName.Length - 4) & ".mp3") Then modVocal.generateMP3(fi.FullName)
        Next
    End Sub
End Class
