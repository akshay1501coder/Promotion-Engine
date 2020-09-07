using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Models;

namespace PromotionEngine.Controllers
{
    public class PromotionEngineController : Controller
    {
        private readonly SkuUnitPrice sku = new SkuUnitPrice();
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
    }
}
