select * from dbo.FieldPoints where RequireNotificationService='true' and TypeOfAddress='Failure'

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_1', 'Failure', 'R1.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_1')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_1', 'Failure', 'R1.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_1')

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_2', 'Failure', 'R2.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_2')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_2', 'Failure', 'R2.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_2')

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_3', 'Failure', 'R3.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_3')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_3', 'Failure', 'R3.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_3')

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_4', 'Failure', 'R4.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_4')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_4', 'Failure', 'R4.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_4')

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_5', 'Failure', 'R5.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_5')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_5', 'Failure', 'R5.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_5')

insert into dbo.FieldPoints values('HeatCoolFailure', 'Hc_6', 'Failure', 'R6.HeatCoolFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_6')
insert into dbo.FieldPoints values('StirrerFailure', 'Stirrer_6', 'Failure', 'R6.StirrerFailureErrorId', 'int', 'false', 'true', 'sensorDataSet_6')

select * from dbo.EquipmentErrorInfo

insert into dbo.EquipmentErrorInfo values('Hc_1', '1', 'Alert', 'Hc Error 1', 'Message for Hc Error 1', 'Help Message for Hc Error 1')
insert into dbo.EquipmentErrorInfo values('Stirrer_1', '1', 'Alert', 'Stirrer Error 1', 'Message for Stirrer Error 1', 'Help Message for Stirrer Error 1')
insert into dbo.EquipmentErrorInfo values('Hc_2', '1', 'Alert', 'Hc Error 2', 'Message for Hc Error 2', 'Help Message for Hc Error 2')
insert into dbo.EquipmentErrorInfo values('Stirrer_2', '1', 'Alert', 'Stirrer Error 2', 'Message for Stirrer Error 2', 'Help Message for Stirrer Error 2')
insert into dbo.EquipmentErrorInfo values('Hc_3', '1', 'Alert', 'Hc Error 3', 'Message for Hc Error 3', 'Help Message for Hc Error 3')
insert into dbo.EquipmentErrorInfo values('Stirrer_3', '1', 'Alert', 'Stirrer Error 3', 'Message for Stirrer Error 3', 'Help Message for Stirrer Error 3')
insert into dbo.EquipmentErrorInfo values('Hc_4', '1', 'Alert', 'Hc Error 4', 'Message for Hc Error 4', 'Help Message for Hc Error 4')
insert into dbo.EquipmentErrorInfo values('Stirrer_4', '1', 'Alert', 'Stirrer Error 4', 'Message for Stirrer Error 4', 'Help Message for Stirrer Error 4')
insert into dbo.EquipmentErrorInfo values('Hc_5', '1', 'Alert', 'Hc Error 5', 'Message for Hc Error 5', 'Help Message for Hc Error 5')
insert into dbo.EquipmentErrorInfo values('Stirrer_5', '1', 'Alert', 'Stirrer Error 5', 'Message for Stirrer Error 5', 'Help Message for Stirrer Error 5')
insert into dbo.EquipmentErrorInfo values('Hc_6', '1', 'Alert', 'Hc Error 6', 'Message for Hc Error 6', 'Help Message for Hc Error 6')
insert into dbo.EquipmentErrorInfo values('Stirrer_6', '1', 'Alert', 'Stirrer Error 6', 'Message for Stirrer Error 6', 'Help Message for Stirrer Error 6')
