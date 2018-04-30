Imports BusinessLayer

Public Class SqlServerCompactRepository
    Implements IRepository

    Private Function IRepository_SaveSpeaker(speaker As Speaker) As Integer Implements IRepository.SaveSpeaker
        Return 1
    End Function
End Class