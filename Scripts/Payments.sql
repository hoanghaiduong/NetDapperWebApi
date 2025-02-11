CREATE PROCEDURE [dbo].[Payments_Create]
    @Amount DECIMAL(18,2),
    @PaymentMethod INT,
    @Status INT,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL,
    @PaymentDate DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.Payments (Amount, PaymentMethod, Status, CreatedAt, UpdatedAt, PaymentDate)
        VALUES (@Amount, @PaymentMethod, @Status, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt, @PaymentDate);

        SELECT 'Payment created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Payments_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Payments WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Payments_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.Payments;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Payments_Update]
    @Id INT,
    @Amount DECIMAL(18,2) = NULL,
    @PaymentMethod INT = NULL,
    @Status INT = NULL,
    @PaymentDate DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Payments
        SET Amount = ISNULL(@Amount, Amount),
            PaymentMethod = ISNULL(@PaymentMethod, PaymentMethod),
            Status = ISNULL(@Status, Status),
            PaymentDate = ISNULL(@PaymentDate, PaymentDate),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Payment updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Payments_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Payments WHERE Id = @Id;
        SELECT 'Payment deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
