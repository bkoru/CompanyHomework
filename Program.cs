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
            Console.Write("Şirket sayısı giriniz: ");
            int compNumber = Convert.ToInt32(Console.ReadLine());
            CreateCompany(compNumber, _dataSlot);

            Console.WriteLine("----------------Get Company List----------------");

            foreach (var company in _dataSlot.Companies)
            {
                Console.WriteLine("{0} {1}", company.Id, company.Name);
            }

            Console.WriteLine("Create Employees from Class void");
            // I'm using employee class create with CreateCompany void class

            Console.Write("Çalışan sayısı giriniz: ");
            int number = Convert.ToInt32(Console.ReadLine());
            new Employee().CreateEmployees(_dataSlot, number);

            // new Employee().CreateEmployees(dataSlot, 20);
            // dataSlot.Employees.Clear();

            Console.WriteLine("----------------Get Employees List----------------");
            foreach (var employee in _dataSlot.Employees)
            {
                Console.WriteLine("employee : {0} {1} {2} {3} {4}", employee.Id,employee.Name,employee.BirthDate.ToString("dd.MM.yyyy"),employee.Company.Name,employee.Company.Id);
            }

            Console.ReadLine();
        }
        private static void CreateCompany(int total, DataSlot dataSlot)
        {
            for (int i = 0; i < total + 1; i++)
            {
                var company = new Company(i, "Company" + i.ToString());
                dataSlot.Companies.Add(company);
            }
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

        private Random rnd = new Random();
        private DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            int randomDays = rnd.Next(range);
            return startDate.AddDays(randomDays);
        }
        public void CreateEmployees(DataSlot dataSlot, int total)
        {
            int companiesCount = dataSlot.Companies.Count;
            for (int i = 1; i < total+1; i++)
            {
                var employee = new Employee(i, "Emp-" + i.ToString(), RandomDate(new DateTime(1950,01,01), new DateTime(2004,01,01)));
                employee.Company = dataSlot.Companies[rnd.Next(companiesCount)];
                dataSlot.Employees.Add(employee);
            }
        }
        //public List<Employee> CreateEmployees (DataSlot dataSlot, int total)
        //{
        //    for (int i = 1; i < total + 1; i++)
        //    {
        //        var employee = new Employee(i, "Emp-" + i.ToString(), RandomDate(new DateTime(1950, 01, 01), new DateTime(2004, 01, 01)));
        //        dataSlot.Employees.Add(employee);
        //    }
        //    return dataSlot.Employees;
        //}
    }
    

}
