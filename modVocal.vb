Imports System.IO
Imports System.Linq
Imports NAudio.MediaFoundation
Imports NAudio.Wave

Module modVocal
    Private Class clsVoiceNoteEntry
        Implements IComparable(Of clsVoiceNoteEntry)
        Public tickOffset As Integer
        Public tickDuration As Integer
        Public msOffset As Integer
        Public msDuration As Integer
        Public noteVal As Integer
        Public frequency As Decimal

        Public Sub New(newnote As Integer, newTime As Integer, newduration As Integer)
            noteVal = newnote
            tickOffset = newTime
            tickDuration = newduration
        End Sub

        Public Function CompareTo(other As clsVoiceNoteEntry) As Integer Implements System.IComparable(Of clsVoiceNoteEntry).CompareTo
            If tickOffset < other.tickOffset Then Return -1
            If tickOffset > other.tickOffset Then Return 1
            If noteVal < other.noteVal Then Return -1
            If noteVal > other.noteVal Then Return 1
            Return 0
        End Function

    End Class

    Private Class trackInfo
        Public number As Integer
        Public name As String
        Public samples As List(Of Short)
        Private notes As List(Of clsVoiceNoteEntry)

        Public Sub New(_number As Integer, _name As String)
            number = _number
            name = _name
        End Sub

        Public Function startTime() As Integer
            Return notes(0).msOffset
        End Function

        Public Function endTime() As Integer
            Return notes(notes.Count - 1).msOffset + notes(notes.Count - 1).msDuration
        End Function

        Public Sub readnotes(mf As NAudio.Midi.MidiFile, tempos As List(Of clsTempoEntry))
            notes = New List(Of clsVoiceNoteEntry)
            Dim talkies As New List(Of Long)
            For i As Integer = mf.Events(number).Count - 1 To 0 Step -1
                Dim mev As NAudio.Midi.MidiEvent = mf.Events(number)(i)
                If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.Lyric Then
                    Dim tev As NAudio.Midi.TextEvent = mev
                    Dim text As String = tev.Text
                    Select Case text.Substring(text.Length - 1, 1)
                        Case "#", "^", "*"
                            talkies.Add(tev.AbsoluteTime)
                    End Select
                End If
            Next
            talkies.Sort()

            If name = "BEAT" Then
                For i As Integer = mf.Events(number).Count - 1 To 0 Step -1
                    Dim mev As NAudio.Midi.MidiEvent = mf.Events(number)(i)
                    If mev.CommandCode = NAudio.Midi.MidiCommandCode.NoteOn Then
                        Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                        If (nev.NoteNumber = 12 Or nev.NoteNumber = 13 Or nev.NoteNumber = 36 Or nev.NoteNumber = 37) Then
                            Dim ne As clsVoiceNoteEntry = Nothing
                            ne = New clsVoiceNoteEntry(nev.NoteNumber, nev.AbsoluteTime, 120)
                            notes.Add(ne)
                            Exit For
                        Else
                            Stop
                        End If
                    End If
                Next
            Else
                For Each mev As NAudio.Midi.MidiEvent In mf.Events(number)
                    If mev.CommandCode = NAudio.Midi.MidiCommandCode.NoteOn Then
                        Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                        If nev.NoteNumber >= 16 And nev.NoteNumber <= 95 And nev.Velocity > 0 Then
                            If nev.NoteNumber <= 28 Or nev.NoteNumber >= 84 Then Stop
                            Dim ne As clsVoiceNoteEntry = Nothing
                            ne = New clsVoiceNoteEntry(nev.NoteNumber, nev.AbsoluteTime, nev.NoteLength)
                            notes.Add(ne)
                        End If
                    End If
                Next
                notes.Sort()
            End If


            Dim t As Integer = 0
            For Each ne As clsVoiceNoteEntry In notes
                If talkies.BinarySearch(ne.tickOffset) >= 0 Then ne.noteVal = -1

                While ne.tickOffset >= tempos(t).tickEnd
                    t = t + 1
                End While
                ne.msOffset = (tempos(t).usStart + tempos(t).rate * (ne.tickOffset - tempos(t).tickStart)) / 1000
                If ne.tickDuration + ne.tickOffset <= tempos(t).tickEnd Then
                    ne.msDuration = ne.tickDuration * tempos(t).rate / 1000
                Else
                    ne.msDuration = (tempos(t).tickEnd - ne.tickOffset) * tempos(t).rate / 1000 + (ne.tickDuration + ne.tickOffset - tempos(t + 1).tickStart) * tempos(t + 1).rate / 1000
                End If
                While ne.noteVal < 58 And ne.noteVal > 0
                    ne.noteVal = ne.noteVal + 12
                End While
                If ne.noteVal > 0 Then ne.frequency = 440 * 2 ^ ((ne.noteVal - 69) / 12) Else ne.frequency = -1
                Debug.Print(ne.msOffset & "(" & ne.msDuration & "):" & ne.frequency)
            Next

            Dim spreadMS_Pre As Integer = 250
            Dim spreadMS_Post As Integer = 100
            For i = 0 To notes.Count - 1
                With notes(i)
                    If i > 0 Then
                        .msOffset = .msOffset - spreadMS_Pre
                        .msDuration = .msDuration + spreadMS_Post + spreadMS_Pre
                    Else
                        .msDuration = .msDuration + spreadMS_Post
                    End If
                End With
            Next
            For i = 0 To notes.Count - 2
                With notes(i)
                    If .msOffset + .msDuration > notes(i + 1).msOffset Then
                        'Dim overage As Integer = .msOffset + .msDuration - tmpNotes(i + 1).msOffset
                        '.msDuration = .msDuration - overage * (spreadMS_Post / (spreadMS_Pre + spreadMS_Post))
                        'tmpNotes(i + 1).msOffset = tmpNotes(i + 1).msOffset + overage * (spreadMS_Pre / (spreadMS_Pre + spreadMS_Post))
                        .msDuration = notes(i + 1).msOffset - .msOffset
                    End If
                End With
            Next
        End Sub

        Public Sub generateSamples(startoffset As Integer, endoffset As Integer)
            samples = New List(Of Short)
            Dim silence As Integer
            For Each ne As clsVoiceNoteEntry In notes
                silence = Math.Floor((ne.msOffset - startoffset) * 44.1) - samples.Count
                If silence > 0 Then samples.AddRange(generateSilence(silence))
                If ne.msDuration > 0 Then
                    If ne.frequency > 0 Then
                        samples.AddRange(generateTone(ne.frequency, ne.msDuration * 44.1))
                    Else
                        samples.AddRange(generateNoise(ne.msDuration * 44.1))
                    End If
                End If
            Next
            silence = Math.Floor((endoffset - startoffset) * 44.1) - samples.Count
            If silence > 0 Then samples.AddRange(generateSilence(silence))
        End Sub
    End Class

    Sub generateAllXMLMP3(dirPath As String)
        For Each fi As IO.FileInfo In New IO.DirectoryInfo(dirPath).GetFiles("*.xml")
            generateGreaseXMLMP3(fi)
        Next
    End Sub

    Function generateGreaseXMLMP3(xmlFI As IO.FileInfo) As IO.FileInfo
        Dim xml As New Xml.XmlDocument
        xml.Load(xmlFI.FullName)
        If xml.SelectNodes("/Song/Pages/Page/Harmonies/*").Count > 0 Then Stop
        Dim noteNodes As Xml.XmlNodeList = xml.SelectNodes("/Song/Pages/Page/Notes/Note")
        '		<Interval t1="35.583644" t2="35.748152" value="71"/>
        Dim neList As New List(Of clsVoiceNoteEntry)
        For Each n As Xml.XmlNode In noteNodes
            Select Case n.InnerText
                Case "START_SONG", "END_SONG"
                Case Else
                    Dim t1 As Decimal = n.Attributes("start").Value
                    Dim t2 As Decimal = n.Attributes("end").Value
                    Dim note As Integer = n.Attributes("midi_note").Value
                    Dim ne As New clsVoiceNoteEntry(note, CInt(1000 * t1), CInt(1000 * (t2 - t1)))
                    ne.frequency = 440 * 2 ^ ((ne.noteVal - 69) / 12)
                    neList.Add(ne)
            End Select
        Next
        neList.Sort()

        Dim startOffset As Integer = neList(0).tickOffset
        Dim samples As New List(Of Short)
        Dim silence As Integer
        For Each ne As clsVoiceNoteEntry In neList
            silence = Math.Floor((ne.tickOffset - startOffset) * 44.1) - samples.Count
            If silence > 0 Then samples.AddRange(generateSilence(silence))
            If ne.tickDuration > 0 Then samples.AddRange(generateTone(ne.frequency, ne.tickDuration * 44.1))
        Next

        Dim wavname As String = IO.Path.ChangeExtension(xmlFI.FullName, ".wav")
        Dim mp3name As String = IO.Path.ChangeExtension(xmlFI.FullName, ".mp3")
        saveWav(samples.ToArray(), wavname)

        Dim psi As New ProcessStartInfo("\\winterfell\c$\program files\lame\lame.exe", "-V1 """ & wavname & """ """ & mp3name & """")
        'psi.CreateNoWindow = True
        psi.WindowStyle = ProcessWindowStyle.Hidden
        Dim ps As Process = Process.Start(psi)
        ps.WaitForExit()

        IO.File.Delete(wavname)

        Return New IO.FileInfo(mp3name)
    End Function

    Function generateXMLMP3(xmlFI As IO.FileInfo) As IO.FileInfo
        Dim xml As New Xml.XmlDocument
        xml.Load(xmlFI.FullName)
        Dim noteNodes As Xml.XmlNodeList = xml.SelectNodes("/AnnotationFile/IntervalLayer[@name='notes']/Interval")
        '		<Interval t1="35.583644" t2="35.748152" value="71"/>
        Dim neList As New List(Of clsVoiceNoteEntry)
        For Each n As Xml.XmlNode In noteNodes
            Dim t1 As Decimal = n.Attributes("t1").Value
            Dim t2 As Decimal = n.Attributes("t2").Value
            Dim note As Integer = n.Attributes("value").Value
            Dim ne As New clsVoiceNoteEntry(note, CInt(1000 * t1), CInt(1000 * (t2 - t1)))
            ne.frequency = 440 * 2 ^ ((ne.noteVal - 69) / 12)
            neList.Add(ne)
        Next
        neList.Sort()

        Dim startOffset As Integer = neList(0).tickOffset
        Dim samples As New List(Of Short)
        Dim silence As Integer
        For Each ne As clsVoiceNoteEntry In neList
            silence = Math.Floor((ne.tickOffset - startOffset) * 44.1) - samples.Count
            If silence > 0 Then samples.AddRange(generateSilence(silence))
            If ne.tickDuration > 0 Then samples.AddRange(generateTone(ne.frequency, ne.tickDuration * 44.1))
        Next

        Dim wavname As String = IO.Path.ChangeExtension(xmlFI.FullName, ".wav")
        Dim mp3name As String = IO.Path.ChangeExtension(xmlFI.FullName, ".mp3")
        saveWav(samples.ToArray(), wavname)

        Dim psi As New ProcessStartInfo("c:\program files\lame\lame.exe", "-V1 """ & wavname & """ """ & mp3name & """")
        'psi.CreateNoWindow = True
        psi.WindowStyle = ProcessWindowStyle.Hidden
        Dim ps As Process = Process.Start(psi)
        ps.WaitForExit()

        IO.File.Delete(wavname)

        Return New IO.FileInfo(mp3name)
    End Function

    Function generateHarmMP3(midipath As String) As List(Of IO.FileInfo)
        Dim offset As Integer = 0
        Dim fiList As List(Of IO.FileInfo) = generateHarmWAV(midipath, offset)
        Dim inputs As New List(Of NAudio.Wave.WaveFileReader)
        NAudio.MediaFoundation.MediaFoundationApi.Startup()
        Dim mt As NAudio.MediaFoundation.MediaType = NAudio.Wave.MediaFoundationEncoder.SelectMediaType(NAudio.MediaFoundation.AudioSubtypes.MFAudioFormat_MP3, New NAudio.Wave.WaveFormat(44100, 1), 0)
        Dim enc As New NAudio.Wave.MediaFoundationEncoder(mt)

        Dim results As New List(Of IO.FileInfo)
        For Each fi As IO.FileInfo In fiList
            Dim wfr As New NAudio.Wave.WaveFileReader(fi.FullName)
            Dim mp3Path As String = IO.Path.ChangeExtension(fi.FullName, "mp3")
            enc.Encode(mp3Path, wfr)
            wfr.Close()
            wfr.Dispose()
            If IO.File.Exists(mp3Path) Then
                fi.Delete()
                Dim mp3 As IO.FileInfo = New IO.FileInfo(mp3Path)
                Dim tf As TagLib.File = TagLib.File.Create(mp3Path)
                tf.Tag.Comment = offset
                tf.Save()
                results.Add(mp3)
            End If
        Next

        Return results
    End Function

    Function generateMP3(midipath As String) As IO.FileInfo
        Dim offset As Integer = 0
        Dim fi As IO.FileInfo = generateWAV(midipath, offset)
        NAudio.MediaFoundation.MediaFoundationApi.Startup()
        Dim mt As NAudio.MediaFoundation.MediaType = NAudio.Wave.MediaFoundationEncoder.SelectMediaType(NAudio.MediaFoundation.AudioSubtypes.MFAudioFormat_MP3, New NAudio.Wave.WaveFormat(44100, 1), 0)
        Dim enc As New NAudio.Wave.MediaFoundationEncoder(mt)
        Dim wfr As New NAudio.Wave.WaveFileReader(fi.FullName)
        Dim mp3Path As String = IO.Path.ChangeExtension(fi.FullName, "mp3")
        enc.Encode(mp3Path, wfr)
        wfr.Close()
        wfr.Dispose()
        If IO.File.Exists(mp3Path) Then
            fi.Delete()
            Dim mp3 As IO.FileInfo = New IO.FileInfo(mp3Path)
            Dim tf As TagLib.File = TagLib.File.Create(mp3Path)
            tf.Tag.Comment = offset
            tf.Save()
            Return mp3
        Else
            Stop
            Return Nothing
        End If
    End Function

    Function generateWAV(midipath As String, Optional ByRef offset As Integer = 0) As IO.FileInfo
        Dim mf As New NAudio.Midi.MidiFile(midipath)
        Dim vocalTrack As trackInfo = Nothing
        Dim beatTrack As trackInfo = Nothing

        With mf
            For i As Integer = 0 To .Tracks - 1
                For j = 0 To .Events(i).Count - 1
                    If .Events(i)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                        If CType(.Events(i)(j), NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName Then
                            Dim name As String = CType(.Events(i)(j), NAudio.Midi.TextEvent).Text
                            Select Case name
                                Case "PART VOCALS"
                                    vocalTrack = New trackInfo(i, name.Substring(5))
                                    Exit For
                                Case "BEAT"
                                    beatTrack = New trackInfo(i, "BEAT")
                                    Exit For
                            End Select
                        End If
                    End If
                Next j
            Next i
        End With

        Dim tempos As New List(Of clsTempoEntry)
        Dim tpq As Integer = mf.DeltaTicksPerQuarterNote
        Dim prevTempo As clsTempoEntry = Nothing
        For Each mev As NAudio.Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SetTempo Then
                Dim tev As NAudio.Midi.TempoEvent = CType(mev, NAudio.Midi.TempoEvent)
                Dim tempo As New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, prevTempo)
                tempos.Add(tempo)
                prevTempo = tempo
            End If
        Next

        Dim tb As New Text.StringBuilder
        For j = 0 To mf.Events(4).Count - 1
            If mf.Events(4)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                Dim mev As NAudio.Midi.MetaEvent = mf.Events(4)(j)
                If mev.MetaEventType = NAudio.Midi.MetaEventType.TextEvent Then
                    Dim tev As NAudio.Midi.TextEvent = mev
                    tb.AppendLine(tev.ToString)
                End If
            End If
        Next

        If tb.Length > 0 Then
            Clipboard.SetText(tb.ToString)
        End If

        Dim filename As String = vbNullString
        If vocalTrack IsNot Nothing Then
            vocalTrack.readnotes(mf, tempos)
            vocalTrack.generateSamples(vocalTrack.startTime, vocalTrack.endTime)
            filename = midipath.Substring(0, InStrRev(midipath, ".") - 1) & ".wav"
            saveWav(vocalTrack.samples.ToArray(), filename)
        End If

        If beatTrack IsNot Nothing Then
            beatTrack.readnotes(mf, tempos)
            beatTrack.generateSamples(beatTrack.startTime, beatTrack.endTime)
            filename = midipath.Substring(0, InStrRev(midipath, ".") - 1) & ".wav"
            saveWav(beatTrack.samples.ToArray(), filename)
        End If

        Return New IO.FileInfo(filename)
    End Function

    Function generateHarmWAV(midipath As String, Optional ByRef offset As Integer = 0) As List(Of IO.FileInfo)
        Dim mf As New NAudio.Midi.MidiFile(midipath)
        Dim tracks As New List(Of trackInfo)
        Dim beat As trackInfo = Nothing

        With mf
            For i As Integer = 0 To .Tracks - 1
                For j = 0 To .Events(i).Count - 1
                    If .Events(i)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                        If CType(.Events(i)(j), NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName Then
                            Dim name As String = CType(.Events(i)(j), NAudio.Midi.TextEvent).Text
                            Select Case name
                                Case "HARM1", "HARM2", "HARM3"
                                    tracks.Add(New trackInfo(i, name))
                                    Exit For
                                Case "BEAT"
                                    beat = New trackInfo(i, "BEAT")
                            End Select
                        End If
                    End If
                Next j
            Next i
        End With

        Dim tempos As New List(Of clsTempoEntry)
        Dim tpq As Integer = mf.DeltaTicksPerQuarterNote
        Dim prevTempo As clsTempoEntry = Nothing
        For Each mev As NAudio.Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SetTempo Then
                Dim tev As NAudio.Midi.TempoEvent = CType(mev, NAudio.Midi.TempoEvent)
                Dim tempo As New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, prevTempo)
                tempos.Add(tempo)
                prevTempo = tempo
            End If
        Next

        Dim tb As New Text.StringBuilder
        For j = 0 To mf.Events(4).Count - 1
            If mf.Events(4)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                Dim mev As NAudio.Midi.MetaEvent = mf.Events(4)(j)
                If mev.MetaEventType = NAudio.Midi.MetaEventType.TextEvent Then
                    Dim tev As NAudio.Midi.TextEvent = mev
                    tb.AppendLine(tev.ToString)
                End If
            End If
        Next
        Clipboard.SetText(tb.ToString)

        Dim startoffset As Integer = Integer.MaxValue
        For Each track As trackInfo In tracks
            track.readnotes(mf, tempos)
            startoffset = Math.Min(startoffset, track.startTime)
        Next

        beat.readnotes(mf, tempos)

        'startoffset = 0
        Dim endoffset As Integer = beat.endTime

        Dim files As New List(Of IO.FileInfo)
        For Each track As trackInfo In tracks
            Dim filename As String = vbNullString
            track.generateSamples(startoffset, track.endTime)
            filename = midipath.Substring(0, InStrRev(midipath, ".") - 1) & "-" & track.name & ".wav"
            saveWav(track.samples.ToArray(), filename)
            files.Add(New IO.FileInfo(filename))
        Next

        offset = startoffset

        Return files
    End Function

    Function generateTone(freq As Decimal, samples As Integer) As Short()
        Dim tmp(samples - 1) As Short
        Dim t As Decimal = (Math.PI * 2 * freq) / 44100
        For i As Integer = 0 To samples - 1
            tmp(i) = Convert.ToInt16(26208 * Math.Sin(t * i))
        Next
        Return tmp
    End Function

    ' This sample was generated by wrapping a plastic bag over a microphone and recording it
    ' and sampling a good 0.5sec clip in the middle.  This has empirically produced the best
    ' results for talkies.
    Private noiseData As Lazy(Of Short()) = New Lazy(Of Short())(AddressOf readNoiseFile)
    Private Function readNoiseFile() As Short()
        Dim wfr As New NAudio.Wave.WaveFileReader(Path.Combine(AppContext.BaseDirectory, "audio", "noise.wav"))
        Dim noise(wfr.SampleCount) As Short
        For i = 0 To wfr.SampleCount - 1
            noise(i) = CShort(30000 * wfr.ReadNextSampleFrame()(0))
        Next

        Return noise
    End Function

    Function generateNoise(samples As Integer) As Short()
        Dim tmp(samples - 1) As Short
        Dim i As Integer = 0
        Dim noise = noiseData.Value
        For i = 0 To samples - 1
            tmp(i) = noise(i Mod noise.Length)
        Next
        Return tmp
    End Function

    Private Function generateSilence(samples As Integer) As Short()
        Dim tmp(samples - 1) As Short
        For i = 0 To samples - 1
            tmp(i) = 0
        Next
        Return tmp
    End Function

    Private Sub saveWav(samples() As Short, path As String)
        Dim fs As New IO.FileStream(path, IO.FileMode.Create)
        Dim bw As New IO.BinaryWriter(fs)

        Const sGroupID As String = "RIFF"
        Dim dwFileLength As UInt32 = samples.Length * 2 + 36
        Dim dwDataChunkSize As UInt32 = 0

        Const sRiffType As String = "WAVE"
        Const sFormatChunkID As String = "fmt "
        Const dwFormatChunkSize As UInt32 = 16
        Const wFormatTag As UInt16 = 1
        Const wChannels As UInt16 = 1
        Const dwSamplesPerSec As UInt32 = 44100
        Const wBitsPerSample As UInt16 = 16
        Const wBlockAlign As UInt16 = CType(wChannels * (wBitsPerSample / 8), UInt16)
        Const dwAvgBytesPerSec As UInt32 = dwSamplesPerSec * wBlockAlign

        Const sDataChunkID As String = "data"

        dwDataChunkSize = samples.Length * (wBitsPerSample / 8)

        'header
        bw.Write(sGroupID.ToCharArray())
        bw.Write(dwFileLength)
        bw.Write(sRiffType.ToCharArray())

        'format
        bw.Write(sFormatChunkID.ToCharArray())
        bw.Write(dwFormatChunkSize)
        bw.Write(wFormatTag)
        bw.Write(wChannels)
        bw.Write(dwSamplesPerSec)
        bw.Write(dwAvgBytesPerSec)
        bw.Write(wBlockAlign)
        bw.Write(wBitsPerSample)

        'Data
        bw.Write(sDataChunkID.ToCharArray())
        bw.Write(dwDataChunkSize)
        For Each s As Short In samples
            bw.Write(s)
        Next

        bw.Close()
        fs.Close()
    End Sub

End Module
