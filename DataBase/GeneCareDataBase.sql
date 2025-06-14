USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GeneCare')
    DROP DATABASE GeneCare;
GO

CREATE DATABASE GeneCare;
GO

USE GeneCare;
GO

-- Bảng Role
CREATE TABLE Role (
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(100)
);

-- Bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    RoleID INT FOREIGN KEY REFERENCES Role(RoleID),
    FullName NVARCHAR(150),
    [Address] NVARCHAR(500),
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone VARCHAR(20) NOT NULL UNIQUE,
    [Password] NVARCHAR(100)
);

-- Bảng Service
CREATE TABLE [Service] (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(200),
    ServiceType NVARCHAR(100),
    [Description] NVARCHAR(MAX)
);

-- Bảng Duration
CREATE TABLE Duration (
    DurationID INT PRIMARY KEY IDENTITY(1,1),
    DurationName NVARCHAR(100),
    [Time] TIME
);

-- Bảng ServicePrice
CREATE TABLE ServicePrice (
    PriceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
    Price INT
);

-- Bảng CollectionMethod
CREATE TABLE CollectionMethod (
    MethodID INT PRIMARY KEY IDENTITY(1,1),
    MethodName NVARCHAR(100)
);

-- Bảng Booking
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    MethodID INT FOREIGN KEY REFERENCES CollectionMethod(MethodID),
    AppointmentTime DATETIME,
    [Status] INT,
    [Date] DATETIME
);

-- Bảng TestProcess
CREATE TABLE TestProcess (
    ProcessID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    StepName NVARCHAR(100),
    Status NVARCHAR(50),
    Description NVARCHAR(MAX),
    UpdatedAt DATETIME
);

-- Bảng Feedback
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    CreatedAt DATETIME,
    Comment NVARCHAR(MAX),
    Rating INT
);

-- Bảng TestResult
CREATE TABLE TestResult (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    [Date] DATETIME,
    ResultSummary NVARCHAR(MAX)
);

-- Bảng Samples
CREATE TABLE Samples (
    SampleID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    [Date] DATETIME,
    SampleVariant NVARCHAR(200),
    CollectBy INT FOREIGN KEY REFERENCES [Users](UserID),
    DeliveryMethod NVARCHAR(100),
    Status NVARCHAR(50)
);

-- Bảng Blog
CREATE TABLE Blog (
    BlogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Title NVARCHAR(200),
    Content NVARCHAR(MAX),
    CreatedAt DATETIME
);

-- Bảng Payment
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    Amount INT,
    PaymentDate DATETIME,
    PaymentMethod NVARCHAR(50),
    Status NVARCHAR(50)
);

-- Bảng RefreshToken
CREATE TABLE RefreshToken (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Token NVARCHAR(MAX),
    JwtID NVARCHAR(255),
    IssuedAt DATETIME,
    ExpiredAt DATETIME
);

-- Bảng VerifyEmail
CREATE TABLE VerifyEmail (
    Email NVARCHAR(200) PRIMARY KEY,
    CreatedAt DATETIME,
    ExpiredAt DATETIME,
    [Key] NVARCHAR(255)
);
