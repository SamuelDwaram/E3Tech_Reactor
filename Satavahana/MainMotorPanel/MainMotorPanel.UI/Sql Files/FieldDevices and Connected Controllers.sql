use [Satavahana.MainMotorPanel]
go
  
/* Insert Field devices */
  insert into dbo.FieldDevices values('Motor_1', 'Main Motor Panel', null)
  
/* Insert Field Devices and Connected Controllers */
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_1')
  
  