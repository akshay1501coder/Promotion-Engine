using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PromotionEngine.Controllers
{
    public class PromotionEngineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
