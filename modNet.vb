Module modNet
    Public Function fetchPageStream(ByVal url As String, Optional auth As Net.NetworkCredential = Nothing,
                                    Optional ByVal cc As Net.CookieContainer = Nothing, Optional postData() As Byte = Nothing,
                                    Optional contentType As String = "application/x-www-form-urlencoded",
                                    Optional acceptType As String = vbNullString,
                                    Optional ByRef statuscode As Net.HttpStatusCode = 0, Optional httpMethod As String = vbNullString,
                                    Optional headers As System.Collections.Specialized.NameValueCollection = Nothing,
                                    Optional ByRef limitReset As Integer = 0,
                                    Optional ByRef respHeaders As System.Collections.Specialized.NameValueCollection = Nothing) As IO.Stream

        Dim retries As Integer = 0
        Do
            Try
                Dim req As Net.HttpWebRequest = Nothing
                req = Net.HttpWebRequest.Create(url)
                If Not headers Is Nothing Then
                    Dim tmp As New Specialized.NameValueCollection
                    Dim rmv As New List(Of String)
                    tmp.Add(headers)
                    For Each nv As String In tmp.Keys
                        Select Case nv.ToLowerInvariant()
                            Case "accept"
                                req.Accept = tmp(nv)
                                rmv.Add(nv)
                            Case "content-type"
                                req.ContentType = tmp(nv)
                                rmv.Add(nv)
                            Case "referer"
                                req.Referer = tmp(nv)
                                rmv.Add(nv)
                            Case "user-agent"
                                req.UserAgent = tmp(nv)
                                rmv.Add(nv)
                        End Select
                    Next
                    For Each nv As String In rmv
                        tmp.Remove(nv)
                    Next
                    req.Headers.Add(tmp)
                End If
                req.Timeout = 120000
                req.Credentials = auth
                If Not cc Is Nothing Then req.CookieContainer = cc Else cc = req.CookieContainer
                If acceptType <> vbNullString Then req.Accept = acceptType

                If postData Is Nothing Then
                    req.Method = IIf(httpMethod = vbNullString, "GET", httpMethod)
                Else
                    req.Method = IIf(httpMethod = vbNullString, "POST", httpMethod)
                    req.ContentType = contentType
                    req.ContentLength = postData.Length
                    Dim rs As IO.Stream = req.GetRequestStream
                    rs.Write(postData, 0, postData.Length)
                End If
                If statuscode <> 0 Then req.AllowAutoRedirect = False
                Dim resp As Net.HttpWebResponse = req.GetResponse
                statuscode = resp.StatusCode
                respHeaders = resp.Headers
                Return resp.GetResponseStream
            Catch e As Net.WebException
                If e.Message = "The operation has timed out" Then
                    statuscode = Net.HttpStatusCode.RequestTimeout
                ElseIf e.Response Is Nothing Then
                    statuscode = Net.HttpStatusCode.NotFound
                Else
                    statuscode = CType(e.Response, Net.HttpWebResponse).StatusCode
                End If
                Select Case statuscode
                    Case Net.HttpStatusCode.NotFound, Net.HttpStatusCode.Unauthorized
                        If e.Response Is Nothing Then Return Nothing Else Return e.Response.GetResponseStream
                    Case 429
                        limitReset = CType(e.Response, Net.HttpWebResponse).Headers("X-Rate-Limit-Reset")
                        If limitReset > 0 Then Return e.Response.GetResponseStream
                    Case Net.HttpStatusCode.BadRequest, 422, Net.HttpStatusCode.Forbidden, Net.HttpStatusCode.NotAcceptable
                        Return e.Response.GetResponseStream
                    Case Net.HttpStatusCode.RequestTimeout
                    Case Else
                        If Debugger.IsAttached Then Stop
                End Select
                retries = retries + 1
                If retries <= 2 Then
                    System.Threading.Thread.Sleep(5000)
                Else
                    Throw e
                End If
            End Try
        Loop Until False
        Return Nothing
    End Function

    Public Function fetchPageText(ByVal url As String, Optional auth As Net.NetworkCredential = Nothing,
                                  Optional ByVal cc As Net.CookieContainer = Nothing, Optional postData() As Byte = Nothing,
                                  Optional contentType As String = "application/x-www-form-urlencoded",
                                  Optional acceptType As String = vbNullString,
                                  Optional ByRef statuscode As Net.HttpStatusCode = 0, Optional httpMethod As String = vbNullString,
                                  Optional headers As System.Collections.Specialized.NameValueCollection = Nothing,
                                  Optional ByRef limitReset As Integer = 0,
                                  Optional ByRef respHeaders As System.Collections.Specialized.NameValueCollection = Nothing) As String
        Dim s As IO.Stream = fetchPageStream(url, auth, cc, postData, contentType, acceptType, statuscode, httpMethod, headers, limitReset, respHeaders)
        If s Is Nothing Then Return vbNullString
        Dim r As IO.StreamReader = New IO.StreamReader(s)
        Dim txt As String = r.ReadToEnd()
        r.Close()
        r.Dispose()
        s.Close()
        s.Dispose()
        Return txt
    End Function

End Module
