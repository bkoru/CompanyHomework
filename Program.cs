using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHomework
{
    static class Program
    {
        private static List<Company> _companies = new List<Company>();
        static void Main(string[] args)
        {
            Console.WriteLine("init");
            var dataSlot = new DataSlot();
            var company1 = new Company();
            company1.Id = 1;
            company1.Name = "Batu";

            _companies.Add(company1);

            Console.WriteLine("Create Company");
            CreateCompany(20);

            Console.WriteLine("----------------Get Company List----------------");
            GetCompanyList();
            List<Company> companies2 = new List<Company>()
            {
                new Company
                {
                    Id = 3,
                    Name = "Enver"
                },

                new Company
                {
                    Id = 85,
                    Name = "Batuhan"
                },
                company1
            };

            new Company(dataSlot, companies2);
            foreach (var company in dataSlot.Companies)
            {
                Console.WriteLine("{0} {1}", company.Id, company.Name);
            }

            Console.WriteLine("Create Employees from Class void");
            // I'm using employee class create with CreateCompany void class

            Console.Write("Çalışan oluşturmak için sayı giriniz: ");
            int number = Convert.ToInt32(Console.ReadLine());
            new Employee().CreateEmployees(dataSlot, number);

            // new Employee().CreateEmployees(dataSlot, 20);
            // dataSlot.Employees.Clear();

            Console.WriteLine("----------------Get Employees List----------------");
            foreach (var employee in dataSlot.Employees)
            {
                Console.WriteLine("employee : {0} {1} {2}", employee.Id,employee.Name,employee.BirthDate.ToString("dd.MM.yyyy"));
            }

            Console.ReadLine();
        }
        private static void CreateCompany(int total)
        {
            for (int i = 0; i < total + 1; i++)
            {
                var company = new Company(i, "Company" + i.ToString());
                _companies.Add(company);
            }
        }
        private static List<Company> GetCompanyList()
        {
            foreach (var company in _companies)
            {
                Console.WriteLine("{0},{1}", company.Id, company.Name);
            }
            return _companies;
        }
    }
    public class DataSlot
    {
        public List<Company> Companies { get; set; }
        public List<Employee> Employees { get; set; }
        public DataSlot()
        {
            Companies = new List<Company>();
            Employees = new List<Employee>();
        }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company()
        {
        }
        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Company(DataSlot dataSlot, List<Company> companies)
        {
            dataSlot.Companies = companies;
        }
    }

    public class Employee
    {
        public int Id;
        public string Name;
        public DateTime BirthDate;

        public Employee()
        {
        }

        public Employee(int id, string name, DateTime birthDate) 
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
        }

        private Random rnd = new Random();
        private DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            int randomDays = rnd.Next(range);
            return startDate.AddDays(randomDays);
        }
        public void CreateEmployees(DataSlot dataSlot, int total)
        {
            for (int i = 1; i < total+1; i++)
            {
                var employee = new Employee(i, "Emp-" + i.ToString(), RandomDate(new DateTime(1950,01,01), new DateTime(2004,01,01)));
                dataSlot.Employees.Add(employee);
            }
        }
    }
    

}
