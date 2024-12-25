using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WZ.Estore.Models.EFModels;
using WZ.Estore.Models.Infra;
using WZ.Estore.Models.ViewModels;

namespace WZ.Estore.Controllers
{
	public class MembersController : Controller
	{
		// GET: Members
		[Authorize]
		public ActionResult Index()
		{
			return View();
		}

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

		/// <summary>
		/// 啟用帳號
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="confirmCode"></param>
		/// <returns></returns>
		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{

			try
			{
				ProcessActiveRegister(memberId, confirmCode);

				return View("ActiveRegister");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}

		private void ProcessActiveRegister(int memberId, string confirmCode)
		{
			using (var db = new AppDbContext())
			{
				// 如果用 memberId,confirmCode 找不到就不做任何事
				var member = db.Members.FirstOrDefault(m => m.Id == memberId && m.ConfirmCode == confirmCode && m.IsConfirmed == false);
				if (member == null) return;

				// updateIsConfirm and confirm code
				member.IsConfirmed = true;
				member.ConfirmCode = null;
				db.SaveChanges();
			}
		}

		// GET: Members/Login
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginVM model)
		{
			if (!ModelState.IsValid) return View(model);
			try
			{
				ValidateLogin(model.Account, model.Password); // 若失敗會拋出例外

				(string returnUrl, HttpCookie cookie) = ProccessLogin(model.Account);

				// 登入成功，設定 cookie
				Response.Cookies.Add(cookie);

				// 導向 returnUrl
				return Redirect(returnUrl);

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			};
		}

		private (string returnUrl, HttpCookie cookie) ProccessLogin(string account)
		{
			var roles = string.Empty; // 本範例沒有用到角色，存入空白

			// 建立一張認證票
			var ticket = new FormsAuthenticationTicket(
				1,// 版本別
				account,
				DateTime.Now,// 發行日
				DateTime.Now.AddMinutes(30), // 到期日
				false, // 是否續存
				roles, // userdata
				"/" // cookie 位置
				);

			// 將它加密
			var value = FormsAuthentication.Encrypt(ticket);

			// 存入cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			// 取得 return url
			var url = FormsAuthentication.GetRedirectUrl(account, true);
			return (url, cookie);
		}

		private void ValidateLogin(string account, string password)
		{
			// 若還沒開通就拋出例外
			//若密碼錯誤就拋出例外
			using (var db = new AppDbContext())
			{
				var member = db.Members.FirstOrDefault(m => m.Account == account);
				if (member == null) throw new Exception("帳號或密碼錯誤");
				if (!HashUtility.VerifySHA256(password, member.EncryptedPassword)) throw new Exception("帳號或密碼錯誤");
				if (member.IsConfirmed == false) throw new Exception("帳號尚未開通");
			}
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Members");
		}

		public ActionResult EditProfile()
		{
			// 取得個人基本資料
			string account = User.Identity.Name;
			using (var db = new AppDbContext())
			{
				var member = db.Members.First(m => m.Account == account);
				// 轉型為 ProfileVM
				ProfileVM model = new ProfileVM
				{
					Account = member.Account,
					Email = member.Email,
					Name = member.Name,
					Mobile = member.Mobile
				};
				return View(model);
			}
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(ProfileVM model)
		{
			string account = User.Identity.Name;

			using (var db = new AppDbContext()) {
				var memberInDb = db.Members.First(m => m.Account == account);
				memberInDb.Name = model.Name;
				memberInDb.Mobile = model.Mobile;
				memberInDb.Email = model.Email;

				db.SaveChanges();

				TempData["Message"] = "個人資料已更新";
				return RedirectToAction("Index");
			}

		}

		public ActionResult ChangePassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(ChangePasswordVM model)
		{
			string account = User.Identity.Name;
			using (var db = new AppDbContext()) { 
				var memberInDb = db.Members.First(m => m.Account == account);
				if (!HashUtility.VerifySHA256(model.OiginalPassword, memberInDb.EncryptedPassword)){ 
					ModelState.AddModelError("OiginalPassword", "原密碼錯誤");
					return View(model);
				}

				memberInDb.EncryptedPassword = HashUtility.ToSHA256(model.Password, HashUtility.GetSalt());
				db.SaveChanges();
				TempData["Message"] = "密碼已變更";
				return RedirectToAction("Index");
			}
		}

	}
}