CREATE PROCEDURE [dbo].[RevenueReports_Create]
    @Month INT,
    @Year INT,
    @TotalRevenue DECIMAL(18,2),
    @TotalBookings INT,
    @TotalServicesUsed INT,
    @CreatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.RevenueReports WHERE Month = @Month AND Year = @Year)
        BEGIN
            RAISERROR('Revenue report for this month and year already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.RevenueReports (Month, Year, TotalRevenue, TotalBookings, TotalServicesUsed, CreatedAt)
        VALUES (@Month, @Year, @TotalRevenue, @TotalBookings, @TotalServicesUsed, ISNULL(@CreatedAt, GETDATE()));

        SELECT 'Revenue report created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RevenueReports_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.RevenueReports WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RevenueReports_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.RevenueReports;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RevenueReports_Update]
    @Id INT,
    @Month INT = NULL,
    @Year INT = NULL,
    @TotalRevenue DECIMAL(18,2) = NULL,
    @TotalBookings INT = NULL,
    @TotalServicesUsed INT = NULL,
    @CreatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.RevenueReports
        SET Month = ISNULL(@Month, Month),
            Year = ISNULL(@Year, Year),
            TotalRevenue = ISNULL(@TotalRevenue, TotalRevenue),
            TotalBookings = ISNULL(@TotalBookings, TotalBookings),
            TotalServicesUsed = ISNULL(@TotalServicesUsed, TotalServicesUsed),
            CreatedAt = ISNULL(@CreatedAt, CreatedAt)
        WHERE Id = @Id;

        SELECT 'Revenue report updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[RevenueReports_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.RevenueReports WHERE Id = @Id;
        SELECT 'Revenue report deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
