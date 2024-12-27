using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WZ.Estore.Models.ViewModels
{
	public class CheckoutVM
	{
		[Display(Name = "收件者")]
		[Required]
		[StringLength(30)]
		public string Receiver { get; set; }

		[Display(Name = "地址")]
		[Required]
		[StringLength(200)]
		public string Address { get; set; }

		[Display(Name = "手機")]
		[Required]
		[StringLength(10)]
		public string CellPhone { get; set; }
	}
}