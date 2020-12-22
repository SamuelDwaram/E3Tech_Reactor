drop table if exists dbo.RegisteredDevices
go
create table dbo.RegisteredDevices(
IpAddress nvarchar(20) primary key,
DeviceType nvarchar(30) check (DeviceType in ('HMI', 'Tablet', 'RemoteView', 'Unknown'))
)

drop table if exists dbo.DevicesAndModulesConfiguration
go
create table dbo.DevicesAndModulesConfiguration(
DeviceType nvarchar(30) check (DeviceType in ('HMI', 'Tablet', 'RemoteView', 'Unknown')),
ModuleAccessible nvarchar(30),
primary key (DeviceType, ModuleAccessible)
)