CREATE PROCEDURE [dbo].[ServiceUsage_Create]
    @BookingId INT,
    @ServiceId INT,
    @Quantity INT = 1,
    @TotalPrice DECIMAL(18,2),
    @UsedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.ServiceUsage (BookingId, ServiceId, Quantity, TotalPrice, UsedAt)
        VALUES (@BookingId, @ServiceId, @Quantity, @TotalPrice, ISNULL(@UsedAt, GETDATE()));

        SELECT 'ServiceUsage created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[ServiceUsage_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT su.*, 
               b.UserId, 
               s.Name AS ServiceName
        FROM dbo.ServiceUsage su
        INNER JOIN dbo.Bookings b ON su.BookingId = b.Id
        INNER JOIN dbo.Services s ON su.ServiceId = s.Id
        WHERE su.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[ServiceUsage_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT su.*, 
               b.UserId, 
               s.Name AS ServiceName
        FROM dbo.ServiceUsage su
        INNER JOIN dbo.Bookings b ON su.BookingId = b.Id
        INNER JOIN dbo.Services s ON su.ServiceId = s.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[ServiceUsage_Update]
    @Id INT,
    @BookingId INT = NULL,
    @ServiceId INT = NULL,
    @Quantity INT = NULL,
    @TotalPrice DECIMAL(18,2) = NULL,
    @UsedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.ServiceUsage
        SET BookingId = ISNULL(@BookingId, BookingId),
            ServiceId = ISNULL(@ServiceId, ServiceId),
            Quantity = ISNULL(@Quantity, Quantity),
            TotalPrice = ISNULL(@TotalPrice, TotalPrice),
            UsedAt = ISNULL(@UsedAt, UsedAt)
        WHERE Id = @Id;

        SELECT 'ServiceUsage updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[ServiceUsage_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.ServiceUsage WHERE Id = @Id;
        SELECT 'ServiceUsage deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
