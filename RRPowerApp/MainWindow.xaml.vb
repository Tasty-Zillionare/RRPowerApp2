
Imports MahApps.Metro.Controls.MetroWindow
Imports MahApps.Metro.Controls.Dialogs.DialogManager
Imports MahApps.Metro.Controls.Dialogs
Public Class MainWindow


    Public Sub New()
        InitializeComponent()
        DataContext = New MainWindowViewModel()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Friend Async Sub ShowDialogPopup(v1 As String, v2 As String)
        Dim result = Await Me.ShowMessageAsync(v1, v2)
    End Sub


End Class
