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
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [AppInit] (
        [Id] int NOT NULL IDENTITY,
        [Count] int NOT NULL,
        CONSTRAINT [PK_AppInit] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Salary] decimal(18,2) NOT NULL,
        [Name] nvarchar(100) NULL,
        [DateBirthday] datetime2 NOT NULL,
        [DNI] nvarchar(8) NULL,
        [Phone] nvarchar(9) NULL,
        [Active] bit NOT NULL,
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Brands] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        CONSTRAINT [PK_Brands] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [DNI] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Points] int NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [DiscountedPrice] decimal(18,2) NOT NULL,
        [MinimumDiscountQuantity] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Empresa] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [RUC] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        CONSTRAINT [PK_Empresa] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [CashRegisters] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [TotalCash] decimal(18,2) NOT NULL,
        [InitialCash] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [Open] bit NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_CashRegisters] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CashRegisters_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
        CONSTRAINT [FK_HistoryCashRegisters_AspNetUsers_EmployedId] FOREIGN KEY ([EmployedId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Purchase] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Taxes] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [VaucherNumber] int NOT NULL,
        [SupplierId] int NOT NULL,
        CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchase_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Stock] decimal(18,2) NOT NULL,
        [SalePrice] decimal(18,2) NOT NULL,
        [PurchasePrice] decimal(18,2) NOT NULL,
        [Image] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Size] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [BarCode] int NOT NULL,
        [QuantitySale] decimal(18,2) NOT NULL,
        [CategoryId] int NOT NULL,
        [UnitMeasurementId] int NOT NULL,
        [BrandId] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Products_UnitMeasurements_UnitMeasurementId] FOREIGN KEY ([UnitMeasurementId]) REFERENCES [UnitMeasurements] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Egresos] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Egreso] int NOT NULL,
        [Monto] decimal(18,2) NOT NULL,
        [CashRegisterId] int NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Egresos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Egresos_CashRegisters_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [CashRegisters] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [Sales] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Taxes] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        [VaucherNumber] int NOT NULL,
        [CustomerId] int NOT NULL,
        [userId] nvarchar(450) NULL,
        [CashRegisterId] int NULL,
        [EStatusCompra] int NULL,
        CONSTRAINT [PK_Sales] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Sales_AspNetUsers_userId] FOREIGN KEY ([userId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Sales_CashRegisters_CashRegisterId] FOREIGN KEY ([CashRegisterId]) REFERENCES [CashRegisters] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Sales_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [ProductDiscount] (
        [ProductId] int NOT NULL,
        [DiscountId] int NOT NULL,
        CONSTRAINT [PK_ProductDiscount] PRIMARY KEY ([ProductId], [DiscountId]),
        CONSTRAINT [FK_ProductDiscount_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProductDiscount_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
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
        CONSTRAINT [FK_PurchaseDetail_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PurchaseDetail_Purchase_PurchaseId] FOREIGN KEY ([PurchaseId]) REFERENCES [Purchase] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE TABLE [SaleDetails] (
        [Id] int NOT NULL IDENTITY,
        [Quantity] decimal(18,2) NOT NULL,
        [ProductId] int NOT NULL,
        [SaleId] int NOT NULL,
        [SubTotal] decimal(18,2) NOT NULL,
        [Descuento] decimal(18,2) NULL,
        CONSTRAINT [PK_SaleDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SaleDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SaleDetails_Sales_SaleId] FOREIGN KEY ([SaleId]) REFERENCES [Sales] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_CashRegisters_UserId] ON [CashRegisters] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Egresos_CashRegisterId] ON [Egresos] ([CashRegisterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_HistoryCashRegisters_EmployedId] ON [HistoryCashRegisters] ([EmployedId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_ProductDiscount_DiscountId] ON [ProductDiscount] ([DiscountId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Products_BrandId] ON [Products] ([BrandId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Products_UnitMeasurementId] ON [Products] ([UnitMeasurementId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Purchase_SupplierId] ON [Purchase] ([SupplierId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_PurchaseDetail_ProductId] ON [PurchaseDetail] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_PurchaseDetail_PurchaseId] ON [PurchaseDetail] ([PurchaseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_SaleDetails_ProductId] ON [SaleDetails] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_SaleDetails_SaleId] ON [SaleDetails] ([SaleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Sales_CashRegisterId] ON [Sales] ([CashRegisterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Sales_CustomerId] ON [Sales] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    CREATE INDEX [IX_Sales_userId] ON [Sales] ([userId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154012_AppInit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240415154012_AppInit', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154054_Roles'
)
BEGIN
            if not exists (Select [Id] from AspNetRoles where [Id] in ('75fa8a57-34a9-4f34-9512-f31f12cbc5fa','bdfdfd04-55b8-46be-b0a1-1556711f6daf'))
             begin
             insert AspNetRoles (Id,[Name],[NormalizedName])
             values 
             ('75fa8a57-34a9-4f34-9512-f31f12cbc5fa','admin','ADMIN'),
             ('bdfdfd04-55b8-46be-b0a1-1556711f6daf','vendedor','VENDEDOR')
             end;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154054_Roles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240415154054_Roles', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154218_InitApp'
)
BEGIN
                  if not exists (select [Id] from AppInit where [Id] = 1)
                  begin
                  insert into AppInit ([Count])
                      values (0)
                  end;    
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240415154218_InitApp'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240415154218_InitApp', N'8.0.1');
END;
GO

COMMIT;
GO

