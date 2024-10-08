Public MustInherit Class CommandBase
    Implements ICommand

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler
        RemoveHandler(value As EventHandler)
            RemoveHandler CommandManager.RequerySuggested, value
        End RemoveHandler
        RaiseEvent(sender As Object, e As EventArgs)
            Execute(e)
        End RaiseEvent
    End Event

    Public MustOverride Sub Execute(parameter As Object) Implements ICommand.Execute

    Public Overridable Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function

    Protected Overridable Sub OnCanExecuteChanged()
        RaiseEvent CanExecuteChanged(Me, New EventArgs())
    End Sub
End Class
