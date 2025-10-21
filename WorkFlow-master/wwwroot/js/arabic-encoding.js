// إعدادات الترميز العربي للواجهة الأمامية
// Arabic encoding settings for frontend

// إعدادات الترميز للنصوص العربية
document.addEventListener('DOMContentLoaded', function() {
    // تعيين الترميز للصفحة
    document.documentElement.setAttribute('lang', 'ar');
    document.documentElement.setAttribute('dir', 'rtl');
    
    // إعدادات الترميز للنماذج
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.setAttribute('accept-charset', 'UTF-8');
    });
    
    // إعدادات الترميز لجميع حقول الإدخال
    const inputs = document.querySelectorAll('input[type="text"], input[type="password"], textarea');
    inputs.forEach(input => {
        input.setAttribute('dir', 'rtl');
        input.setAttribute('lang', 'ar');
    });
    
    // إعدادات AJAX للترميز العربي
    if (typeof $ !== 'undefined') {
        $.ajaxSetup({
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function(xhr) {
                xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
            }
        });
    }
});

// دالة لضمان الترميز الصحيح عند إرسال البيانات
function ensureArabicEncoding(data) {
    if (typeof data === 'string') {
        return encodeURIComponent(data);
    }
    return data;
}

// دالة لضمان الترميز الصحيح عند استقبال البيانات
function decodeArabicText(text) {
    try {
        return decodeURIComponent(text);
    } catch (e) {
        return text;
    }
}

// إعدادات إضافية للترميز
window.arabicEncoding = {
    ensureEncoding: ensureArabicEncoding,
    decodeText: decodeArabicText,
    setFormEncoding: function(form) {
        form.setAttribute('accept-charset', 'UTF-8');
        form.setAttribute('enctype', 'application/x-www-form-urlencoded');
    }
};
