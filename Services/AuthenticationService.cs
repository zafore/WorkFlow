using Microsoft.EntityFrameworkCore;
using WorkFlow.Models;
using System.Security.Cryptography;
using System.Text;

namespace WorkFlow.Services
{
    public class AuthenticationService
    {
        private readonly Workflow2Context _context;

        public AuthenticationService(Workflow2Context context)
        {
            _context = context;
        }

        public async Task<UserInfo?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.UserInfos
                .Where(u => u.UserName == username )
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            // تحقق من كلمة المرور (يمكنك استخدام التشفير المناسب)
            //if (VerifyPassword(password, user.Password))
            //{
                return user;
            //}

            return null;
        }

        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.RoleUsers
                .Where(ru => ru.UserId == userId)
                .Include(ru => ru.Role)
                .Select(ru => ru.Role)
                .Where(r => r.RoleInUse==true)
                .ToListAsync();
        }

        public async Task<bool> HasRoleAsync(int userId, string roleName)
        {
            return await _context.RoleUsers
                .Where(ru => ru.UserId == userId)
                .Include(ru => ru.Role)
                .AnyAsync(ru => ru.Role.RoleName == roleName );
        }

        public async Task<UserInfo?> GetUserByIdAsync(int userId)
        {
            return await _context.UserInfos
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserInfo?> GetUserByUsernameAsync(string username)
        {
            return await _context.UserInfos
                .Where(u => u.UserName == username && u.UserActive==true)
                .FirstOrDefaultAsync();
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // يمكنك استخدام SHA256 أو BCrypt للتشفير
            // هذا مثال بسيط - يجب استخدام تشفير أقوى في الإنتاج
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedString = Convert.ToBase64String(hashedBytes);
                return hashedString == hashedPassword;
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}