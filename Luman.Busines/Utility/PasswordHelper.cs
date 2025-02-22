﻿using System.Security.Cryptography;
using System.Text;

namespace Luman.Busines.Utility
{
    public static class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }
        public static string EncodeProSecurity(string pass)
        {
            var first = EncodePasswordMd5(pass);
            var second = EncodePasswordMd5(first);
            return EncodePasswordMd5(second);
        }
    }
}
