DROP TABLE estoques;
CREATE TABLE estoques(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	valor DECIMAL (8,2),
	quantidade INT
	
);

INSERT INTO estoques (nome, valor, quantidade)
VALUES ('Biela', 150, 20)