use Magar
go

/* Insert Controllers */
insert into dbo.Controllers values('Slave_1', 'Slave 1', 'ModbusRtu', '1', 'COM2', 1000)

/* Insert Used Memory Addresses */
insert into dbo.UsedMemoryAddressesInControllers values('Slave_1', '40000|40')
