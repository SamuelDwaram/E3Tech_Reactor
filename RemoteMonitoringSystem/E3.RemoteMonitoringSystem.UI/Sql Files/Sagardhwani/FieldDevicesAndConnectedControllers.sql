use Sagardhwani
go
  
/* Insert Field devices */
  insert into dbo.FieldDevices values('Motor_2', 'AC COMPRESSOR MOTOR NO-1', 'AC & REF COMPARTMENT-1')
  insert into dbo.FieldDevices values('Motor_1', 'AC COMPRESSOR MOTOR NO-2', 'AC & REF COMPARTMENT-1')
  insert into dbo.FieldDevices values('Motor_7', 'AC COMPRESSOR MOTOR NO-3', 'AC & REF COMPARTMENT-1')

  insert into dbo.FieldDevices values('Motor_3', 'AC SEA WATER PUMP NO-1', 'AC & REF COMPARTMENT-1')
  insert into dbo.FieldDevices values('Motor_4', 'AC SEA WATER PUMP NO-2', 'AC & REF COMPARTMENT-2')
  
  insert into dbo.FieldDevices values('Motor_5', 'AC CHILLED WATER PUMP NO-1', 'AC & REF COMPARTMENT-2')
  insert into dbo.FieldDevices values('Motor_6', 'AC CHILLED WATER PUMP NO-2', 'AC & REF COMPARTMENT-2')
  
  insert into dbo.FieldDevices values('Motor_8', 'FIRE MAIN NO-1(AC)', 'AC & REF COMPARTMENT-2')
  
  
/* Insert Field Devices and Connected Controllers */

  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_1', 'ModbusRtuController_1')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_2', 'ModbusRtuController_2')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_3')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_4')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_5')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_6')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_7', 'ModbusRtuController_7')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_8')
  
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_3', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_4', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_5', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_6', 'ModbusRtuController_22')
  insert into dbo.FieldDevicesAndConnectedControllers values('Motor_8', 'ModbusRtuController_22')

	