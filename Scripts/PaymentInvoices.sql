CREATE PROCEDURE [dbo].[PaymentInvoices_Create]
    @PaymentId INT,
    @InvoiceId INT,
    @Amount DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.PaymentInvoices WHERE PaymentId = @PaymentId AND InvoiceId = @InvoiceId)
        BEGIN
            RAISERROR('PaymentInvoice record already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.PaymentInvoices (PaymentId, InvoiceId, Amount)
        VALUES (@PaymentId, @InvoiceId, @Amount);

        SELECT 'PaymentInvoice created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[PaymentInvoices_GetByID]
    @PaymentId INT,
    @InvoiceId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT pi.*, 
               p.Amount AS PaymentAmount, 
               i.TotalAmount AS InvoiceTotalAmount
        FROM dbo.PaymentInvoices pi
        INNER JOIN dbo.Payments p ON pi.PaymentId = p.Id
        INNER JOIN dbo.Invoices i ON pi.InvoiceId = i.Id
        WHERE pi.PaymentId = @PaymentId AND pi.InvoiceId = @InvoiceId;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[PaymentInvoices_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT pi.*, 
               p.Amount AS PaymentAmount, 
               i.TotalAmount AS InvoiceTotalAmount
        FROM dbo.PaymentInvoices pi
        INNER JOIN dbo.Payments p ON pi.PaymentId = p.Id
        INNER JOIN dbo.Invoices i ON pi.InvoiceId = i.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[PaymentInvoices_Update]
    @PaymentId INT,
    @InvoiceId INT,
    @Amount DECIMAL(18,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.PaymentInvoices
        SET Amount = ISNULL(@Amount, Amount)
        WHERE PaymentId = @PaymentId AND InvoiceId = @InvoiceId;

        SELECT 'PaymentInvoice updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[PaymentInvoices_Delete]
    @PaymentId INT,
    @InvoiceId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.PaymentInvoices
        WHERE PaymentId = @PaymentId AND InvoiceId = @InvoiceId;
        SELECT 'PaymentInvoice deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
