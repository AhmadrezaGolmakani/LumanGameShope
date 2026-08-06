namespace Luman.Busines.Utility
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // BCrypt خودش یک Salt تصادفی تولید و داخل خروجی ذخیره می‌کنه
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}