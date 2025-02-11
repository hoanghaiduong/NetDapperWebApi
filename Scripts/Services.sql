CREATE PROCEDURE [dbo].[Services_Create]
    @Name NVARCHAR(255),
    @Description NVARCHAR(500) = NULL,
    @Price DECIMAL(18,2),
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Services WHERE Name = @Name)
        BEGIN
            RAISERROR('Service with this name already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Services (Name, Description, Price, CreatedAt, UpdatedAt)
        VALUES (@Name, @Description, @Price, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Service created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Services_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Services WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Services_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Services;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Services_Update]
    @Id INT,
    @Name NVARCHAR(255) = NULL,
    @Description NVARCHAR(500) = NULL,
    @Price DECIMAL(18,2) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Services
        SET Name = ISNULL(@Name, Name),
            Description = ISNULL(@Description, Description),
            Price = ISNULL(@Price, Price),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Service updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Services_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Services WHERE Id = @Id;
        SELECT 'Service deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
