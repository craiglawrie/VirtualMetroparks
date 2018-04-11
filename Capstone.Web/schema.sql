CREATE DATABASE ParkInfo;

GO

USE ParkInfo;

GO


CREATE TABLE [dbo].[Users]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(MAX) NOT NULL, 
    [PasswordHash] VARCHAR(MAX) NULL, 
    [SecurityStamp] VARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[UserRoles]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Role] VARCHAR(100) NOT NULL,

	CONSTRAINT pk_UserRoles PRIMARY KEY (UserId, Role),
	CONSTRAINT fk_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE [Parks]
(
[ParkId] integer identity(1,1) NOT NULL, 
[ParkName]  VARCHAR(MAX) NOT NULL, 
[ParkDescription]  VARCHAR(MAX) NOT NULL,
[ParkLat] FLOAT NOT NULL,
[ParkLong] FLOAT NOT NULL,
[defaultZoom] int NOT NULL,
CONSTRAINT pk_ParkId PRIMARY KEY (ParkId)
);

CREATE TABLE [Trails]
(
[TrailId] integer identity NOT NULL,
[ParkId] integer NOT NULL, 
[TrailName]  VARCHAR(MAX) NOT NULL, 
[TrailDescription]  VARCHAR(MAX) NOT NULL,
CONSTRAINT pk_TrailId PRIMARY KEY (TrailId),
CONSTRAINT fk_ParkId foreign key (ParkId) REFERENCES Parks(ParkId)
);
 