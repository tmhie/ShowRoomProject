CREATE TABLE Admin(
	AdminId INT PRIMARY KEY IDENTITY(1,1),
	AdminName nvarchar(50),
	Email nvarchar(50),
	Password nvarchar(50),
	Status int
);

ALTER TABLE Sale
ADD CONSTRAINT FK_Sale_Admin
FOREIGN KEY (AdminId) REFERENCES Admin(AdminId);

ALTER TABLE Vehicle
ADD CONSTRAINT FK_Vehicle_Admin
FOREIGN KEY (CreateBy) REFERENCES Admin(AdminId);

ALTER TABLE ShowRoom
ADD CONSTRAINT FK_ShowRoom_Admin
FOREIGN KEY (AdminId) REFERENCES Admin(AdminId);

CREATE TABLE Vehicle(
	VehicleId INT PRIMARY KEY IDENTITY(1,1),
	VehicleName nvarchar(50),
	VehicleType nvarchar(50),
	Description text,
	Price float,
	CreateDate DATETIME,
	CreateBy int,
	Status int
);

ALTER TABLE Sale
ADD CONSTRAINT FK_Vehicle_Sale
FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId);

ALTER TABLE VehicleImg
ADD CONSTRAINT FK_VehicleImg_Vehicle
FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId);

ALTER TABLE ShowRoom
ADD CONSTRAINT FK_ShowRoom_Vehicle
FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId);


CREATE TABLE VehicleImg(
	ImgId INT PRIMARY KEY IDENTITY(1,1),
	VehicleId int,
	ImgPath nvarchar(200),
);

CREATE TABLE Sale(
	SaleId int PRIMARY KEY IDENTITY(1,1),
	VehicleId int,
	RoomName nvarchar(50),
	CustomerId int,
	AdminId int,
	Price float,
	OrderDate datetime,
	DaliveryDate datetime,
	Status int
);

CREATE TABLE Customer(
	CustomerId int PRIMARY KEY IDENTITY(1,1),
	CustomerName nvarchar(50),
	Email nvarchar(50),
	Password nvarchar(50),
	Address nvarchar(50),
	Gender nvarchar(20),
	Status int
);

ALTER TABLE Sale
ADD CONSTRAINT FK_Sale_Customer
FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId);

ALTER TABLE ShowRoom
ADD CONSTRAINT FK_ShowRoom_Customer
FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId);


CREATE TABLE ShowRoom(
	RoomId int Primary Key identity(1,1),
	RoomAddress nvarchar(50),
	RoomName nvarchar(50),
	AdminId int,
	CustomerId int,
	VehicleId int,
	Status int
);


Insert into Admin(AdminName,Email,Password,Status) values 
('admin1','admin1@gmail.com','12345',1);

Insert into Vehicle(VehicleName,VehicleType,Description,Price,CreateDate,CreateBy,Status) values 
('BMW I8','BMW','Xe hang sang',2000,'10/10/2021',1,1);

Insert into VehicleImg(VehicleId,ImgPath) values 
(1,'O to BMW');

Insert into Sale(VehicleId,RoomName,CustomerId,AdminId,Price,OrderDate,DaliveryDate,Status) values 
(1,'C2009I Showroom',1,1,2000,'2022-10-10','2022-11-11',1);

Insert into Customer(CustomerName,Email,Password,Address,Gender,Status) values
('Tran Minh Hieu','tmhieu73@gmail.com','10032002','Ha Noi','Nam',1);

Insert into ShowRoom(RoomAddress,RoomName,AdminId,CustomerId,VehicleId,Status) values
('54 Le Thanh Nghi','C2009I Showroom','1',1,1,1);
