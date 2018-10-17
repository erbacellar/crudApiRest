using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CrudApiRest.Application.Helpers
{
    public static class Utils
    {
        public static string GetSalt(string pass)
        {
            return Sha256(pass + GetDate());
        }

        public static string GetSenhaEncrypt(string pass, string salt)
        {
            return Sha256(pass + salt);
        }

        private static string GetDate()
        {
            TextInfo myTi = new CultureInfo("en-US", false).TextInfo;
            string data = $"{DateTime.Now.DayOfWeek.ToString().Substring(0, 3)} {myTi.ToTitleCase(DateTime.Today.ToString("MMMM"))} {DateTime.Now.ToString("dd HH:mm:ss BRT yyyy")}";
            return data;
        }

        private static string Sha256(string value)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));

            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
