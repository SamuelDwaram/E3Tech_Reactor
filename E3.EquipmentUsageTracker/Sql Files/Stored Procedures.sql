
/* Get Available Equipments */
create procedure dbo.GetAvailableEquipments
as
begin
	select distinct EquipmentIdentifier, FieldDeviceIdentifier from dbo.EquipmentUsageLog;
end
go

/* Get Equipment Occupancy For An Equipment */
create procedure dbo.GetEquipmentOccupancy
@FieldDeviceIdentifier nvarchar(50),
@EquipmentIdentifier nvarchar(50),
@StartDate nvarchar(50),
@EndDate nvarchar(50)
as
begin
	set nocount on;
	select PowerConsumption, NumberOfHoursUsed, Date from dbo.EquipmentUsageLog 
	where FieldDeviceIdentifier = @FieldDeviceIdentifier and EquipmentIdentifier = @EquipmentIdentifier and Date between @StartDate and @EndDate order by Date;
end
go

/* Get Equipment Usage Log Data */
create procedure [dbo].[GetEquipmentUsageLog]
@FieldDeviceIdentifier nvarchar(50)
as
begin
	set nocount on;
	select Name, Number, FieldDeviceIdentifier, TimeStarted, TimeCompleted from dbo.BatchTable where FieldDeviceIdentifier=@FieldDeviceIdentifier;
end
go

/* Log the Equipment Occupancy */
CREATE procedure [dbo].[LogEquipmentOccupancy]
@FieldDeviceIdentifier nvarchar(50),
@EquipmentIdentifier nvarchar(50),
@ParameterName nvarchar(max),
@ParameterValue nvarchar(10)
as
begin
	set nocount on;
	Declare @query nvarchar(max);
	Declare @previousEquipmentUsageData int;
	set @previousEquipmentUsageData = (select COUNT(*) from [dbo].EquipmentUsageLog where Date = CAST(GETDATE() as date) and FieldDeviceIdentifier = @FieldDeviceIdentifier and EquipmentIdentifier = @EquipmentIdentifier);

	if COL_LENGTH('[dbo].EquipmentUsageLog',@ParameterName) is not null
		if @previousEquipmentUsageData = 0
			set @query = 'insert into [dbo].EquipmentUsageLog(FieldDeviceIdentifier, EquipmentIdentifier, ' + @ParameterName + ', Date)  values(''' + @FieldDeviceIdentifier + ''',''' + @EquipmentIdentifier + ''',' + @ParameterValue + ', CAST(GETDATE() as date))';
		else
			set @query = 'update [dbo].EquipmentUsageLog set ' + @ParameterName + '=' + CAST(@ParameterValue as nvarchar) + 'where Date = CAST(GETDATE() as date) and FieldDeviceIdentifier = ''' + @FieldDeviceIdentifier + ''' and EquipmentIdentifier = ''' + @EquipmentIdentifier + '''';
		exec sp_executesql @query;
end
go