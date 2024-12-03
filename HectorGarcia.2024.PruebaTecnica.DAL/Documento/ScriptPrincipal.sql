CREATE DATABASE HG_PruebaTecnica2024;
GO
USE HG_PruebaTecnica2024;
GO
CREATE TABLE Categorias(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL
);
GO
CREATE TABLE Productos(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
Precio DECIMAL(10,2) NOT NULL,
IdCategoria INT NOT NULL FOREIGN KEY REFERENCES Categorias(Id)
);