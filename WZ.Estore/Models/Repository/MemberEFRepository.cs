using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WZ.Estore.Controllers;
using WZ.Estore.Models.EFModels;

namespace WZ.Estore.Models.Repository
{
	public class MemberEFRepository
	{
		public void Create(RegisterDto dto) {
			using (var db = new AppDbContext()) {
				var member = new Member
				{
					Account = dto.Account,
					EncryptedPassword = dto.EncryptedPassword,
					Email = dto.Email,
					Name = dto.Name,
					Mobile = dto.Mobile,
					IsConfirmed = false,
					ConfirmCode = Guid.NewGuid().ToString()
				};

				db.Members.Add(member);
				db.SaveChanges();
			}
		}

		public bool IsExist(string account)
		{
			using (var db = new AppDbContext())
			{
				return db.Members.Any(m => m.Account == account);
			}
		}
	}
}