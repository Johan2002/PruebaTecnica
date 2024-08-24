create database Aseguradora;

use Aseguradora;

create table Clientes(
IdCliente int primary key identity,
DocumentoIdentidad varchar(50) NOT NULL UNIQUE,
PrimerNombre varchar(50) NOT NULL,
SegundoNombre varchar(50),
PrimerApellido varchar(50) NOT NULL,
SegundoApellido varchar(50) NOT NULL,
Telefono varchar(50) NOT NULL,
Email varchar(50) NOT NULL UNIQUE,
FechaNacimiento date NOT NULL,
ValorSeguro decimal(10, 2) NOT NULL,
Observaciones varchar(50)
)


INSERT INTO Clientes (DocumentoIdentidad, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Telefono, Email, FechaNacimiento, ValorSeguro, Observaciones) VALUES
(1002027743, 'Ana', 'Maria', 'Ramos', 'Garc�a', '3202115666', 'anamaria@gmail.com', '1985-03-15', 1200000, 'Cliente VIP'),
(1002027744, 'Luis', 'Felipe', 'Mart�nez', 'Torres', '3202115667', 'luisfelipe@gmail.com', '1990-12-01', 1500000, 'Requiere seguimiento'),
(1002027745, 'Carlos', 'Andr�s', 'G�mez', 'Hern�ndez', '3202115668', 'carlosandres@gmail.com', '1988-07-22', 500000, 'Pago pendiente'),
(1002027746, 'Laura', 'Isabel', 'Ram�rez', 'Castro', '3202115669', 'lauraisabel@gmail.com', '1995-05-30', 700000, 'Sin observaciones'),
(1002027747, 'Ricardo', '�ngel', 'Mu�oz', 'S�nchez', '3202115670', 'ricardoangel@gmail.com', '1983-11-14', 2000000, 'Cuenta activa'),
(1002027748, 'Mar�a', 'Camila', 'Torres', 'Vega', '3202115671', 'mariacamila@gmail.com', '1992-06-19', 1000000, 'Cliente nuevo'),
(1002027749, 'Sof�a', 'Alejandra', 'Garc�a', 'Morales', '3202115672', 'sofiaalejandra@gmail.com', '1987-09-26', 850000, 'Solicit� aumento'),
(1002027750, 'Diego', 'Alonso', '�lvarez', 'M�ndez', '3202115673', 'diegoalonso@gmail.com', '1989-10-05', 1300000, 'Excelente historial'),
(1002027751, 'Natalia', 'S�nchez', 'C�rdenas', 'Hern�ndez', '3202115674', 'natalia@gmail.com', '1994-04-17', 750000, 'Pendiente de revisi�n'),
(1002027752, 'Andr�s', 'Felipe', 'Su�rez', 'Garc�a', '3202115675', 'andresfelipe@gmail.com', '1982-12-23', 900000, 'Pago completo'),
(1002027753, 'Gabriela', 'Jaramillo', 'P�rez', 'L�pez', '3202115676', 'gabrielajaramillo@gmail.com', '1991-03-09', 1100000, 'Aumento solicitado'),
(1002027754, 'Felipe', 'Santos', 'Casta�o', 'Ospina', '3202115677', 'felipesantos@gmail.com', '1986-08-30', 950000, 'Deuda alta'),
(1002027755, 'Isabella', 'Ceballos', 'Ruiz', 'C�rdoba', '3202115678', 'isabellaceballos@gmail.com', '1980-02-14', 1200000, 'Clientes frecuentes'),
(1002027756, 'Alejandro', 'Medina', 'Hurtado', 'Cardona', '3202115679', 'alejandromedina@gmail.com', '1993-07-18', 1300000, 'En proceso de validaci�n'),
(1002027757, 'Valeria', 'M�ndez', 'Ospina', 'Restrepo', '3202115680', 'valeriamendez@gmail.com', '1997-05-12', 800000, 'Sin observaciones'),
(1002027758, 'Juan', 'David', 'C�rdoba', 'Pati�o', '3202115681', 'juandavidcordoba@gmail.com', '1985-11-26', 1400000, 'Cliente regular'),
(1002027759, 'Camila', 'G�mez', 'C�rdoba', 'Alvarez', '3202115682', 'camilagomez@gmail.com', '1990-09-03', 950000, 'Cliente VIP'),
(1002027760, 'Manuel', 'Mart�nez', 'Ardila', 'Arango', '3202115683', 'manuelmartinez@gmail.com', '1994-12-22', 1100000, 'Requiere revisi�n'),
(1002027761, 'Juliana', 'Jim�nez', 'Salazar', 'Correa', '3202115684', 'julianajimenez@gmail.com', '1988-06-13', 1300000, 'Cliente frecuente'),
(1002027762, 'Sebasti�n', 'Vega', 'Uribe', 'G�mez', '3202115685', 'sebastianvega@gmail.com', '1987-10-25', 850000, 'Pago retrasado');


select * from Clientes;

go

create proc sp_listaClientes
as 
begin
select 
IdCliente, 
DocumentoIdentidad,
PrimerNombre, 
SegundoNombre, 
PrimerApellido, 
SegundoApellido, 
Telefono, 
Email, 
CONVERT(char(10), FechaNacimiento, 103)[FechaNacimiento], 
ValorSeguro,
Observaciones
from Clientes;
end

go

create proc sp_obtenerCliente(
@IdCliente int
)
as 
begin
select 
IdCliente, 
DocumentoIdentidad,
PrimerNombre, 
SegundoNombre, 
PrimerApellido, 
SegundoApellido, 
Telefono, 
Email, 
CONVERT(char(10), FechaNacimiento, 103)[FechaNacimiento], 
ValorSeguro,
Observaciones
from Clientes where IdCliente = @IdCliente;
end

go

create proc sp_crearCliente(
@DocumentoIdentidad varchar(50),
@PrimerNombre varchar(50),
@SegundoNombre varchar(50),
@PrimerApellido varchar(50),
@SegundoApellido varchar(50),
@Telefono varchar(50),
@Email varchar(50),
@FechaNacimiento varchar(10),
@ValorSeguro varchar(50),
@Observaciones varchar(50)
)
as
begin
set dateformat dmy
insert into Clientes (DocumentoIdentidad, 
					  PrimerNombre, 
					  SegundoNombre, 
					  PrimerApellido, 
					  SegundoApellido, 
					  Telefono, 
					  Email, 
					  FechaNacimiento, 
					  ValorSeguro,
					  Observaciones) values
					(@DocumentoIdentidad, 
					 @PrimerNombre,
					 @SegundoNombre,
					 @PrimerApellido,
					 @SegundoApellido,
					 @Telefono,
					 @Email,
				     convert (date,@FechaNacimiento),
					 @ValorSeguro,
					 @Observaciones)
end

go

create proc sp_editarCliente(
@IdCliente int, 
@DocumentoIdentidad varchar(50),
@PrimerNombre varchar(50),
@SegundoNombre varchar(50),
@PrimerApellido varchar(50),
@SegundoApellido varchar(50),
@Telefono varchar(50),
@Email varchar(50),
@FechaNacimiento varchar(10),
@ValorSeguro varchar(50),
@Observaciones varchar(50)
)
as
begin
set dateformat dmy
update Clientes set
					  PrimerNombre = @PrimerNombre,
					  SegundoNombre = @SegundoNombre,
					  PrimerApellido = @PrimerApellido,
					  SegundoApellido = @SegundoApellido,
					  Telefono = @Telefono,
					  Email =  @Email,
					  FechaNacimiento = convert (date,@FechaNacimiento),
					  ValorSeguro = @ValorSeguro,
					  Observaciones =  @Observaciones
					  where IdCliente = @IdCliente
end

go

create proc sp_eliminarCliente(
@IdCliente int
)
as
begin
	delete from Clientes where IdCliente = @IdCliente
end





