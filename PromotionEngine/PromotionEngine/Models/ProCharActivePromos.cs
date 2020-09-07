using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class ProCharActivePromos
    {
        readonly Dictionary<string, int> lstProCharActivePromos = new Dictionary<string, int>();

        /// <summary>
        /// To get all Pro Active Promos
        /// </summary>
        /// <returns>Dictionary<string,int></string></returns>
        public Dictionary<string, int> GetlstProCharActivePromos()
        {
            lstProCharActivePromos.Add("3A", 130);
            lstProCharActivePromos.Add("2B", 45);
            lstProCharActivePromos.Add("C+D", 30);

            return lstProCharActivePromos;
        }
    }
}
