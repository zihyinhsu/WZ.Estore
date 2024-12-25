using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WZ.Estore.Models.Infra;

namespace WZ.Estore.Models.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = DAHelper.Required)]
		public string Account { get; set; }
		
		[Display(Name = "密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}