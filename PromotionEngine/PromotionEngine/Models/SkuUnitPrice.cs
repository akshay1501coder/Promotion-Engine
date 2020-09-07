using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class SkuUnitPrice
    {
        readonly Dictionary<string, int> lstSkuUnitPrice = new Dictionary<string, int>();

        /// <summary>
        /// Method to get All available SKU and their price
        /// </summary>
        /// <returns>Dictionary<string, int></returns>
        public Dictionary<string, int> GetlstSKUUnitPrice()
        {
            lstSkuUnitPrice.Add("A", 50);
            lstSkuUnitPrice.Add("B", 30);
            lstSkuUnitPrice.Add("C", 20);
            lstSkuUnitPrice.Add("D", 15);

            return lstSkuUnitPrice;
        }
    }
}
