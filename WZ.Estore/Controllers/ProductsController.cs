using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WZ.Estore.Models.EFModels;
using WZ.Estore.Models.ViewModels;
using System.Data.Entity;
namespace WZ.Estore.Controllers
{
	public class ProductsController : Controller
	{
		// GET: Products
		public ActionResult Index(ProductFilterVM vm)
		{
			IQueryable<Product> products;
			List<Product> data;
			using (var db = new AppDbContext())
			{
				products = db.Products.AsNoTracking().Include(p => p.Category);
				if (string.IsNullOrEmpty(vm.Name) == false) products = products.Where(p => p.Name.Contains(vm.Name));

				if(vm.PriceStart.HasValue) products = products.Where(p => p.Price >= vm.PriceStart);
				if (vm.PriceEnd.HasValue) products = products.Where(p => p.Price <= vm.PriceEnd);


				data = products.ToList();
			}

			List<ProductIndexVM> model = new List<ProductIndexVM>();

			foreach (var product in data)
			{
				var p = new ProductIndexVM
				{
					Id = product.Id,
					CategoryId = product.CategoryId,
					Name = product.Name,
					Price = product.Price,
				};
				model.Add(p);
			}

			vm.Data = model;

			return View(vm);
		}
	}
}