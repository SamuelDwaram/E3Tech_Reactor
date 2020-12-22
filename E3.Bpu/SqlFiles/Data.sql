delete from dbo.RegisteredDevices

insert into dbo.RegisteredDevices values('192.168.0.105', 'HMI')
insert into dbo.RegisteredDevices values('192.168.0.109', 'Tablet')
insert into dbo.RegisteredDevices values('192.168.0.103', 'RemoteView');

delete from dbo.DevicesAndModulesConfiguration

insert into dbo.DevicesAndModulesConfiguration values('Tablet', 'ReactorControl')
insert into dbo.DevicesAndModulesConfiguration values('Tablet', 'OtherEquipment')
insert into dbo.DevicesAndModulesConfiguration values('Tablet', 'ChillerControl')