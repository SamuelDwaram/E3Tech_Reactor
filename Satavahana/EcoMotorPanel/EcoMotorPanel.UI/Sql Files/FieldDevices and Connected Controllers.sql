use [Satavahana.EcoMotorPanel]
go
  
/* Insert Field devices */
  insert into dbo.FieldDevices values('Motor_2', 'Eco Motor Panel', null)
  
/* Insert Field Devices and Connected Controllers */
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_2')
  
  