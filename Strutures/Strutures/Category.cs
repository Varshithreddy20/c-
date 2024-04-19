using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strutures
{
    public struct Category 
    {
        private int _categoryID;
        private string _categoryName;

        public Category(int categoryID, string categoryName)
        {
            _categoryID = categoryID;
            _categoryName = categoryName;
        }

        public int CategoryID
        {
            set { _categoryID = value; }
            get { return _categoryID; }
        }
        public string CategoryName
        {
            set
            {
                if (value.Length <= 40)
                { _categoryName = value; }
            }
            get { return _categoryName; }
        }

        public int GetCategoryNameLength()
        {
            return this._categoryName.Length;
        }


    }
}
