# نظام إدارة سير العمل (Workflow Management System)

## نظرة عامة
نظام إدارة سير العمل مبني باستخدام ASP.NET Core 7 مع Entity Framework Core و SQL Server. النظام يدعم المصادقة المخصصة ويحتوي على تصميم حديث بألوان خضراء.

## الميزات الرئيسية
- ✅ نظام مصادقة مخصص باستخدام جداول UserInfo و RoleUser
- ✅ تصميم حديث ومتجاوب مع درجات اللون الأخضر
- ✅ إدارة التطبيقات والطلبات
- ✅ نظام أدوار المستخدمين
- ✅ واجهة مستخدم عربية (RTL)

## متطلبات النظام
- .NET 7.0 أو أحدث
- SQL Server
- Visual Studio 2022 أو Visual Studio Code

## إعداد النظام

### 1. إعداد قاعدة البيانات
تأكد من أن قاعدة البيانات `workflow2` موجودة وأن الاتصال يعمل بشكل صحيح.

### 2. إنشاء الجداول المطلوبة
قم بتشغيل ملف `create_auth_tables.sql` في قاعدة البيانات لإنشاء الجداول التالية:
- `UserInfo` - جدول المستخدمين
- `Roles` - جدول الأدوار  
- `RoleUser` - جدول ربط المستخدمين بالأدوار

```bash
# تشغيل الملف باستخدام sqlcmd
sqlcmd -S (local) -d workflow2 -i create_auth_tables.sql
```

أو يمكنك تشغيل الملف مباشرة في SQL Server Management Studio.

### 3. بيانات تجريبية
سيتم إنشاء البيانات التجريبية التالية تلقائياً عند تشغيل ملف `create_auth_tables.sql`:
- مستخدم: `admin` / كلمة المرور: `123456`
- أدوار: `Admin` و `User`
- ربط المستخدم `admin` بدور `Admin`

### 4. تشغيل النظام
```bash
# استنساخ المشروع
git clone <repository-url>
cd WorkFlow

# تثبيت الحزم
dotnet restore

# بناء المشروع
dotnet build

# تشغيل النظام
dotnet run
```

## استخدام النظام

### تسجيل الدخول
1. افتح المتصفح وانتقل إلى `https://localhost:7230`
2. اضغط على "تسجيل الدخول" في الشريط العلوي
3. استخدم البيانات التالية:
   - اسم المستخدم: `admin`
   - كلمة المرور: `123456`

### الصفحات الرئيسية
- **الصفحة الرئيسية**: لوحة تحكم مع إحصائيات سريعة
- **التطبيقات**: إدارة التطبيقات وأنظمتها
- **الطلبات الجديدة**: إنشاء طلبات جديدة
- **تعديل التطبيقات**: إدارة تفاصيل التطبيقات

## الملفات المهمة

### النماذج (Models)
- `UserInfo.cs`: نموذج المستخدمين
- `Role.cs`: نموذج الأدوار
- `RoleUser.cs`: نموذج ربط المستخدمين بالأدوار
- `Application.cs`: نموذج التطبيقات
- `Request.cs`: نموذج الطلبات

### الخدمات (Services)
- `AuthenticationService.cs`: خدمة المصادقة والتفويض

### المتحكمات (Controllers)
- `AccountController.cs`: تسجيل الدخول والخروج
- `HomeController.cs`: الصفحة الرئيسية
- `ApplicationsController.cs`: إدارة التطبيقات
- `Requests.cs`: إدارة الطلبات

### العروض (Views)
- `Views/Account/Login.cshtml`: صفحة تسجيل الدخول
- `Views/Home/Index.cshtml`: الصفحة الرئيسية
- `Views/Applications/`: صفحات إدارة التطبيقات
- `Views/Requests/`: صفحات إدارة الطلبات

## التخصيص

### تغيير الألوان
يمكنك تعديل الألوان من خلال ملف `Views/Shared/_Layout.cshtml` في قسم CSS Variables:

```css
:root {
    --primary-color: #059669;
    --secondary-color: #6b7280;
    --success-color: #10b981;
    --gradient-primary: linear-gradient(135deg, #10b981 0%, #059669 50%, #047857 100%);
    /* ... المزيد من المتغيرات */
}
```

### إضافة أدوار جديدة
1. أضف الدور في جدول `Roles`
2. اربط المستخدمين بالأدوار في جدول `RoleUser`
3. استخدم `[Authorize(Roles = "RoleName")]` في المتحكمات

## استكشاف الأخطاء

### مشاكل شائعة
1. **خطأ في الاتصال بقاعدة البيانات**: تأكد من صحة connection string في `appsettings.json`
2. **خطأ في المصادقة**: تأكد من وجود البيانات التجريبية في قاعدة البيانات
3. **مشاكل في التصميم**: تأكد من تحميل ملفات CSS و JavaScript بشكل صحيح

### سجلات النظام
يمكنك مراجعة سجلات النظام في مجلد `logs` أو في وحدة التحكم عند تشغيل النظام.

## المساهمة
1. Fork المشروع
2. أنشئ branch جديد للميزة (`git checkout -b feature/AmazingFeature`)
3. Commit التغييرات (`git commit -m 'Add some AmazingFeature'`)
4. Push إلى Branch (`git push origin feature/AmazingFeature`)
5. افتح Pull Request

## الترخيص
هذا المشروع مرخص تحت رخصة MIT - راجع ملف [LICENSE](LICENSE) للتفاصيل.

## الدعم
إذا واجهت أي مشاكل أو لديك أسئلة، يرجى فتح issue في GitHub أو التواصل مع فريق التطوير.