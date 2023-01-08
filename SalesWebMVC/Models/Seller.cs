using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [DataType(DataType.Currency)]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        [Display(Name = "Department")]
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
