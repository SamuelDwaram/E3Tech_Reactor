create table dbo.Sessions
(
Id int identity primary key,
TrainerName nvarchar(50) not null,
TraineeName nvarchar(50) not null,
TraineeRegion nvarchar(50) not null,
DeviceId nvarchar(50) not null foreign key references dbo.FieldDevices(Identifier),
Remarks nvarchar(50),
StartTimeStamp datetime,
EndTimeStamp datetime
)
