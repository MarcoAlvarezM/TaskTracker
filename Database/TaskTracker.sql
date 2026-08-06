CREATE DATABASE TaskTracker;
GO

USE TaskTracker;
GO

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
    Status NVARCHAR(50) NOT NULL,

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
    Status NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    ModifiedDate DATETIME NOT NULL,
    Responsible NVARCHAR(100) NOT NULL,
    Priority NVARCHAR(50) NOT NULL,
    Comments NVARCHAR(MAX) NOT NULL,

    CONSTRAINT FK_Task_Milestone
        FOREIGN KEY(MilestoneId)
        REFERENCES Milestone(Id)
);