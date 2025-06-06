﻿USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GeneCare')
DROP DATABASE GeneCare;
GO

-- Tạo database
CREATE DATABASE GeneCare;
go

USE GeneCare;
go

-- Bảng Role
CREATE TABLE Role (
	RoleID INT PRIMARY KEY,
	RoleName NVARCHAR(100)
);

-- Bảng User
CREATE TABLE Users (
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

CREATE TABLE CollectionMethod (
    MethodID INT PRIMARY KEY,
    MethodName NVARCHAR(100) -- Ví dụ: 'Tự thu tại nhà', 'Thu tại nhà bởi nhân viên', 'Thu tại cơ sở'
);

-- Bảng Booking
CREATE TABLE Booking (
	BookingID INT PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(UserID),
	DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
	ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
	MethodID INT FOREIGN KEY REFERENCES CollectionMethod(MethodID),
	AppointmentTime DATETime,
	[Status] INT,
	[Date] DATETIME
);

-- Bảng TestProcess
CREATE TABLE TestProcess (
    ProcessID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    StepName NVARCHAR(100), -- Ví dụ: "Đăng ký", "Thu mẫu", "Xét nghiệm", "Trả kết quả"
    Status NVARCHAR(50),    -- Đang chờ, Hoàn tất, ...
    Description NVARCHAR(MAX),
    UpdatedAt DATETIME
);

-- Bảng Feedback
CREATE TABLE Feedback (
	FeedbackID INT PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(UserID),
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
CREATE TABLE Samples (
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