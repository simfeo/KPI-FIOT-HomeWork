
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/23/2018 07:55:30
-- Generated from EDMX file: c:\users\sim\documents\visual studio 2015\Projects\Smtirpz\Smtirpz\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SMTirpz];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Abonents'
CREATE TABLE [dbo].[Abonents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PasportNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CurrentName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AbonentAddresses'
CREATE TABLE [dbo].[AbonentAddresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AbonentID] int  NOT NULL,
    [AddressID] int  NOT NULL
);
GO

-- Creating table 'ContractNumbers'
CREATE TABLE [dbo].[ContractNumbers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AbonentAdressID] int  NOT NULL,
    [Discount] float  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [DeviseCount] int  NOT NULL,
    [Blockrator] int  NOT NULL,
    [Cedit] float  NOT NULL
);
GO

-- Creating table 'CallsDBs'
CREATE TABLE [dbo].[CallsDBs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [ContractNum] int  NOT NULL,
    [StartTime] time  NOT NULL,
    [DestinationID] int  NOT NULL,
    [Duration] bigint  NOT NULL
);
GO

-- Creating table 'CallTypes'
CREATE TABLE [dbo].[CallTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Destinations'
CREATE TABLE [dbo].[Destinations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Cost] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Abonents'
ALTER TABLE [dbo].[Abonents]
ADD CONSTRAINT [PK_Abonents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AbonentAddresses'
ALTER TABLE [dbo].[AbonentAddresses]
ADD CONSTRAINT [PK_AbonentAddresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContractNumbers'
ALTER TABLE [dbo].[ContractNumbers]
ADD CONSTRAINT [PK_ContractNumbers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CallsDBs'
ALTER TABLE [dbo].[CallsDBs]
ADD CONSTRAINT [PK_CallsDBs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CallTypes'
ALTER TABLE [dbo].[CallTypes]
ADD CONSTRAINT [PK_CallTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Destinations'
ALTER TABLE [dbo].[Destinations]
ADD CONSTRAINT [PK_Destinations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AbonentID] in table 'AbonentAddresses'
ALTER TABLE [dbo].[AbonentAddresses]
ADD CONSTRAINT [FK_AbonentAbonentAddress]
    FOREIGN KEY ([AbonentID])
    REFERENCES [dbo].[Abonents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AbonentAbonentAddress'
CREATE INDEX [IX_FK_AbonentAbonentAddress]
ON [dbo].[AbonentAddresses]
    ([AbonentID]);
GO

-- Creating foreign key on [AddressID] in table 'AbonentAddresses'
ALTER TABLE [dbo].[AbonentAddresses]
ADD CONSTRAINT [FK_AddressAbonentAddress]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressAbonentAddress'
CREATE INDEX [IX_FK_AddressAbonentAddress]
ON [dbo].[AbonentAddresses]
    ([AddressID]);
GO

-- Creating foreign key on [AbonentAdressID] in table 'ContractNumbers'
ALTER TABLE [dbo].[ContractNumbers]
ADD CONSTRAINT [FK_AbonentAddressContractNumber]
    FOREIGN KEY ([AbonentAdressID])
    REFERENCES [dbo].[AbonentAddresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AbonentAddressContractNumber'
CREATE INDEX [IX_FK_AbonentAddressContractNumber]
ON [dbo].[ContractNumbers]
    ([AbonentAdressID]);
GO

-- Creating foreign key on [Type] in table 'CallsDBs'
ALTER TABLE [dbo].[CallsDBs]
ADD CONSTRAINT [FK_CallTypesCallsDB]
    FOREIGN KEY ([Type])
    REFERENCES [dbo].[CallTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CallTypesCallsDB'
CREATE INDEX [IX_FK_CallTypesCallsDB]
ON [dbo].[CallsDBs]
    ([Type]);
GO

-- Creating foreign key on [DestinationID] in table 'CallsDBs'
ALTER TABLE [dbo].[CallsDBs]
ADD CONSTRAINT [FK_DestinationsCallsDB]
    FOREIGN KEY ([DestinationID])
    REFERENCES [dbo].[Destinations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DestinationsCallsDB'
CREATE INDEX [IX_FK_DestinationsCallsDB]
ON [dbo].[CallsDBs]
    ([DestinationID]);
GO

-- Creating foreign key on [ContractNum] in table 'CallsDBs'
ALTER TABLE [dbo].[CallsDBs]
ADD CONSTRAINT [FK_ContractNumberCallsDB]
    FOREIGN KEY ([ContractNum])
    REFERENCES [dbo].[ContractNumbers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractNumberCallsDB'
CREATE INDEX [IX_FK_ContractNumberCallsDB]
ON [dbo].[CallsDBs]
    ([ContractNum]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------