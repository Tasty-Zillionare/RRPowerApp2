Public Class RelayCommand
    Implements ICommand

    Private ReadOnly _execute As Action(Of Object)
    Private ReadOnly _canExecute As Predicate(Of Object)

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

    Public Sub New(execute As Action(Of Object), Optional canExectue As Predicate(Of Object) = Nothing)
        If execute Is Nothing Then Throw New ArgumentNullException("execute")

        _execute = execute
        _canExecute = canExectue

    End Sub




    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _execute(parameter)
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return If(_canExecute Is Nothing, True, _canExecute(parameter))
    End Function
End Class
