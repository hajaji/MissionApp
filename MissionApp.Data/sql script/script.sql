USE [MissionDB]
GO
SET IDENTITY_INSERT [dbo].[Agents] ON 

INSERT [dbo].[Agents] ([Id], [Name]) VALUES (1, N'007')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (2, N'005')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (3, N'011')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (4, N'003')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (5, N'008')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (6, N'013')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (7, N'002')
INSERT [dbo].[Agents] ([Id], [Name]) VALUES (8, N'009')
SET IDENTITY_INSERT [dbo].[Agents] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Brazil')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (2, N'Poland')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (3, N'Morocco')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Missions] ON 

INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (1, 1, 1, N'Avenida Vieira Souto 168 Ipanema, Rio de Janeiro', CAST(N'1995-12-17T21:45:17.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (2, 2, 2, N'Rynek Glowny 12, Krakow', CAST(N'2011-04-05T17:05:12.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (3, 1, 3, N'27 Derb Lferrane, Marrakech', CAST(N'2001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (4, 2, 1, N'Rua Roberto Simonsen 122, Sao Paulo', CAST(N'1986-05-05T08:40:23.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (5, 3, 2, N'swietego Tomasza 35, Krakow', CAST(N'1997-09-07T19:12:53.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (6, 4, 3, N'Rue Al-Aidi Ali Al-Maaroufi, Casablanca', CAST(N'2012-08-29T10:17:05.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (7, 5, 1, N'Rua tamoana 418, tefe', CAST(N'2005-11-10T13:25:13.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (8, 6, 2, N'Zlota 9, Lublin', CAST(N'2002-10-17T10:52:19.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (9, 7, 3, N'Riad Sultan 19, Tangier', CAST(N'2017-01-01T17:00:00.0000000' AS DateTime2))
INSERT [dbo].[Missions] ([Id], [AgentId], [CountryId], [Address], [Date]) VALUES (10, 8, 3, N'atlas marina beach, agadir', CAST(N'2016-12-01T21:21:21.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Missions] OFF
SET IDENTITY_INSERT [dbo].[AppSettings] ON 

INSERT [dbo].[AppSettings] ([Id], [Name], [Value], [Description]) VALUES (1, N'API_KEY', N'AIzaSyBEKrDwDdJm37gsY3uI7tpY1qvWn9CZPXo', N'API key for Google MAP API')
INSERT [dbo].[AppSettings] ([Id], [Name], [Value], [Description]) VALUES (2, N'GoogleMapURL', N'https://maps.googleapis.com/maps/api/geocode/json', N'Google MAP API URL')
SET IDENTITY_INSERT [dbo].[AppSettings] OFF
