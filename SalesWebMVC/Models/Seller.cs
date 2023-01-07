namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; }

        private readonly ICollection<SalesRecord> _salesRecords;

        public Seller()
        {
            _salesRecords = new List<SalesRecord>();
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) : this()
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            SalesRecords.Add(sr);
        } 
        public void RemoveSales(SalesRecord sr)
        {
            SalesRecords.Remove(sr);
        }

        public double TotalSales(DateTime initialDate, DateTime finalDate)
        {
            return SalesRecords.Where(sr => sr.Date > initialDate && sr.Date < finalDate).Sum(sr => sr.Amount);
        }
    }
}
