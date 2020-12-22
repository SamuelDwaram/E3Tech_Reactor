use Magar
go

/* Insert Field devices */
  insert into dbo.FieldDevices values('Device_1', 'Device 1', 'Tanks')

/* Insert Field Devices and Connected Controllers */
  insert into dbo.FieldDevicesAndConnectedControllers values('Device_1', 'Slave_1')
 