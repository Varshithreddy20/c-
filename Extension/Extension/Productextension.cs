using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ExtensionNamespace
{
    public static class Productextension
    {
        public static double GetDiscount(this Product product)
        {
          return product.ProductCost*product.DiscountPercentage / 100;
        }
    }
}
