using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WZ.Estore.Models.Infra;

namespace WZ.Estore.Models.ViewModels
{
	public class ResetPasswordVM
	{
		[Display(Name = "新密碼")]
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
	}
}