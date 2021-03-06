CREATE DATABASE [PartyDB]
 
USE [PartyDB]

CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[SponsorId] [int] NOT NULL,
PRIMARY KEY CLUSTERED ([Id]))

CREATE TABLE [dbo].[Participants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsAttend] [bit] NOT NULL,
	[Avatar] [nvarchar](50) NULL,
	[Reason] [nvarchar](50) NULL,
	[PartyId] [int] NOT NULL,
PRIMARY KEY CLUSTERED ([Id]))

CREATE TABLE [dbo].[Parties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED ([Id]))

CREATE TABLE [dbo].[Sponsors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Logo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED ([Id]))

CREATE TABLE [dbo].[SponsorsParties](
	[SponsorId] [int] NOT NULL,
	[ParyId] [int] NOT NULL
)

ALTER TABLE [dbo].[Contacts]  ADD CONSTRAINT [FK_Contacts_ToSponsors] FOREIGN KEY([SponsorId]) REFERENCES [dbo].[Sponsors] ([Id])
ALTER TABLE [dbo].[Participants] ADD CONSTRAINT [FK_Participants_ToParties] FOREIGN KEY([PartyId]) REFERENCES [dbo].[Parties] ([Id])
ALTER TABLE [dbo].[SponsorsParties] ADD CONSTRAINT [FK_SponsorsParties_ToParties] FOREIGN KEY([ParyId]) REFERENCES [dbo].[Parties] ([Id])
ALTER TABLE [dbo].[SponsorsParties] ADD CONSTRAINT [FK_SponsorsParties_ToSponsors] FOREIGN KEY([SponsorId]) REFERENCES [dbo].[Sponsors] ([Id])

INSERT [dbo].[Contacts] ([Id], [Email], [Phone], [SponsorId]) VALUES (N'inbox@nike.com', N'+1358718385', 1)
INSERT [dbo].[Contacts] ([Id], [Email], [Phone], [SponsorId]) VALUES (N'inbox@hollywood.com', N'+12384979804', 2)
INSERT [dbo].[Contacts] ([Id], [Email], [Phone], [SponsorId]) VALUES (N'inbox@kumunarka.com', N'+37529734859', 3)

INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Valery', 1, NULL, NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Ignat', 1, NULL, NULL, 3)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Flack', 1, NULL, NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Alena', 1, NULL, NULL, 3)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Billy', 1, NULL, NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'George', 1, N'a2344e25-ec5a-4d7e-b460-e301955851bd.png', NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Janet', 1, N'4f246159-879e-43f8-94cd-19776a873be8.png', NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Tako', 1, N'77ce429e-5906-4e57-ba91-0ee2e98d8f25.jpg', NULL, 1)
INSERT [dbo].[Participants] ([Id], [Name], [IsAttend], [Avatar], [Reason], [PartyId]) VALUES (N'Wechy', 1, N'c6e0609a-fe17-4ac8-9c2b-ff80d1a8a4ae.jpg', NULL, 1)

INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Super Party', N'Wall str 17', CAST(N'2019-10-24T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Uber Party', N'Wall str 18', CAST(N'2019-10-25T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'After Party', N'Wall str 19', CAST(N'2019-10-26T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Chill Party', N'Wall str 20', CAST(N'2019-10-27T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Great Party', N'Wall str 21', CAST(N'2019-10-28T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Magnificent Party', N'Wall str 22', CAST(N'2019-10-29T03:23:14.1644313' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Outstanding Party', N'Wall str 23', CAST(N'2019-09-23T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Superb Party', N'Wall str 24', CAST(N'2019-09-24T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Setional Party', N'Wall str 25', CAST(N'2019-09-25T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Topnotch Party', N'Wall str 26', CAST(N'2019-09-26T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Divine Party', N'Wall str 27', CAST(N'2019-09-27T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Cool Party', N'Wall str 28', CAST(N'2019-09-28T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Glorious Party', N'Wall str 29', CAST(N'2019-09-29T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Wonderful Party', N'Wall str 30', CAST(N'2019-09-30T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Peerless Party', N'Wall str 31', CAST(N'2019-10-05T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Neat Party', N'Wall str 32', CAST(N'2019-10-01T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Matchless Party', N'Wall str 33', CAST(N'2019-10-02T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Incomparable Party', N'Wall str 34', CAST(N'2019-10-03T03:23:14.1793800' AS DateTime2))
INSERT [dbo].[Parties] ([Id], [Name], [Location], [Date]) VALUES (N'Hot Party', N'Wall str 35', CAST(N'2019-10-04T03:23:14.1793800' AS DateTime2))

INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Nike', N'New York', NULL)
INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Hollywood', N'California', NULL)
INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Komunarka', N'Minsk', NULL)
INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Nike', N'New York', NULL)
INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Hollywood', N'California', NULL)
INSERT [dbo].[Sponsors] ([Id], [Name], [Location], [Logo]) VALUES (N'Komunarka', N'Minsk', NULL)

INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 1)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 2)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 3)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 4)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 5)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 6)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 7)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 8)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 9)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 10)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 11)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 12)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 13)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 14)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 15)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 16)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (2, 17)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (3, 18)
INSERT [dbo].[SponsorsParties] ([SponsorId], [ParyId]) VALUES (1, 19)


--Получение списка всех вечеринок
SELECT * from dbo.Parties

--Получение списка вечеринок, которые еще не прошли
SELECT * from dbo.Parties WHERE Date > GETDATE()

--Получение списка 5 ближайших вечеринок
SELECT TOP 5 * from dbo.Parties WHERE Date > GETDATE() ORDER BY Date Desc

--Получение списка всех проголосовавших об участии в вечеринке
SELECT * from dbo.Participants WHERE IsAttend IS NOT NULL

--Получение списка идущих на вечеринку в алфавитном порядке по имени
SELECT * from dbo.Participants WHERE IsAttend = 1 ORDER BY NAME

--Обновление адреса вечеринки
UPDATE dbo.Parties SET Location = 'Minsk' WHERE Location IS NULL

--*Получение списка организаторов вечеринок и количества организованных вечеринок
SELECT dbo.Sponsors.Name AS Sponsor, COUNT(dbo.SponsorsParties.ParyId) AS PartiesCount 
FROM dbo.SponsorsParties 
INNER JOIN dbo.Sponsors 
ON dbo.Sponsors.Id = dbo.SponsorsParties.SponsorId 
GROUP BY dbo.Sponsors.Name
