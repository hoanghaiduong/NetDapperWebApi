CREATE PROCEDURE [dbo].[Invoices_Create]
    @BookingId INT,
    @TotalAmount DECIMAL(18,2),
    @PaidAmount DECIMAL(18,2),
    @DueAmount DECIMAL(18,2),
    @Status NVARCHAR(50),
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Tùy chọn: Kiểm tra nếu đã có Invoice cho Booking này
        IF EXISTS(SELECT 1 FROM dbo.Invoices WHERE BookingId = @BookingId)
        BEGIN
            RAISERROR('Invoice for this booking already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Invoices (BookingId, TotalAmount, PaidAmount, DueAmount, Status, CreatedAt, UpdatedAt)
        VALUES (@BookingId, @TotalAmount, @PaidAmount, @DueAmount, @Status, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Invoice created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Invoices_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT i.*, b.UserId, b.RoomId
        FROM dbo.Invoices i
        INNER JOIN dbo.Bookings b ON i.BookingId = b.Id
        WHERE i.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Invoices_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT i.*, b.UserId, b.RoomId
        FROM dbo.Invoices i
        INNER JOIN dbo.Bookings b ON i.BookingId = b.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Invoices_Update]
    @Id INT,
    @BookingId INT = NULL,
    @TotalAmount DECIMAL(18,2) = NULL,
    @PaidAmount DECIMAL(18,2) = NULL,
    @DueAmount DECIMAL(18,2) = NULL,
    @Status NVARCHAR(50) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Invoices
        SET BookingId = ISNULL(@BookingId, BookingId),
            TotalAmount = ISNULL(@TotalAmount, TotalAmount),
            PaidAmount = ISNULL(@PaidAmount, PaidAmount),
            DueAmount = ISNULL(@DueAmount, DueAmount),
            Status = ISNULL(@Status, Status),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Invoice updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Invoices_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Invoices WHERE Id = @Id;
        SELECT 'Invoice deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
