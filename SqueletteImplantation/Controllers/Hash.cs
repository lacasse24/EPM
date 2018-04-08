using System;
using System.Security.Cryptography;
using System.Text;

namespace SqueletteImplantation.Controllers
{
    public class Hash
    {

        private static string salt = "jY603KQuBAAwiEVSPwj_4014j_1EzQ7ct_2gI6JZ";

        public static string GetHash(string PWD)
        {
            byte[] mdpHash;
            string strHash;
            using (var sha256 = SHA256.Create())
            {
                PWD += salt;
                mdpHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(PWD));
                strHash = BitConverter.ToString(mdpHash).Replace("-", "").ToLower();
            }
            return strHash;

        }
    }
}
