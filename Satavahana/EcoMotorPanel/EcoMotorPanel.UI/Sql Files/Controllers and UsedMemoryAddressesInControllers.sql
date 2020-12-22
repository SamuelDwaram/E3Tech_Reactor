use [Satavahana.EcoMotorPanel]
go

/* Insert Controllers */
insert into dbo.Controllers values('ModbusRtuController_2', 'PCB 2', 'ModbusRtu', '2', 'COM4', 100)

/* Insert Used Memory Addresses */
insert into dbo.UsedMemoryAddressesInControllers values('ModbusRtuController_2', '40000|30')


