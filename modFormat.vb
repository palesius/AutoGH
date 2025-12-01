Imports System.Text.RegularExpressions
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

    Public Function unformatMS(src As String) As Integer
        Dim re As New Regex("^((((?<d>\d+)d)?((?<h>\d+)h)?((?<m>\d+)m)?((?<s>\d+)s)?((?<ms>\d+)ms)?)|((?<h>\d+):(?<m>\d\d):(?<s>\d\d)(\.(?<msd>\d{1,3}))?)|((?<m>\d+):(?<s>\d\d)(\.(?<msd>\d{1,3}))?)|((?<s>\d+)?(\.(?<msd>\d{1,3}))?))$")
        Dim m As System.Text.RegularExpressions.Match = re.Match(src)
        If Not m.Success Then Return -1
        Dim strMs As String = m.Groups("msd").Value
        If strMs = vbNullString Then strMs = m.Groups("ms").Value Else strMs = strMs.PadRight(3, "0")
        If strMs = vbNullString Then strMs = 0
        Dim strS As String = m.Groups("s").Value
        If strS = vbNullString Then strS = 0
        Dim strM As String = m.Groups("m").Value
        If strM = vbNullString Then strM = 0
        Dim strH As String = m.Groups("h").Value
        If strH = vbNullString Then strH = 0
        Dim strD As String = m.Groups("d").Value
        If strD = vbNullString Then strD = 0

        Dim ts As New TimeSpan(strD, strH, strM, strS, strMs)
        Return Math.Floor(ts.TotalMilliseconds)
    End Function
End Module
