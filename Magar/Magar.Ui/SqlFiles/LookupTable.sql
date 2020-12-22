use Magar
go

drop table if exists dbo.LookupTable
create table dbo.LookupTable
(
FieldPointId nvarchar(50),
Height float,
Volume float,
primary key(FieldPointId, Height, Volume)
)

INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (100, 2.535, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (400, 9.295, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (600, 14.365, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (900, 22.815, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1100, 28.73, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1300, 34.645, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1500, 40.56, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1600, 43.94, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1800, 49.855, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1900, 53.235, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2100, 59.15, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2300, 65.91, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2400, 69.29, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2500, 72.67, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2600, 76.05, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2700, 79.43, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2800, 81.965, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3000, 88.725, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3100, 92.015, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3300, 98.02, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3400, 101.4, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3600, 108.16, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3700, 111.54, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3800, 114.92, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3855, 116.118, N'Tank_25')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (0, 1, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (100, 2, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (200, 4, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (300, 6, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (400, 9, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (500, 10.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (600, 14.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (700, 17.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (800, 20, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (900, 23, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1000, 25, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1100, 29, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1200, 30.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1300, 35, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1400, 38, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1500, 41, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1600, 44, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1700, 47, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1800, 50, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1900, 53, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2000, 58, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2100, 59.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2200, 62.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2300, 65.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2400, 68.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2500, 71.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2600, 75, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2700, 78, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2800, 81, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2900, 84.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3000, 87.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3100, 90.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3200, 93.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3300, 97, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3400, 100, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3500, 103, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3600, 125.5, N'Tank_17')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (100, 2.535, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (400, 9.295, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (600, 14.365, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (900, 22.815, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1100, 28.73, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1300, 34.645, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1500, 40.56, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1600, 43.94, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1800, 49.855, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (1900, 53.235, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2100, 59.15, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2300, 65.91, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2400, 69.29, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2500, 72.67, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2600, 76.05, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2700, 79.43, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (2800, 81.965, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3000, 88.725, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3100, 92.015, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3300, 98.02, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3400, 101.4, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3600, 108.16, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3700, 111.54, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3800, 114.92, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([Height], [Volume], [FieldPointId]) VALUES (3855, 116.118, N'Tank_30')
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 100, 1.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 200, 4)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 300, 5.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 400, 6.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 500, 7.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 600, 8.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 700, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 800, 11.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 900, 13)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1000, 14.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1100, 16.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1200, 18)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1300, 19.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1400, 21.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1500, 23.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1600, 25.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1700, 27.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1800, 29.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 1900, 31)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 2000, 33)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 2100, 34.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 2200, 36.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 2300, 38.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_1', 2350, 38.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 500, 7.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 600, 8.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 700, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 800, 11.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 900, 13)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1000, 14.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1100, 16.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1200, 18)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1300, 19.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1400, 21.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1500, 23.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1600, 25.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1700, 27.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1800, 29.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 1900, 31)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 2000, 33)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 2100, 34.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 2200, 36.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 2300, 38.25)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_10', 2350, 38.75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 0, 0)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 50, 9)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 100, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 150, 11)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 200, 12.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 250, 13)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 300, 15)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 350, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 400, 17)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 450, 19)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 500, 20)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 550, 21)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 600, 22.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 650, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 700, 26)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 750, 27)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 800, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 850, 30)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 900, 31)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 950, 33)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1000, 34)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1050, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1100, 37)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1150, 39)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1200, 41)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1250, 42)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1300, 44)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1350, 45)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1400, 47)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1450, 48)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1500, 50)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1550, 51)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1600, 53)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1650, 55)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1700, 57)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1750, 58)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1800, 60)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1850, 61)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1900, 63)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 1950, 65)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2000, 67)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2050, 68)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2100, 70)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2150, 71)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2200, 73)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2250, 75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2300, 77)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2350, 78)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2400, 80)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2450, 82)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2500, 84)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2550, 85)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2600, 87)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2650, 88)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2700, 90.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2750, 92)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2800, 94)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2850, 95)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2900, 97)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 2950, 99)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 3000, 101)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_15', 3030, 102)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 0, 0)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 50, 9)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 100, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 150, 11)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 200, 12.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 250, 13)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 300, 15)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 350, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 400, 17)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 450, 19)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 500, 20)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 550, 21)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 600, 22.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 650, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 700, 26)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 750, 27)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 800, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 850, 30)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 900, 31)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 950, 33)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1000, 34)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1050, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1100, 37)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1150, 39)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1200, 41)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1250, 42)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1300, 44)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1350, 45)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1400, 47)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1450, 48)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1500, 50)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1550, 51)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1600, 53)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1650, 55)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1700, 57)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1750, 58)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1800, 60)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1850, 61)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1900, 63)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 1950, 65)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2000, 67)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2050, 68)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2100, 70)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2150, 71)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2200, 73)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2250, 75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2300, 77)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2350, 78)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2400, 80)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2450, 82)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2500, 84)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2550, 85)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2600, 87)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2650, 88)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2700, 90.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2750, 92)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2800, 94)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2850, 95)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2900, 97)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 2950, 99)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 3000, 101)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_16', 3030, 102)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 100, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 200, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 300, 40)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 400, 52)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 500, 64)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 600, 76)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 700, 92)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 800, 104)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 900, 116)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1000, 128)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1100, 144)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1200, 156)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1300, 168)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1400, 180)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1500, 192)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1600, 208)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1700, 220)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1800, 232)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 1900, 244)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2000, 256)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2100, 272)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2200, 284)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2300, 296)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2400, 308)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2500, 320)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2600, 336)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2700, 348)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2800, 360)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 2900, 372)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3000, 384)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3100, 398)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3200, 412)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3300, 422)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3400, 436)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3500, 448)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_31', 3650, 454)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 100, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 200, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 300, 40)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 400, 52)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 500, 64)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 600, 76)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 700, 92)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 800, 104)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 900, 116)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1000, 128)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1100, 144)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1200, 156)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1300, 168)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1400, 180)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1500, 192)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1600, 208)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1700, 220)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1800, 232)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 1900, 244)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2000, 256)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2100, 272)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2200, 284)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2300, 296)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2400, 308)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2500, 320)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2600, 336)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2700, 348)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2800, 360)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 2900, 372)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3000, 384)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3100, 398)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3200, 412)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3300, 422)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3400, 436)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3500, 448)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_32', 3650, 454)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 100, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 200, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 300, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 400, 44)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 500, 52)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 600, 60)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 700, 72)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 800, 80)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 900, 88)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1000, 96)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1100, 108)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1200, 116)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1300, 128)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1400, 136)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1500, 144)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1600, 152)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1700, 164)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1800, 172)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 1900, 180)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2000, 192)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2100, 200)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2200, 208)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2300, 218)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2400, 228)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2500, 236)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2600, 244)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2700, 256)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2800, 264)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 2900, 272)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3000, 280)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3100, 292)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3200, 300)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3300, 308)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3400, 320)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3500, 328)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3600, 336)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_27', 3740, 348)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 100, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 150, 14)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 200, 18)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 250, 22)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 300, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 350, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 400, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 450, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 500, 38)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 550, 42)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 600, 44)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 650, 50)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 700, 52)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 750, 56)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 800, 58)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 850, 62)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 900, 66)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 950, 70)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1000, 72)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1050, 76)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1100, 80)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1150, 84)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1200, 86)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1250, 90)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1300, 92)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1350, 96)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1400, 100)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1450, 104)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1500, 106)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1550, 110)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1600, 114)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1650, 118)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1700, 120)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1750, 124)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1800, 128)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1850, 132)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1900, 134)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 1950, 138)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2000, 142)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2050, 146)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2100, 148)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2150, 152)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2200, 154)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2250, 158)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2300, 162)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2350, 166)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2400, 168)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2450, 172)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2500, 176)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2550, 180)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2600, 182)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2650, 186)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2700, 190)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2750, 194)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2800, 196)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2850, 200)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2900, 204)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 2950, 206)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3000, 210)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3050, 214)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3100, 216)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3150, 220)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3200, 224)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3250, 228)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3300, 230)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3350, 234)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_26', 3440, 240)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 0, 1)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 100, 3)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 200, 9)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 300, 14)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 400, 18)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 500, 23)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 600, 27)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 700, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 800, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 900, 40)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1000, 45)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1100, 49)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1200, 54)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1300, 59)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1400, 64)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1500, 68)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1600, 73)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1700, 78)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1800, 83)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 1900, 88)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2000, 93)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2100, 99)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2200, 104)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2300, 109)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2400, 114)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2500, 119)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2600, 125)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2700, 130)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2800, 135)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 2900, 140)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 3000, 145)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 3100, 150)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 3200, 156)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 3300, 162)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_21', 3400, 166)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 0, 1)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 100, 3)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 200, 9)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 300, 14)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 400, 18)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 500, 23)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 600, 27)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 700, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 800, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 900, 40)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1000, 45)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1100, 49)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1200, 54)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1300, 59)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1400, 64)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1500, 68)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1600, 73)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1700, 78)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1800, 83)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 1900, 88)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2000, 93)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2100, 99)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2200, 104)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2300, 109)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2400, 114)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2500, 119)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2600, 125)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2700, 130)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2800, 135)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 2900, 140)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 3000, 145)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 3100, 150)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 3200, 156)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 3300, 162)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_22', 3400, 166)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 100, 0.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 200, 1)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 300, 2)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 400, 3)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 500, 4)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 600, 5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 700, 6)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 800, 8)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 900, 10.5)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1000, 13)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1100, 16)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1200, 20)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1300, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1400, 27)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1500, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1600, 36)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1700, 42)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1800, 48)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 1900, 54)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 2000, 61)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 2100, 66)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_11', 2200, 70)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 100, 4)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 200, 7)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 300, 10)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 400, 14)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 500, 17)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 600, 21)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 700, 24)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 800, 28)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 900, 32)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1000, 35)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1100, 39)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1200, 42)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1300, 46)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1400, 50)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1500, 53)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1600, 57)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1700, 61)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1800, 64)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 1900, 68)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2000, 72)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2100, 75)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2200, 79)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2300, 83)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2400, 86)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2500, 90)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2600, 94)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2700, 97)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2800, 101)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 2900, 105)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3000, 108)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3100, 112)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3200, 116)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3300, 119)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3400, 123)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3560, 127)
GO
INSERT [dbo].[LookupTable] ([FieldPointId], [Height], [Volume]) VALUES (N'Tank_20', 3615, 131)
GO
