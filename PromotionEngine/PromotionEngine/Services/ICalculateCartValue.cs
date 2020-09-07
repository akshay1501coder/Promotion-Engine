using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromotionEngine.Services
{
    public interface ICalculateCartValue
    {
        Task<(decimal, IEnumerable<string>)> GetTotalAndCartItems(string[] cartArray);
    }
}