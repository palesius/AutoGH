Module modTest
    Sub test()
        Exit Sub
        Dim fa As New frmAudio
        fa.ShowDialog()
        Stop

        For Each d As String In NAudio.Wave.AsioOut.GetDriverNames
            Debug.Print(d)
        Next
        Dim ao As New NAudio.Wave.AsioOut("ASIO4ALL v2")
        Debug.Print(ao.DriverOutputChannelCount)

        Dim channels As Integer = ao.DriverOutputChannelCount
        'Dim devs As New NAudio.CoreAudioApi.MMDeviceEnumerator()
        'Dim outdev As NAudio.CoreAudioApi.MMDevice = Nothing
        'outdev = devs.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia)

        For rpt As Integer = 1 To 100
            If ao Is Nothing Then ao = New NAudio.Wave.AsioOut("ASIO4ALL v2")
            Dim sndIn As New List(Of NAudio.Wave.MediaFoundationReader)
            Dim length As Decimal = 0
            For i As Integer = 1 To channels
                sndIn.Add(New NAudio.Wave.MediaFoundationReader("audio\" & i & ".mp3"))
                Dim curLength As Decimal = sndIn(i - 1).Length / sndIn(i - 1).WaveFormat.AverageBytesPerSecond
                Debug.Print(sndIn(i - 1).Length)
                length = Math.Max(length, curLength)
            Next
            Dim mwp As New NAudio.Wave.MultiplexingWaveProvider(sndIn, channels)
            For i As Integer = 2 To channels - 1
                mwp.ConnectInputToOutput(i, i)
            Next

            ao.Init(mwp)
            ao.Play()
            Dim start As DateTime = Now
            While (Now - start).TotalSeconds < length
                System.Threading.Thread.Sleep(10)
            End While
            ao.Stop()
            For Each wfr As NAudio.Wave.MediaFoundationReader In sndIn
                wfr.Close()
                wfr.Dispose()
            Next
            ao.Dispose()
            ao = Nothing
        Next rpt

        Stop


        'Dim channels As Integer = 8
        'Dim devs As New NAudio.CoreAudioApi.MMDeviceEnumerator()
        'Dim outdev As NAudio.CoreAudioApi.MMDevice = Nothing
        'outdev = devs.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia)

        'For rpt As Integer = 1 To 100
        '    Dim sndIn As New List(Of NAudio.Wave.MediaFoundationReader)
        '    For i As Integer = 1 To channels
        '        sndIn.Add(New NAudio.Wave.MediaFoundationReader("audio\" & i & ".mp3"))
        '    Next
        '    Dim mwp As New NAudio.Wave.MultiplexingWaveProvider(sndIn, channels)
        '    'For i As Integer = 6 To channels - 1
        '    '    mwp.ConnectInputToOutput(i, i)
        '    'Next

        '    Dim sndOut As New NAudio.Wave.WasapiOut(outdev, NAudio.CoreAudioApi.AudioClientShareMode.Shared, True, 0)
        '    sndOut.Init(mwp)
        '    sndOut.Play()
        '    While sndOut.PlaybackState = NAudio.Wave.PlaybackState.Playing
        '        System.Threading.Thread.Sleep(10)
        '    End While
        '    sndOut.Stop()
        '    For Each wfr As NAudio.Wave.MediaFoundationReader In sndIn
        '        wfr.Close()
        '        wfr.Dispose()
        '    Next
        '    System.Threading.Thread.Sleep(100)
        '    sndOut.Dispose()
        'Next rpt
        'outdev.Dispose()

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
    End Sub
End Module
