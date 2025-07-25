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
    RoleID INT NOT NULL FOREIGN KEY REFERENCES Role(RoleID),
    FullName NVARCHAR(150),
	IdentifyID VARCHAR(13),
    [Address] NVARCHAR(500),
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone VARCHAR(12),
    [Password] NVARCHAR(100),
	LastPwdChange DATETIME NOT NULL DEFAULT GETDATE()
);

-- Bảng RefreshToken
CREATE TABLE RefreshToken (
    RefreshTokenId INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
    Token NVARCHAR(500) UNIQUE,
    JwtId NVARCHAR(100) UNIQUE,
    CreatedAt DATETIME NOT NULL,
    ExpiredAt DATETIME NOT NULL,
	Revoked BIT NOT NULL DEFAULT 0,
	IPAddress NVARCHAR(255),
	UserAgent NVARCHAR(MAX)
);

--Bảng LogLogin
CREATE TABLE LogLogin(
	LogId INT PRIMARY KEY IDENTITY(1,1),
	UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
	RefreshTokenId INT NULL FOREIGN KEY REFERENCES RefreshToken(RefreshTokenId),
	Success BIT NOT NULL DEFAULT 0,
	FailReason NVARCHAR(255),
	IPAddress NVARCHAR(255),
	UserAgent NVARCHAR(MAX),
	LoginTime DATETIME NOT NULL DEFAULT GETDATE()
);

--Bảng AccessTokenBlacklist
CREATE TABLE AccessTokenBlacklist(
	JwtId VARCHAR(50) PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
	ExpireAt DATETIME NOT NULL,
	Reason NVARCHAR(500)
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
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- Bảng ServicePrice
CREATE TABLE ServicePrice (
    PriceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
    Price MONEY NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- Bảng CollectionMethod
CREATE TABLE CollectionMethod (
    MethodID INT PRIMARY KEY IDENTITY(1,1),
    MethodName NVARCHAR(100)
);


-- Bảng Status
CREATE TABLE [Status] (
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50)
);

-- Bảng TestResult
CREATE TABLE TestResult (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    [Date] DATETIME,
    ResultSummary NVARCHAR(MAX)
);

-- Bảng Booking
CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DurationID INT FOREIGN KEY REFERENCES Duration(DurationID),
    ServiceID INT FOREIGN KEY REFERENCES [Service](ServiceID),
    MethodID INT FOREIGN KEY REFERENCES CollectionMethod(MethodID),
	ResultID INT FOREIGN KEY REFERENCES TestResult(ResultID),
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
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID) ON DELETE CASCADE,
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

-- Bảng Samples
CREATE TABLE Samples (
    SampleID INT PRIMARY KEY IDENTITY(1,1),
	SampleName NVARCHAR(200)
);

-- Bảng Patient
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY IDENTITY(1,1), 
    BookingID  INT  NOT NULL FOREIGN KEY REFERENCES Booking(BookingID) ON DELETE CASCADE,
	SampleID INT FOREIGN KEY REFERENCES Samples(SampleID),
    FullName NVARCHAR(200) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender NVARCHAR(10) NOT NULL, -- 'Nam', 'Nữ'
    IdentifyID VARCHAR(13),
    HasTestedDNA BIT NOT NULL,
    Relationship NVARCHAR(100) -- Quan hệ với người còn lại trong cùng booking
);

-- Bảng Blog
CREATE TABLE Blog (
    BlogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Title NVARCHAR(200),
    Content NVARCHAR(MAX),
    CreatedAt DATETIME
);

--Bảng PaymentMethod
CREATE TABLE PaymentMethod(
	PaymentMethodId BIGINT PRIMARY KEY NOT NULL,
	MethodName NVARCHAR(10) NOT NULL,
	IconURL VARCHAR(MAX) NOT NULL
);

-- Bảng Payment
CREATE TABLE Payment (
    PaymentId VARCHAR(200) PRIMARY KEY NOT NULL,
	BookingId INT NULL FOREIGN KEY REFERENCES Booking(BookingID) ON DELETE SET NULL,
	PaymentMethodId BIGINT FOREIGN KEY REFERENCES PaymentMethod(PaymentMethodId) NOT NULL,
	TransactionStatus NVARCHAR(50),
	ResponseCode NVARCHAR(50),
	TransactionNo NVARCHAR(255),
	BankTranNo NVARCHAR(50),
	Amount DECIMAL NOT NULL,
	Currency NVARCHAR(50) NOT NULL,
	PaymentDate DATETIME NOT NULL,
	OrderInfo NVARCHAR(256),
	SecureHash NVARCHAR(MAX),
	RawData NVARCHAR(MAX),
	HavePaid BIT NOT NULL
);

-- Bảng PaymentIPNLog
CREATE TABLE PaymentIPNLog(
	IPNLogId BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PaymentId VARCHAR(200) FOREIGN KEY REFERENCES Payment(PaymentId) NOT NULL,
	RawData NVARCHAR(MAX) NOT NULL,
	ReceivedAt DateTime NOT NULL,
	TransactionStatus NVARCHAR(50) NOT NULL,
	ResponseCode NVARCHAR(50) NOT NULL
);

-- Bảng PaymentReturnLog
CREATE TABLE PaymentReturnLog(
	ReturnLogId	 BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PaymentId VARCHAR(200) FOREIGN KEY REFERENCES Payment(PaymentId) NOT NULL,
	RawData NVARCHAR(MAX) NOT NULL,
	ReturnedAt DateTime NOT NULL,
	TransactionStatus NVARCHAR(50) NOT NULL,
	ResponseCode NVARCHAR(50) NOT NULL
);

-- Bảng VerifyEmail
CREATE TABLE VerifyEmail (
	[Key] NVARCHAR(255) NOT NULL PRIMARY KEY,
    Email NVARCHAR(200) NOT NULL,
	IsResetPwd BIT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ExpiredAt DATETIME NOT NULL,
    
);


-------------------------------------------INSERT DATA---------------------------------------------------------------
INSERT INTO Role (RoleID, RoleName) VALUES
(1, N'Customer'),
(2, N'Staff'),
(3, N'Manage'),
(4, N'Admin');
go
INSERT INTO Users (RoleID,FullName,IdentifyID,Address,Email,Phone,Password, LastPwdChange)
VALUES 
(1, N'ThuanCustomer','097209090921',N'HCM',N't@cus','0909508280','123', DATEADD(day, -7, GETDATE())),
(2, N'ThuanStaff','09720909092',N'HCM',N't@sta','0909508280','123', DATEADD(day, -7, GETDATE())),
(3, N'ThuanManager','09720909092',N'HCM',N't@mana','0909508280','123', DATEADD(day, -7, GETDATE())),
(4, N'ThuanAdmin','09720909092',N'HCM',N't@ad','0909508280','123', DATEADD(day, -7, GETDATE()));
go
INSERT INTO Service (ServiceName ,ServiceType)
VALUES 
(N'Dân sự', N'Cha-Con'),
(N'Dân sự', N'Mẹ-Con'),
(N'Dân sự', N'Anh-Em'),
(N'Dân sự', N'Chị-Em'),
(N'Dân sự', N'song sinh'),
(N'Dân sự', N'Chú/Cậu-Cháu'),
(N'Dân sự', N'Cô/Dì-Cháu'),
(N'Dân sự', N'Ông-Cháu'),
(N'Dân sự', N'Bà-Cháu'),

(N'Pháp lý', N'Cha-Con'),
(N'Pháp lý', N'Mẹ-Con'),
(N'Pháp lý', N'Anh-Em'),
(N'Pháp lý', N'Chị-Em'),
(N'Pháp lý', N'song sinh'),
(N'Pháp lý', N'Chú/Cậu-Cháu'),
(N'Pháp lý', N'Cô/Dì-Cháu'),
(N'Pháp lý', N'Ông-Cháu'),
(N'Pháp lý', N'Bà-Cháu'),
(N'Pháp lý', N'Truy vết tội phạm');
go
INSERT INTO Duration(DurationName )
VALUES
(N'Gói 6h'),
(N'Gói 24h'),
(N'Gói 48h');
go
INSERT INTO ServicePrice(ServiceID,DurationID,Price)
VALUES
(1,1,2500000),(1,2,2000000),(1,3,1500000),
(2,1,2500000),(2,2,2000000),(2,3,1500000),
(3,1,2500000),(3,2,2000000),(3,3,1500000),
(4,1,2500000),(4,2,2000000),(4,3,1500000),
(5,1,2500000),(5,2,2000000),(5,3,1500000),
(6,1,2500000),(6,2,2000000),(6,3,1500000),
(6,1,2500000),(6,2,2000000),(6,3,1500000),
(7,1,2500000),(7,2,2000000),(7,3,1500000),
(8,1,2500000),(8,2,2000000),(8,3,1500000),
(9,1,2500000),(9,2,2000000),(9,3,1500000),

(10,1,3500000),(10,2,3000000),(10,3,2500000),
(11,1,3500000),(11,2,3000000),(11,3,2500000),
(12,1,3500000),(12,2,3000000),(12,3,2500000),
(13,1,3500000),(13,2,3000000),(13,3,2500000),
(14,1,3500000),(14,2,3000000),(14,3,2500000),
(15,1,3500000),(15,2,3000000),(15,3,2500000),
(16,1,3500000),(16,2,3000000),(16,3,2500000),
(17,1,3500000),(17,2,3000000),(17,3,2500000),
(18,1,3500000),(18,2,3000000),(18,3,2500000),
(19,1,3500000),(19,2,3000000),(19,3,2500000);
go
INSERT INTO CollectionMethod (MethodName)
VALUES
(N'Tự thu mẫu tại nhà'),
(N'Thu mẫu tại nhà'),
(N'Thu mẫu tại cơ sở y tế');
go
INSERT INTO Status(StatusName)
VALUES
(N'Chưa thanh toán'),
(N'Chưa thực hiện'),
(N'Đang thực hiện'),
(N'Hoàn thành'),
(N'Đã hủy');
go
INSERT INTO Samples(SampleName)
 VALUES
 (N'Máu'),
 (N'Móng tay/chân'),
 (N'Tóc'),
 (N'Niêm mạc miệng');
go
INSERT INTO TestStep(StepName)
  VALUES
  (N'Đang gửi bộ kit'),
  (N'Đã thu mẫu '),
  (N'Đang xét nghiệm'),
  (N'Trả kết quả xét nghiệm');
go
INSERT INTO PaymentMethod(PaymentMethodId,MethodName, IconURL)
VALUES
(1, N'VNPAY', 'https://cdn.brandfetch.io/idV02t6WJs/theme/dark/logo.svg?c=1dxbfHSJFAPEGdCLU4o5B'),
(2, N'MOMO', 'https://developers.momo.vn/v3/img/logo.svg');
go