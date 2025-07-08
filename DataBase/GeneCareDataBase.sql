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

-- Bảng Samples
CREATE TABLE Samples (
    SampleID INT PRIMARY KEY IDENTITY(1,1),
	SampleName NVARCHAR(200)
);

-- Bảng Patient
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY IDENTITY(1,1), 
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID) NOT NULL,
	SampleID INT FOREIGN KEY REFERENCES Samples(SampleID),
    FullName NVARCHAR(200) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender NVARCHAR(10) NOT NULL, -- 'Nam', 'Nữ'
    IdentifyID NVARCHAR(50),
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
	PaymentMethodId BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	MethodName NVARCHAR(10) NOT NULL,
	[Description] NVARCHAR(500),
	EndpointURL VARCHAR(250) NOT NULL,
	IconURL VARCHAR(255) NOT NULL
);

-- Bảng KeyVersion
CREATE TABLE KeyVersion(
	KeyVersionId BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PaymentMethodId BIGINT FOREIGN KEY REFERENCES PaymentMethod(PaymentMethodId) NOT NULL,
    [Version] NVARCHAR(20) NOT NULL,
    HashSecret NVARCHAR(MAX) NOT NULL,
    TmnCode NVARCHAR(50) NOT NULL,
    CreatedAt DateTime NOT NULL,
    ExpiredAt DateTime,
    IsActive BIT NOT NULL
);

-- Bảng Payment
CREATE TABLE Payment (
    PaymentId BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	BookingId INT FOREIGN KEY REFERENCES Booking(BookingID) NOT NULL,
	KeyVersionId BIGINT FOREIGN KEY REFERENCES KeyVersion(KeyVersionId) NOT NULL,
	TransactionId NVARCHAR(255) NOT NULL,
	Amount DECIMAL NOT NULL,
	Currency NVARCHAR(50) NOT NULL,
	PaymentDate DATETIME NOT NULL,
	BankCode NVARCHAR(50),
	OrderInfo NVARCHAR(256) NOT NULL,
	ResponseCode NVARCHAR(20),
	SecureHash NVARCHAR(MAX) NOT NULL,
	RawData NVARCHAR(MAX) NOT NULL,
	HavePaid BIT NOT NULL
);

-- Bảng PaymentIPNLog
CREATE TABLE PaymentIPNLog(
	IPNLogId BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PaymentId BIGINT FOREIGN KEY REFERENCES Payment(PaymentId) NOT NULL,
	RawData NVARCHAR(MAX) NOT NULL,
	ReceivedAt DateTime NOT NULL,
	[Status] NVARCHAR(50) NOT NULL
);

-- Bảng PaymentReturnLog
CREATE TABLE PaymentReturnLog(
	ReturnLogId	 BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PaymentId BIGINT FOREIGN KEY REFERENCES Payment(PaymentId) NOT NULL,
	RawData NVARCHAR(MAX) NOT NULL,
	ReturnedAt DateTime NOT NULL,
	[Status] NVARCHAR(50) NOT NULL
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


-------------------------------------------INSERT DATA---------------------------------------------------------------
INSERT INTO Role (RoleID, RoleName) VALUES
(1, N'Customer'),
(2, N'Employee'),
(3, N'Manage'),
(4, N'Admin');
go
INSERT INTO Users (RoleID,FullName,IdentifyID,Address,Email,Phone,Password)
VALUES 
(1, N'ThuanCustomer','090909',N'HCM',N't@cus','0909090','123'),
(2, N'ThuanStaff','090909',N'HCM',N't@sta','0909090','123'),
(3, N'ThuanManager','090909',N'HCM',N't@mana','0909090','123'),
(4, N'ThuanAdmin','090909',N'HCM',N't@ad','0909090','123');
go
INSERT INTO Service (ServiceName ,ServiceType)
VALUES 
(N'dân sự', N'Cha/Mẹ-Con'),
(N'dân sự', N'Anh/Chị-Em'),
(N'dân sự', N'song sinh'),
(N'dân sự', N'Cô/Chú-Cháu'),
(N'dân sự', N'Dì/Cậu-Cháu'),
(N'dân sự', N'Ông/Bà-Cháu'),

(N'pháp lý', N'Cha/Mẹ-Con'),
(N'pháp lý', N'Anh/Chị-Em'),
(N'pháp lý', N'song sinh'),
(N'pháp lý', N'Cô/Chú-Cháu'),
(N'pháp lý', N'Dì/Cậu-Cháu'),
(N'pháp lý', N'Ông/Bà-Cháu');
go
INSERT INTO Duration(DurationName )
VALUES
(N'gói 6h'),
(N'gói 24h'),
(N'gói 48h');
go
INSERT INTO ServicePrice(ServiceID,DurationID,Price)
VALUES
(1,1,N'2500000'),
(1,2,N'2000000'),
(1,3,N'1500000'),
(7,1,N'3500000'),
(7,2,N'3000000'),
(7,3,N'2500000');
go
INSERT INTO CollectionMethod (MethodName)
VALUES
(N'tự thu mẫu tại nhà'),
(N'thu mẫu tại nhà'),
(N'thu mẫu tại cơ sở y tế');
go
INSERT INTO Status(StatusName)
VALUES
(N'chờ xác nhận'),
(N'đã thu mẫu'),
(N'đang thực hiện'),
(N'hoàn thành');
go
INSERT INTO Booking(UserID,DurationID,ServiceID,MethodID,AppointmentTime,StatusID,Date)
VALUES
(1,1,1,1,'2025-07-02 13:36:47.930',1,'2025-07-02 13:36:47.930');
go
INSERT INTO Samples(SampleName)
 VALUES
 (N'máu'),
 (N'Móng tay/chân'),
 (N'Tóc'),
 (N'Niêm mạc miệng');
go

-- Bảng PaymentMethod
INSERT INTO PaymentMethod(MethodName,[Description],EndpointURL,IconURL)
VALUES
(N'VNPAY',NULL, N'https://sandbox.vnpayment.vn/paymentv2/vpcpay.html', N'https://cdn.brandfetch.io/idV02t6WJs/w/820/h/249/theme/dark/logo.png?c=1bxid64Mup7aczewSAYMX&t=1750645747861');
GO

INSERT INTO KeyVersion(PaymentMethodId,[Version],HashSecret,TmnCode,CreatedAt,ExpiredAt,IsActive)
VALUES
(1,N'v1',N'VWGQTRVC3W1V305M2TG3VA740H658WNP',N'NDNNTOY0', '2025-7-4',NULL,1);
GO
