use Kabra
go

drop procedure if exists dbo.GetRunningTime;
go
create procedure dbo.GetRunningTime
@DeviceId nvarchar(25),
@StartDate nvarchar(25),
@EndDate nvarchar(25)
as
begin
	set nocount on;
	declare @query nvarchar(max);
	create table #TempTable(RunningTime float);
	set @query = 'select count(*) from dbo.' + @DeviceId + ' where TimeStamp between '''+ @StartDate +''' and '''+ @EndDate +''' and Status = 1';
	insert into #TempTable exec sp_executesql @query;
	select top(1) RunningTime from #TempTable;
end
go