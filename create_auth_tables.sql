-- إنشاء جدول UserInfo
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserInfo' AND xtype='U')
BEGIN
    CREATE TABLE [UserInfo] (
        [UserId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserName] nvarchar(50) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [UserIdentity] nvarchar(50) NOT NULL,
        [UserActive] bit NOT NULL DEFAULT 1,
        [CrDate] datetime NOT NULL DEFAULT GETDATE(),
        [CrUserID] int NOT NULL DEFAULT 1,
        [Password] nvarchar(255) NOT NULL
    );
END

-- إنشاء جدول Roles
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' AND xtype='U')
BEGIN
    CREATE TABLE [Roles] (
        [RoleID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [RoleName] nvarchar(50) NOT NULL,
        [RoleNameAr] nvarchar(50) NOT NULL,
        [RoleInUse] bit NOT NULL DEFAULT 1
    );
END

-- إنشاء جدول RoleUser
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RoleUser' AND xtype='U')
BEGIN
    CREATE TABLE [RoleUser] (
        [RoleUserId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [RoleID] int NOT NULL,
        [UserId] int NOT NULL,
        [CrDate] datetime NOT NULL DEFAULT GETDATE(),
        [CrUserId] int NOT NULL DEFAULT 1,
        FOREIGN KEY ([RoleID]) REFERENCES [Roles]([RoleID]),
        FOREIGN KEY ([UserId]) REFERENCES [UserInfo]([UserId])
    );
END

-- إضافة بيانات تجريبية
-- إضافة مستخدم تجريبي (كلمة المرور: 123456)
IF NOT EXISTS (SELECT * FROM [UserInfo] WHERE [UserName] = 'admin')
BEGIN
    INSERT INTO [UserInfo] ([UserName], [FullName], [UserIdentity], [UserActive], [CrDate], [CrUserID], [Password]) 
    VALUES ('admin', 'مدير النظام', 'ADMIN001', 1, GETDATE(), 1, 'e10adc3949ba59abbe56e057f20f883e');
END

-- إضافة أدوار تجريبية
IF NOT EXISTS (SELECT * FROM [Roles] WHERE [RoleName] = 'Admin')
BEGIN
    INSERT INTO [Roles] ([RoleName], [RoleNameAr], [RoleInUse]) 
    VALUES ('Admin', 'مدير', 1);
END

IF NOT EXISTS (SELECT * FROM [Roles] WHERE [RoleName] = 'User')
BEGIN
    INSERT INTO [Roles] ([RoleName], [RoleNameAr], [RoleInUse]) 
    VALUES ('User', 'مستخدم', 1);
END

-- ربط المستخدم بالأدوار
DECLARE @AdminUserId int = (SELECT [UserId] FROM [UserInfo] WHERE [UserName] = 'admin');
DECLARE @AdminRoleId int = (SELECT [RoleID] FROM [Roles] WHERE [RoleName] = 'Admin');

IF NOT EXISTS (SELECT * FROM [RoleUser] WHERE [UserId] = @AdminUserId AND [RoleID] = @AdminRoleId)
BEGIN
    INSERT INTO [RoleUser] ([RoleID], [UserId], [CrDate], [CrUserId]) 
    VALUES (@AdminRoleId, @AdminUserId, GETDATE(), 1);
END

PRINT 'تم إنشاء الجداول والبيانات التجريبية بنجاح!';
