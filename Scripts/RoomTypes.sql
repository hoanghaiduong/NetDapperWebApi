CREATE PROCEDURE [dbo].[RoomTypes_Create]
    @Name NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @PricePerNight DECIMAL(18,2),
    @Capacity INT,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.RoomTypes WHERE Name = @Name)
        BEGIN
            RAISERROR('RoomType with this name already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.RoomTypes (Name, Description, PricePerNight, Capacity, CreatedAt, UpdatedAt)
        VALUES (@Name, @Description, @PricePerNight, @Capacity, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'RoomType created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RoomTypes_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.RoomTypes WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RoomTypes_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.RoomTypes;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RoomTypes_Update]
    @Id INT,
    @Name NVARCHAR(100) = NULL,
    @Description NVARCHAR(500) = NULL,
    @PricePerNight DECIMAL(18,2) = NULL,
    @Capacity INT = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.RoomTypes
        SET Name = ISNULL(@Name, Name),
            Description = ISNULL(@Description, Description),
            PricePerNight = ISNULL(@PricePerNight, PricePerNight),
            Capacity = ISNULL(@Capacity, Capacity),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'RoomType updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RoomTypes_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.RoomTypes WHERE Id = @Id;
        SELECT 'RoomType deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
