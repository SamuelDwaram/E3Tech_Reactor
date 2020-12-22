drop procedure if exists dbo.LogFieldDeviceImage
go
/* Logs the FieldDevice Image */
create procedure [dbo].LogFieldDeviceImage
@FieldDeviceIdentifier nvarchar(50),
@ImageData varbinary(max),
@ParametersData varbinary(max) = null
as
begin
	set nocount on;
	insert into dbo.FieldDeviceImages values(@FieldDeviceIdentifier, @ImageData, @ParametersData, CURRENT_TIMESTAMP);
end
go

drop procedure if exists dbo.GetFieldDeviceImages
go
/* Gets the Field Device Images */
create procedure dbo.GetFieldDeviceImages
@FieldDeviceIdentifier nvarchar(50),
@StartTime nvarchar(25),
@EndTime nvarchar(25)
as
begin
	set nocount on;
	select * from [dbo].FieldDeviceImages where FieldDeviceIdentifier = @FieldDeviceIdentifier and TimeStamp between @StartTime and @EndTime order by TimeStamp;
end
go