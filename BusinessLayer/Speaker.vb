Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class Speaker

    Public Property FirstName As String

    Public Property LastName As String

    Public Property Email As String

    Public Property Exp As Integer?

    Public Property HasBlog As Boolean

    Public Property BlogURL As String

    Public Property Browser As WebBrowser

    Public Property Certifications As List(Of String)

    Public Property Employer As String

    Public Property RegistrationFee As Integer

    Public Property Sessions As List(Of Session)

    Public Function Register(ByVal repository As IRepository) As Integer?
        Dim speakerId As Integer? = Nothing
        Dim good As Boolean = False
        Dim appr As Boolean = False
        Dim ot = New List(Of String)() From {"Cobol", "Punch Cards", "Commodore", "VBScript"}
        Dim domains = New List(Of String)() From {"aol.com", "hotmail.com", "prodigy.com", "CompuServe.com"}
        If Not String.IsNullOrWhiteSpace(FirstName) Then
            If Not String.IsNullOrWhiteSpace(LastName) Then
                If Not String.IsNullOrWhiteSpace(Email) Then
                    Dim emps = New List(Of String)() From {"Microsoft", "Google", "Fog Creek Software", "37Signals"}
                    good = ((Exp > 10 OrElse HasBlog OrElse Certifications.Count() > 3 OrElse emps.Contains(Employer)))
                    If Not good Then
                        Dim emailDomain As String = Email.Split("@"c).Last()
                        If Not domains.Contains(emailDomain) AndAlso (Not (Browser.Name = WebBrowser.BrowserName.InternetExplorer AndAlso Browser.MajorVersion < 9)) Then
                            good = True
                        End If
                    End If

                    If good Then
                        If Sessions.Count() <> 0 Then
                            For Each session As Session In Sessions
                                For Each tech As String In ot
                                    If session.Title.Contains(tech) OrElse session.Description.Contains(tech) Then
                                        session.Approved = False
                                        Exit For
                                    Else
                                        session.Approved = True
                                        appr = True
                                    End If
                                Next
                            Next
                        Else
                            Throw New ArgumentException("Can't register speaker with no sessions to present.")
                        End If

                        If appr Then
                            If Exp <= 1 Then
                                RegistrationFee = 500
                            ElseIf Exp >= 2 AndAlso Exp <= 3 Then
                                RegistrationFee = 250
                            ElseIf Exp >= 4 AndAlso Exp <= 5 Then
                                RegistrationFee = 100
                            ElseIf Exp >= 6 AndAlso Exp <= 9 Then
                                RegistrationFee = 50
                            Else
                                RegistrationFee = 0
                            End If

                            Try
                                speakerId = repository.SaveSpeaker(Me)
                            Catch e As Exception
                            End Try
                        Else
                            Throw New NoSessionsApprovedException("No sessions approved.")
                        End If
                    Else
                        Throw New SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.")
                    End If
                Else
                    Throw New ArgumentNullException("Email is required.")
                End If
            Else
                Throw New ArgumentNullException("Last name is required.")
            End If
        Else
            Throw New ArgumentNullException("First Name is required")
        End If

        Return speakerId
    End Function

    Public Class SpeakerDoesntMeetRequirementsException
        Inherits Exception

        Public Sub New(ByVal message As String)
        End Sub

        Public Sub New(ByVal format As String, ParamArray args As Object())
        End Sub
    End Class

    Public Class NoSessionsApprovedException
        Inherits Exception

        Public Sub New(ByVal message As String)
        End Sub
    End Class
End Class
