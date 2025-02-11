CREATE PROCEDURE [dbo].[Bookings_Create]
    @UserId INT,
    @RoomId INT,
    @CheckInDate DATETIME2(7),
    @CheckOutDate DATETIME2(7),
    @Status INT,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL,
    @TotalPrice DECIMAL(18,2) = 0.0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Bookings 
                  WHERE UserId = @UserId 
                    AND RoomId = @RoomId 
                    AND CheckInDate = @CheckInDate 
                    AND CheckOutDate = @CheckOutDate)
        BEGIN
            RAISERROR('Booking already exists for the given details.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Bookings (UserId, RoomId, CheckInDate, CheckOutDate, Status, CreatedAt, UpdatedAt, TotalPrice)
        VALUES (@UserId, @RoomId, @CheckInDate, @CheckOutDate, @Status, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt, @TotalPrice);

        SELECT 'Booking created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Bookings_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT b.*, 
               u.FirstName, u.LastName, 
               r.RoomNumber
        FROM dbo.Bookings b
        INNER JOIN dbo.Users u ON b.UserId = u.Id
        INNER JOIN dbo.Rooms r ON b.RoomId = r.Id
        WHERE b.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Bookings_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT b.*, 
               u.FirstName, u.LastName, 
               r.RoomNumber
        FROM dbo.Bookings b
        INNER JOIN dbo.Users u ON b.UserId = u.Id
        INNER JOIN dbo.Rooms r ON b.RoomId = r.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Bookings_Update]
    @Id INT,
    @UserId INT = NULL,
    @RoomId INT = NULL,
    @CheckInDate DATETIME2(7) = NULL,
    @CheckOutDate DATETIME2(7) = NULL,
    @Status INT = NULL,
    @UpdatedAt DATETIME2(7) = NULL,
    @TotalPrice DECIMAL(18,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Bookings
        SET UserId = ISNULL(@UserId, UserId),
            RoomId = ISNULL(@RoomId, RoomId),
            CheckInDate = ISNULL(@CheckInDate, CheckInDate),
            CheckOutDate = ISNULL(@CheckOutDate, CheckOutDate),
            Status = ISNULL(@Status, Status),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE()),
            TotalPrice = ISNULL(@TotalPrice, TotalPrice)
        WHERE Id = @Id;

        SELECT 'Booking updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Bookings_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Bookings WHERE Id = @Id;
        SELECT 'Booking deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
