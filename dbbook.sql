create database dbbook;
go
use dbbook;
go

create table tbl_role(
role_id int identity primary key,
role_name nvarchar(20),
role_description nvarchar(100)
);
go

create table tbl_account(
acc_id int identity primary key,
acc_username nvarchar(20) not null unique,
acc_password nvarchar(200) not null,
acc_role_fk int references tbl_role(role_id)
);
go

create table tbl_customer(
cus_id int identity primary key,
cus_name nvarchar(50) not null,
cus_phone nvarchar(12) not null,
cus_address nvarchar(200) not null,
cus_acc_fk int references tbl_account(acc_id));
go

create table tbl_avtofcus(
avtofcus_id int primary key,
avtofcus_img image,
constraint avtofcusFK foreign key (avtofcus_id) references tbl_customer(cus_id) 
);
go

create table tbl_author(
au_id int identity primary key,
au_name nvarchar(50),
au_description nvarchar(max));
go

create table tbl_publisher(
pu_id int identity primary key,
pu_name nvarchar(50),
pu_description nvarchar(max));
go

create table tbl_category(
cate_id int identity primary key,
cate_name nvarchar(50));
go

create table tbl_book(
book_id int identity primary key,
book_name nvarchar(200),
book_description nvarchar(max),
book_price int,
book_fk_auid int references tbl_author(au_id),
book_fk_puid int references tbl_publisher(pu_id),
book_fk_cateid int references tbl_category(cate_id));
go

create table tbl_avtofbook(
avtofbook_id int primary key,
avtofbook_img image,
constraint avtofbookFK foreign key (avtofbook_id) references tbl_book(book_id) 
);
go

create table tbl_cart(
cart_fk_cusid int references tbl_customer(cus_id),
cart_fk_bookid int references tbl_book(book_id),
cart_book_amount int,
constraint cart_PK primary key(cart_fk_cusid, cart_fk_bookid));
go

create table tbl_status(
stt_id int identity primary key,
stt_name nvarchar(50)
);
go

create table tbl_order(
order_fk_cusid int references tbl_customer(cus_id),
order_fk_bookid int references tbl_book(book_id),
order_time datetime,
amount int,
order_stt_fk int references tbl_status(stt_id),
constraint order_PK primary key(order_fk_cusid, order_fk_bookid, order_time));
go

--add default acc
insert into tbl_role values ('Admin','chu cua hang');
insert into tbl_role values ('Shipper','nguoi van chuyen');
insert into tbl_role values ('User','nguoi dung');
go

insert into tbl_account values ('admin','gFuYE2Bpl7A=',1)
insert into tbl_account values ('shipper','gFuYE2Bpl7A=',2)
insert into tbl_account values ('user','gFuYE2Bpl7A=',3)
go

insert into tbl_customer values ('Nghia Dang','0364956694', 'Thu Duc, TP.HCM', 3)
insert into tbl_customer values ('Tuan Nhan', '0547327432', 'Thu Duc, TP.HCM', 2)
go


--
alter table tbl_book add book_img nvarchar(max);
alter table tbl_book add book_quantity int;

--
--
--
