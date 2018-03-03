using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTest
{
    public class MenuItemType
    {
        private MenuItemType()
        {
        }

        public MenuItemType(string description, params MenuItem[] items)
        {
            Description = description;
            _Items = items.ToList();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        private List<MenuItem> _Items { get; set; } = new List<MenuItem>();

        public IReadOnlyList<MenuItem> Items => _Items;
    }
}
