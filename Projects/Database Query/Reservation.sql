--table to store train details
create table Train_Details
([Train-No] Numeric(5) primary key not null,
[Train-Name] varchar(45)not null,
Source varchar(20) not null,
Destination varchar(20) not null,
[Train-Status] varchar (20) default 'Active')


--Data into Train-Details
insert into Train_Details values
(12111,'Vande-Express','Varanasi','Pune','Active'),
(12121,'GKP-LTT-EXP','Gorakhpur','Mumbai','Active'),
(12131,'VNS-GKP-EXP','Varanasi','Gorakhpur','Active')

--table to store class details and fare
create table Class_Fare
([Serial-No] int identity,
[Train-No] numeric(5) foreign key references Train_Details([Train-No])not null,
[1-A] float not null,
[2-A] float not null,
[3-A] float not null,
SL float not null)

--Fare info into Train Class details
insert into Class_Fare values
(12111,4200,2900,1800,900),
(12121,5000,3200,2200,1100),
(12131,3200,2500,1700,800)
select * from Class_Fare
--table to store seat details of train
create table Seat_Availability
([Serial-No] int identity,
[Train-No] numeric(5) foreign key references Train_Details([Train-No]),
[1-A] int,
[2-A] int,
[3-A] int,
SL int)
--seat availability information
insert into Seat_Availability values
(12111,100,250,400,900),
(12121,100,300,500,1100),
(12131,100,350,600,1200)

--table to store user details
create table User_details
([User-id] numeric(3) primary key,
[User-Name] varchar(30),
Age int,
)
alter table user_details add Passcode varchar(20)

--table for admin details
create table Admin_Details
([Admin-id] numeric(3) primary key,
[Admin-Name] varchar(35),
passcode varchar(30) unique) 
--setting two admin for reservation system
insert into Admin_Details values
(111,'Vikash','Vikash@123'),
(121,'Admin','Admin@123')

--table to store booked ticket
create table Booked_Ticket
([PNR-No] numeric(8) primary key not null,
[User-id] numeric(3) foreign key references User_Details([User-id]),
[Train-No] numeric(5) foreign key references Train_Details([Train-No]),
[Passanger-Name] varchar(30) not null,
[Passanger-Age] int not null,
[Ticket-Class] varchar(20) not null,
TotalFare float not null,
[Booking-Date-Time] datetime not null)



--to store conceled ticket info
create table Canceled_Ticket
([Canceled-id] int Primary key,
[PNR-No] numeric(8) foreign key references Booked_Ticket([PNR-No]),
[User-id] numeric(3) foreign key references User_Details([User-id]),
[Train-No] numeric(5) foreign key references Train_Details([Train-No]),
[Cancellation-Date-Time] datetime,
[Refund-Ammount] int)


--procesor to manage seat
create or alter proc SeatManageProc( @TrainNo int, @Class varchar(10))
AS
BEGIN
 
    IF @Class = '1AC'
        UPDATE Seat_Availability
        SET [1-A] = [1-a] - 1
        WHERE [Train-No] = @TrainNo;
    ELSE IF @Class = '2AC'
        UPDATE Seat_Availability
        SET [2-A] = [2-A] - 1
        WHERE [Train-No] = @TrainNo;
    ELSE IF @Class = '3AC'
        UPDATE Seat_Availability
        SET [3-A] = [3-A] - 1
        WHERE [Train-No] = @TrainNo;
	ELSE IF @Class = 'SL'
        UPDATE Seat_Availability
        SET [SL] = [SL] - 1
        WHERE [Train-No] = @TrainNo;

END
--for adding back the seats
CREATE or alter PROCEDURE SeatManageProcCancel( @TrainNo int, @Class varchar(10),@Seat int)
AS
BEGIN
 
    IF @Class = '1AC'
        UPDATE Seat_Availability
        SET [1-A] = [1-a] + @Seat
        WHERE [Train-No] = @TrainNo;
    ELSE IF @Class = '2AC'
        UPDATE Seat_Availability
        SET [2-A] = [2-A] + @Seat
        WHERE [Train-No] = @TrainNo;
    ELSE IF @Class = '3AC'
        UPDATE Seat_Availability
        SET [3-A] = [3-A] + @Seat
        WHERE [Train-No] = @TrainNo;
	ELSE IF @Class = 'SL'
        UPDATE Seat_Availability
        SET [SL] = [SL] + @Seat
        WHERE [Train-No] = @TrainNo;

END
--cancel ticket
create or alter proc CancelTicket(@canId int,@pnrno numeric(8))
as 
begin
	declare @userid int,@trainno int,@Refund int
	set @userid=(select [user-id] from Booked_Ticket where [PNR-No]=@pnrno)
	set @trainno=(select [Train-No] from Booked_Ticket where [PNR-No]=@pnrno)
	set @Refund=(select [TotalFare] from Booked_Ticket where [PNR-No]=@pnrno)
	insert into Canceled_Ticket  values(@canId,@pnrno,@userid,@trainno,GETDATE(),@Refund-120)
end
--exec CancelTicket 111,20572 (Checking)

--adding status column to booked ticket
create or alter proc cancelBooking(@pnr int)
as 
begin
	update Booked_Ticket set [Status]='Cancelled' where [PNR-No]=@pnr
end
--alter table booked_ticket add  [Status] varchar(15) default'Confirm'

select * from Train_Details
select * from User_details
select * from Booked_Ticket
select * from Canceled_Ticket
select * from Seat_Availability

--add seats to new train
create or alter proc Add_Seat(@trno int,@FirstACSeat int,@ScdACSeat int,@ThirdACSeat int,@SLSeat int)
as 
begin
	insert into Seat_Availability values(@trno,@FirstACSeat,@ScdACSeat,@ThirdACSeat,@SLSeat)
end
--setting fair for new train
create or alter proc Add_Fare(@trno int,@FirstACFare int,@ScdACFare int,@ThirdACFare int,@SLFare int)
as 
begin
	insert into Seat_Availability values(@trno,@FirstACFare,@ScdACFare,@ThirdACFare,@SLFare)
end

---- for multiple ticket booking----
--passenger table
create table Passenger
(P_Id int primary key,
[PNR-No] numeric(8) foreign key references Booked_ticket([PNR-No]),
P_Name varchar(30),
P_Age int)

--for partial removing the passenger after cancel ticket
create or alter proc Removepassenger(@pid int)
as
	begin
	 delete from passenger where P_Id=@pid
end

--for all remove passenger
create or alter proc RemoveAllPassenger(@pnr int)
as
	begin
		delete from passenger where [PNR-No]=@pnr
	end
--drop table Passenger
--proc to add passenger details
create or alter proc AddPassenger(@pid int,@pnr int,@pname varchar(30),@Age int)
as
begin
	insert into Passenger values(@pid,@pnr,@pname,@Age)
end

select * from User_details
select * from Canceled_Ticket

--removing passenger and name and age from booked_ticket
alter table booked_ticket drop column  [passanger-age]


select * from Booked_Ticket
select * from passenger