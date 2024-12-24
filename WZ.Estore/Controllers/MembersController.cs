using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WZ.Estore.Models.EFModels;
using WZ.Estore.Models.Infra;
using WZ.Estore.Models.ViewModels;

namespace WZ.Estore.Controllers
{
	public class MembersController : Controller
	{
		// GET: Members
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterVM model)
		{
			if (!ModelState.IsValid) return View(model);
			try
			{
				ProcessRegister(model);

				// 建檔成功，導向註冊成功頁面
				return View("RegisterConfirm");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}

		}
		/// <summary>
		/// 處理註冊
		/// </summary>
		/// <param name="model"></param>
		private void ProcessRegister(RegisterVM model)
		{
			using (var db = new AppDbContext())
			{
				// 判斷帳號是否已存在
				if (db.Members.Any(m => m.Account == model.Account))
				{
					throw new Exception("帳號已存在");
				}
				// 密碼加密
				var hashedPassword = HashUtility.ToSHA256(model.Password, HashUtility.GetSalt());
				var member = new Member
				{
					Account = model.Account,
					EncryptedPassword = hashedPassword,
					Email = model.Email,
					Name = model.Name,
					Mobile = model.Mobile,
					IsConfirmed = false,
					ConfirmCode = Guid.NewGuid().ToString()
				};

				db.Members.Add(member);
				db.SaveChanges();
			}
		}
	}
}