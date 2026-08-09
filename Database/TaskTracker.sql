CREATE DATABASE TaskTracker;
GO

USE TaskTracker;
GO

CREATE TABLE [User]
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Project
(
    ProjectId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Objetive NVARCHAR(MAX) NOT NULL,
    Team NVARCHAR(200) NOT NULL,
    EstimatedTimeOfCompletion NVARCHAR(100) NOT NULL
);

CREATE TABLE Milestone
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Status INT NOT NULL,

    CONSTRAINT FK_Milestone_Project
        FOREIGN KEY(ProjectId)
        REFERENCES Project(ProjectId)
);

CREATE TABLE Task
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    MilestoneId INT NOT NULL,

    Status INT NOT NULL,

    CreatedDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    ModifiedDate DATETIME NOT NULL,

    ResponsibleId INT NOT NULL,
	AssigneeId INT NULL,

    Priority INT NOT NULL,

    Comments NVARCHAR(MAX) NULL,

    CONSTRAINT FK_Task_Milestone
        FOREIGN KEY(MilestoneId)
        REFERENCES Milestone(Id),

	CONSTRAINT FK_Task_Responsible
		FOREIGN KEY(ResponsibleId)
		REFERENCES [User](UserId),

	CONSTRAINT FK_Task_Assignee
		FOREIGN KEY(AssigneeId)
		REFERENCES [User](UserId)
);

