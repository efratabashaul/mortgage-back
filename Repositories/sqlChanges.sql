-- for example:
--ALTER TABLE [dbo].[exampleTable] ADD exampleField NVARCHAR(50)

  ALTER TABLE Customers
ADD UserId INT NOT NULL DEFAULT 0;

--Add notification table
CREATE TABLE [dbo].[Notifications] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,  -- מזהה ייחודי לכל רשומה
    [UserId] INT NOT NULL,               -- מזהה המשתמש
    [Message] NVARCHAR(MAX) NOT NULL,    -- הודעה למשתמש
    [IsRead] BIT NOT NULL DEFAULT 0,     -- האם ההודעה נקראה (1) או לא (0)
    [created_at] DATETIME NOT NULL DEFAULT GETDATE()  -- תאריך יצירת ההודעה
);
--Adding fields related to admin to the customer task table
ALTER TABLE [mortgageDatabase].[dbo].[CustomerTasks]
ADD [Document_path2] NVARCHAR(MAX);
  ALTER TABLE [mortgageDatabase].[dbo].[CustomerTasks]
ADD [status2] int;
  ALTER TABLE [mortgageDatabase].[dbo].[CustomerTasks]
ADD [Id2] int;
