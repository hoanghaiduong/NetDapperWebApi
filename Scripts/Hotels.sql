CREATE PROCEDURE [dbo].[Hotels_Create]
    @Name NVARCHAR(MAX),
    @Address NVARCHAR(MAX),
    @Phone NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @Thumbnail NVARCHAR(MAX),
    @Images NVARCHAR(MAX),
    @Stars INT,
    @CheckinTime NVARCHAR(MAX) = NULL,
    @CheckoutTime NVARCHAR(MAX) = NULL,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Hotels WHERE Name = @Name AND Address = @Address)
        BEGIN
            RAISERROR('Hotel with this name and address already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Hotels (Name, Address, Phone, Email, Thumbnail, Images, Stars, CheckinTime, CheckoutTime, CreatedAt, UpdatedAt)
        VALUES (@Name, @Address, @Phone, @Email, @Thumbnail, @Images, @Stars, @CheckinTime, @CheckoutTime, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Hotel created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Hotels_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Hotels WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Hotels_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Hotels;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Hotels_Update]
    @Id INT,
    @Name NVARCHAR(MAX) = NULL,
    @Address NVARCHAR(MAX) = NULL,
    @Phone NVARCHAR(MAX) = NULL,
    @Email NVARCHAR(MAX) = NULL,
    @Thumbnail NVARCHAR(MAX) = NULL,
    @Images NVARCHAR(MAX) = NULL,
    @Stars INT = NULL,
    @CheckinTime NVARCHAR(MAX) = NULL,
    @CheckoutTime NVARCHAR(MAX) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Hotels
        SET Name = ISNULL(@Name, Name),
            Address = ISNULL(@Address, Address),
            Phone = ISNULL(@Phone, Phone),
            Email = ISNULL(@Email, Email),
            Thumbnail = ISNULL(@Thumbnail, Thumbnail),
            Images = ISNULL(@Images, Images),
            Stars = ISNULL(@Stars, Stars),
            CheckinTime = ISNULL(@CheckinTime, CheckinTime),
            CheckoutTime = ISNULL(@CheckoutTime, CheckoutTime),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Hotel updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Hotels_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Hotels WHERE Id = @Id;
        SELECT 'Hotel deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
