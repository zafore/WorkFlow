// WorkFlow System - Modern UI JavaScript Enhancements

$(document).ready(function() {
    // Initialize modern UI components
    initializeModernUI();
    
    // Initialize animations
    initializeAnimations();
    
    // Initialize form enhancements
    initializeFormEnhancements();
    
    // Initialize tooltips
    initializeTooltips();
});

// Initialize Modern UI Components
function initializeModernUI() {
    // Add loading states to buttons
    $('.btn-modern').on('click', function() {
        var $btn = $(this);
        var originalText = $btn.html();
        
        if (!$btn.hasClass('loading')) {
            $btn.addClass('loading');
            $btn.html('<i class="fas fa-spinner fa-spin me-2"></i>جاري التحميل...');
            
            // Remove loading state after 3 seconds (adjust as needed)
            setTimeout(function() {
                $btn.removeClass('loading');
                $btn.html(originalText);
            }, 3000);
        }
    });
    
    // Add ripple effect to buttons
    $('.btn-modern').on('click', function(e) {
        var $btn = $(this);
        var ripple = $('<span class="ripple"></span>');
        var rect = this.getBoundingClientRect();
        var size = Math.max(rect.width, rect.height);
        var x = e.clientX - rect.left - size / 2;
        var y = e.clientY - rect.top - size / 2;
        
        ripple.css({
            width: size,
            height: size,
            left: x,
            top: y
        });
        
        $btn.append(ripple);
        
        setTimeout(function() {
            ripple.remove();
        }, 600);
    });
    
    // Add hover effects to cards
    $('.card-modern').hover(
        function() {
            $(this).addClass('animate-slide-up');
        },
        function() {
            $(this).removeClass('animate-slide-up');
        }
    );
}

// Initialize Animations
function initializeAnimations() {
    // Animate elements on scroll
    $(window).on('scroll', function() {
        $('.animate-on-scroll').each(function() {
            var elementTop = $(this).offset().top;
            var elementBottom = elementTop + $(this).outerHeight();
            var viewportTop = $(window).scrollTop();
            var viewportBottom = viewportTop + $(window).height();
            
            if (elementBottom > viewportTop && elementTop < viewportBottom) {
                $(this).addClass('animate-fade-in');
            }
        });
    });
    
    // Add stagger animation to lists
    $('.animate-stagger li').each(function(index) {
        $(this).css('animation-delay', (index * 0.1) + 's');
    });
}

// Initialize Form Enhancements
function initializeFormEnhancements() {
    // Add floating labels
    $('.form-control, .form-select').on('focus blur', function() {
        var $this = $(this);
        var $label = $this.siblings('label');
        
        if ($this.val() || $this.is(':focus')) {
            $label.addClass('floating');
        } else {
            $label.removeClass('floating');
        }
    });
    
    // Add real-time validation
    $('input[required], select[required], textarea[required]').on('blur', function() {
        validateField($(this));
    });
    
    // Add form submission enhancement
    $('form').on('submit', function(e) {
        var $form = $(this);
        var isValid = true;
        
        // Validate all required fields
        $form.find('input[required], select[required], textarea[required]').each(function() {
            if (!validateField($(this))) {
                isValid = false;
            }
        });
        
        if (!isValid) {
            e.preventDefault();
            showValidationError();
        }
    });
}

// Validate individual field
function validateField($field) {
    var value = $field.val();
    var isValid = value && value.trim() !== '';
    
    if (isValid) {
        $field.removeClass('is-invalid').addClass('is-valid');
        $field.siblings('.invalid-feedback').hide();
    } else {
        $field.removeClass('is-valid').addClass('is-invalid');
        $field.siblings('.invalid-feedback').show();
    }
    
    return isValid;
}

// Show validation error message
function showValidationError() {
    Swal.fire({
        title: 'تنبيه!',
        text: 'يرجى ملء جميع الحقول المطلوبة بشكل صحيح',
        icon: 'warning',
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#2563eb'
    });
}

// Initialize Tooltips
function initializeTooltips() {
    // Initialize Bootstrap tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    // Add custom tooltip functionality
    $('.tooltip-modern').hover(
        function() {
            $(this).addClass('show');
        },
        function() {
            $(this).removeClass('show');
        }
    );
}

// Enhanced AJAX functions
function ajaxRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        beforeSend: function() {
            showLoadingOverlay();
        },
        success: function(response) {
            hideLoadingOverlay();
            if (successCallback) successCallback(response);
        },
        error: function(xhr, status, error) {
            hideLoadingOverlay();
            if (errorCallback) {
                errorCallback(xhr, status, error);
            } else {
                showErrorMessage('حدث خطأ أثناء تنفيذ العملية');
            }
        }
    });
}

// Show loading overlay
function showLoadingOverlay() {
    if ($('#loadingOverlay').length === 0) {
        $('body').append(`
            <div id="loadingOverlay" class="loading-overlay">
                <div class="loading-content">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">جاري التحميل...</span>
                    </div>
                    <p class="mt-3">جاري التحميل...</p>
                </div>
            </div>
        `);
    }
    $('#loadingOverlay').show();
}

// Hide loading overlay
function hideLoadingOverlay() {
    $('#loadingOverlay').hide();
}

// Show success message
function showSuccessMessage(message) {
    Swal.fire({
        title: 'تم بنجاح!',
        text: message,
        icon: 'success',
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#10b981'
    });
}

// Show error message
function showErrorMessage(message) {
    Swal.fire({
        title: 'خطأ!',
        text: message,
        icon: 'error',
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#ef4444'
    });
}

// Show warning message
function showWarningMessage(message) {
    Swal.fire({
        title: 'تنبيه!',
        text: message,
        icon: 'warning',
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#f59e0b'
    });
}

// Show confirmation dialog
function showConfirmationDialog(title, text, confirmCallback) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'نعم، تأكيد',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#2563eb',
        cancelButtonColor: '#6c757d'
    }).then((result) => {
        if (result.isConfirmed && confirmCallback) {
            confirmCallback();
        }
    });
}

// Enhanced table functionality
function initializeDataTable(tableId, options = {}) {
    var defaultOptions = {
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Arabic.json"
        },
        "responsive": true,
        "pageLength": 10,
        "order": [[0, "desc"]],
        "dom": '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>' +
               '<"row"<"col-sm-12"tr>>' +
               '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        "drawCallback": function() {
            // Add animations to table rows
            $('tbody tr').addClass('animate-slide-right');
        }
    };
    
    var mergedOptions = $.extend(defaultOptions, options);
    return $(tableId).DataTable(mergedOptions);
}

// Enhanced form validation
function validateForm(formId) {
    var $form = $('#' + formId);
    var isValid = true;
    var firstInvalidField = null;
    
    $form.find('input[required], select[required], textarea[required]').each(function() {
        var $field = $(this);
        var value = $field.val();
        
        if (!value || value.trim() === '') {
            $field.addClass('is-invalid');
            if (!firstInvalidField) {
                firstInvalidField = $field;
            }
            isValid = false;
        } else {
            $field.removeClass('is-invalid').addClass('is-valid');
        }
    });
    
    if (!isValid && firstInvalidField) {
        $('html, body').animate({
            scrollTop: firstInvalidField.offset().top - 100
        }, 500);
        firstInvalidField.focus();
    }
    
    return isValid;
}

// Enhanced file upload
function initializeFileUpload(inputId, options = {}) {
    var $input = $('#' + inputId);
    var $preview = $('<div class="file-preview"></div>');
    var $progress = $('<div class="upload-progress"></div>');
    
    $input.after($preview);
    $preview.after($progress);
    
    $input.on('change', function() {
        var files = this.files;
        $preview.empty();
        
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            var $fileItem = $('<div class="file-item"></div>');
            
            if (file.type.startsWith('image/')) {
                var reader = new FileReader();
                reader.onload = function(e) {
                    $fileItem.html('<img src="' + e.target.result + '" class="file-thumbnail">');
                };
                reader.readAsDataURL(file);
            } else {
                $fileItem.html('<i class="fas fa-file me-2"></i>' + file.name);
            }
            
            $fileItem.append('<span class="file-size">' + formatFileSize(file.size) + '</span>');
            $preview.append($fileItem);
        }
    });
}

// Format file size
function formatFileSize(bytes) {
    if (bytes === 0) return '0 Bytes';
    var k = 1024;
    var sizes = ['Bytes', 'KB', 'MB', 'GB'];
    var i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
}

// Enhanced search functionality
function initializeSearch(searchInputId, targetSelector, options = {}) {
    var $input = $('#' + searchInputId);
    var $target = $(targetSelector);
    
    $input.on('input', function() {
        var searchTerm = $(this).val().toLowerCase();
        
        $target.each(function() {
            var $item = $(this);
            var text = $item.text().toLowerCase();
            
            if (text.includes(searchTerm)) {
                $item.show().addClass('animate-fade-in');
            } else {
                $item.hide();
            }
        });
    });
}

// Enhanced pagination
function initializePagination(containerId, options = {}) {
    var $container = $('#' + containerId);
    var currentPage = 1;
    var itemsPerPage = options.itemsPerPage || 10;
    var $items = $container.find('.pagination-item');
    var totalPages = Math.ceil($items.length / itemsPerPage);
    
    function showPage(page) {
        var start = (page - 1) * itemsPerPage;
        var end = start + itemsPerPage;
        
        $items.hide();
        $items.slice(start, end).show().addClass('animate-slide-up');
        
        updatePaginationControls(page);
    }
    
    function updatePaginationControls(page) {
        var $controls = $container.find('.pagination-controls');
        if ($controls.length === 0) {
            $controls = $('<div class="pagination-controls"></div>');
            $container.after($controls);
        }
        
        $controls.empty();
        
        // Previous button
        if (page > 1) {
            $controls.append('<button class="btn btn-outline-primary btn-sm me-2" onclick="goToPage(' + (page - 1) + ')">السابق</button>');
        }
        
        // Page numbers
        for (var i = 1; i <= totalPages; i++) {
            var activeClass = i === page ? 'btn-primary' : 'btn-outline-primary';
            $controls.append('<button class="btn ' + activeClass + ' btn-sm me-1" onclick="goToPage(' + i + ')">' + i + '</button>');
        }
        
        // Next button
        if (page < totalPages) {
            $controls.append('<button class="btn btn-outline-primary btn-sm ms-2" onclick="goToPage(' + (page + 1) + ')">التالي</button>');
        }
    }
    
    window.goToPage = function(page) {
        currentPage = page;
        showPage(page);
    };
    
    showPage(1);
}

// Utility functions
function debounce(func, wait) {
    var timeout;
    return function executedFunction() {
        var later = function() {
            clearTimeout(timeout);
            func();
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

function throttle(func, limit) {
    var inThrottle;
    return function() {
        var args = arguments;
        var context = this;
        if (!inThrottle) {
            func.apply(context, args);
            inThrottle = true;
            setTimeout(function() {
                inThrottle = false;
            }, limit);
        }
    };
}

// Export functions for global use
window.WorkFlowUI = {
    ajaxRequest: ajaxRequest,
    showLoadingOverlay: showLoadingOverlay,
    hideLoadingOverlay: hideLoadingOverlay,
    showSuccessMessage: showSuccessMessage,
    showErrorMessage: showErrorMessage,
    showWarningMessage: showWarningMessage,
    showConfirmationDialog: showConfirmationDialog,
    initializeDataTable: initializeDataTable,
    validateForm: validateForm,
    initializeFileUpload: initializeFileUpload,
    initializeSearch: initializeSearch,
    initializePagination: initializePagination,
    debounce: debounce,
    throttle: throttle
};
