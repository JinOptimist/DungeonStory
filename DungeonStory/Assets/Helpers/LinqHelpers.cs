using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    public static class LinqHelpers
    {
        private static Random _random = new Random();
        
        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            if (!array.Any())
            {
                return default(T);
            }

            var index = _random.Next(array.Count());
            return array.ToList()[index];
        }
    }
}
