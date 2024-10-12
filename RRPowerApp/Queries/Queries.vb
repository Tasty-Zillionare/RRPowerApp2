Public Class Queries


    '     with q as (select ROW_NUMBER() over(partition by vin order by datein desc) rn , * from soheaderhist where Customerlastname <> '' or customernumber <> '') update a set owner = q.customernumber from Vehicles a inner join q on a.VIN = q.VIN where q.rn = 1 and a.Owner = '' and q.customernumber <> '';
    '   with q as (select ROW_NUMBER() over(partition by vin order by datein desc) rn , * from soheaderhist where customerlastName <> '' or customernumber <> '') update a set LastNameOwner = q.CustomerLastName  from Vehicles a inner join q on a.VIN = q.VIN where q.rn = 1 and a.LastNameOwner = '' and q.CustomerLastName <> '';
    Public Shared Property Query As String = "update Vehicles set LastNameOwner = OriginalOwner where LastNameOwner = '' and OriginalOwner <> '';
        with q as (select ROW_NUMBER() over (partition by sonumber order by requestline ) rn , * from SORequestHist) update q set requestline = rn;
        insert into SOLabourHist (SONumber, RequestLine, OpCode,originalsonumber) select distinct SONumber, RequestLine,OpCode, OriginalSONumber    from SORequestHist;
        update SORequestHist set Complaint = CONCAT(cause, complaint) , Cause = '';
        insert into SOHeaderHist (SONumber) select distinct a.sonumber from SORequestHist a left join SOHeaderHist b on a.SONumber = b.SONumber where b.SONumber is null;
        update a set requestline = b.RequestLine from SOPartHist a inner join SORequestHist b on a.SONumber = b.SONumber and b.OriginalSONumber = a.RequestLine;
        insert into ARCustomers (ARCustomerNumber,LastName,FirstName,MiddleName,Address,City,ProvinceState,PostalZip,Country,HomePhone,BusinessPhone,CellPhone,EmailAddress,CreditLimit) select  CustomerNumber,LastName, FirstName, MiddleName, Address , City ,ProvinceState , PostalZip, Country, HomePhone, BusinessPhone, CellPhone, EmailAddress ,CreditLimit from Customers where memo = '1';
        insert into APVendors (APVendorNumber, LastName ,FirstName, middlename, address, city, ProvinceState, PostalZip, Country, HomePhone, BusinessPhone, CellPhone,EmailAddress) select CustomerNumber, LastName , FirstName , MiddleName , Address, City, ProvinceState, PostalZip ,Country, HomePhone , BusinessPhone, cellphone,	EmailAddress from Customers where CriticalMemo = 'Y';
        update Customers set customernumber = trim(customernumber);
        update PartsInvoice set customernumber = trim(customernumber);
        update a set a.CustomerLastName = b.LastName, a.CustomerFirstName = b.FirstName, a.CustomerHomePhone = b.HomePhone, a.CustomerBusinessPhone = BusinessPhone, a.CustomerAddress = b.Address, a.CustomerCity = b.City, a.CustomerPostalZip = b.PostalZip from PartsInvoice a inner join Customers b on trim(a.CustomerNumber) = trim(b.CustomerNumber);
        update Customers set Memo = '', CriticalMemo = '';
        delete from VehicleInventory where Owner <> '';
        with q as (select row_number () over(partition by vin  order by (select null) ) rn , * from vehicleinventory) delete from q where rn > 1;
        with q as (select row_number () over(partition by vin  order by (select null) ) rn , * from Vehicles) delete from q where rn > 1;
        update SOPartHist set CostPrice = '' where TRY_CAST(CostPrice as money ) is null;
        update SOPartHist set ListPrice = '' where TRY_CAST(ListPrice as money ) is null;
        update SOHeaderHist set CustomerNumber = replace(CHECKSUM(CustomerLastName),'-','M') where CustomerNumber = '' and CustomerLastName <> '';
        update a set PayType = b.PayType from SOLabourHist a  inner join soparthist b on a.SONumber = b.SONumber and a.RequestLine = b.RequestLine;
        update a set PayType = b.PayType from SORequestHist a  inner join SOLabourHist b on a.SONumber = b.SONumber and a.RequestLine = b.RequestLine;
        update a set paytype = b.paytype from SOHeaderHist a inner join SORequestHist b on a.SONumber = b.SONumber;"
End Class
