if (exists (select * from sys.objects where name = 'get_customer_by_username'))
    drop proc get_customer_by_username
go
create proc get_customer_by_username
    @UserNameOrEmailOrPhoneNumber			nvarchar(MAX)
as
begin    
     select * from Customer where Username=@UserNameOrEmailOrPhoneNumber 
    or Email=@UserNameOrEmailOrPhoneNumber or PhoneNumber=@UserNameOrEmailOrPhoneNumber
end


if (exists (select * from sys.objects where name = 'get_customersession_last_by_customerid'))
    drop proc get_customersession_last_by_customerid
go
create proc get_customersession_last_by_customerid
    @CustomerId			int
as
begin    
     select top 1 * from CustomerSession where CustomerId=@CustomerId order by LastAccessed desc
end

if (exists (select * from sys.objects where name = 'get_shoppingcart_by_type_customersessionguid'))
    drop proc get_shoppingcart_by_cartype_customersessionguid
go
create proc get_shoppingcart_by_cartype_customersessionguid
    @ShoppingCartTypeId			int,
	@CustomerSessionGuid	    varchar(255)
as
begin    
     select * from ShoppingCartItems where ShoppingCartTypeId=@ShoppingCartTypeId and CustomerSessionGuid=@CustomerSessionGuid 
end

----ddd----
if (exists (select * from sys.objects where name = 'get_customer_by_id'))
    drop proc get_customer_by_id
go
create proc get_customer_by_id
    @Id			int
as
begin    
     select * from Customer where Id=@Id 
end

if (exists (select * from sys.objects where name = 'get_customer_by_guid'))
    drop proc get_customer_by_guid
go
create proc get_customer_by_guid
    @Guid			uniqueidentifier
as
begin    
     select * from Customer where CustomerGUID=@Guid 
end



