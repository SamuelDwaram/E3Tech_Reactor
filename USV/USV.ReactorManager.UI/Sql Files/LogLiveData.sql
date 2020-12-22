
/* Logs Live Data for the Field Device*/
create procedure [dbo].[LogLiveData]
@FieldDeviceIdentifier nvarchar(50),
@ReactorMassTemperature nvarchar(10)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'insert into dbo.' + @FieldDeviceIdentifier + ' values(' + @ReactorMassTemperature + ',GetDate())';
	exec sp_executesql @query;
end
go
