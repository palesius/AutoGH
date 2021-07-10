Option Strict On
Option Explicit On

Imports SlimDX
Imports SlimDX.XAudio2
Imports slimDX.Multimedia
Imports System.IO

Module modTone
    Dim xaudio2 As XAudio2
    Dim masteringVoice As MasteringVoice
    Const twoPi As Double = Math.PI * 2

    Public Sub makeTone()
        xaudio2 = New XAudio2
        masteringVoice = New MasteringVoice(xaudio2)
        Dim ms As New MemoryStream
        Dim format As WaveFormat = CreateWaveFormat(44100, 1, 16) ' 44.1kSamples/sec, mono, 16 bit
        Dim bl As New List(Of Byte)
        bl.AddRange(CreateSineData16Bit(format, 261.63, 1))
        bl.AddRange(CreateSineData16Bit(format, 329.63, 1))
        bl.AddRange(CreateSineData16Bit(format, 392.0, 1))

        Dim bytes() As Byte = bl.ToArray

        '329.63
        '392.00
        ms.Write(bytes, 0, bytes.Length)
        ms.Seek(0, SeekOrigin.Begin)
        Dim audioBuffer As New AudioBuffer
        audioBuffer.AudioData = ms
        audioBuffer.AudioBytes = bytes.Length
        audioBuffer.Flags = BufferFlags.EndOfStream
        Dim sourceVoice As New SourceVoice(xaudio2, format)
        sourceVoice.SubmitSourceBuffer(audioBuffer)
        sourceVoice.Start()
        While (sourceVoice.State.BuffersQueued > 0)
            Threading.Thread.Sleep(10)
        End While
        sourceVoice.Dispose()
        audioBuffer.Dispose()
        ms.Dispose()
        If masteringVoice IsNot Nothing Then masteringVoice.Dispose()
        If xaudio2 IsNot Nothing Then xaudio2.Dispose()
        Stop
    End Sub

    Private Function CreateWaveFormat(ByVal samplesPerSecond As Integer, ByVal channels As Short, ByVal bitsPerSample As Short) As WaveFormat
        Dim format As New WaveFormat
        format.FormatTag = WaveFormatTag.Pcm
        format.BitsPerSample = bitsPerSample
        format.Channels = channels
        format.SamplesPerSecond = samplesPerSecond
        format.BlockAlignment = format.Channels * format.BitsPerSample \ 8S
        format.AverageBytesPerSecond = format.SamplesPerSecond * format.BlockAlignment
        Return format
    End Function

    Private Function CreateSineData16Bit(ByVal format As WaveFormat, ByVal frequency As Double, ByVal duration As Integer) As Byte()
        Dim buffer(duration * format.AverageBytesPerSecond - 1) As Byte
        Dim theta As Double = 0
        Dim thetaStep As Double = (frequency * twoPi) / format.SamplesPerSecond
        For i As Integer = 0 To buffer.Length - 1 Step format.BlockAlignment
            Dim value As Short = CType(Short.MaxValue * Math.Sin(theta), Short)
            theta += thetaStep
            Dim bytes() As Byte = BitConverter.GetBytes(value)
            For channel As Integer = 0 To format.Channels - 1
                buffer(i + (channel * 2)) = bytes(0)
                buffer(i + (channel * 2) + 1) = bytes(1)
            Next
        Next
        Return buffer
    End Function

End Module
