
/* Equipment Log */
create table [dbo].[EquipmentUsageLog]
(
FieldDeviceIdentifier nvarchar(50) foreign key references [dbo].FieldDevices(Identifier),
EquipmentIdentifier nvarchar(50),
NumberOfHoursUsed float,
PowerConsumption float,
Date date
)
