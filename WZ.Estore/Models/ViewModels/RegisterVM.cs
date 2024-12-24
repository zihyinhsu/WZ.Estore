using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WZ.Estore.Models.Infra;

namespace WZ.Estore.Models.ViewModels
{
	public class RegisterVM
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(30, ErrorMessage = DAHelper.StringLength)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(15, MinimumLength = 5, ErrorMessage = "{0} 長度必須介於 {1} ~ {2} ")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(15, ErrorMessage = DAHelper.StringLength)]
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "電子郵件")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(256, ErrorMessage = DAHelper.StringLength)]
		[EmailAddress(ErrorMessage = "{0} 格式有誤")]
		public string Email { get; set; }

		[Display(Name = "姓名")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(30, ErrorMessage = DAHelper.StringLength)]
		public string Name { get; set; }

		[Display(Name = "手機")]
		[StringLength(10, ErrorMessage = DAHelper.StringLength)]
		public string Mobile { get; set; }
	}
}