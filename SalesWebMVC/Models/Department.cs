namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; }


        private readonly ICollection<Seller> _sellers;

        public Department()
        {
            _sellers = new SortedSet<Seller>();
        }

        public Department(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller sl)
        {
            Sellers.Add(sl);
        }

        public double TotalSales(DateTime initialDate, DateTime finalDate)
        {
            return Sellers.Sum(sl => sl.TotalSales(initialDate,finalDate));
        }
    }
}
