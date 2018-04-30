Public Class WebBrowser

    Public Property Name As BrowserName

    Public Property MajorVersion As Integer

    Public Sub New(ByVal _name As String, ByVal _majorVersion As Integer)
        Name = TranslateStringToBrowserName(_name)
        MajorVersion = _majorVersion
    End Sub

    Private Function TranslateStringToBrowserName(ByVal name As String) As BrowserName
        If name.Contains("IE") Then Return BrowserName.InternetExplorer
        Return BrowserName.Unknown
    End Function

    Public Enum BrowserName
        Unknown
        InternetExplorer
        Firefox
        Chrome
        Opera
        Safari
        Dolphin
        Konqueror
        Linx
    End Enum
End Class
