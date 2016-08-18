Module modFormat
    Public Function formatMS(ms As Integer) As String
        Dim s As New TimeSpan(CLng(ms) * 10000)
        Dim sb As New System.Text.StringBuilder
        If s.Days > 0 Then sb.Append(Math.Floor(s.TotalDays) & "d")
        If s.Hours = 0 And s.Minutes = 0 And s.Seconds = 0 And s.Milliseconds = 0 Then Return sb.ToString
        If sb.Length > 0 OrElse s.Hours > 0 Then sb.Append(s.Hours.ToString(IIf(sb.Length = 0, "0", "00")) & "h")
        If s.Minutes = 0 And s.Seconds = 0 And s.Milliseconds = 0 Then Return sb.ToString
        If sb.Length > 0 OrElse s.Minutes > 0 Then sb.Append(s.Minutes.ToString(IIf(sb.Length = 0, "0", "00")) & "m")
        If s.Seconds = 0 And s.Milliseconds = 0 Then Return sb.ToString
        If sb.Length > 0 OrElse s.Seconds > 0 Then sb.Append(s.Seconds.ToString(IIf(sb.Length = 0, "0", "00")) & "s")
        If s.Milliseconds = 0 Then Return sb.ToString
        If sb.Length > 0 OrElse s.Milliseconds > 0 Then sb.Append(s.Milliseconds.ToString(IIf(sb.Length = 0, "0", "000")) & "ms")
        Return sb.ToString
    End Function
End Module
