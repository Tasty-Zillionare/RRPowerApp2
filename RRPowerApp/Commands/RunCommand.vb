Imports System.Data
Imports System.IO
Public Class RunCommand
    Inherits AsyncCommandBase

    Private _customers As String
    Private _partsinventory As String
    Private _partsinvoice As String
    Private _soheaderhist As String
    Private _solabourhist As String
    Private _vehicleinventory As String
    Private _vehicles As String
    Private _customerscsv As String
    Private _mainWindowViewModel As MainWindowViewModel


    Dim tagsCustomers As New Dictionary(Of String, String)



    Public Sub New(mainWindowViewModel As MainWindowViewModel)
        _mainWindowViewModel = mainWindowViewModel
        tagsCustomers.Add("NAD|| ", "CustomerNumber")
        tagsCustomers.Add("NAME|| ", "LastName")
        tagsCustomers.Add("ADDR1|| ", "Address")
        tagsCustomers.Add("CITY|| ", "City")
        tagsCustomers.Add("STATE|| ", "ProvinceState")
        tagsCustomers.Add("ZIP||", "PostalZip")
        tagsCustomers.Add("PHONE|| ", "HomePhone")
        tagsCustomers.Add("PO|| ", "Comments")
        tagsCustomers.Add("INTERNETADD||", "EmailAddress")
        tagsCustomers.Add("CELLPHONE|| ", "CellPhone")
        tagsCustomers.Add("BUSPHONE|| ", "BusinessPhone")
        tagsCustomers.Add("AUTHVENDOR|| ", "CriticalMemo")
        tagsCustomers.Add("CREDIT|| ", "Memo")
        tagsCustomers.Add("CREDLIM||", "CreditLimit")




    End Sub

    Private Function DFWriter(map As Dictionary(Of String, String), filePathCleaned As String)
        Dim dataTab As DataTable = New DataTable()
        Dim lines As Array = IO.File.ReadAllLines(filePathCleaned)

        For Each h As String In map.Values()
            dataTab.Columns.Add(New DataColumn(h, GetType(String)))
        Next


        Dim dataRow As DataRow = dataTab.NewRow()

        For Each line In lines
            If (line = "") Then
                If dataRow(0).ToString() <> "" Then
                    dataTab.Rows.Add(dataRow)
                End If
                dataRow = dataTab.NewRow()
                Continue For
            End If
            For Each k In map.Keys.ToList()
                If line.ToString.StartsWith(k) Then
                    If line.ToString.Split("|| ").Length > 1 Then
                        dataRow(map(k)) = line.ToString.Split("|| ")(1).Trim()
                    Else
                        dataRow(map(k)) = ""
                    End If
                    Exit For
                End If
            Next
        Next

        Return dataTab

    End Function

    Private Function FileCleaner(filepath)
        Dim NLines As List(Of String) = New List(Of String)
        Dim lines As Array = File.ReadAllLines(filepath)
        Dim lenLines As Integer = lines.Length()
        Dim i As Integer = 0
        Dim cleanedFile As String = filepath.ToString.Replace(".txt", "").Replace(".TXT", "")
        cleanedFile = cleanedFile & "Cleaned.txt"
        If IO.Directory.Exists(cleanedFile) Then
            IO.File.Delete(cleanedFile)
        End If

        While i < lenLines - 1
            i += 1
            If lines(i).ToString.StartsWith("UPSPRINT") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("KEYS SEARCHED") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("RECORDS READ =  ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("ALTERNATE RECORDS READ = ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("ALTERNATE RECORDS FOUND = ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("RECORDS SELECTED =    ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("RECORDS REJECTED =    ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("RECORD SORT SIZE =    ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("JOB ENDED WITH: END OF DATA") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("SEARCH BEGAN AT:    ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("SEARCH ENDED AT:   ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("PRINT STARTED AT:     ") Then
                Continue While
            ElseIf lines(i).ToString.StartsWith("") Then
                i += 1
                Continue While
            ElseIf lines(i).ToString.Equals("\x0c\n") Then
                i = i + 1
            Else
                NLines.Add(lines(i))
            End If
        End While

        File.WriteAllLines(cleanedFile, NLines)
        Return cleanedFile

    End Function

    Public Overrides Function ExecuteAsync(paramter As Object) As Task
        _customers = _mainWindowViewModel.Customers
        _partsinventory = _mainWindowViewModel.PartsInventory
        _partsinvoice = _mainWindowViewModel.PartsInvoice
        _soheaderhist = _mainWindowViewModel.SOHeaderHist
        _solabourhist = _mainWindowViewModel.SOLabourHist
        _vehicleinventory = _mainWindowViewModel.VehicleInventory
        _vehicles = _mainWindowViewModel.Vehicles
        _customerscsv = _mainWindowViewModel.CustomersCSV
        '_dataWorld = _mainWindowViewModel.DataWorld



        Dim CustomerCleaned As String = FileCleaner(_customers)
        Dim dt As DataTable = DFWriter(tagsCustomers, CustomerCleaned)
        Console.WriteLine("Done!")


    End Function
End Class
