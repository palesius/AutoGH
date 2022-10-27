Imports System.Linq
Imports NAudio
'https://github.com/TheBoxyBear/ChartTools/blob/stable/docs/FileFormats/midi.md
Friend Class clsNoteAction
    Implements IComparable(Of clsNoteAction)
    Public controller As Byte
    Public msOffset As Integer
    Public noteMask As Integer
    Public press As Boolean
    Public comment As String

    Public Sub New(_controller As Byte, _note As Integer, _msOffset As Integer, _press As Boolean, _comment As String)
        controller = _controller
        noteMask = _note
        msOffset = _msOffset
        press = _press
        comment = _comment
    End Sub

    Public Function CompareTo(other As clsNoteAction) As Integer Implements System.IComparable(Of clsNoteAction).CompareTo
        If msOffset < other.msOffset Then Return -1
        If msOffset > other.msOffset Then Return 1
        If controller.CompareTo(other.controller) <> 0 Then Return controller.CompareTo(other.controller)
        If noteMask < other.noteMask Then Return -1
        If noteMask > other.noteMask Then Return 1
        If press <> other.press Then Return IIf(press, -1, 1)
        Return 0
    End Function
End Class

Friend Class clsSectionEntry
    Public tickStart As Integer
    Public tickEnd As Integer
    Public name As String

    Public Sub New(_tickstart As Integer, _name As String)
        tickStart = _tickstart
        name = _name
    End Sub

    Public Sub New(_tickstart As Integer, _name As String, prevSection As clsSectionEntry)
        tickStart = _tickstart
        name = _name
        tickEnd = Integer.MaxValue
        If Not prevSection Is Nothing Then
            prevSection.tickEnd = tickStart
        End If
    End Sub

End Class

Friend Class clsTempoEntry
    Public tickStart As Integer
    Public tickEnd As Integer
    Public usStart As Decimal
    Public rate As Decimal

    Public Sub New(newtickstart As Integer, newrate As Integer)
        tickStart = newtickstart
        rate = newrate
    End Sub

    Public Sub New(newtickstart As Integer, newrate As Decimal, prevTempo As clsTempoEntry)
        tickStart = newtickstart
        rate = newrate
        tickEnd = Integer.MaxValue
        If prevTempo Is Nothing Then
            usStart = 0
        Else
            Dim prevTicks As Integer = tickStart - prevTempo.tickStart
            Dim prevUS As Decimal = prevTicks * prevTempo.rate
            prevTempo.tickEnd = tickStart
            usStart = prevTempo.usStart + prevUS
        End If
    End Sub

End Class

Friend Class clsNoteEntry
    Implements IComparable(Of clsNoteEntry)
    Public controller As Byte
    Public tickOffset As Integer
    Public tickDuration As Integer
    Public msOffset As Integer
    Public msDuration As Integer
    Public noteValue As Integer
    Public noteMask As Integer
    Public hopo As Boolean = False
    Public solo As Boolean = False
    Public comment As String
    Public eventIndex As Integer
    Public section As String

    Private Function formatMS(ms As Integer) As String
        Dim t As New Date(10000 * CLng(ms))
        Return t.ToString("mm:ss." & t.Millisecond.ToString.PadLeft(3, "0"))
    End Function

    Public Sub New(_controller As String, _track As clsTrack, _level As clsLevel, _nev As Midi.NoteOnEvent, _solo As Boolean, _hopo As Boolean, _eventIndex As Integer, _section As String)
        controller = _controller
        noteValue = _nev.NoteNumber - _level.baseNote
        tickOffset = _nev.AbsoluteTime
        tickDuration = _nev.NoteLength
        solo = _solo
        hopo = _hopo
        Select Case noteValue
            Case 0
                noteMask = _track.noteGreen(hopo, solo)
            Case 1
                noteMask = _track.noteRed(hopo, solo)
            Case 2
                noteMask = _track.noteYellow(hopo, solo)
            Case 3
                noteMask = _track.noteBlue(hopo, solo)
            Case 4
                noteMask = _track.noteOrange(hopo, solo)
        End Select
        eventIndex = _eventIndex
        section = _section
    End Sub

    Public Sub setComment()
        comment = section & "[" & eventIndex & "] " & formatMS(msOffset)
    End Sub

    Public Sub merge(ne As clsNoteEntry)
        noteMask = noteMask Or ne.noteMask
        hopo = False
    End Sub

    Public Function CompareTo(other As clsNoteEntry) As Integer Implements System.IComparable(Of clsNoteEntry).CompareTo
        If tickOffset < other.tickOffset Then Return -1
        If tickOffset > other.tickOffset Then Return 1
        If controller.CompareTo(other.controller) <> 0 Then Return controller.CompareTo(other.controller)
        If noteMask < other.noteMask Then Return -1
        If noteMask > other.noteMask Then Return 1
        Return 0
    End Function
End Class

Module modMusicConverter
    Private Function checkQ(nevTime As Long, q As Queue(Of Midi.NoteOnEvent)) As Boolean
        If q.Count = 0 Then Return False
        Dim startTime As Long = q.Peek.AbsoluteTime
        If nevTime < startTime Then Return False
        Dim endTime As Long = startTime + q.Peek.NoteLength
        If nevTime <= startTime + q.Peek.NoteLength Then Return True
        q.Dequeue()
        Return checkQ(nevTime, q)
    End Function
    Private Function scanBeatQ(nevTime As Long, q As Queue(Of trainerInfo)) As trainerInfo
        If q.Count = 0 Then Return Nothing
        Dim startTime As Long = q.Peek.startTime
        If nevTime < startTime Then Return Nothing
        If nevTime <= q.Peek.endTime Then Return q.Peek
        q.Dequeue()
        Return scanBeatQ(nevTime, q)
    End Function

    Private Class trainerInfo
        Public number As Integer
        Public startTime As Long
        Public endTime As Long
        Public bpm As Integer
        Public tempo As clsTempoEntry
        Public notes As New List(Of clsNoteEntry)

        Public Sub New(_number As Integer, _startTime As Long, _endTime As Long, _bpm As Integer, _tempo As clsTempoEntry)
            number = _number
            startTime = _startTime
            endTime = _endTime
            bpm = _bpm
            tempo = _tempo
        End Sub
    End Class

    Public Sub getTrainers(controller As Byte, track As clsTrack, evtTrackIdx As Integer)
        Dim trainerList As New List(Of trainerInfo)
        Dim trainerQ As New Queue(Of trainerInfo)
        Dim mf As Midi.MidiFile = track.mf

        If track.name = "BASS" Then track = New clsTrack(track.mf, track.index, "DRUMS", track._song)

        Dim baseTempo As clsTempoEntry = Nothing
        For Each mev As Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, Midi.MetaEvent).MetaEventType = Midi.MetaEventType.SetTempo Then
                Dim tev As Midi.TempoEvent = CType(mev, Midi.TempoEvent)
                'Debug.Print(tev.ToString)
                If baseTempo Is Nothing Then
                    baseTempo = New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, Nothing)
                Else
                    'MsgBox("too many tempo entries")
                    'Exit Sub
                End If
            End If
        Next

        Dim beatNumber As Integer = -1
        Dim startTime As Integer = -1
        Dim bpm As Integer = 60
        Dim reTrainer As New Text.RegularExpressions.Regex("^\[start_drum_trainer_beat drum_trainer_beat_(?<number>\d+)\]$")
        Dim reBPM As New Text.RegularExpressions.Regex("^\[bpm (?<bpm>\d+)\]$")
        For Each mev As Midi.MidiEvent In mf.Events(evtTrackIdx)
            If mev.CommandCode = Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, Midi.MetaEvent).MetaEventType = Midi.MetaEventType.TextEvent Then
                Dim tev As Midi.TextEvent = mev
                'Debug.Print(tev.AbsoluteTime & ":" & tev.Text)
                If beatNumber = -1 Then
                    Dim match As Text.RegularExpressions.Match = reTrainer.Match(tev.Text)
                    If match.Success Then
                        beatNumber = match.Groups("number").Value
                        startTime = tev.AbsoluteTime
                        bpm = 60
                    End If
                Else
                    If tev.Text = "[end_drum_trainer_beat]" Then
                        'Debug.Print(beatNumber & "," & startTime & "," & tev.AbsoluteTime & "," & bpm)
                        Dim beatTempo As New clsTempoEntry(baseTempo.tickStart, (60000000 / CDec(bpm)) / mf.DeltaTicksPerQuarterNote)
                        Dim ti As New trainerInfo(beatNumber, startTime, tev.AbsoluteTime, bpm, beatTempo)
                        trainerQ.Enqueue(ti)
                        trainerList.Add(ti)
                        beatNumber = -1
                        startTime = -1
                    Else
                        Dim match As Text.RegularExpressions.Match = reBPM.Match(tev.Text)
                        If match.Success Then bpm = match.Groups("bpm").Value
                    End If
                End If
            End If
        Next

        Dim level As New clsLevel(enumLevel.lvlEasy)
        Dim baseNote As Integer = level.baseNote
        Dim curBeat As trainerInfo = Nothing
        For i = 0 To mf.Events(track.index).Count - 1
            Dim mev As NAudio.Midi.MidiEvent = mf.Events(track.index)(i)
            Select Case mev.CommandCode
                Case NAudio.Midi.MidiCommandCode.NoteOn
                    curBeat = scanBeatQ(mev.AbsoluteTime, trainerQ)
                    Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                    If Not curBeat Is Nothing Then
                        'If nev.Velocity > 0 Then Debug.Print(curBeat.number & "," & mev.AbsoluteTime & "," & nev.NoteNumber)
                        If nev.NoteNumber >= baseNote And nev.NoteNumber <= baseNote + 4 And nev.Velocity > 0 Then
                            Dim ne As clsNoteEntry = Nothing
                            ne = New clsNoteEntry(controller, track, level, nev, False, False, i, vbNullString)
                            curBeat.notes.Add(ne)
                            'If ne.hopo Then Debug.Print(tmpNotes.Count & " : " & nev.AbsoluteTime & " : " & ne.tickDuration)
                        End If
                    Else
                        'Debug.Print("0," & mev.AbsoluteTime & "," & nev.NoteNumber)
                    End If

            End Select
        Next

        For Each ti As trainerInfo In trainerList
            If ti.notes.Count = 0 Then
                MsgBox("Some beats had no notes. You may want to try selecting the [BASS] track." & vbCrLf & "The drum trainer notes tend to be stored in the bass track. Yes, it makes no sense.")
                Exit Sub
            End If

            ti.notes.Sort()
            For Each ne As clsNoteEntry In ti.notes
                ne.msOffset = track._game.dilation * ti.tempo.rate * (ne.tickOffset - ti.startTime) / 1000
                ne.msDuration = ne.tickDuration * ti.tempo.rate / 1000
                ne.msDuration = ne.msDuration * track._game.dilation - track._game.truncation
                If ne.msDuration < track._game.minimumDuration Then ne.msDuration = track._game.minimumDuration
                ne.setComment()
            Next

            Dim mergedNotes As New List(Of clsNoteEntry)
            Dim lastNote As clsNoteEntry = ti.notes(0)
            mergedNotes.Add(lastNote)
            For i = 1 To ti.notes.Count - 1
                If ti.notes(i).tickOffset = lastNote.tickOffset Then
                    lastNote.merge(ti.notes(i))
                Else
                    mergedNotes.Add(ti.notes(i))
                    lastNote = ti.notes(i)
                End If
            Next

            Dim game As clsRhythmGame = track._game

            Dim noteActions As New List(Of clsNoteAction)
            For Each ne As clsNoteEntry In mergedNotes
                noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + game.loadTime, True, ne.comment))
                noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + ne.msDuration + game.loadTime, False, ne.comment))
            Next

            noteActions.Sort()

            Dim actions As New List(Of clsAction)
            Dim curOffset As Integer = 0
            Dim a As clsAction

            Dim startMS As Integer = Integer.MaxValue
            Dim startController As String = vbNullString
            Dim endMS As Integer = 0
            If noteActions.Count > 0 Then
                startMS = noteActions(0).msOffset
                endMS = noteActions(noteActions.Count - 1).msOffset
                startController = noteActions(0).controller
            End If
            Dim endSpan As DateTime = (New DateTime).AddMilliseconds(endMS)
            Dim info As String = "[" & startController & "] starts at " & ((startMS - game.loadTime) / 1000) & "s (of " & endSpan.ToString("m:ss") & ")"

            Dim curmask As Integer = 0
            For i As Integer = 0 To noteActions.Count - 1
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

            lastNote = mergedNotes(mergedNotes.Count - 1)
            Dim beatLengthMS As Integer = (ti.endTime - ti.startTime) * track._game.dilation * ti.tempo.rate / 1000
            Dim endWaitMS As Integer = beatLengthMS - (lastNote.msOffset + lastNote.msDuration)

            If endWaitMS > 0 Then
                a = New clsActionWait(endWaitMS, Nothing)
                a.index = actions.Count
                actions.Add(a)
            End If

            'a = New clsActionPress(1, clsController.XBButtons.btnGuide, -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), 500, 500, 1, Nothing)
            'a.index = actions.Count
            'actions.Add(a)

            Dim path As String = track._song.fi.FullName
            path = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\scripts\" & game.code & "-" & IO.Path.GetFileNameWithoutExtension(path) & "-" & ti.number & ".axb"
            saveTrainer(game.name, "Drum Trainer [" & track._song.name & "] #" & ti.number, info, actions, path)

            a = New clsActionLoop(actions(0), 50, Nothing)
            a.index = actions.Count
            actions.Add(a)

            path = track._song.fi.FullName
            path = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\scripts\" & game.code & "-" & IO.Path.GetFileNameWithoutExtension(path) & "-" & ti.number & "_loop.axb"
            saveTrainer(game.name, "Drum Trainer LOOP [" & track._song.name & "] #" & ti.number, info, actions, path)
        Next

        MsgBox("Scripts saved in " & IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\scripts")

    End Sub

    Public Sub saveTrainer(gameName As String, scriptTitle As String, description As String, actions As List(Of clsAction), path As String)
        Dim doc As New Xml.XmlDocument
        Dim root As Xml.XmlElement = doc.CreateElement("XBScript")
        doc.AppendChild(root)
        Dim desc As Xml.XmlElement = doc.CreateElement("Information")
        root.AppendChild(desc)
        desc.AppendChild(doc.CreateElement("Game")).InnerText = gameName
        desc.AppendChild(doc.CreateElement("Title")).InnerText = scriptTitle
        desc.AppendChild(doc.CreateElement("Description")).InnerText = description
        desc.AppendChild(doc.CreateElement("Version")).InnerText = 2
        Dim agsNode As Xml.XmlElement = doc.CreateElement("ActionGroups")
        root.AppendChild(agsNode)

        Dim agNode As Xml.XmlElement = doc.CreateElement("ActionGroup")
        agsNode.AppendChild(agNode)
        agNode.AppendChild(doc.CreateElement("Name")).InnerText = "[Main]"

        For Each action As clsAction In actions
            agNode.AppendChild(action.toXML(doc))
        Next

        Dim ws As New Xml.XmlWriterSettings()
        ws.Indent = True
        Dim w As Xml.XmlWriter = Xml.XmlWriter.Create(path, ws)
        doc.WriteTo(w)
        w.Close()
        w.Dispose()
    End Sub


    Public Function getNotes(controller As Byte, Track As clsTrack, Level As clsLevel, HopoThreshold As Integer) As List(Of clsNoteEntry)
        Dim baseNote As Integer = Level.baseNote
        Dim mf As Midi.MidiFile = Track.mf
        Dim tempos As New List(Of clsTempoEntry)
        Dim tpq As Integer = mf.DeltaTicksPerQuarterNote
        Dim prevTempo As clsTempoEntry = Nothing

        Dim sections As New List(Of clsSectionEntry)
        Dim evtTrack As Integer = -1
        For i As Integer = 1 To mf.Tracks - 1
            Dim mev As Midi.MidiEvent = mf.Events(i)(0)
            If mev.CommandCode = Midi.MidiCommandCode.MetaEvent _
                AndAlso CType(mev, Midi.MetaEvent).MetaEventType = Midi.MetaEventType.SequenceTrackName _
                AndAlso CType(mev, Midi.TextEvent).Text = "EVENTS" Then
                evtTrack = i
                Exit For
            End If
        Next

        Dim prevSection As clsSectionEntry = Nothing
        If evtTrack > 0 Then
            For i As Integer = 0 To mf.Events(evtTrack).Count - 1
                Dim mev As Midi.MidiEvent = mf.Events(evtTrack)(i)
                If mev.CommandCode = Midi.MidiCommandCode.MetaEvent _
                    AndAlso CType(mev, Midi.MetaEvent).MetaEventType = Midi.MetaEventType.TextEvent Then
                    Dim tev As Midi.TextEvent = CType(mev, Midi.TextEvent)
                    If tev.Text = "[drum_trainer_begin]" Then
                        Select Case MsgBox("This looks like a drum trainer track, do you want to create a file for each beat?", MsgBoxStyle.YesNoCancel)
                            Case MsgBoxResult.Yes
                                getTrainers(controller, Track, evtTrack)
                                Return Nothing
                            Case MsgBoxResult.No
                                Exit For
                            Case MsgBoxResult.Cancel
                                Return Nothing
                        End Select
                    End If
                    If tev.Text.StartsWith("[section ") Then
                        Dim sectName As String = CType(mev, Midi.TextEvent).Text.Substring(9)
                        sectName = sectName.Substring(0, sectName.Length - 1)
                        Dim section As clsSectionEntry = New clsSectionEntry(mev.AbsoluteTime, sectName, prevSection)
                        sections.Add(section)
                        prevSection = section
                    End If
                End If
            Next
        End If

        For Each mev As Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, Midi.MetaEvent).MetaEventType = Midi.MetaEventType.SetTempo Then
                Dim tev As Midi.TempoEvent = CType(mev, Midi.TempoEvent)
                Dim tempo As New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, prevTempo)
                tempos.Add(tempo)
                prevTempo = tempo
            End If
        Next

        If sections.Count = 0 Then
            sections.Add(New clsSectionEntry(0, "Full"))
            sections(0).tickEnd = Integer.MaxValue
        End If

        Dim soloQ As New Queue(Of Midi.NoteOnEvent)
        Dim forceHOPOQ As New Queue(Of Midi.NoteOnEvent)
        Dim forceStrumQ As New Queue(Of Midi.NoteOnEvent)
        Select Case Track.name
            Case "GUITAR", "BASS"
                For i As Integer = 0 To mf.Events(Track.index).Count - 1
                    Dim mev As Midi.MidiEvent = mf.Events(Track.index)(i)
                    If mev.CommandCode = Midi.MidiCommandCode.NoteOn Then
                        Dim nev As Midi.NoteOnEvent = mev
                        If nev.Velocity > 0 Then
                            Select Case nev.NoteNumber
                                Case 103
                                    soloQ.Enqueue(nev)
                                Case baseNote + 5
                                    forceHOPOQ.Enqueue(nev)
                                Case baseNote + 6
                                    forceStrumQ.Enqueue(nev)
                            End Select
                        End If
                    End If
                Next
        End Select

        Dim nevGroups As New List(Of List(Of Tuple(Of Integer, Midi.NoteOnEvent)))
        Dim lastTime As Long = 0
        Dim curGroup As List(Of Tuple(Of Integer, Midi.NoteOnEvent)) = Nothing
        For i As Integer = 0 To mf.Events(Track.index).Count - 1
            Dim mev As Midi.MidiEvent = mf.Events(Track.index)(i)
            Select Case mev.CommandCode
                Case Midi.MidiCommandCode.NoteOn
                    Dim nev As Midi.NoteOnEvent = CType(mev, Midi.NoteOnEvent)
                    If nev.NoteNumber >= baseNote And nev.NoteNumber <= baseNote + 4 And nev.Velocity > 0 Then
                        If nev.AbsoluteTime <> lastTime Then
                            lastTime = nev.AbsoluteTime
                            curGroup = New List(Of Tuple(Of Integer, Midi.NoteOnEvent))
                            nevGroups.Add(curGroup)
                        End If
                        curGroup.Add(New Tuple(Of Integer, Midi.NoteOnEvent)(i, nev))
                    End If
            End Select
        Next

        '        If Not lastnote Is Nothing AndAlso tickOffset - lastnote.tickOffset < dtpqn / 3 AndAlso tickOffset <> lastnote.tickOffset Then hopo = lastnote.noteValue <> noteValue
        '        hopo = False


        Dim tmpNotes As New List(Of clsNoteEntry)
        Dim curSection As Integer = 0
        Dim inSolo As Boolean = False
        Dim lastGroup As List(Of Tuple(Of Integer, Midi.NoteOnEvent)) = Nothing
        For Each nevGroup As List(Of Tuple(Of Integer, Midi.NoteOnEvent)) In nevGroups
            Dim nevTime As Long = nevGroup(0).Item2.AbsoluteTime
            While nevTime >= sections(curSection).tickEnd
                curSection += 1
            End While
            inSolo = checkQ(nevTime, soloQ)

            If Track.hopo Then
                Dim doHopo As Boolean = False
                If lastGroup Is Nothing OrElse checkQ(nevTime, forceStrumQ) Then
                    doHopo = False
                Else
                    If checkQ(nevTime, forceHOPOQ) Then
                        doHopo = True
                    Else
                        If nevTime - lastGroup(0).Item2.AbsoluteTime <= HopoThreshold Then
                            If nevGroup.Count = 1 Then
                                If Not lastGroup.Select(Function(x) x.Item2.NoteNumber).Contains(nevGroup(0).Item2.NoteNumber) Then
                                    doHopo = True
                                End If
                            End If
                        End If
                    End If
                End If

                For Each nevTup As Tuple(Of Integer, Midi.NoteOnEvent) In nevGroup
                    Dim ne As clsNoteEntry = Nothing
                    ne = New clsNoteEntry(controller, Track, Level, nevTup.Item2, inSolo, doHopo, nevTup.Item1, sections(curSection).name & "-" & nevTup.Item1)
                    tmpNotes.Add(ne)
                Next
            Else
                For Each nevTup As Tuple(Of Integer, Midi.NoteOnEvent) In nevGroup
                    Dim ne As clsNoteEntry = Nothing
                    ne = New clsNoteEntry(controller, Track, Level, nevTup.Item2, inSolo, False, nevTup.Item1, sections(curSection).name & "-" & nevTup.Item1)
                    tmpNotes.Add(ne)
                Next
            End If
            lastGroup = nevGroup
        Next

        tmpNotes.Sort()
        Dim t As Integer = 0

        For Each ne As clsNoteEntry In tmpNotes
            While ne.tickOffset >= tempos(t).tickEnd
                t = t + 1
            End While
            ne.msOffset = Track._game.dilation * (tempos(t).usStart + tempos(t).rate * (ne.tickOffset - tempos(t).tickStart)) / 1000
            If ne.tickDuration + ne.tickOffset <= tempos(t).tickEnd Then
                ne.msDuration = ne.tickDuration * tempos(t).rate / 1000
            Else
                'If ne.tickDuration + ne.tickOffset > tempos(t + 1).tickEnd Then Stop
                ne.msDuration = (tempos(t).tickEnd - ne.tickOffset) * tempos(t).rate / 1000 + (ne.tickDuration + ne.tickOffset - tempos(t + 1).tickStart) * tempos(t + 1).rate / 1000
            End If
            ne.msDuration = ne.msDuration * Track._game.dilation - Track._game.truncation
            If ne.msDuration < Track._game.minimumDuration Then ne.msDuration = Track._game.minimumDuration
            ne.setComment()
        Next
        Return tmpNotes
    End Function


End Module
