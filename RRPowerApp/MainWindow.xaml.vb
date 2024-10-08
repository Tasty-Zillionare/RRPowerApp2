Imports MahApps.Metro.Controls.MetroWindow
Imports MahApps.Metro.Controls.Dialogs.DialogManager
Imports MahApps.Metro.Controls.Dialogs
Class MainWindow


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DataContext = New MainWindowViewModel()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


End Class
