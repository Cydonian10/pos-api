IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE SEQUENCE [dbo].[MiEntidad_Id_seq] AS int START WITH 100 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE SEQUENCE [dbo].[Purchase_Id_seq] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Salary] decimal(18,2) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Birthday] datetime2 NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [CashRegisters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [TotalCash] decimal(18,2) NOT NULL,
        [InitialCash] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [Open] bit NOT NULL,
        CONSTRAINT [PK_CashRegisters] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [HistoryPriceProduct] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [OldPrice] decimal(18,2) NOT NULL,
        [Name] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_HistoryPriceProduct] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [Suppliers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NULL,
        [Adress] nvarchar(150) NULL,
        [Phone] nvarchar(9) NULL,
        CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [UnitMeasurements] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Symbol] nvarchar(max) NULL,
        CONSTRAINT [PK_UnitMeasurements] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [HistoryCashRegisters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [CashRegisterId] int NOT NULL,
        [TotalCash] decimal(18,2) NOT NULL,
        [EmployedId] nvarchar(450) NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_HistoryCashRegisters] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HistoryCashRegisters_AspNetUsers_EmployedId] FOREIGN KEY ([EmployedId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [Sales] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Taxes] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [VaucherNumber] int NOT NULL DEFAULT (next value for dbo.MiEntidad_Id_seq),
        [CustomerId] nvarchar(450) NULL,
        [EmployedId] nvarchar(450) NULL,
        [CashRegisterId] int NULL,
        [EStatusCompra] int NULL,
        CONSTRAINT [PK_Sales] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Sales_AspNetUsers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Sales_AspNetUsers_EmployedId] FOREIGN KEY ([EmployedId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Sales_CashRegisters_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [CashRegisters] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [Purchase] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Taxes] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [VaucherNumber] int NOT NULL DEFAULT (next value for dbo.Purchase_Id_seq),
        [SupplierId] int NOT NULL,
        CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchase_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Stock] decimal(18,2) NOT NULL,
        [SalePrice] decimal(18,2) NOT NULL,
        [PurchasePrice] decimal(18,2) NOT NULL,
        [PurchaseDesc] decimal(18,2) NOT NULL,
        [Image] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Size] nvarchar(max) NULL,
        [CondicionDiscount] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [UnitMeasurementId] int NOT NULL,
        [BarCode] int NOT NULL,
        [QuantitySale] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Products_UnitMeasurements_UnitMeasurementId] FOREIGN KEY ([UnitMeasurementId]) REFERENCES [UnitMeasurements] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [PurchaseDetail] (
        [Id] int NOT NULL IDENTITY,
        [Quantity] decimal(18,2) NOT NULL,
        [SubTotal] decimal(18,2) NOT NULL,
        [PurchasePrice] decimal(18,2) NOT NULL,
        [ProductId] int NOT NULL,
        [PurchaseId] int NOT NULL,
        CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PurchaseDetail_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PurchaseDetail_Purchase_PurchaseId] FOREIGN KEY ([PurchaseId]) REFERENCES [Purchase] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE TABLE [SaleDetails] (
        [Id] int NOT NULL IDENTITY,
        [Quantity] decimal(18,2) NOT NULL,
        [SubTotal] decimal(18,2) NOT NULL,
        [ProductId] int NOT NULL,
        [SaleId] int NOT NULL,
        [Descuento] decimal(18,2) NULL,
        [UnitPrice] decimal(18,2) NULL,
        CONSTRAINT [PK_SaleDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SaleDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SaleDetails_Sales_SaleId] FOREIGN KEY ([SaleId]) REFERENCES [Sales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_HistoryCashRegisters_EmployedId] ON [HistoryCashRegisters] ([EmployedId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Products_UnitMeasurementId] ON [Products] ([UnitMeasurementId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Purchase_SupplierId] ON [Purchase] ([SupplierId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_PurchaseDetail_ProductId] ON [PurchaseDetail] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_PurchaseDetail_PurchaseId] ON [PurchaseDetail] ([PurchaseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_SaleDetails_ProductId] ON [SaleDetails] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_SaleDetails_SaleId] ON [SaleDetails] ([SaleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Sales_CashRegisterId] ON [Sales] ([CashRegisterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Sales_CustomerId] ON [Sales] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    CREATE INDEX [IX_Sales_EmployedId] ON [Sales] ([EmployedId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240312225059_INit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240312225059_INit', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170530_usercashregister'
)
BEGIN
    ALTER TABLE [CashRegisters] ADD [UserId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170530_usercashregister'
)
BEGIN
    ALTER TABLE [CashRegisters] ADD [UsuarioId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170530_usercashregister'
)
BEGIN
    CREATE INDEX [IX_CashRegisters_UserId] ON [CashRegisters] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170530_usercashregister'
)
BEGIN
    ALTER TABLE [CashRegisters] ADD CONSTRAINT [FK_CashRegisters_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170530_usercashregister'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240314170530_usercashregister', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170908_adduser'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CashRegisters]') AND [c].[name] = N'UsuarioId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CashRegisters] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [CashRegisters] DROP COLUMN [UsuarioId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240314170908_adduser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240314170908_adduser', N'8.0.1');
END;
GO

COMMIT;
GO

