using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Utils
{
    public class Encrypting
    {
		#region MD5
		public static string MD5Hash(string input)
		{
			using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
			{
				// Return the hexadecimal string.
				return GetMd5Hash(md5Hash, input);
			}
		}

		public static byte[] MD5Hash(byte[] bytes)
		{
			using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
			{
				// Return the hexadecimal string.
				return GetMd5Hash(md5Hash, bytes);
			}
		}

		public static bool MD5Verify(string input, string hash)
		{
			using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
			{
				// Hash the input.
				string hashOfInput = GetMd5Hash(md5Hash, input);

				// Create a StringComparer an compare the hashes.
				StringComparer comparer = StringComparer.OrdinalIgnoreCase;

				return (0 == comparer.Compare(hashOfInput, hash));
			}
		}

		private static string GetMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		private static byte[] GetMd5Hash(System.Security.Cryptography.MD5 md5Hash, byte[] bytes)
		{

			// Convert the input string to a byte array and compute the hash.
			return md5Hash.ComputeHash(bytes);
		}
		#endregion

		#region AES
		public static string AesEncrypt(string value, string key, string salt)
		{
			return AesEncrypt(value, key, salt, Encoding.Unicode);
		}

		public static string AesEncrypt(string value, string key, string salt, Encoding encoding)
		{
			salt = GetSalt(salt);
			DeriveBytes rgb = new Rfc2898DeriveBytes(key, Encoding.Unicode.GetBytes(salt));

			byte[] rgbKey = rgb.GetBytes(256 >> 3);
			byte[] rgbIV = rgb.GetBytes(128 >> 3);

			return AesEncrypt(value, rgbKey, rgbIV, Encoding.Unicode);

			//using (Aes aes = Aes.Create())
			//{
			//    byte[] rgbKey = rgb.GetBytes(aes.KeySize >> 3);
			//    byte[] rgbIV = rgb.GetBytes(aes.BlockSize >> 3);

			//    ICryptoTransform transform = aes.CreateEncryptor(rgbKey, rgbIV);

			//    using (MemoryStream buffer = new MemoryStream())
			//    {
			//        using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
			//        {
			//            using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
			//            {
			//                writer.Write(value);
			//            }
			//        }

			//        return Convert.ToBase64String(buffer.ToArray());
			//    }
			//}
		}

		/// <summary>
		/// Encryting input string with Aes crypto
		/// </summary>
		/// <param name="value">Input string</param>
		/// <param name="rgbKey">Secret key for the symmetric algorithm</param>
		/// <param name="rgbIV">Initialization vector (IV) for the symmetric algorithm</param>
		/// <param name="encoding">Input string encoding</param>
		/// <returns>Encrypted base64 string</returns>
		public static string AesEncrypt(string value, byte[] rgbKey, byte[] rgbIV, Encoding encoding)
		{
			using (Aes aes = Aes.Create())
			{
				ICryptoTransform transform = aes.CreateEncryptor(rgbKey, rgbIV);

				using (MemoryStream buffer = new MemoryStream())
				{
					using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
					{
						using (StreamWriter writer = new StreamWriter(stream, encoding))
						{
							writer.Write(value);
						}
					}

					return Convert.ToBase64String(buffer.ToArray());
				}
			}
		}

		public static string AesDecrypt(string text, string key, string salt)
		{
			return AesDecrypt(text, key, salt, Encoding.Unicode);
		}

		public static string AesDecrypt(string text, string key, string salt, Encoding encoding)
		{
			salt = GetSalt(salt);
			DeriveBytes rgb = new Rfc2898DeriveBytes(key, encoding.GetBytes(salt));

			byte[] rgbKey = rgb.GetBytes(256 >> 3);
			byte[] rgbIV = rgb.GetBytes(128 >> 3);

			return AesDecrypt(text, rgbKey, rgbIV, encoding);
		}

		/// <summary>
		/// Decryting input string with Aes crypto
		/// </summary>
		/// <param name="text">Input base64 encrypted string</param>
		/// <param name="rgbKey">Secret key for the symmetric algorithm</param>
		/// <param name="rgbIV">Initialization vector (IV) for the symmetric algorithm</param>
		/// <param name="encoding">Input string encoding</param>
		/// <returns>Decrypted string</returns>
		public static string AesDecrypt(string text, byte[] rgbKey, byte[] rgbIV, Encoding encoding)
		{
			using (Aes aes = Aes.Create())
			{
				ICryptoTransform transform = aes.CreateDecryptor(rgbKey, rgbIV);

				using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
				{
					using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
					{
						using (StreamReader reader = new StreamReader(stream, encoding))
						{
							return reader.ReadToEnd();
						}
					}
				}
			}
		}

		private static string GetSalt(string salt)
		{
			if (string.IsNullOrEmpty(salt))
				return "fpt00001";

			if (salt.Length < 8)
				return salt + "fpt00001";

			return salt;
		}
		#endregion
		public static string DecryptStringAES(string encryptedValue, string key, string idv)
        {
            var keybytes = Encoding.UTF8.GetBytes(key);
            var iv = Encoding.UTF8.GetBytes(idv);

            //DECRYPT FROM CRIPTOJS
            var encrypted = Convert.FromBase64String(encryptedValue);
            var decryptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);

            return decryptedFromJavascript;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
