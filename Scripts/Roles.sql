CREATE PROCEDURE [dbo].[Roles_Create]
    @Name NVARCHAR(MAX),
    @Description NVARCHAR(MAX) = NULL,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Roles WHERE Name = @Name)
        BEGIN
            RAISERROR('Role with this name already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Roles (Name, Description, CreatedAt, UpdatedAt)
        VALUES (@Name, @Description, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Role created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Roles_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Roles WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Roles_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Roles;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Roles_Update]
    @Id INT,
    @Name NVARCHAR(MAX) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Roles
        SET Name = ISNULL(@Name, Name),
            Description = ISNULL(@Description, Description),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Role updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Roles_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Roles WHERE Id = @Id;
        SELECT 'Role deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
