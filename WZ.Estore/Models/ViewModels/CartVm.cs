using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WZ.Estore.Models.ViewModels
{
	public class CartVm
	{
		public int Id { get; set; }
		public string MemberAccount { get; set; }
		public IEnumerable<CartItemVM> CartItems { get; set; }

		public int Total => CartItems.Sum(XmlSiteMapProvider => XmlSiteMapProvider.SubTotal);
		public bool AllowCheckout => CartItems.Any(); // 至少要有一項商品才允許結帳
	}

	public class CartItemVM
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public ProductIndexVM Product { get; set; }
		public int Qty { get; set; }
		public int SubTotal => Product.Price * Qty; // 小計
	}
}