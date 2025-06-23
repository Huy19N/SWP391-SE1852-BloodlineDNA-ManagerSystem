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
	IdentifyID INT,
    [Address] NVARCHAR(500),
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone VARCHAR(20),
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

-- Bảng DeliveryMethod
CREATE TABLE DeliveryMethod (
    DeliveryMethodID INT PRIMARY KEY IDENTITY(1,1),
    DeliveryMethodName NVARCHAR(100)
);
-- Bảng Status
CREATE TABLE [Status] (
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50)
);

-- Bảng Booking
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    MethodID INT FOREIGN KEY REFERENCES CollectionMethod(MethodID),
    AppointmentTime DATETIME,
    StatusID INT FOREIGN KEY REFERENCES [Status](StatusID),
    [Date] DATETIME
);

-- Bảng TestStep
CREATE TABLE TestStep (
    StepID INT PRIMARY KEY IDENTITY(1,1),
    StepName NVARCHAR(100)
);

-- Bảng TestProcess (dùng StepID, StatusID)
CREATE TABLE TestProcess (
    ProcessID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    StepID INT FOREIGN KEY REFERENCES TestStep(StepID),
    StatusID INT FOREIGN KEY REFERENCES Status(StatusID),
    Description NVARCHAR(MAX),
    UpdatedAt DATETIME
);

-- Bảng Feedback
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID) NOT NULL,
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Comment NVARCHAR(MAX),
    Rating INT NOT NULL
);

-- Bảng TestResult
CREATE TABLE TestResult (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    [Date] DATETIME,
    ResultSummary NVARCHAR(MAX)
);

-- Bảng Patient
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY IDENTITY(1,1), 
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID) NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender NVARCHAR(10) NOT NULL, -- 'Nam', 'Nữ'
    IdentifyID NVARCHAR(50),
    SampleType NVARCHAR(200) NOT NULL, -- 'Niêm mạc miệng; Máu'...
    HasTestedDNA BIT NOT NULL,
    Relationship NVARCHAR(100) -- Quan hệ với người còn lại trong cùng booking
);

-- Bảng Samples
CREATE TABLE Samples (
    SampleID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
	PatientID INT FOREIGN KEY REFERENCES Patient(PatientID),
    [Date] DATETIME,
    SampleVariant NVARCHAR(200),
    CollectBy INT FOREIGN KEY REFERENCES Users(UserID),
    DeliveryMethodID INT FOREIGN KEY REFERENCES DeliveryMethod(DeliveryMethodID),
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
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    Amount INT,
    PaymentDate DATETIME,
    PaymentMethod NVARCHAR(50),
    Status NVARCHAR(50)
);

-- Bảng RefreshToken
CREATE TABLE RefreshToken (
    TokenID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Token NVARCHAR(500),
    JwtId NVARCHAR(255),
    IssueAt DATETIME,
    ExpiredAt DATETIME
);

-- Bảng VerifyEmail
CREATE TABLE VerifyEmail (
    Email NVARCHAR(200) PRIMARY KEY,
    CreatedAt DATETIME,
    ExpiredAt DATETIME,
    [Key] NVARCHAR(255)
);


----------------------------------------------------------------------------------------------------------
INSERT INTO Role (RoleID, RoleName) VALUES
(1, N'Customer'),
(2, N'Employee'),
(3, N'Manage'),
(4, N'Admin');
