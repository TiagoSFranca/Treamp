using System;
using System.Text;

namespace Presentation.Util
{
    public class Services
    {
        public static string CalculateSHA1(string text)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public static string AddUserMessage(string nameUser)
        {
            string result = nameUser + " quer te adicionar como contato.";
            return result;
        }

        public static string InviteToTravel(string nameUser)
        {
            string result = nameUser + " quer te adicionar para a viagem.";
            return result;
        }
    }
}