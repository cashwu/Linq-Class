using System;
using System.Linq;

namespace EF01
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            QueryDataUsingLike();

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void InsertData()
        {
            using (var db = new MyEFEntities())
            {
                var employee1 = new Employee {Name = "AA"};
                var employee2 = new Employee {Name = "BB"};
                var employee3 = new Employee {Name = "CC"};

                db.Employee.Add(employee1);
                db.Employee.Add(employee2);
                db.Employee.Add(employee3);

                db.SaveChanges();

                Console.WriteLine("done");
                Console.ReadLine();
            }
        }

        private static void QueryData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                foreach (var item in db.Employee.Where(a => a.Name == "AA"))
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

        private static void QueryDataUsingLike()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                foreach (var item in db.Employee.Where(a => a.Name.Contains("A")))
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

    }
}