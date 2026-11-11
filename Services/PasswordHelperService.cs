using System.Security.Cryptography;

namespace APPC.Services
{
    public interface IPasswordHelperService
    {
        (string passwordHash, string passwordSalt) CreatePasswordHashWithSalt(string password);
        bool VerifyPasswordHash(string enteredPassword, string storedHash, string storedSalt);
    }
    public class PasswordHelperService : IPasswordHelperService
    {
        public (string passwordHash,string passwordSalt) CreatePasswordHashWithSalt(string password)
        {
            // 1. First generate a random salt
            var salt = new byte[128 / 8];

            using (var randomNumber = RandomNumberGenerator.Create()) 
            {
                randomNumber.GetBytes(salt);
            }

            string saltBase64 = Convert.ToBase64String(salt);
            
            // 2. Genrate the password hash using the salt and the password
            
            var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            
            string hashBase64 = Convert.ToBase64String(hash.GetBytes(8));

            //3. Return the password hash and salt as a tuple
            return (hashBase64, saltBase64);
        }

        public bool VerifyPasswordHash(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using(var hash = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                string enteredHash = Convert.ToBase64String(hash.GetBytes(8));
                return enteredHash == storedHash;
            }
        }

    }
}
