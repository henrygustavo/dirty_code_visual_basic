Public Class Session

    Public Property Title As String

    Public Property Description As String

    Public Property Approved As Boolean

    Public Sub New(ByVal title As String, ByVal description As String)
        MyBase.New
        Me.Title = title
        Me.Description = description
    End Sub
End Class