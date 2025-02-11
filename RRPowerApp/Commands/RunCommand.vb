﻿Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
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
    Private _dataWorld As String


    Dim tagsCustomers As New Dictionary(Of String, String) From
           {{"NAD|| ", "CustomerNumber"} _
        , {"NAME|| ", "LastName"} _
        , {"ADDR1|| ", "Address"} _
        , {"CITY|| ", "City"} _
        , {"STATE|| ", "ProvinceState"} _
        , {"ZIP||", "PostalZip"} _
        , {"PHONE|| ", "HomePhone"} _
        , {"PO|| ", "Comments"} _
        , {"INTERNETADD||", "EmailAddress"} _
        , {"CELLPHONE|| ", "CellPhone"} _
        , {"BUSPHONE|| ", "BusinessPhone"} _
        , {"AUTHVENDOR|| ", "CriticalMemo"} _
        , {"CREDIT|| ", "Memo"} _
        , {"CREDLIM||", "CreditLimit"}}



    Dim tagsPartsInventory As New Dictionary(Of String, String) From
         {{"PART|| ", "PartsNumber"} _
        , {"DESC|| ", "PartDescription"} _
        , {"BIN|| ", "Bin1"} _
        , {"LIST|| ", "ListPrice"} _
        , {"DLRNET|| ", "CostPrice"} _
        , {"EXCHANGE|| ", "TradePrice"} _
        , {"ONHAND|| ", "QuantityOnHand"} _
        , {"LASTCHGDATE|| ", "LastTransactionDate"} _
        , {"DATELASTSAL|| ", "LastPurchaseDate"} _
        , {"STATUS|| ", "Status"} _
        , {"ALTPART|| ", "AlternatePart"} _
        , {"VEND|| ", "Manufacturer"} _
        , {"SOURCE|| ", "Source"} _
        , {"GROUP|| ", "PGroup"} _
        , {"MAX|| ", "MaxQuantity"} _
        , {"MIN|| ", "MinQuantity"} _
        , {"PREVYRSALES|| ", "Comments"}}

    Dim tagsSOPartHist As New Dictionary(Of String, String) From
           {{"Part", "PartNumber"} _
        , {"Description", "PartDescription"} _
        , {"Qty", "QuantitySold"} _
        , {"Cost", "CostPrice"} _
        , {"List", "ListPrice"} _
        , {"RO#", "SONumber"} _
        , {"Line", "RequestLine"} _
        , {"Slsp", "CSR"} _
        , {"PayType", "PayType"}}

    Dim tagsPartsInvoice As New Dictionary(Of String, String) From
                {{"NAD#", "CustomerNumber"} _
                , {"Name", "CustomerLastName"} _
                , {"Part", "PartNumber"} _
                , {"Description", "PartDescription"} _
                , {"Qty", "QuantitySold"} _
                , {"Cost", "CostPrice"} _
                , {"List", "ListPrice"} _
                , {"Invoice#", "InvoiceNumber"} _
                , {"Slsp", "CSR"} _
                , {"PayType", "PayType"} _
                , {"InvoicedDate", "DateOpened"}}

    Dim tagsSOHeaderHist As New Dictionary(Of String, String) From
    {{"RONUMBER|| ", "SONumber"} _
        , {"NAD|| ", "CustomerNumber"} _
        , {"VIN|| ", "VIN"} _
        , {"OPENED|| ", "DateIn"} _
        , {"MILEAGE|| ", "OdometerIn"} _
        , {"MILEAGEOUT||", "OdometerOut"} _
        , {"FINALAMT1|| ", "CustomerTotal"} _
        , {"DEDUCTAMT1|| ", "SubbletTotal"} _
        , {"NAME|| ", "CustomerLastName"} _
        , {"NOTE|| ", "Comments"}}



    Dim tagsSOLabourHist As New Dictionary(Of String, String) From
     {{"RO-NUMBER|| ", "SONumber"} _
        , {"LINE-NUMBER|| ", "RequestLine"} _
        , {"DLROPCODE01|| ", "OpCode"} _
        , {"CONCERN-1|| ", "Cause"} _
        , {"CONCERN-2|| ", "Complaint"}}

    Dim tagsVehicleInventory As New Dictionary(Of String, String) From
                {{"VIN|| ", "VIN"} _
                 , {"STOCKNO|| ", "StockNumber"} _
                 , {"MAKE|| ", "Make"} _
                 , {"MODEL|| ", "Model"} _
                 , {"MODELNO|| ", "ModelNumber"} _
                 , {"BODYTYPE||", "Body"} _
                 , {"COLORCODE|| ", "ExteriorColor"} _
                 , {"KEY1|| ", "DoorKeyCode"} _
                 , {"KEY2|| ", "IgnitionKeyCode"} _
                 , {"RECEIVEDATE|| ", "ReceivedDate"} _
                 , {"INSVCDATE||", "InServiceDate"} _
                 , {"CYLINDERS||", "Cylinders"} _
                 , {"ENGINE||", "Engine"} _
                 , {"TRANS||", "Transmission"} _
                 , {"LIST||", "RetailPrice"} _
                 , {"NEWUSEDFLAG|| ", "Status"} _
                 , {"ORIGCOST||", "CostPrice"} _
                 , {"TRIM||", "Trim"} _
                 , {"YEAR|| ", "Year"} _
                 , {"SOLD-TO||", "Owner"} _
                 , {"INT-PRICE||", "InternetPrice"}}

    Dim tagsVehicles As New Dictionary(Of String, String) From
                  {{"VIN|| ", "VIN"} _
                 , {"NEWUSEDGLAG|| ", "Status"} _
                 , {"STOCKNO|| ", "StockNumber"} _
                 , {"MAKE|| ", "Make"} _
                 , {"MODEL|| ", "Model"} _
                 , {"TRIM||", "Trim"} _
                 , {"YEAR|| ", "Year"} _
                 , {"MODELNO|| ", "ModelNumber"} _
                 , {"LICENSE|| ", "LicenseNumber"} _
                 , {"COLORCODE|| ", "ExteriorColor"} _
                 , {"LASTSVCDATE|| ", "LastServiceDate"} _
                 , {"ODOMETERDEL||", "Odometer"} _
                 , {"PREV-OWNER||", "OriginalOwner"} _
                 , {"ENGINE||", "Engine"} _
                 , {"BODYTYPE||", "Body"}}





    Dim _connectionString = "Data Source=localhost;Initial Catalog=DataWorldBlank;ENCRYPT=no;Trusted_Connection=true;User Id=pbsuser; Password=pbs8805;"

    Private Sub ConnectionStringUpdater(dw As String)
        _connectionString = "Data Source=localhost;Initial Catalog=" & dw & ";ENCRYPT=no;Trusted_Connection=true;User Id=pbsuser; Password=pbs8805;"

    End Sub

    Public Sub New(mainWindowViewModel As MainWindowViewModel)
        _mainWindowViewModel = mainWindowViewModel














    End Sub

    Private Sub LoadData(dt As DataTable, connect_string As String, tableName As String, mapping As Dictionary(Of String, String))
        Dim bads As String() = {}

        Using sourceConnection = New SqlConnection(connect_string)
            Try
                sourceConnection.Open()
            Catch ex As Exception

            End Try

            Using bulkCopy As System.Data.SqlClient.SqlBulkCopy = New SqlBulkCopy(sourceConnection)
                bulkCopy.DestinationTableName = "dbo." & tableName
                For Each map In mapping
                    Dim destination As String
                    If bads.Contains(map.Value.ToLower()) Then
                        destination = "[" & map.Value & "]"
                    Else
                        destination = map.Value
                    End If
                    bulkCopy.ColumnMappings.Add(map.Value, destination)
                Next
                Try
                    bulkCopy.WriteToServer(dt)
                    Console.WriteLine("fish")
                Catch ex As Exception

                End Try


            End Using
        End Using
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

    Private Function ParseLine(line As String) As String()
        Dim result = line.Split(Chr(34))
        result = result.Select(Function(x, i) If(i Mod 2 = 0, x, x.Replace(",", "[comma]").Replace(vbCrLf, "[newline]").Replace(vbCr, "[newline]").Replace(vbLf, "[newline]"))).ToArray
        line = String.Join(Chr(34), result)
        line = line.Replace(Chr(34) & Chr(34), "[Quote]")
        line = line.Replace(Chr(34), "")
        line = line.Replace("[Quote]", Chr(34))
        Dim items = line.Split(",")
        For i = 0 To items.Count - 1
            If items(i) = Chr(34) Then items(i) = ""
        Next
        Return items.Select(Function(x) x.Replace("[comma]", ",").Replace("[newline]", vbCrLf).Trim).ToArray
    End Function

    Public Function ConvertCSVtoDataTable(ByVal strFilePath As String) As DataTable
        Dim dt As DataTable = New DataTable()

        Using sReader As StreamReader = New StreamReader(strFilePath)

            Dim headers As String() = sReader.ReadLine().Split(","c)

            For Each header As String In headers

                header = header.Replace("""", String.Empty)
                header = header.Replace(" ", String.Empty)
                dt.Columns.Add(New DataColumn(header.ToString, GetType(String)))

            Next
            While Not sReader.EndOfStream
                Dim line As String = sReader.ReadLine()
                While line.Where(Function(x) x = Chr(34)).Count Mod 2 = 1
                    line &= vbCrLf & sReader.ReadLine
                End While
                Dim row As String() = ParseLine(line)
                Dim dr As DataRow = dt.NewRow()
                For i As Integer = 0 To Math.Min(dt.Columns.Count - 2, row.Length - 1)
                    row(i) = row(i).Replace("""", String.Empty).Trim
                    dr(i) = row(i)





                Next

                dt.Rows.Add(dr)

            End While

        End Using

        Return dt

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

    Private Async Function SendCommand(Query As String, conn_string As String) As Task(Of Integer)
        Using connection = New SqlClient.SqlConnection(conn_string)
            connection.Open()
            Dim command = New SqlClient.SqlCommand(Query, connection)
            command.CommandTimeout = 0
            Return Await command.ExecuteNonQueryAsync()
        End Using
    End Function


    Public Overrides Async Function ExecuteAsync(paramter As Object) As Task
        _customers = _mainWindowViewModel.Customers
        _partsinventory = _mainWindowViewModel.PartsInventory
        _partsinvoice = _mainWindowViewModel.PartsInvoice
        _soheaderhist = _mainWindowViewModel.SOHeaderHist
        _solabourhist = _mainWindowViewModel.SOLabourHist
        _vehicleinventory = _mainWindowViewModel.VehicleInventory
        _vehicles = _mainWindowViewModel.Vehicles
        _customerscsv = _mainWindowViewModel.CustomersCSV
        _dataWorld = _mainWindowViewModel.DataWorld
        ConnectionStringUpdater(_dataWorld)

        Dim PartsCleaned As String
        Await Task.Run(Sub() PartsCleaned = FileCleaner(_partsinventory))
        _mainWindowViewModel.Status = "Parsing Parts Inventory"
        Dim writingParts As DataTable = DFWriter(tagsPartsInventory, PartsCleaned)
        _mainWindowViewModel.Status = "Loading Parts Inventory"
        Await Task.Run(Sub() LoadData(writingParts, _connectionString, "PartsInventory", tagsPartsInventory))

        Dim CustomerCleaned As String
        _mainWindowViewModel.Status = "Parsing Customers"
        Await Task.Run(Sub() CustomerCleaned = FileCleaner(_customers))
        Dim dt As DataTable = DFWriter(tagsCustomers, CustomerCleaned)
        _mainWindowViewModel.Status = "Loading Customers"
        Await Task.Run(Sub() LoadData(dt, _connectionString, "Customers", tagsCustomers))

        Dim VehicleInventoryCleaned As String
        _mainWindowViewModel.Status = "Parsing VehicleInventory"
        Await Task.Run(Sub() VehicleInventoryCleaned = FileCleaner(_vehicleinventory))
        Dim writingeVehicleInventory As DataTable = DFWriter(tagsVehicleInventory, VehicleInventoryCleaned)
        _mainWindowViewModel.Status = "Loading VehicleInventory"
        Await Task.Run(Sub() LoadData(writingeVehicleInventory, _connectionString, "VehicleInventory", tagsVehicleInventory))

        Dim VehiclesCleaned As String
        _mainWindowViewModel.Status = "Parsing Vehicles"
        Await Task.Run(Sub() VehiclesCleaned = FileCleaner(_vehicles))
        Dim WritingVehicles As DataTable = DFWriter(tagsVehicles, VehiclesCleaned)
        _mainWindowViewModel.Status = "Loading Vehicles"
        Await Task.Run(Sub() LoadData(WritingVehicles, _connectionString, "Vehicles", tagsVehicles))

        Dim SOHeaderCleaned As String
        _mainWindowViewModel.Status = "Parsing SOHeaderHist"
        Await Task.Run(Sub() SOHeaderCleaned = FileCleaner(_soheaderhist))
        Dim WritingSOHeaderHist As DataTable = DFWriter(tagsSOHeaderHist, SOHeaderCleaned)
        _mainWindowViewModel.Status = "Loading SOHeaderHist"
        Await Task.Run(Sub() LoadData(WritingSOHeaderHist, _connectionString, "SOHeaderHist", tagsSOHeaderHist))



        'SOPartInvoice 'TODO
        Dim WritingPartsInvoice As DataTable
        _mainWindowViewModel.Status = "Parsing PartsInvoice"
        Await Task.Run(Sub() WritingPartsInvoice = ConvertCSVtoDataTable(_partsinvoice))
        Dim SOPartHistDWRaw As DataTable = New DataTable()
        Dim partsInvoiceDWRaw As DataTable = New DataTable()
        Dim qry = From dr As DataRow In WritingPartsInvoice.AsEnumerable()
                  Where Not dr.Field(Of String)("RO#").Equals("")
                  Select dr
        SOPartHistDWRaw = qry.CopyToDataTable()
        'Dim SOPartHistDWRaw2 As DataTable = SOPartHistDWRaw.Copy()

        Dim cols As DataColumn() = New DataColumn(SOPartHistDWRaw.Columns.Count - 1) {}

        SOPartHistDWRaw.Columns.CopyTo(cols, 0)
        For Each col In cols
            If Not tagsSOPartHist.Keys.Contains(col.ToString()) Then
                SOPartHistDWRaw.Columns.Remove(col)
            Else
                SOPartHistDWRaw.Columns(col.ToString()).ColumnName = tagsSOPartHist(col.ToString())
            End If
        Next
        Dim qry2 = From dr2 As DataRow In WritingPartsInvoice.AsEnumerable()
                   Where Not dr2.Field(Of String)("Invoice#").Equals("")
                   Select dr2
        partsInvoiceDWRaw = qry2.CopyToDataTable()
        'Dim partsInvoiceDWRaw2 As DataTable = partsInvoiceDWRaw.Copy()
        Dim cols2 As DataColumn() = New DataColumn(partsInvoiceDWRaw.Columns.Count - 1) {}
        partsInvoiceDWRaw.Columns.CopyTo(cols2, 0)
        For Each col In cols2
            If Not tagsPartsInvoice.Keys.Contains(col.ToString()) Then
                partsInvoiceDWRaw.Columns.Remove(col)
            Else
                partsInvoiceDWRaw.Columns(col.ToString()).ColumnName = tagsPartsInvoice(col.ToString())
            End If
        Next




        _mainWindowViewModel.Status = "Loading SOPartHist"

        Await Task.Run(Sub() LoadData(SOPartHistDWRaw, _connectionString, "SOPartHist", tagsSOPartHist))
        _mainWindowViewModel.Status = "Loading PartsInvoice"
        Await Task.Run(Sub() LoadData(partsInvoiceDWRaw, _connectionString, "PartsInvoice", tagsPartsInvoice))


        Dim SOLabourHistCleaned As String
        _mainWindowViewModel.Status = "Parsing SOLabourHist"
        Await Task.Run(Sub() SOLabourHistCleaned = FileCleaner(_solabourhist))
        Dim WritingSOLabourHist As DataTable = DFWriter(tagsSOLabourHist, SOLabourHistCleaned)
        _mainWindowViewModel.Status = "Loading SOLabourHist"
        Await Task.Run(Sub() LoadData(WritingSOLabourHist, _connectionString, "SORequestHist", tagsSOLabourHist))

        _mainWindowViewModel.Status = "Sending CleanUp Queries"

        Await SendCommand(Queries.Query, _connectionString)


        _mainWindowViewModel.Status = "Conversion Finished!"

















        'Console.WriteLine("done!")






    End Function
End Class
