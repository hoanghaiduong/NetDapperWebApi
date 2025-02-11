CREATE PROCEDURE [dbo].[Users_Create]
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @EmailVerified BIT = NULL,
    @Avatar NVARCHAR(MAX) = NULL,
    @RefreshToken NVARCHAR(MAX) = NULL,
    @IsDisabled BIT,
    @LastLogin DATETIME2(7) = NULL,
    @HotelId INT = NULL,
    @CreatedAt DATETIME2(7) = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Kiểm tra tồn tại dựa trên Email
        IF EXISTS (SELECT 1 FROM dbo.Users WHERE Email = @Email)
        BEGIN
            RAISERROR('User with this email already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.Users 
            (Email, PasswordHash, PhoneNumber, FirstName, LastName, EmailVerified, Avatar, RefreshToken, IsDisabled, LastLogin, HotelId, CreatedAt, UpdatedAt)
        VALUES 
            (@Email, @PasswordHash, @PhoneNumber, @FirstName, @LastName, @EmailVerified, @Avatar, @RefreshToken, @IsDisabled, @LastLogin, @HotelId, ISNULL(@CreatedAt, GETDATE()), @UpdatedAt);

        SELECT 'User created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Users_GetByID]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT u.*, 
               h.Name AS HotelName, 
               h.Address AS HotelAddress
        FROM dbo.Users u
        LEFT JOIN dbo.Hotels h ON u.HotelId = h.Id
        WHERE u.Id = @Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[Users_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT u.*, 
               h.Name AS HotelName, 
               h.Address AS HotelAddress
        FROM dbo.Users u
        LEFT JOIN dbo.Hotels h ON u.HotelId = h.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
CREATE PROCEDURE [dbo].[Users_Update]
    @Id INT OUTPUT,
    @Email NVARCHAR(255) = NULL,
    @PasswordHash NVARCHAR(255) = NULL,
    @PhoneNumber NVARCHAR(20) = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @EmailVerified BIT = NULL,
    @Avatar NVARCHAR(MAX) = NULL,
    @RefreshToken NVARCHAR(MAX) = NULL,
    @IsDisabled BIT = NULL,
    @LastLogin DATETIME2(7) = NULL,
    @HotelId INT = NULL,
    @UpdatedAt DATETIME2(7) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        UPDATE dbo.Users
        SET Email         = ISNULL(@Email, Email),
            PasswordHash  = ISNULL(@PasswordHash, PasswordHash),
            PhoneNumber   = ISNULL(@PhoneNumber, PhoneNumber),
            FirstName     = ISNULL(@FirstName, FirstName),
            LastName      = ISNULL(@LastName, LastName),
            EmailVerified = COALESCE(@EmailVerified, EmailVerified),
            Avatar        = ISNULL(@Avatar, Avatar),
            RefreshToken  = ISNULL(@RefreshToken, RefreshToken),
            IsDisabled    = COALESCE(@IsDisabled, IsDisabled),
            LastLogin     = ISNULL(@LastLogin, LastLogin),
            HotelId       = ISNULL(@HotelId, HotelId),
            UpdatedAt     = ISNULL(@UpdatedAt, GETDATE())
        WHERE Id = @Id;
        
        COMMIT TRANSACTION;
        SELECT @Id AS Id, 'User updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

GO

CREATE PROCEDURE [dbo].[Users_Delete]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.Users WHERE Id = @Id;
        SELECT 'User deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
