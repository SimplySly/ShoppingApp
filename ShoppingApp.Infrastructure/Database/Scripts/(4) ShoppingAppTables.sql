USE [ShoppingApp];

DROP TABLE IF EXISTS [dbo].[Product];
DROP TABLE IF EXISTS [dbo].[OrderItem];
DROP TABLE IF EXISTS [dbo].[Order];


CREATE TABLE [dbo].[Product]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Sku] INT NOT NULL,
    [Price] FLOAT NOT NULL
);

CREATE UNIQUE INDEX IX_Product_Name
ON [dbo].[Product] ([Name]);

INSERT INTO 
    [dbo].[Product] ([Name], [Sku], [Price])
VALUES
    ('T-Shirts', 2406, 10.5),
    ('Pants', 1567, 25),
    ('Jackets', 879, 52);

CREATE TABLE [dbo].[Order]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] NVARCHAR(450) NOT NULL,

    CONSTRAINT FK_Order_AspNetUsers_UserId
        FOREIGN KEY ([UserId])
        REFERENCES [Auth].[AspNetUsers]([Id])
);

CREATE TABLE [dbo].[OrderItem]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [OrderId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL,

    CONSTRAINT FK_OrderItem_Order
        FOREIGN KEY ([OrderId])
        REFERENCES [dbo].[Order]([Id]),

    CONSTRAINT FK_OrderItem_Product
        FOREIGN KEY ([ProductId])
        REFERENCES [dbo].[Product]([Id])
);