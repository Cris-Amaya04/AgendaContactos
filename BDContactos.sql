Create database DBContactos
use DBContactos

Create table Contacto 
(IDContacto int identity, 
Nombre varchar(100), 
Apellido varchar(100), 
Telefono varchar(100), 
Correo varchar(100))

INSERT INTO Contacto (Nombre, Apellido, Telefono, Correo) VALUES 
('Ernesto', 'Araujo', '2284-5578', 'Neto22@gmail.com'),
('Juan', 'Perez', '5512-4123', 'JuanPe1680@gmail.com')

SELECT * FROM Contacto

CREATE PROCEDURE sp_Crear(
	@IDContacto int,
	@Nombre varchar(100),
	@Apellido varchar(100),
	@Telefono varchar(100),
	@Correo varchar(100)
)
AS
BEGIN
	INSERT INTO Contacto(Nombre, Apellido, Telefono, Correo) VALUES 
	(@Nombre, @Apellido, @Telefono, @Correo)
END


CREATE PROCEDURE sp_Actualizar(
	@IDContacto int,
	@Nombre varchar(100),
	@Apellido varchar(100),
	@Telefono varchar(100),
	@Correo varchar(100)
)
AS
BEGIN
	UPDATE Contacto SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Correo = @Correo WHERE IDContacto = @IDContacto
END

CREATE PROCEDURE sp_Eliminar(
	@IDContacto int
)
AS
BEGIN
	DELETE FROM Contacto WHERE IDContacto = @IDContacto
END

