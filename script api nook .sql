CREATE DATABASE IF NOT EXISTS nook_api;
USE nook_api;

CREATE TABLE  IF NOT EXISTS usuario (
    id_usu INT PRIMARY KEY AUTO_INCREMENT,
    nome_usu VARCHAR(100) NOT NULL,
    email_usu VARCHAR(150) NOT NULL UNIQUE,
    senha_usu VARCHAR(200) NOT NULL,
    perfil_usu VARCHAR(30) NOT NULL
);

INSERT INTO usuario (nome_usu, email_usu, senha_usu, perfil_usu)
VALUES ('Admin', 'admin@nook.com', '123', 'Admin');

CREATE TABLE cliente (
    id_cli INT PRIMARY KEY AUTO_INCREMENT,
    nome_cli VARCHAR(100) NOT NULL,
    cpf_cli VARCHAR(15) NOT NULL UNIQUE,
    email_cli VARCHAR(150) NOT NULL UNIQUE,
    tel_cli VARCHAR(20),
    endereco_cli VARCHAR(200)
);

CREATE TABLE funcionario (
    id_fun INT PRIMARY KEY AUTO_INCREMENT,
    nome_fun VARCHAR(100) NOT NULL,
    cpf_fun VARCHAR(15) NOT NULL UNIQUE,
    email_fun VARCHAR(150) NOT NULL UNIQUE,
    tel_fun VARCHAR(20),
    funcao_fun VARCHAR(100) NOT NULL
);

CREATE TABLE livro (
    id_liv INT PRIMARY KEY AUTO_INCREMENT,
    titulo_liv VARCHAR(200) NOT NULL,
    autor_liv VARCHAR(150) NOT NULL,
    ano_liv INT,
    quantidade_liv INT NOT NULL
);

CREATE TABLE evento (
    id_eve INT PRIMARY KEY AUTO_INCREMENT,
    titulo_eve VARCHAR(200) NOT NULL,
    descricao_eve TEXT NOT NULL,
    data_eve DATETIME NOT NULL,
    vagas_eve INT NOT NULL
);

CREATE TABLE reserva (
    id_res INT PRIMARY KEY AUTO_INCREMENT,
    id_cli_fk INT NOT NULL,
    id_liv_fk INT NOT NULL,
    data_reserva DATETIME NOT NULL,
    status_reserva VARCHAR(30) NOT NULL,
    FOREIGN KEY (id_cli_fk) REFERENCES cliente(id_cli) ON DELETE RESTRICT,
    FOREIGN KEY (id_liv_fk) REFERENCES livro(id_liv) ON DELETE RESTRICT
);

CREATE TABLE participacao_evento (
    id_par INT PRIMARY KEY AUTO_INCREMENT,
    id_cli_fk INT NOT NULL,
    id_eve_fk INT NOT NULL,
    data_registro DATETIME NOT NULL,
    FOREIGN KEY (id_cli_fk) REFERENCES cliente(id_cli) ON DELETE RESTRICT,
    FOREIGN KEY (id_eve_fk) REFERENCES evento(id_eve) ON DELETE RESTRICT
);

INSERT INTO usuario (nome_usu, email_usu, senha_usu, perfil_usu)
VALUES (
    'Administrador',
    'admin@gmail.com',
    '123456',
    'ADMIN'
);

ALTER TABLE participacao_evento CHANGE COLUMN EventoId evento_id INT NOT NULL;
ALTER TABLE participacao_evento ADD COLUMN cliente_id INT NOT NULL;
ALTER TABLE participacao_evento ADD COLUMN evento_id INT NOT NULL;
ALTER TABLE participacao_evento ADD FOREIGN KEY (cliente_id) REFERENCES cliente(id_cli);
ALTER TABLE participacao_evento ADD FOREIGN KEY (evento_id) REFERENCES evento(id_eve);
ALTER TABLE participacao_evento ADD CONSTRAINT fk_participacao_evento FOREIGN KEY (evento_id) REFERENCES evento(id_eve) ON DELETE RESTRICT;
ALTER TABLE participacao_evento DROP COLUMN id_cli_fk;
ALTER TABLE participacao_evento DROP COLUMN id_eve_fk;

SHOW TABLES;

SELECT * FROM cliente;
SELECT * FROM evento;
SELECT * FROM funcionario;
SELECT * FROM livro;
SELECT * FROM participacao_evento;
SELECT * FROM reserva;
SELECT * FROM usuario;


