using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Models;
using PromotionEngine.Services;

namespace PromotionEngine.Controllers
{
    public class PromotionEngineController : Controller
    {
        private readonly SkuUnitPrice sku = new SkuUnitPrice();
        private readonly ICalculateCartValue _calculateCartValue;

        public PromotionEngineController(ICalculateCartValue calculateCartValue)
        {
            _calculateCartValue = calculateCartValue;
        }
        public IActionResult Index()
        {
            return View();
        }
        //To get all available Sku
        public JsonResult GetSku()
        {
            List<string> skus = sku.GetlstSKUUnitPrice().Keys.ToList();
            return Json(data: skus);
        }

        [HttpPost]
        public async Task<JsonResult> GetTotalCartValue(string[] cartArray)
        {
            //decimal totalSum = 0;
            List<string> cartItem;
            if (cartArray.Length > 0)
            {
                cartItem = cartArray.ToList();
            }
            var (total, finalCart) = await _calculateCartValue.GetTotalAndCartItems(cartArray);
            return new JsonResult(new { total = total, cartitems = finalCart });
        }
    }
}
