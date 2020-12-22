drop table if exists dbo.FieldDeviceImages
go
/* FieldDevice Images along with parameters */
create table dbo.FieldDeviceImages
(
FieldDeviceIdentifier nvarchar(MAX),
ImageData varbinary(MAX),
ParametersData varbinary(MAX),
TimeStamp datetime,
)
