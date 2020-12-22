drop procedure if exists dbo.InsertSession
go
create procedure dbo.InsertSession
@TrainerName nvarchar(50),
@TraineeName nvarchar(50),
@TraineeRegion nvarchar(50),
@DeviceId nvarchar(50),
@Remarks nvarchar(50),
@StartTimeStamp datetime
as
begin
	insert into dbo.Sessions
	values(@TrainerName, @TraineeName, @TraineeRegion, @DeviceId, @Remarks, @StartTimeStamp, null);
	select SCOPE_IDENTITY() from dbo.Sessions;
end
go

drop procedure if exists dbo.EndSession
go
create procedure dbo.EndSession
@Id int,
@EndTimeStamp datetime
as
begin
	set nocount on;
	update dbo.Sessions set EndTimeStamp = @EndTimeStamp where Id = @Id;
end
go

drop procedure if exists dbo.GetAllSessions
go
create procedure dbo.GetAllSessions
@DeviceId nvarchar(50)
as
begin
	set nocount on;
	select * from dbo.Sessions where DeviceId = @DeviceId;
end
go

drop procedure if exists dbo.GetSession
go
create procedure dbo.GetSession
@DeviceId nvarchar(50),
@SessionId int
as
begin
	set nocount on;
	select top(1) * from dbo.Sessions where DeviceId = @DeviceId and Id = @SessionId;
end
go