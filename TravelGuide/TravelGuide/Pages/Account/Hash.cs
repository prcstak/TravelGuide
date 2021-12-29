using System.Security.Cryptography;
using System.Text;

namespace TravelGuide.Pages.Account
{
    public class Hash
    {
        public static string GetHash(string input)
        {
            var hashFunc = MD5.Create();

            var r = hashFunc.ComputeHash(Encoding.Default.GetBytes(input));

            var sb = new StringBuilder();

            for(int i = 0; i< r.Length; i++ )
            {
                sb.Append(r[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}