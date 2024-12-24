using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WZ.Estore.Models.Infra
{
	public class HashUtility
	{
		/// <summary>
		/// 將字串轉換成SHA256
		/// </summary>
		/// <param name="plainText"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string ToSHA256(string plainText, string salt)

		{
			using (var sha256 = SHA256.Create()) { 
				var passwordBytes = Encoding.UTF8.GetBytes(salt + plainText);
				var hash = sha256.ComputeHash(passwordBytes);
				var sb = new StringBuilder();
				foreach (var b in hash)
				{
					sb.Append(b.ToString("X2"));
				}
				return sb.ToString();
			}
		}

		public static string GetSalt()
		{
			return System.Configuration.ConfigurationManager.AppSettings["Salt"];
		}
	}
}