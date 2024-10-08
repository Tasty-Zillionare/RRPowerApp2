Public MustInherit Class AsyncCommandBase
    Inherits CommandBase


    Public Overrides Async Sub Execute(parameter As Object)
        Await ExecuteAsync(parameter)

    End Sub


    Public MustOverride Async Function ExecuteAsync(paramter As Object) As Task
End Class
