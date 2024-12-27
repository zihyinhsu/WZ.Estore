using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WZ.Estore.Models.ViewModels
{

	public class ProductFilterVM
	{
		// 篩選條件
		public string Name { get; set; }

		[DataType(DataType.Currency)]
		public int? PriceStart { get; set; }

		[DataType(DataType.Currency)]
		public int? PriceEnd { get; set; }

		// 篩選結果
		public List<ProductIndexVM> Data { get; set; }
	}
	public class ProductIndexVM
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[StringLength(3000)]
		public string Description { get; set; }

		public int Price { get; set; }
	}
}