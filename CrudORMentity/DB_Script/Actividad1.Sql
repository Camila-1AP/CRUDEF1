create database Actividad1;
go

use Actividad1;
go

create table Productos (
	ProductoID int primary key identity(1,1) not null,
	NombreProducto nvarchar(100) not null,
	Descripcion nvarchar(255) not null,
	Precio decimal(10,2) not null,
	Stock int not null default 0,
	CategoriaID int foreign key references Categoria(CategoriaID) not null
);

create table Categorias (
	CategoriaID int primary key identity(1,1) not null,
	NombreCategoria nvarchar(50) not null

);

create table Clientes (
	ClienteID int primary key identity(1,1) not null,
	NombreCompleto nvarchar(150) not null,
	CarreoElectronico nvarchar(100) not null,
	Telefono nvarchar(15) not null,
	Direccion nvarchar(255) not null
);

create table Facturas (
	FacturaID int primary key identity(1,1) not null,
	ClienteID int foreign key references Clientes(ClienteID) not null,
	FechaFactura date not null,
	Total decimal(10,2) not null
);

create table DetallesFactura (
	DetalleID int primary key identity(1,1) not null,
	FacturaID int foreign key references Facturas(FacturaID) not null,
	ProductoID int foreign key references Productos(ProductoID) not null,
	Cantidad int not null,
	Precio decimal(10,2) not null,
	Impuesto decimal(10,2) not null,
	Subtotal decimal(10,2) not null
	);

	create table Proveedores (
		ProveedorID int primary key identity(1,1) not null,
		NombreProveedor nvarchar(100) not null,
		Telefono nvarchar(15) not null,
		CorreoElectronico nvarchar(100) not null
	);

	create table Compras (
		CompraID int primary key identity(1,1) not null,
		ProveedorID int foreign key references Proveedores(ProveedorID) not null,
		FechaCompra date not null,
		Total decimal(10,2) not null
	);

	create table DetallesCompra(
		DetalleCompraID int primary key identity(1,1) not null,
		CompraID int foreign key references Compras(CompraID) not null,
		ProductoID int foreign key references Productos(ProductoID) not null,
		Cantidad int not null,
		CostoUnitario decimal(10,2) not null,
		Impuesto decimal(10,2) not null,
		Subtotal decimal(10,2) not null
	);

	insert into dbo.Categorias (NombreCategoria)
	values ('Electrónica', 'Alimentos', 'Ropa', 'Libros');

	insert into Productos (NombreProducto, Descripcion, Precio, Stock, CategoriaID)
	values
	('Laptop XPS 16 DELL', 'Portatil potente de 16.3 pulgadas 4k OLED pantalla táctil 16-Cores Ultra 9, 32 GB RAM y 1TB SSD.', 185787.00, 3, 1),
	('Spacetop Bundle', 'Gafas XREAL Air 2 Ultra + 1 año de software Spacetop.', 56637.00, 2, 1 ),
	('Audífonos Intraurales', 'Auriculares Boytond inalámbricos Bluetooth, auriculares eronómicos con clip, conducción ósea sobre la oreja con diseño impermeable, color morado.', 3038, 10, 1 ),

	('Ketchup Heinz 397 g', 'Perfecto para sándwiches o como salsa para untar.', 230.00, 20, 2),
	('Sparkling Ice', 'Agua gasificada de Kiwi y Fresa.', 100.00,5,2),


	('Camisa Lino', 'Camisa confeccioanda en hitatura de lino 100%. Cuello solapa y manga larga acabada en puño. Cierre frontal con botones.', 2995.00, 8, 2),
	('Jeans 2W collection wide leg tiro alto', 'Confeccionado en hilatura de lino 100%.', 2995.00, 4, 2),


	('Si lo crees, lo creas', 'Si lo crees, lo creas, está lleno de consejos reveladores del autor Brian Tracy.', 990.00, 12, 4),
	('Hábitos Átomicos', 'Un discurso que se apoya en bases científicas, manual de instrucciones para implantar cambios a nuestro favor.', 742.00, 5, 4),
	('Bill Gates Los Negocios en la Era Digital', 'En este revelador libro se explica cómo un sistema nervioso digital.', 617.00, 1, 4);

	insert into dbo.Clientes (NombreCompleto, CorreoElectronico, Telefono, Direccion)
	values
	('Lila Fill', 'lilaFill@gmail.com', '8297845124', 'Calle Santiago #705, Zona Universitaria'),
	('Eda Guerrero','eda@gmail.com','8295178452','Av. Charles de Gaulle #03, Santo Domingo'),
	('Onur Matos', 'onur01@gmail.com','8094517845','Av. Charles de Gaulle #19, Santo Domingo'),
	('Raiza Abreu', 'raizaA@gmail.com','8297845214','Calle Juan Baron Fajardo #261, Santo Domingo'),
	('Daniel Fill','daniFill@gmail.com','8298547125','Calle Juan Fajardo #270, Santo Domingo'),
	('Ernesto Báez','ernestoB@gmail.com','8498547899','Calle Juan Fajardo #265, Santo Domingo');

insert into dbo.Facturas (ClienteID, FechaFactura, Total)
values
(1, '2025-05-11', 0),
(2, GETDATE(), 0),
(3, GETDATE(), 0),
(4, '2025-07-02', 0),
(5, '2025-9-29', 0),
(6, GETDATE(), 0);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(1, 1, 1, 185787.00, 33441.66, 185787.00),
(1, 3, 2, 3038.00, 1093.68, 6076.00),
(1, 8, 1, 990.39, 178.27, 990.39),
(1, 5, 1, 100.00, 18.00, 100.00);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(2, 6, 1, 2995.00, 539.10, 2995.00),
(2, 9, 1, 742.79, 133.70, 742.79),
(2, 3, 1, 3038.00, 546.84, 3038.00),
(2, 7, 1, 2995.00, 569.00, 2995.00);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(3, 6, 1, 2995.00, 539.10, 2995.00),
(3, 4, 1, 230.00, 41.40, 230.00),
(3, 3, 1, 3038.00, 546.84, 3038.00),
(3, 7, 1, 2995.00, 539.10, 2995.00);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(4, 8, 1, 990.00, 178.27, 992.39),
(4, 5, 1, 100.00, 18.00, 100.00),
(4, 3, 2, 3038.00, 1093.68, 6076.00),
(4, 7, 1, 2995.00, 539.10, 2995.00);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(5, 2, 1, 56637.00, 10194.66, 56637.00),
(5, 4, 3, 230.00, 124.20, 690.00),
(5, 8, 2, 990.39, 356.54, 1989.78),
(5, 3, 1, 3038.00, 546.84, 3038.00);

insert into dbo.DetallesFactura (FacturaID, ProductoID, Cantidad, Precio, Impuesto, Subtotal)
values
(6, 1, 1, 18578.00, 33441.66, 185787.00),
(6, 9, 1, 742.79, 133.70, 742.79),
(6, 5, 1, 100.00, 18.00, 100.00),
(6, 3, 1, 3038.00, 546.847, 3038.00);

go

update dbo.Facturas
set Total = (Select Sum(Subtotal + Impuesto) from dbo.DetallesFactura where DetallesFactura.FacturaID = Facturas.FacturaID);

insert into dbo.Proveedores (NombreProveedor, Telefono, CorreoElectronico)
values
('Los Gonzáles Tech', '8298475123', 'techGonzales@gmail.com'),
('Los Food Core', '8298478447', 'foodCore@gmail.com'),
('Los Reading Core', '8498475123', 'readingCore@gmail.com'),
('Los Shop Fashion', '8298895123', 'shopFashion@gmail.com'),
('Los EXATECH', '8298478510', 'exatech@gmail.com');

go

insert into dbo.Compras (ProveedorID, FechaCompra, Total)
values
(1, '2025-04-30', 0),
(2, '2025-08-25', 0),
(3, '2025-08-25', 0),
(4, '2025-06-05', 0),
(5, '2025-08-01', 0);
go

insert into dbo.DetallesCompra (CompraID, ProductoID, Cantidad, CostoUnitario, Impuesto, Subtotal)
values
(1, 1, 1, 185787.00, 33441.66, 185787.00),
(1, 3, 5, 3038.00, 2734.20, 15190.00),
(1, 8, 6, 990.39, 1069-62, 5942.34),
(1, 9, 1, 742.79, 133.70, 742.79);

insert into dbo.DetallesCompra (CompraID, ProductoID, Cantidad, CostoUnitario, Impuesto, Subtotal)
values
(2, 4, 10, 230.00, 414.00, 2300.00),
(2, 5, 3, 100.00, 54.00, 300.00),
(2, 6, 4, 2995.00, 2156.40, 11980.00),
(2, 7, 2, 2995.00, 1078.20, 5990.00);

insert into dbo.DetallesCompra (CompraID, ProductoID, Cantidad, CostoUnitario, Impuesto, Subtotal)
values
(3, 8, 6, 990.39, 1069.62, 5942.34),
(3, 9, 4, 742.79, 534.80, 2971.16),
(3, 10, 1, 618.99, 11.41, 618.99),
(3, 2, 1, 56637.00, 10194.66, 56637.00);

insert into dbo.DetallesCompra (CompraID, ProductoID, Cantidad, CostoUnitario, Impuesto, Subtotal)
values
(4, 6, 4, 230.00, 165.60, 920.00),
(4, 7, 2, 2995.00, 1078.20, 5990.00),
(4, 1, 1, 185787.00, 33441.66, 185787.00),
(4, 3, 5, 3038.00, 2734.20, 15190.00);

insert into dbo.DetallesCompra (CompraID, ProductoID, Cantidad, CostoUnitario, Impuesto, Subtotal)
values
(5, 1, 1, 185787.00, 33441.66, 185787.00),
(5, 2, 1, 56637.00, 10194.66, 56637.00),
(5, 4, 10, 230.00, 414.00, 2300.00),
(5, 5, 2, 100.00, 36.00, 200.00);

go

update dbo.Compras set Total = (select sum(Subtotal + Impuesto) from dbo.DetallesCompra where DetallesCompra.CompraID = Compras.CompraID);