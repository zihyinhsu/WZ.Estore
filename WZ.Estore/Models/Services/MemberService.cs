using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WZ.Estore.Controllers;
using WZ.Estore.Models.EFModels;
using WZ.Estore.Models.Infra;
using WZ.Estore.Models.Repository;

namespace WZ.Estore.Models.Services
{
	public class MemberService
	{
		private MemberEFRepository _repo;

		public MemberService()
		{
			_repo = new MemberEFRepository();
		}
		public void Register(RegisterDto dto) {
			using (var db = new AppDbContext())
			{
				// 判斷帳號是否已存在
				if(_repo.IsExist(dto.Account)) throw new Exception("帳號已存在");

				// 密碼加密
				dto.EncryptedPassword = HashUtility.ToSHA256(dto.Password, HashUtility.GetSalt());
			
			}
		}
	}
}