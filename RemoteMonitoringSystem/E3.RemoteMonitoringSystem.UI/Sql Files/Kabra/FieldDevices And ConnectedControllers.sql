use Kabra
go

/* Insert Field devices */
  insert into dbo.FieldDevices values('Motor_1', 'BILGE BALLAST PUMP NO 1', 'AER')
  insert into dbo.FieldDevices values('Motor_2', 'BILGE PUMP 1', 'AER')
  insert into dbo.FieldDevices values('Motor_3', 'AER SUPPLY FAN', 'AER')
  
  insert into dbo.FieldDevices values('Motor_4', 'FIRE MAIN NO 1', 'FER')
  insert into dbo.FieldDevices values('Motor_5', 'BILGE PUMP 2', 'FER')
  insert into dbo.FieldDevices values('Motor_6', 'FER SUPPLY FAN', 'FER')
 
  insert into dbo.FieldDevices values('Motor_7', 'AC PLANT NO 1', 'AMR-1')
  insert into dbo.FieldDevices values('Motor_8', 'AC PLANT NO 2', 'AMR-1')
  insert into dbo.FieldDevices values('Motor_9', 'AC PLANT NO 3', 'AMR-1')
  insert into dbo.FieldDevices values('Motor_10', 'AC CHILLED WATER PUMP NO 1', 'AMR-1')

/* Insert Field Devices and Connected Controllers */
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_2')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_4')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_5')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_6')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_7', 'ModbusRtuController_7')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_8')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_9', 'ModbusRtuController_9')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_10', 'ModbusRtuController_10')
  
   insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_7', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_9', 'ModbusRtuController_23')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_10', 'ModbusRtuController_23')
  