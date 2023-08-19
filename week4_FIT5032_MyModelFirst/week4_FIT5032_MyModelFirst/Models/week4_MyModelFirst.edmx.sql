
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/15/2023 09:06:12
-- Generated from EDMX file: C:\Users\谈韬\Desktop\FIT5032\week4_FIT5032_MyModelFirst\week4_FIT5032_MyModelFirst\Models\week4_MyModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [week4_MyModelFirst];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StudentUnit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnitSet] DROP CONSTRAINT [FK_StudentUnit];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StudentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentSet];
GO
IF OBJECT_ID(N'[dbo].[UnitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StudentSet'
CREATE TABLE [dbo].[StudentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [testId] int  NOT NULL
);
GO

-- Creating table 'UnitSet'
CREATE TABLE [dbo].[UnitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StudentId] int  NOT NULL
);
GO

-- Creating table 'testSet'
CREATE TABLE [dbo].[testSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [PK_StudentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UnitSet'
ALTER TABLE [dbo].[UnitSet]
ADD CONSTRAINT [PK_UnitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'testSet'
ALTER TABLE [dbo].[testSet]
ADD CONSTRAINT [PK_testSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentId] in table 'UnitSet'
ALTER TABLE [dbo].[UnitSet]
ADD CONSTRAINT [FK_StudentUnit]
    FOREIGN KEY ([StudentId])
    REFERENCES [dbo].[StudentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentUnit'
CREATE INDEX [IX_FK_StudentUnit]
ON [dbo].[UnitSet]
    ([StudentId]);
GO

-- Creating foreign key on [testId] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [FK_testStudent]
    FOREIGN KEY ([testId])
    REFERENCES [dbo].[testSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_testStudent'
CREATE INDEX [IX_FK_testStudent]
ON [dbo].[StudentSet]
    ([testId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------