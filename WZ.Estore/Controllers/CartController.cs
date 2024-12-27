using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WZ.Estore.Models.EFModels;
using WZ.Estore.Models.ViewModels;

namespace WZ.Estore.Controllers
{
	public class CartController : Controller
	{
		// GET: Cart
		[Authorize]
		public ActionResult AddItem(int productId)
		{
			string account = User.Identity.Name;
			int qty = 1;

			Add2Cart(account, productId, qty); // Add to Cart

			return new EmptyResult();
		}

		private void Add2Cart(string account, int productId, int qty)
		{
			// 取得目前購物車 Id,若沒購物車則新增一筆
			CartVm cart = GetCartInfo(account);
			int cartId = cart.Id;

			// 將商品加入購物車
			AddCartItem(cartId, productId, qty);
		}

		/// <summary>
		/// 加入購物車，若明細不存在就新增一筆，若存在就更新數量
		/// </summary>
		/// <param name="cartId"></param>
		/// <param name="productId"></param>
		/// <param name="qty"></param>
		/// <exception cref="NotImplementedException"></exception>
		private void AddCartItem(int cartId, int productId, int qty)
		{
			using (var db = new AppDbContext())
			{
				var cartItem = db.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);
				if (cartItem != null)
				{
					cartItem.Qty += qty;
				}
				else
				{
					// 若不存在就新增一筆
					var newItem = new CartItem
					{
						CartId = cartId,
						ProductId = productId,
						Qty = qty
					};

					db.CartItems.Add(newItem);
				}
				db.SaveChanges();
			}
		}

		/// <summary>
		/// 取得目前購物車主檔資訊，若無則新增一筆
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		private CartVm GetCartInfo(string account)
		{
			using (var db = new AppDbContext())
			{
				var cart = db.Carts.FirstOrDefault(c => c.MemberAccount == account);
				if (cart == null)
				{ // 找不到立刻建一筆
					cart = new Cart { MemberAccount = account };
					db.Carts.Add(cart);
					db.SaveChanges(); //這時的 cart 裡 ，Id 會自動有新的 id
				}

				var cartItems = db.CartItems.Where(ci => ci.CartId == cart.Id).Select(ci => new CartItemVM
				{
					Id = ci.Id,
					CartId = ci.CartId,
					ProductId = ci.ProductId,
					Product = new ProductIndexVM
					{
						Id = ci.Product.Id,
						Name = ci.Product.Name,
						Price = ci.Product.Price
					},
					Qty = ci.Qty
				}).ToList();

				var cartVM = new CartVm
				{
					Id = cart.Id,
					MemberAccount = cart.MemberAccount,
					CartItems = cartItems
				};

				return cartVM;
			}
		}

		[Authorize]
		public ActionResult Info()
		{
			string account = User.Identity.Name;
			var cart = GetCartInfo(account);
			return View(cart);
		}

		[Authorize]
		public ActionResult UpdateItem(int productId,int newQty)
		{
			string account = User.Identity.Name;
			newQty = newQty < 0 ? 0 : newQty;
			UpdateItemQty(account, productId, newQty);
			return new EmptyResult();
		}

		private void UpdateItemQty(string account, int productId, int newQty)
		{
			var cart = GetCartInfo(account);
			var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
			using (var db = new AppDbContext()) { 
				var entity = db.CartItems.FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == productId);
				if (entity == null) return; // 幾乎不可能找不到此 Item

				if (newQty == 0)
				{
					// 數量為 0 就刪除
					db.CartItems.Remove(entity);
				}
				else {
					// 更新數量
					entity.Qty = newQty;
				}
				db.SaveChanges();
			}
		}
	}
}