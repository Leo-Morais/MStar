Create Database [MStarDB]
USE [MStarDB]

CREATE TABLE TipoMercadoria(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Tipo VARCHAR(255) NOT NULL,
);

CREATE TABLE Mercadorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(255) NOT NULL,
	Fabricante VARCHAR(255) NOT NULL,
	Descricao VARCHAR(500) NOT NULL,
	DataCriacao DATETIME NOT NULL, 
	TipoMercadoriaId Int NOT NULL,
	FOREIGN KEY (TipoMercadoriaId) REFERENCES TipoMercadoria(Id),
);


CREATE TABLE Movimentacao(
	Id INT PRIMARY KEY IDENTITY(1,1),
	IdMercadoria INT NOT NULL,
	Quantidade INT NOT NULL,
	DataCriacao DATETIME NOT NULL,
	LocalMovimentacao VARCHAR(255) NOT NULL,
	TipoMovimentacao CHAR(1) NOT NULL --"E" para entrada e "S" para saida.
	FOREIGN KEY (IdMercadoria) REFERENCES Mercadorias(Id)
);

CREATE TABLE Estoque(
	Id INT PRIMARY KEY IDENTITY(1,1),
	IdMercadoria INT NOT NULL,
	Quantidade INT NOT NULL,
	DataAtualizacao DATETIME NOT NULL,
	FOREIGN KEY (IdMercadoria) REFERENCES Mercadorias(Id)
);