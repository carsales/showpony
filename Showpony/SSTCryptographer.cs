/*
 * Taken from the Code Project article "Simple String Encryption and Decryption with Source Code", by "Lovely M"
 * http://www.codeproject.com/Articles/23769/Simple-String-Encryption-and-Decryption-with-Sourc
 * 
 * Licensed under the The Code Project Open License (CPOL) 1.02
 * http://www.codeproject.com/info/cpol10.aspx
 * 
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace Showpony
{
    /// <summary>
    /// Summary description for SSTCryptographer
    /// </summary>
    public class SstCryptographer
    {
        private static string _key;

        public static string Key
        {
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// Encrypt the given string using the default key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt)
        {
            try
            {
                return Encrypt(strToEncrypt, _key);
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }

        }

        /// <summary>
        /// Decrypt the given string using the default key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted)
        {
            try
            {
                return Decrypt(strEncrypted, _key);
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            var objDesCrypto = new TripleDESCryptoServiceProvider();
            var objHashMd5 = new MD5CryptoServiceProvider();

            var strTempKey = strKey;

            var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
            objDesCrypto.Key = byteHash;
            objDesCrypto.Mode = CipherMode.ECB; //CBC, CFB

            var byteBuff = Encoding.ASCII.GetBytes(strToEncrypt);
            return Convert.ToBase64String(objDesCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            var objDesCrypto = new TripleDESCryptoServiceProvider();
            var objHashMd5 = new MD5CryptoServiceProvider();

            var strTempKey = strKey;

            var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
            objDesCrypto.Key = byteHash;
            objDesCrypto.Mode = CipherMode.ECB; //CBC, CFB

            var byteBuff = Convert.FromBase64String(strEncrypted);
            var strDecrypted = Encoding.ASCII.GetString(objDesCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

            return strDecrypted;
        }
    }
}
