using System.Collections.Generic;

namespace RecruitmentTest
{
    internal class MenuItemNameComparer : IEqualityComparer<MenuItem>
    {
        public bool Equals(MenuItem x, MenuItem y)
            => x.Name == y.Name;

        public int GetHashCode(MenuItem obj)
            => obj.Name.GetHashCode();
    }
}
