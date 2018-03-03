using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTest
{
    public class MenuItem
    {
        public int Id { get; set; }

        public int MenuItemTypeId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual MenuItemType MenuItemType { get; set; }
    }
}
