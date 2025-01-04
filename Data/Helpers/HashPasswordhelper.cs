using System.Security.Cryptography;
using System.Text;
namespace Apoint_pro.Data.Helpers
{
    public class HashPasswordhelper
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // 16 bytes of salt
                rng.GetBytes(salt); // Fill the salt with random bytes

                // Hash the password with the salt using PBKDF2
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000)) // 10,000 iterations
                {
                    byte[] hash = pbkdf2.GetBytes(20); // Generate a 20-byte hash

                    // Combine salt and hash into a single byte array
                    byte[] hashBytes = new byte[36]; // Salt(16 bytes) + Hash(20 bytes)
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);

                    // Return the hash as a Base64 string
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        // Method to verify if the password matches the hashed password
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Convert the stored hash to byte array
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash (first 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Use PBKDF2 to hash the entered password with the extracted salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Compare the new hash with the stored hash
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false; // Password doesn't match
                    }
                }
            }

            return true; // Password matches
        }
    }
}
