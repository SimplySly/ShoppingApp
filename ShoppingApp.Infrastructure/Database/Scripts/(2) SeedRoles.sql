USE [ShoppingApp]

IF NOT EXISTS (SELECT 1 FROM [Auth].AspNetRoles WHERE NormalizedName = 'ADMIN')
BEGIN
    INSERT INTO [Auth].AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID());
END;

IF NOT EXISTS (SELECT 1 FROM [Auth].AspNetRoles WHERE NormalizedName = 'CUSTOMER')
BEGIN
    INSERT INTO [Auth].AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (NEWID(), 'Customer', 'CUSTOMER', NEWID());
END;