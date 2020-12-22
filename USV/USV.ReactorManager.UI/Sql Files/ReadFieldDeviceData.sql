
/* Reads the field device data*/
create procedure [dbo].[ReadFieldDeviceData]
@FieldDeviceIdentifier nvarchar(50),
@ReactorMassTemperature nvarchar(10),
@startTime nvarchar(25),
@endTime nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'select ';
	if @ReactorMassTemperature is not null
		set @query = @query + 'ReactorMassTemperature,';
	set @query = @query + 'TimeStamp from dbo.' + @FieldDeviceIdentifier + ' where TimeStamp between '''+ @startTime+''' and '''+ @endTime +''' order by TimeStamp';
	exec sp_executesql @query;
end
go
