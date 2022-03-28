using System;
using System.Collections.Generic;
using System.IO;
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
            _dataSlot.LoadFromCsv();
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
            _dataSlot.SaveToCsv();


            Console.WriteLine("----------------Get Employee List----------------");

            foreach (var employee in _dataSlot.Employees)
            {
                Console.WriteLine("Employee : Id:{0} Name:{1} BirthDate:{2} Company Name:{3} Company Id:{4} Salary:{5} TL Age:{6}", 
                    employee.Id,employee.Name,employee.BirthDate.ToString("dd.MM.yyyy"),employee.Company?.Name,employee.Company?.Id,employee.Salary,employee.Age);
            }

            Console.WriteLine("----------------Company list with Odd ID----------------");
            var resultOddId = _dataSlot.Companies.Where(c => c.Id % 2 == 1);
            foreach (var company in resultOddId)
            {
                Console.WriteLine(company.Name);
            }
            Console.WriteLine("----------------Employee list order by Salary----------------");
            var resultSalary = _dataSlot.Employees.OrderByDescending(e => e.Salary);

            foreach (Employee employee in resultSalary)
            {
                Console.WriteLine("{0}: {1} TL", employee.Name, employee.Salary);
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
        private void LoadCompaniesFromCsv() 
        {
        }
        private void LoadEmployeesFromCsv()
        {
            var empStrList = CsvHandler.ReadAsync(@"D:/projects/demos/employees.csv");
            foreach (var item in empStrList)
            {
                var fields = item.Split(';');
                Employees.Add(new Employee()
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1],
                    BirthDate = DateTime.ParseExact(fields[2], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Salary = decimal.Parse(fields[4]),
                    Age = int.Parse(fields[5]),
                    Company = int.Parse(fields[3])
                });
            }
        }
        public void LoadFromCsv()
        {
            LoadCompaniesFromCsv();
            LoadEmployeesFromCsv();
        }
        private void SaveEmployeesToCsv()
        {
            var lines = new List<string>();
            lines.Add("Id;Name;BirthDate;CompanyId;Salary;Age");
            foreach (var item in Employees)
            {
                lines.Add(item.Id + ";" + item.Name + ";" + item.BirthDate.ToString("dd.MM.yyyy") + ";" + item.Company?.Id + ";" + item.Salary + ";" + item.Age);
            }
            CsvHandler.WriteAsync(@"D:/projects/demos/employees.csv", lines);
        }
        private void SaveCompaniesToCsv()
        {

        }

        public void SaveToCsv()
        {
            SaveEmployeesToCsv();
            SaveCompaniesToCsv();
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
        public decimal Salary;
        public int Age;

        public Employee()
        {
        }

        public Employee(int id, string name, DateTime birthDate, decimal salary, int age) 
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Salary = salary;
            Age = age;
        }

        private static Random rnd = new Random();
        private static DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            int randomDays = rnd.Next(range);
            return startDate.AddDays(randomDays);
        }

        private static int AgeCalculate(DateTime randomDate)
        {
            int todayYear = DateTime.Now.Year;
            int todayMonth = DateTime.Now.Month;
            int todayDay = DateTime.Now.Day;
            int birthYear = randomDate.Year;
            int birthMonth = randomDate.Month;
            int birthDay = randomDate.Day;
            int age = todayYear - birthYear;
            if (todayMonth < birthMonth || (todayMonth == birthMonth && todayDay < birthDay))
                age--;

            return age;
        }

        public static void CreateEmployee(DataSlot dataSlot, int total)
        {
            int companiesCount = dataSlot.Companies.Count;
            var startDate = new DateTime(1950, 01, 01);
            var endDate = new DateTime(2004, 01, 01);
            for (int i = 0; i < total; i++)
            {
                var randomId = rnd.Next(1, 999999999);
                while (dataSlot.Employees.Where(e=>e.Id == randomId).FirstOrDefault()!= null)
                {
                    randomId = rnd.Next(1, 999999999);
                }
                var randomDate = RandomDate(startDate, endDate);
                var employee = new Employee(randomId, "Emp-" + (i + 1).ToString(), randomDate, rnd.Next(1111, 9999), AgeCalculate(randomDate));
                employee.Company = dataSlot.Companies[rnd.Next(companiesCount)];
                dataSlot.Employees.Add(employee);
            }
        }
    }
    public class CsvHandler
    {
        public static List<string> ReadAsync(string filePath, bool hasHeader = true)
        {
            List<string> lines = new List<string>();
            if (!File.Exists(filePath))
            {
                return lines;
            }
            lines = File.ReadAllLines(filePath).ToList();
            if (hasHeader)
            {
                lines.RemoveAt(0);
            }
            return lines.Where(e=>!String.IsNullOrEmpty(e)).ToList();
        }

        public static void WriteAsync(string filePath, List<string>lines)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var item in lines)
                {
                    sw.WriteLine(item);
                }
            }
        }

    }
    

}
