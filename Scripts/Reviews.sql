CREATE PROCEDURE [dbo].[Reviews_Create]
    @BookingId INT,
    @Rating INT,
    @Comment NVARCHAR(500),
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Reviews WHERE BookingId = @BookingId)
        BEGIN
            RAISERROR('Review for this booking already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Reviews (BookingId, Rating, Comment, CreatedAt, UpdatedAt)
        VALUES (@BookingId, @Rating, @Comment, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Review created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Reviews_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT r.*, 
               b.UserId, b.RoomId
        FROM dbo.Reviews r
        INNER JOIN dbo.Bookings b ON r.BookingId = b.Id
        WHERE r.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Reviews_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT r.*, 
               b.UserId, b.RoomId
        FROM dbo.Reviews r
        INNER JOIN dbo.Bookings b ON r.BookingId = b.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Reviews_Update]
    @Id INT,
    @BookingId INT = NULL,
    @Rating INT = NULL,
    @Comment NVARCHAR(500) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Reviews
        SET BookingId = ISNULL(@BookingId, BookingId),
            Rating = ISNULL(@Rating, Rating),
            Comment = ISNULL(@Comment, Comment),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Review updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Reviews_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Reviews WHERE Id = @Id;
        SELECT 'Review deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
