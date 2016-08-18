'https://xboxapi.com/xml/profile/palesius
Public Class clsXboxStatus
    Private Shared lastCheck As Date = #1/1/2000#
    Private Shared lastStatus As String = vbNullString
    Private Shared _priorStatus As String = vbNullString

    Public Shared ReadOnly Property status As String
        Get
            If (Now - lastCheck).TotalSeconds < 30 Then Return lastStatus
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create("https://xboxapi.com/xml/profile/palesius")
            Dim xml As New System.Xml.XmlDocument
            xml.Load(req.GetResponse.GetResponseStream)
            Dim curStatus As String = xml.SelectSingleNode("/Data/Player/Status/Online_Status").InnerText
            lastCheck = Now
            If curStatus <> lastStatus Then
                _priorStatus = lastStatus
                lastStatus = curStatus
            End If
            Return lastStatus
        End Get
    End Property

    Public Shared ReadOnly Property priorStatus As String
        Get
            If _priorStatus = vbNullString Then
                If lastStatus = vbNullString Then Return status Else Return lastStatus
            Else
                Return _priorStatus
            End If
        End Get
    End Property
End Class
