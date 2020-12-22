use test_db
go

/* Get all available controllers */
create procedure [dbo].[GetAllControllers]
as
begin
	set nocount on;
	select * from dbo.Controllers;
end
GO

/* Get Available FieldDevices */
create procedure [dbo].[GetAvailableFieldDevices]
as
begin
	set nocount on;
	select Identifier, Label from dbo.FieldDevices;
end
GO

/* Gets the controller info from database */
create procedure [dbo].[GetControllerInfo]
@ControllerIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.Controllers where Identifier = @ControllerIdentifier;
end
GO

/* Gets the controllers connected to the field device */
create procedure [dbo].[GetControllersConnectedToFieldDevice]
@FieldDeviceIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.FieldDevicesAndConnectedControllers where FieldDeviceIdentifier = @FieldDeviceIdentifier;
end
GO

/* Gets the Field Devices List from database */
create procedure [dbo].[GetFieldDevicesList]
as
begin
	set nocount on;
	select * from dbo.FieldDevices;
end
GO

/* Gets the field points in the Sensor Data Set */
create procedure [dbo].[GetFieldPointsInSensorsDataSet]
@SensorsDataSetIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.FieldPoints where SensorDataSetIdentifier = @SensorsDataSetIdentifier;
end
GO

/* Gets the sensors data set in the field device */
create procedure [dbo].[GetSensorsDataSetInFieldDevice]
@FieldDeviceIdentifier nvarchar(max)
as
begin
	set nocount on;
	select * from dbo.SensorsDataSet where FieldDeviceIdentifier = @FieldDeviceIdentifier;
end
GO

/* Get the Used Memory Addresses of a controller */
create procedure [dbo].[GetUsedMemoryAddressesOfGivenController]
@ControllerIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.UsedMemoryAddressesInControllers where ControllerIdentifier=@ControllerIdentifier;
end
GO

/* Fetches the command points for the FieldDevice */
CREATE procedure [dbo].[SelectCommandPoints]
@FieldDeviceIdentifier nvarchar(50)
as
	begin
	set nocount on;
	select * from dbo.FieldPoints 
	where SensorDataSetIdentifier = any(select SensorDataSetIdentifier from [dbo].CommandPoints where FieldDeviceIdentifier = @FieldDeviceIdentifier)
	and Label = any(select Label from [dbo].CommandPoints where FieldDeviceIdentifier = @FieldDeviceIdentifier);
end
GO

/* Gets the Field Points for the SensorsDataSet*/
create procedure [dbo].[SelectFieldPoints]
@SensorDataSetIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from [dbo].[FieldPoints] where SensorDataSetIdentifier = @SensorDataSetIdentifier
end
go

/* Gets the SensorsDataSet for the Field Device*/
create procedure [dbo].[SelectSensorDataSet]
@FieldDeviceIdentifier nvarchar(50)
as
begin
	set nocount on;
	select * from [dbo].[SensorsDataSet] where FieldDeviceIdentifier = @FieldDeviceIdentifier
end
GO