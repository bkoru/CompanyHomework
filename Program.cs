using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHomework
{
    static class Program
    {
        static DataSlot _dataSlot = new DataSlot();
        static void Main(string[] args)
        {
            Console.WriteLine("init");

            Console.WriteLine("Create Company");
            
            int companyCount = 0;
            Console.Write("Şirket sayısı giriniz: ");
            while (!int.TryParse(Console.ReadLine(), out companyCount))
            {
                Console.Clear();
                Console.WriteLine("Lütfen geçerli bir sayı giriniz");
                Console.Write("Şirket sayısı giriniz: ");
            }
            Company.CreateCompany(_dataSlot, companyCount);

            Console.WriteLine("----------------Get Company List----------------");

            foreach (var company in _dataSlot.Companies)
            {
                Console.WriteLine("{0} {1}", company.Id, company.Name);
            }

            Console.WriteLine("Create Employee from Class void");
            
            int employeeCount = 0;
            Console.Write("Çalışan sayısı giriniz: ");
            
            while (!int.TryParse(Console.ReadLine(), out employeeCount))
            {
                Console.WriteLine("Lütfen geçerli bir sayı giriniz");
                Console.Write("Çalışan sayısı giriniz: ");
            }
            Employee.CreateEmployee(_dataSlot, employeeCount);

            Console.WriteLine("----------------Get Employee List----------------");
            foreach (var employee in _dataSlot.Employees)
            {
                Console.WriteLine("employee : {0} {1} {2} {3} {4}", employee.Id,employee.Name,employee.BirthDate.ToString("dd.MM.yyyy"),employee.Company.Name,employee.Company.Id);
            }

            Console.ReadLine();
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

        public static void CreateCompany(DataSlot dataSlot, int total)
        {
            for (int i = 0; i < total; i++)
            {
                var company = new Company(i + 1, "Company" + (i + 1).ToString());
                dataSlot.Companies.Add(company);
            }
        }
    }

    public class Employee
    {
        public int Id;
        public string Name;
        public DateTime BirthDate;
        public Company Company;

        public Employee()
        {
        }

        public Employee(int id, string name, DateTime birthDate) 
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
        }

        private static Random rnd = new Random();
        private static DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            int randomDays = rnd.Next(range);
            return startDate.AddDays(randomDays);
        }
        public static void CreateEmployee(DataSlot dataSlot, int total)
        {
            int companiesCount = dataSlot.Companies.Count;
            for (int i = 1; i < total+1; i++)
            {
                var employee = new Employee(i, "Emp-" + i.ToString(), RandomDate(new DateTime(1950,01,01), new DateTime(2004,01,01)));
                employee.Company = dataSlot.Companies[rnd.Next(companiesCount)];
                dataSlot.Employees.Add(employee);
            }
        }
    }
    

}
