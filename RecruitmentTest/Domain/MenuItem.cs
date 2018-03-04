namespace RecruitmentTest
{
    public class MenuItem
    {
        private MenuItem()
        {
        }

        public MenuItem(string name, decimal price)
        {
            Name = name;
            price = Price;
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
