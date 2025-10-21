-- إصلاح إعدادات الترميز العربي في قاعدة البيانات
-- Fix Arabic encoding settings in the database

-- تغيير Collation لقاعدة البيانات
ALTER DATABASE [db_aba915_workflow] COLLATE Arabic_100_CI_AI;

-- إصلاح جداول النصوص العربية الرئيسية
-- Fix main Arabic text tables

-- جدول UserInfo
ALTER TABLE [UserInfo] 
ALTER COLUMN [UserName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [UserInfo] 
ALTER COLUMN [FullName] NVARCHAR(100) COLLATE Arabic_100_CI_AI;

ALTER TABLE [UserInfo] 
ALTER COLUMN [UserIdentity] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Roles
ALTER TABLE [Role] 
ALTER COLUMN [RoleName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Role] 
ALTER COLUMN [RoleNameAr] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Applications
ALTER TABLE [Applications] 
ALTER COLUMN [Application_Name_Ar] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Applications] 
ALTER COLUMN [Application_Name_Eng] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Department
ALTER TABLE [Department] 
ALTER COLUMN [Department_Name_Ar] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Department] 
ALTER COLUMN [Department_Name_Eng] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Section
ALTER TABLE [Section] 
ALTER COLUMN [Section_Name_Eng] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Application_Levels
ALTER TABLE [Application_Levels] 
ALTER COLUMN [Application_Level_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Request_Status
ALTER TABLE [Request_Status] 
ALTER COLUMN [Request_Status_Ar] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Request_Status] 
ALTER COLUMN [Request_Status_Eng] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Request_Details_Status
ALTER TABLE [Request_Details_Status] 
ALTER COLUMN [Request_Details_Status_Ar] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Request_Details_Status] 
ALTER COLUMN [Request_Details_Status_Eng] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Actions
ALTER TABLE [Action] 
ALTER COLUMN [ActionName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Action] 
ALTER COLUMN [ActionNameArabic] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Archives_master
ALTER TABLE [Archives_master] 
ALTER COLUMN [Archives_master_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Archives_master] 
ALTER COLUMN [Archives_master_Note] NTEXT COLLATE Arabic_100_CI_AI;

-- جدول Employees
ALTER TABLE [Employee] 
ALTER COLUMN [Employee_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Employee] 
ALTER COLUMN [CivilRankName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Employee] 
ALTER COLUMN [Military_Rank_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Tools
ALTER TABLE [Tool] 
ALTER COLUMN [Tool_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Tool] 
ALTER COLUMN [Tool_value] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Shared_Table
ALTER TABLE [Shared_Table] 
ALTER COLUMN [Shared_Table_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Shared_Table] 
ALTER COLUMN [Shared_Table_Name_displayed] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Shared_Table] 
ALTER COLUMN [Shared_Table_Value] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول _SharedTableData
ALTER TABLE [_SharedTableData] 
ALTER COLUMN [Drop_Name] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول Notification
ALTER TABLE [Notification] 
ALTER COLUMN [ControllerName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

ALTER TABLE [Notification] 
ALTER COLUMN [PageName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول NotificationApp
ALTER TABLE [NotificationApp] 
ALTER COLUMN [NotificationAppName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول ManagerType
ALTER TABLE [ManagerType] 
ALTER COLUMN [ManagerTypeName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول RelativeType
ALTER TABLE [RelativeType] 
ALTER COLUMN [RelativeTypeName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول AttachmentsType
ALTER TABLE [AttachmentsType] 
ALTER COLUMN [AttachTypeName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

-- جدول SystemInfo
ALTER TABLE [SystemInfo] 
ALTER COLUMN [SystemInfoName] NVARCHAR(50) COLLATE Arabic_100_CI_AI;

PRINT 'تم إصلاح إعدادات الترميز العربي بنجاح!';
PRINT 'Arabic encoding settings have been fixed successfully!';
