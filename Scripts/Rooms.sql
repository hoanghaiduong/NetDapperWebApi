CREATE PROCEDURE [dbo].[Rooms_Create]
    @HotelID INT,
    @RoomTypeId INT,
    @RoomNumber NVARCHAR(20),
    @Thumbnail NVARCHAR(MAX),
    @Images NVARCHAR(MAX),
    @Price DECIMAL(18,2),
    @Status INT,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.Rooms WHERE HotelID = @HotelID AND RoomNumber = @RoomNumber)
        BEGIN
            RAISERROR('Room with this number already exists in the hotel.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Rooms (HotelID, RoomTypeId, RoomNumber, Thumbnail, Images, Price, Status, CreatedAt, UpdatedAt)
        VALUES (@HotelID, @RoomTypeId, @RoomNumber, @Thumbnail, @Images, @Price, @Status, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'Room created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Rooms_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT r.*, 
               h.Name AS HotelName, 
               rt.Name AS RoomTypeName
        FROM dbo.Rooms r
        INNER JOIN dbo.Hotels h ON r.HotelID = h.Id
        INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.Id
        WHERE r.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Rooms_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT r.*, 
               h.Name AS HotelName, 
               rt.Name AS RoomTypeName
        FROM dbo.Rooms r
        INNER JOIN dbo.Hotels h ON r.HotelID = h.Id
        INNER JOIN dbo.RoomTypes rt ON r.RoomTypeId = rt.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Rooms_Update]
    @Id INT,
    @HotelID INT = NULL,
    @RoomTypeId INT = NULL,
    @RoomNumber NVARCHAR(20) = NULL,
    @Thumbnail NVARCHAR(MAX) = NULL,
    @Images NVARCHAR(MAX) = NULL,
    @Price DECIMAL(18,2) = NULL,
    @Status INT = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Rooms
        SET HotelID = ISNULL(@HotelID, HotelID),
            RoomTypeId = ISNULL(@RoomTypeId, RoomTypeId),
            RoomNumber = ISNULL(@RoomNumber, RoomNumber),
            Thumbnail = ISNULL(@Thumbnail, Thumbnail),
            Images = ISNULL(@Images, Images),
            Price = ISNULL(@Price, Price),
            Status = ISNULL(@Status, Status),
            UpdatedAt = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;

        SELECT 'Room updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Rooms_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Rooms WHERE Id = @Id;
        SELECT 'Room deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
