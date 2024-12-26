using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WZ.Estore.Models.Infra;

namespace WZ.Estore.Models.ViewModels
{
	public class ForgetPasswordVM
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(30, ErrorMessage = DAHelper.StringLength)]
		public string Account { get; set; }

		[Display(Name = "電子郵件")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(256, ErrorMessage = DAHelper.StringLength)]
		[EmailAddress(ErrorMessage = "{0} 格式有誤")]
		public string Email { get; set; }
	}
}