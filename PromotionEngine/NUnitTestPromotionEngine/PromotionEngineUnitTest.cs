using NUnit.Framework;
using PromotionEngine.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NUnitTestPromotionEngine
{
    public class Tests
    {
        [Test]
        public void TestScenarioACartValue()
        {
            string[] cartArray = { "A", "B", "C" };
            decimal expectedtotal = 100;
            CalculateCartValue calculateCartValue = new CalculateCartValue();
            Task<(decimal, IEnumerable<string>)> dictSku = GetTotalAndCartItems(cartArray);
            Assert.AreEqual(expectedtotal, dictSku.Result.Item1);
        }

        [Test]
        public void TestScenarioBCartValue()
        {
            string[] cartArray = { "A", "A", "A", "A", "A", "B", "B", "B", "B", "B", "C" };
            decimal expectedtotal = 370;
            Task<(decimal, IEnumerable<string>)> dictSku = GetTotalAndCartItems(cartArray);
            Assert.AreEqual(expectedtotal, dictSku.Result.Item1);
        }

        [Test]
        public void TestScenarioCCartValue()
        {
            string[] cartArray = { "A", "A", "A", "B", "B", "B", "B", "B", "C", "D" };
            decimal expectedtotal = 280;
            Task<(decimal, IEnumerable<string>)> dictSku = GetTotalAndCartItems(cartArray);
            Assert.AreEqual(expectedtotal, dictSku.Result.Item1);
        }

        public async Task<(decimal, IEnumerable<string>)> GetTotalAndCartItems(string[] cartArray)
        {
            CalculateCartValue calculateCartValue = new CalculateCartValue();
            return await calculateCartValue.GetTotalAndCartItems(cartArray);
        }
    }
}