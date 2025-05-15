Imports System.Windows.Threading

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)
        MainWindow = New MainWindow()

        MainWindow.Show()

    End Sub


    Private Sub Application_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        IO.Directory.CreateDirectory("C:\test")
        IO.File.WriteAllText("C:\test\ConversionError.txt", e.Exception.ToString())
        Dim mainWindow As MainWindow = Application.Current.MainWindow
        mainWindow.ShowDialogPopup("Fatal Error", $"Fatal Error Log has been written to C:\test\ConversionError.txt")
        MessageBox.Show(e.Exception.Message)
    End Sub

End Class
