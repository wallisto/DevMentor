PRINT N'Dropping database WorkTrek...';
DROP DATABASE WorkTrek
GO

PRINT N'Creating database WorkTrek...';
CREATE DATABASE WorkTrek
GO

USE WORKTREK
GO

PRINT N'Creating Table WorkItems...';
CREATE TABLE [dbo].[WorkItems] (
    [ItemId]         INT            NOT NULL,
    [Title]           NVARCHAR (200) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [Category]        INT            NULL,
    [Status]          INT            NULL,
    [Resolution]      INT            NULL,
    [Priority]        INT            NULL,
	[CreatedOn] [Date] NOT NULL,
	[FinishedOn] [Date] NULL,
);

GO

PRINT N'Creating PK_Item...';
GO
ALTER TABLE [dbo].[WorkItems]
    ADD CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([ItemId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

INSERT INTO WorkItems (ItemId, title, description, category, status, resolution, priority, CreatedOn) values (1, 'Set up project', 'Set up the project solutions and source control', 1, 1, 1, 1, '2010-06-12')
INSERT INTO WorkItems (ItemId, title, description, category, status, resolution, priority, CreatedOn) values (2, 'Add project users', 'Set up security for the project', 1, 1, 1, 1, '2010-06-12')
INSERT INTO WorkItems (ItemId, title, description, category, status, resolution, priority, CreatedOn) values (3, 'Create database', 'Define the database tables', 1, 1, 1, 1, '2010-06-12')
INSERT INTO WorkItems (ItemId, title, description, category, status, resolution, priority, CreatedOn) values (4, 'Define UI', 'Contact a design company', 1, 1, 1, 1, '2010-06-12')
GO