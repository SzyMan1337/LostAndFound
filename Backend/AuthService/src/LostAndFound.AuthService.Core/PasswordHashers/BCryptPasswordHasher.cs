using Microsoft.AspNetCore.Identity;

namespace LostAndFound.AuthService.Core.PasswordHashers
{
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        private const int workFactor = 12;

        public string HashPassword(TUser user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

        public PasswordVerificationResult VerifyHashedPassword(
          TUser user, string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            if (string.IsNullOrWhiteSpace(providedPassword))
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, workFactor))
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }

            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
