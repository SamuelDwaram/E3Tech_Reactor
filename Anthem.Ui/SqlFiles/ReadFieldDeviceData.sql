
/* Reads the field device data*/
alter procedure [dbo].[ReadFieldDeviceData]
@FieldDeviceIdentifier nvarchar(50),
@ReactorMassTemperature nvarchar(10) = null,
@Pressure nvarchar(10) = null,
@PHvalue nvarchar(10) = null,
@RPM nvarchar(10) = null,
@JacketOutletTemperature nvarchar(10) = null,
@startTime nvarchar(25),
@endTime nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'select ';
	if @ReactorMassTemperature is not null
		set @query = @query + 'ReactorMassTemperature as [Mass Temp(°C)],';
	if @Pressure is not null
		set @query = @query + 'Pressure as [Pressure(bar)],';
	if @PHvalue is not null
		set @query = @query + 'PHvalue as [pH],';
	if @RPM is not null
		set @query = @query + 'RPM,';
	if @JacketOutletTemperature is not null
		set @query = @query + 'JacketOutletTemperature as [Jacket Inlet(°C)],';
	set @query = @query + 'TimeStamp as [Time Stamp] from dbo.' + @FieldDeviceIdentifier + ' where TimeStamp between '''+ @startTime+''' and '''+ @endTime +''' order by TimeStamp';
	exec sp_executesql @query;
end
go
