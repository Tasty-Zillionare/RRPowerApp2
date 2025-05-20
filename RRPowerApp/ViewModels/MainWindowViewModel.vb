Public Class MainWindowViewModel
    Inherits ViewModelBase

    Private _customers As String = ""
    Private _partsinventory As String = ""
    Private _partsinventorycsv As String = ""
    Private _partsinvoice As String = ""
    Private _soheaderhist As String = ""
    Private _solabourhist As String = ""
    Private _solabourhistcsv As String = ""
    Private _vehicleInventory As String = ""
    Private _vehicles As String = ""
    Private _customerscsv As String = ""
    Private _dataworld As String = ""
    Private _status As String = ""
    Private Enum DataFile
        Customers
        PartsInventory
        PartsInventoryCSV
        PartsInvoice
        SOHeaderHist
        SOLabourHist
        SOLabourHistCSV
        VehicleInventory
        Vehicles
        CustomersCSV
    End Enum


    Public Property Customers() As String
        Get
            Return _customers
        End Get
        Set(ByVal value As String)
            If _customers <> value Then
                _customers = value
                If IO.File.Exists(value) Then SetData(New IO.FileInfo(value).Directory)
                OnPropertyChanged(NameOf(Customers))
            End If
            'If IO.File.Exists(value) Then (New IO.FileInfo(value).Directory)
        End Set
    End Property

    Public Property PartsInventory() As String
        Get
            Return _partsinventory
        End Get
        Set(ByVal value As String)
            If _partsinventory <> value Then
                _partsinventory = value
                OnPropertyChanged(NameOf(PartsInventory))

            End If
        End Set
    End Property

    Public Property PartsInventoryCSV() As String
        Get
            Return _partsinventorycsv
        End Get
        Set(ByVal value As String)
            If _partsinventorycsv <> value Then
                _partsinventorycsv = value
                OnPropertyChanged(NameOf(PartsInventoryCSV))

            End If
        End Set
    End Property

    Public Property PartsInvoice() As String
        Get
            Return _partsinvoice
        End Get
        Set(ByVal value As String)
            If _partsinvoice <> value Then
                _partsinvoice = value
                OnPropertyChanged(NameOf(PartsInvoice))
            End If
        End Set
    End Property

    Public Property SOHeaderHist() As String
        Get
            Return _soheaderhist
        End Get
        Set(ByVal value As String)
            If _soheaderhist <> value Then
                _soheaderhist = value
                OnPropertyChanged(NameOf(SOHeaderHist))
            End If
        End Set
    End Property

    Public Property SOLabourHistCSV() As String
        Get
            Return _solabourhistcsv
        End Get
        Set(ByVal value As String)
            If _solabourhistcsv <> value Then
                _solabourhistcsv = value
                OnPropertyChanged(NameOf(SOLabourHistCSV))
            End If
        End Set
    End Property

    Public Property SOLabourHist() As String
        Get
            Return _solabourhist
        End Get
        Set(ByVal value As String)
            If _solabourhist <> value Then
                _solabourhist = value
                OnPropertyChanged(NameOf(SOLabourHist))
            End If
        End Set
    End Property

    Public Property VehicleInventory() As String
        Get
            Return _vehicleInventory
        End Get
        Set(ByVal value As String)
            If _vehicleInventory <> value Then
                _vehicleInventory = value
                OnPropertyChanged(NameOf(VehicleInventory))
            End If
        End Set
    End Property

    Public Property Vehicles() As String
        Get
            Return _vehicles
        End Get
        Set(ByVal value As String)
            If _vehicles <> value Then
                _vehicles = value
                OnPropertyChanged(NameOf(Vehicles))
            End If
        End Set
    End Property

    Public Property CustomersCSV() As String
        Get
            Return _customerscsv
        End Get
        Set(ByVal value As String)
            If _customerscsv <> value Then
                _customerscsv = value
                OnPropertyChanged(NameOf(CustomersCSV))
            End If
        End Set
    End Property

    Public Property DataWorld() As String
        Get
            Return _dataworld
        End Get
        Set(ByVal value As String)
            If _dataworld <> value Then
                _dataworld = value
                OnPropertyChanged(NameOf(DataWorld))
            End If
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            If _status <> value Then
                _status = value
                OnPropertyChanged(NameOf(Status))
            End If
        End Set
    End Property

    Private Sub SetData(dir As IO.DirectoryInfo)
        'For Each element In System.Enum.GetValues(GetType(DataFile))
        '    Dim name As String = [Enum].GetName(GetType(DataFile), element)
        Dim items As Array = System.[Enum].GetNames(GetType(DataFile))
        For Each prop In Me.GetType().GetProperties()
            If Array.IndexOf(items, prop.Name) >= 0 Then
                Dim value As String = prop.GetValue(Me)
                If String.IsNullOrWhiteSpace(value) Then
                    Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf(GetFileName(CInt([Enum].Parse(GetType(DataFile), prop.Name))), StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
                    If result IsNot Nothing Then prop.SetValue(Me, result.FullName)
                End If
            End If
        Next
    End Sub


    'Private Sub SetDataOLD(dir As IO.DirectoryInfo)
    '    If String.IsNullOrWhiteSpace(PartsCust) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("PARTSCUST.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then PartsCust = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(PartsInv) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("PARTSINV.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then PartsInv = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(VehInv) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("VEHINV.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then VehInv = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(SerCCC) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("SERCCC.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then SerCCC = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(ServHist) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("SERVHIST.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then ServHist = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(PartsInvoice) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("PARTSINVOICE.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then PartsInvoice = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(APVendors) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("APVENDORS.csv", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then APVendors = result.FullName
    '    End If
    '    If String.IsNullOrWhiteSpace(ARCustomers) Then
    '        Dim result = dir.GetFiles.Where(Function(y) y.Name.IndexOf("ARCustomers.CSV", StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault
    '        If result IsNot Nothing Then ARCustomers = result.FullName
    '    End If

    'End Sub



    Public Sub New()
        RunCommand = New RunCommand(Me)
    End Sub

    Private Function GetFileName(value As DataFile) As String
        Select Case value
            Case DataFile.Customers
                Return "Customers.TXT"
            Case DataFile.PartsInventory
                Return "PartsInventory.TXT"
            Case DataFile.PartsInventoryCSV
                Return "PartsInventory.CSV"
            Case DataFile.PartsInvoice
                Return "PartsInvoice.CSV"
            Case DataFile.SOHeaderHist
                Return "SOHeaderHist.TXT"
            Case DataFile.SOLabourHist
                Return "SOLabourHist.TXT"
            Case DataFile.VehicleInventory
                Return "VehicleInventory.TXT"
            Case DataFile.Vehicles
                Return "Vehicles.TXT"
            Case DataFile.CustomersCSV
                Return "Customers.CSV"
            Case DataFile.SOLabourHistCSV
                Return "SOLabourHist.csv"
        End Select
    End Function

    Public Property RunCommand As ICommand

    Public Property SetFieldCommand As ICommand = New RelayCommand(Sub(x)
                                                                       Dim ofd As New Microsoft.Win32.OpenFileDialog
                                                                       ofd.Title = "Select" & x
                                                                       If Not ofd.ShowDialog Then Exit Sub
                                                                       Me.GetType.GetProperty(x).SetValue(Me, ofd.FileName)


                                                                   End Sub)





End Class
