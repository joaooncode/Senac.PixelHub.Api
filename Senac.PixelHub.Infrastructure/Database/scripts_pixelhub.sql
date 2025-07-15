-- Criação da tabela Categories (representa o enum)
CREATE TABLE Categories (
    Id TINYINT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- Inserindo os valores do enum CategoriesEnum
INSERT INTO Categories (Id, Name) VALUES
(0, 'BRONZE'),
(1, 'SILVER'),
(2, 'GOLD'),
(3, 'PLATINUM'),
(4, 'DIAMOND');


-- Criação da tabela Games
CREATE TABLE Games (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    isAvailable BIT NOT NULL,
    Responsible NVARCHAR(100),
    WithdrawalDate DATETIME,
    Category TINYINT NOT NULL,
    CONSTRAINT FK_Games_Categories FOREIGN KEY (Category) REFERENCES Categories(Id)
);

-- Inserindo dados de exemplo em Games
INSERT INTO Games (Title, Description, isAvailable, Responsible, WithdrawalDate, Category)
VALUES 
('The Witcher 3', 'RPG de mundo aberto com grande enredo.', 1, NULL, NULL, 3), -- PLATINUM
('Minecraft', 'Jogo de construção com blocos.', 1, NULL, NULL, 1),             -- SILVER
('FIFA 22', 'Simulador de futebol.', 0, 'João Silva', '2025-07-13 15:30:00', 2), -- GOLD
('God of War', 'Ação com mitologia nórdica.', 0, 'Maria Oliveira', '2025-07-12 17:45:00', 4), -- DIAMOND
('Stardew Valley', 'Simulador de fazenda e vida rural.', 1, NULL, NULL, 0); -- BRONZE


SELECT 
    g.Id,
    g.Title,
    g.Description,
    g.isAvailable,
    g.Responsible,
    g.WithdrawalDate,
    c.Name AS CategoryName
FROM 
    Games g
JOIN 
    Categories c ON g.Category = c.Id;
