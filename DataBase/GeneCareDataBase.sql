-- Tạo database
CREATE DATABASE GeneCare;

USE GeneCare;

-- Bảng Role
CREATE TABLE Role (
	RoleID INT PRIMARY KEY,
	RoleName NVARCHAR(100)
);

-- Bảng User
CREATE TABLE [User] (
	UserID INT PRIMARY KEY,
	RoleID INT FOREIGN KEY REFERENCES Role(RoleID),
	FullName NVARCHAR(150),
	[Address] NVARCHAR(500),
	Email NVARCHAR(200),
	Phone VARCHAR(20),
	[Password] NVARCHAR(100)
);

-- Bảng Service
CREATE TABLE [Service] (
	ServiceID INT PRIMARY KEY,
	ServiceName NVARCHAR(200),
	ServiceType NVARCHAR(100),
	[Description] NVARCHAR(MAX)
);

-- Bảng Duration
CREATE TABLE Duration (
	DurationID INT PRIMARY KEY,
	DurationName NVARCHAR(100),
	[Time] TIME
);

-- Bảng ServicePrice
CREATE TABLE ServicePrice (
	PriceID INT PRIMARY KEY,
	ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
	DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
	Price INT
);

-- Bảng Booking
CREATE TABLE Booking (
	BookingID INT PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
	DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
	ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
	[Status] INT,
	Method NVARCHAR(150),
	[Date] DATETIME
);

-- Bảng Feedback
CREATE TABLE Feedback (
	FeedbackID INT PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
	ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
	CreatedAt DATETIME,
	Comment NVARCHAR(MAX),
	Rating INT
);

-- Bảng TestResult
CREATE TABLE TestResult (
	ResultID INT PRIMARY KEY,
	BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
	[Date] DATETIME,
	ResultSummary NVARCHAR(MAX)
);

-- Bảng Sample
CREATE TABLE [Sample] (
	SampleID INT PRIMARY KEY,
	BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
	[Date] DATETIME,
	SampleVariant NVARCHAR(200),
	CollectBy NVARCHAR(100),
	DeliveryMethod NVARCHAR(100),
	Status NVARCHAR(50)
);

-- Bảng Blog
CREATE TABLE Blog (
	BlogID INT PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES [User](UserID),
	Title NVARCHAR(200),
	Content NVARCHAR(MAX),
	CreatedAt DATETIME
);
