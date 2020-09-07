using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PromotionEngine.Services
{
    public class CalculateCartValue : ICalculateCartValue
    {
        SkuUnitPrice sku = new SkuUnitPrice();
        ProCharActivePromos promo = new ProCharActivePromos();
        Dictionary<string, int> dictSkuUnitPrice = new Dictionary<string, int>();
        Dictionary<string, int> dictProCharActivePromos = new Dictionary<string, int>();

        public Task<(decimal, IEnumerable<string>)> GetTotalAndCartItems(string[] cartArray)
        {
            dictSkuUnitPrice = sku.GetlstSKUUnitPrice();
            dictProCharActivePromos = promo.GetlstProCharActivePromos();
            List<string> FinalSkulst = new List<string>();
            decimal total = 0;

            //Get all the added Sku items along with promotion and their count
            Dictionary<string, int> dictSkuCount = GetSkuCount(cartArray, dictSkuUnitPrice.Keys.ToList(),
                dictProCharActivePromos.Keys.ToList());

            throw new NotImplementedException();
        }

        private Dictionary<string, int> GetSkuCount(string[] cartArray, List<string> lstSku, List<string> lstPromo)
        {
            Dictionary<string, int> dictSku = new Dictionary<string, int>();
            Dictionary<string, int> dictCartSku = new Dictionary<string, int>();
            string promoVal = string.Empty;
            int promoCount = 0;
            string[] promo;

            try
            {
                //Get count of each SKU item in Cart
                foreach (var item in lstSku)
                {
                    string skuitem = item; // (A,B,C,D)
                    int skuCount = cartArray.Count(x => x == skuitem);
                    if (skuCount > 0)
                    {
                        dictSku.Add(skuitem, skuCount);
                    }
                }

                foreach (var item in lstPromo)
                {
                    promo = Regex.Split(item, @"\D+");
                    if (!item.Contains("+"))
                    {
                        if (promo.Length > 0)
                        {
                            //3A
                            promoCount = Convert.ToInt16(promo[0]); //3
                            promoVal = item.Replace(promoCount.ToString(), string.Empty); //A
                            if (dictSku.ContainsKey(promoVal))
                            {
                                int totalCount = dictSku[promoVal] / promoCount;
                                int remainingCount = dictSku[promoVal] % promoCount;
                                if (totalCount > 0)
                                {
                                    dictCartSku.Add(item, totalCount);
                                }
                                if (remainingCount > 0)
                                {
                                    dictCartSku.Add(promoVal, remainingCount);
                                }
                            }
                        }
                    }
                    else
                    {
                        bool containsInt = item.Any(char.IsDigit);
                        if (!containsInt)
                        {
                            string newitem = item.Replace("+", string.Empty).Trim();
                            char[] promoSkus = newitem.ToCharArray();//['C','D']
                            bool isAllExists = CheckIfAllSkuExists(promoSkus, dictSku);
                            if (isAllExists == false)
                            {
                                foreach (var promoitem in promoSkus)
                                {
                                    var value = dictSku.FirstOrDefault(x => x.Key == promoitem.ToString()).Value;
                                    if (value > 0)
                                    {
                                        dictCartSku.Add(promoitem.ToString(), value);
                                    }
                                }
                            }
                            else
                            {
                                string skuWithLeastValue = GetSkuWithLeastValue(promoSkus, dictSku);
                                int skuvalue = dictSku.FirstOrDefault(x => x.Key == skuWithLeastValue).Value;
                                dictCartSku.Add(item, skuvalue);
                                foreach (var promoitem in promoSkus)
                                {
                                    int value = dictSku.FirstOrDefault(x => x.Key == promoitem.ToString()).Value;
                                    int remainingValue = value - skuvalue;
                                    if (remainingValue > 0)
                                    {
                                        dictCartSku.Add(promoitem.ToString(), remainingValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dictCartSku;
        }

        /// <summary>
        /// For promotion with two or multiple Sku, check whether all items available in cart or not
        /// </summary>
        /// <param name="promoSkus"></param>
        /// <param name="dictSku"></param>
        /// <returns>bool</returns>
        private bool CheckIfAllSkuExists(char[] promoSkus, Dictionary<string, int> dictSku)
        {
            foreach (var data in promoSkus)
            {
                if (!dictSku.ContainsKey(data.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// For promotion with two or multiple Sku , if all items are available 
        /// check item with least count in the added cart items
        /// </summary>
        /// <param name="promoSkus"></param>
        /// <param name="dictSku"></param>
        /// <returns>string (Sku) with least count</returns>
        
        private string GetSkuWithLeastValue(char[] promoSkus, Dictionary<string, int> dictSku)
        {
            int i = 0;
            Dictionary<string, int> tempDictionary = new Dictionary<string, int>();
            while (i < promoSkus.Length)
            {
                string sku = promoSkus[i].ToString();
                int count = dictSku.FirstOrDefault(x => x.Key == sku).Value;
                tempDictionary.Add(sku, count);
                i++;
            }
            string SkuWithLeastValue = tempDictionary.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
            return SkuWithLeastValue;
        }
    }
}
