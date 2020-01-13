use Lib;

CREATE TABLE [dbo].[Reader] (
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,
);


CREATE TABLE [dbo].[Catalog] (
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Author] VARCHAR(50) NOT NULL,
	[Title] VARCHAR(50) NOT NULL,
);

CREATE TABLE [dbo].[Event] (
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[EventType] VARCHAR(50) NOT NULL,
	[DateTime] DATETIME NOT NULL,
)

CREATE TABLE [dbo].[Book] (
	[IdNumber] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CatalogId] INT,
	[ReaderId] INT,
	CONSTRAINT [FK_Book_ToCatalog] FOREIGN KEY ([CatalogId]) REFERENCES [dbo].[Catalog] ([Id]),
	CONSTRAINT [FK_Book_ToReader] FOREIGN KEY ([ReaderId]) REFERENCES [dbo].[Reader] ([Id])
)

INSERT INTO Catalog(Author, Title) VALUES ('Shakespeare', 'Sonnet 116');
INSERT INTO Reader(FirstName, LastName) VALUES ('John', 'Kowalsky');
INSERT INTO Reader(FirstName, LastName) VALUES  ('Adam', 'Nowak');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (1, 1);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (1, 2);


------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Book(CatalogId, ReaderId) VALUES  (1, NULL);

INSERT INTO Catalog(Author, Title) VALUES ('Shakespeare', 'Sonnet 130');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (2, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('Hemingway', 'The Old Man and the Sea');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (3, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (3, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('Hemingway', 'The Sun Also Rises');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (4, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (4, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('King', 'It');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (5, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (5, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('King', 'The Shining');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (6, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('J. K. Rowling', 'Harry Potter and the Deathly Hallows');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (7, NULL);

INSERT INTO Catalog(Author, Title) VALUES ('J. K. Rowling', 'Harry Potter and the Goblet of Fire');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (8, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (8, NULL);

INSERT INTO Catalog(Author, Title) VALUES ('Twain', 'Adventures of Huckleberry Finn');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (9, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (9, NULL);


INSERT INTO Catalog(Author, Title) VALUES ('Twain', 'Adventures of Tom Sawyer');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (10, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (10, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (10, NULL);

INSERT INTO Catalog(Author, Title) VALUES ('Author1', 'Book1');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (11, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (11, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (11, NULL);

INSERT INTO Catalog(Author, Title) VALUES ('Author2', 'Book2');
INSERT INTO Book(CatalogId, ReaderId) VALUES  (12, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (12, NULL);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (12, NULL);

-----------------------------------------------------------------------------

INSERT INTO Book(CatalogId, ReaderId) VALUES  (5, 1);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (9, 1);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (10, 1);

INSERT INTO Book(CatalogId, ReaderId) VALUES  (2, 2);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (9, 2);

INSERT INTO Reader(FirstName, LastName) VALUES  ('Reader', 'Reader');

INSERT INTO Book(CatalogId, ReaderId) VALUES  (11, 3);
INSERT INTO Book(CatalogId, ReaderId) VALUES  (12, 3);

    

