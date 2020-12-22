
/* Logs Live Data for the Field Device*/
create procedure [dbo].[LogLiveData]
@FieldDeviceIdentifier nvarchar(50),
@HeatCoolSetPoint nvarchar(10),
@ReactorMassTemperature nvarchar(10),
@Pressure nvarchar(10),
@pHValue nvarchar(10),
@RPM nvarchar(10),
@JacketOutletTemperature nvarchar(10)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	set @query = 'insert into dbo.' + @FieldDeviceIdentifier + ' values(' + @ReactorMassTemperature + ',' + @Pressure + ',' + @pHValue + ',' + @RPM + ',' + @HeatCoolSetPoint + ',' + @JacketOutletTemperature + ',GetDate())';
	exec sp_executesql @query;
end
go
