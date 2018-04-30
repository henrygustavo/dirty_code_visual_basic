Imports NUnit.Framework

Module ExceptionAssert

    Function Throws(Of T As Exception)(ByVal action As Action) As T
        Try
            action()
        Catch ex As T
            Return ex
        End Try

        Assert.Fail("Expected exception of type {0}.", GetType(T))
        Return Nothing
    End Function
End Module
