CREATE PROCEDURE [dbo].[UserRoles_Create]
    @UserId INT,
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM dbo.UserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
        BEGIN
            RAISERROR('UserRole already exists.',16,1);
            RETURN;
        END

        INSERT INTO dbo.UserRoles (UserId, RoleId)
        VALUES (@UserId, @RoleId);

        SELECT 'UserRole created successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[UserRoles_GetByID]
    @UserId INT,
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT ur.*, 
               u.Email, 
               r.Name AS RoleName
        FROM dbo.UserRoles ur
        INNER JOIN dbo.Users u ON ur.UserId = u.Id
        INNER JOIN dbo.Roles r ON ur.RoleId = r.Id
        WHERE ur.UserId = @UserId AND ur.RoleId = @RoleId;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[UserRoles_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT ur.*, 
               u.Email, 
               r.Name AS RoleName
        FROM dbo.UserRoles ur
        INNER JOIN dbo.Users u ON ur.UserId = u.Id
        INNER JOIN dbo.Roles r ON ur.RoleId = r.Id;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

-- Update cho bảng many-to-many không thường dùng; tuy nhiên, nếu cần ví dụ cập nhật Role cho User:
CREATE PROCEDURE [dbo].[UserRoles_Update]
    @UserId INT,
    @OldRoleId INT,
    @NewRoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.UserRoles
        SET RoleId = @NewRoleId
        WHERE UserId = @UserId AND RoleId = @OldRoleId;

        SELECT 'UserRole updated successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

CREATE PROCEDURE [dbo].[UserRoles_Delete]
    @UserId INT,
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.UserRoles WHERE UserId = @UserId AND RoleId = @RoleId;
        SELECT 'UserRole deleted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO
