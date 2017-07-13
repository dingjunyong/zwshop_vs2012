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



-------
if (exists (select * from sys.objects where name = 'customersession_get_by_guid'))
    drop proc customersession_get_by_guid
go
create proc customersession_get_by_guid
    @CustomerSessionGuid			uniqueidentifier
as
begin    
     select * from CustomerSession where CustomerSessionGuid=@CustomerSessionGuid 
end

if (exists (select * from sys.objects where name = 'customersession_get_last_by_customerId'))
    drop proc customersession_get_last_by_customerId
go
create proc customersession_get_last_by_customerId
    @CustomerId			int
as
begin    
     select top 1 * from CustomerSession where CustomerId=@CustomerId order by LastAccessed desc 
end

if (exists (select * from sys.objects where name = 'customersession_delete_by_guid'))
    drop proc customersession_delete_by_guid
go
create proc customersession_delete_by_guid
    @CustomerSessionGuid			uniqueidentifier
as
begin    
     delete from CustomerSession where CustomerSessionGuid=@CustomerSessionGuid
end


if (exists (select * from sys.objects where name = 'customersession_get_all'))
    drop proc customersession_get_all
go
create proc customersession_get_all
as
begin    
     select * from CustomerSession

end



if (exists (select * from sys.objects where name = 'customersession_get_with_nonempty_shoppingcart'))
    drop proc customersession_get_with_nonempty_shoppingcart
go
create proc customersession_get_with_nonempty_shoppingcart
as
begin    
     select * from CustomerSession where CustomerSessionGuid in (select CustomerSessionGuid from ShoppingCartItems where TypeId=1)
end


if (exists (select * from sys.objects where name = 'customersession_delete_expire'))
    drop proc customersession_delete_expire
go
create proc customersession_delete_expire
	@OlderThan datetime
as
begin    
     delete from CustomerSession where CustomerSessionGuid not in (select CustomerSessionGuid from ShoppingCartItems) and LastAccessed < @OlderThan
end

---------------------------
if (exists (select * from sys.objects where name = 'customerattribute_delete_by_id'))
    drop proc customerattribute_delete_by_id
go
create proc customerattribute_delete_by_id
    @Id			int
as
begin    
    delete from CustomerAttribute where Id=@Id

end

if (exists (select * from sys.objects where name = 'customerattribute_get_by_id'))
    drop proc customerattribute_get_by_id
go
create proc customerattribute_get_by_id
    @Id			int
as
begin    
    select * from CustomerAttribute where Id=@Id

end

if (exists (select * from sys.objects where name = 'customerattribute_get_by_customerId'))
    drop proc customerattribute_get_by_customerId
go
create proc customerattribute_get_by_customerId
    @CustomerId			int
as
begin    
    select * from CustomerAttribute where CustomerId=@CustomerId

end

if (exists (select * from sys.objects where name = 'customerattribute_add_by_id'))
    drop proc customerattribute_add_by_id
go
create proc customerattribute_add_by_id
    @CustomerId			int,
	@Key                varchar(255),
	@Value              varchar(max)  
as
begin    
    insert into CustomerAttribut values(@CustomerId,@Key,@Value)
end

if (exists (select * from sys.objects where name = 'customerattribute_update_by_id'))
    drop proc customerattribute_update_by_id
go
create proc customerattribute_update_by_id
	@Id					int,
    @CustomerId			int,
	@Key                varchar(255),
	@Value              varchar(max)  
as
begin    
    update  CustomerAttribut set  CustomerId=@CustomerId,[Key]=@Key,Value=@Value where Id=@Id
end

---------------------------

if (exists (select * from sys.objects where name = 'address_delete_by_id'))
    drop proc address_delete_by_id
go
create proc address_delete_by_id
    @Id			int
as
begin    
    delete from [Address] where Id=@Id

end

if (exists (select * from sys.objects where name = 'address_get_by_id'))
    drop proc address_get_by_id
go
create proc address_get_by_id
    @Id			int
as
begin    
    select * from [Address] where Id=@Id

end


if (exists (select * from sys.objects where name = 'address_get_by_customerid'))
    drop proc address_get_by_customerid
go
create proc address_get_by_customerid
    @CustomerId 		int
as
begin    
    select * from [Address] where CustomerId=@CustomerId

end

if (exists (select * from sys.objects where name = 'address_add'))
    drop proc address_add
go
create proc address_add
	@CustomerId			int,
	@Name				varchar(255),
	@PhoneNumber		varchar(255),
	@Address1			varchar(255),
	@Address2			varchar(255),
	@City				varchar(255),
	@StateProvinceId	int,
	@ZipPostalCode		varchar(255),
	@CreatedOn			datetime,
	@UpdatedOn			datetime
as
begin    
    insert into [Address](CustomerId,Name,PhoneNumber,Address1,Address2,City,StateProvinceId,ZipPostalCode,CreatedOn,UpdatedOn)
	values(@CustomerId,@Name,@PhoneNumber,@Address1,@Address2,@City,@StateProvinceId,@ZipPostalCode,@CreatedOn,@UpdatedOn)
	
end

if (exists (select * from sys.objects where name = 'address_update'))
    drop proc address_update
go
create proc address_update
    @Id 				int,
	@CustomerId			int,
	@Name				varchar(255),
	@PhoneNumber		varchar(255),
	@Address1			varchar(255),
	@Address2			varchar(255),
	@City				varchar(255),
	@StateProvinceId	int,
	@ZipPostalCode		varchar(255),
	@CreatedOn			datetime,
	@UpdatedOn			datetime
as
begin    
    update [Address] set CustomerId=@CustomerId,Name=@Name,PhoneNumber=@PhoneNumber,
	Address1=@Address1,Address2=@Address2,City=@City,StateProvinceId=@StateProvinceId,ZipPostalCode=@ZipPostalCode,
	CreatedOn=@CreatedOn,UpdatedOn=@UpdatedOn where Id=@Id
end





if (exists (select * from sys.objects where name = 'customersession_add'))
    drop proc customersession_add
go
create proc customersession_add
    @CustomerSessionGuid  uniqueidentifier,
	@CustomerId			int,
	@LastAccessed				datetime,
	@IsExpired		bit
as
begin    
    insert into [CustomerSession](CustomerSessionGuid,CustomerId,LastAccessed,IsExpired) 
	values(@CustomerSessionGuid,@CustomerId,@LastAccessed,@IsExpired)
end

