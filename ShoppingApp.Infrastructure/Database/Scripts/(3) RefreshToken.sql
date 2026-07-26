CREATE TABLE [Auth].[RefreshToken]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] NVARCHAR(450) NOT NULL,
    [Token] NVARCHAR(200) NOT NULL,
    [ExpiresAt] DATETIME2 NOT NULL,
    [Valid] BIT NOT NULL

    CONSTRAINT FK_RefreshToken_AspNetUsers_UserId
        FOREIGN KEY ([UserId])
        REFERENCES [Auth].[AspNetUsers]([Id])
);

CREATE UNIQUE INDEX IX_RefreshToken_Token
ON [Auth].[RefreshToken] ([Token]);