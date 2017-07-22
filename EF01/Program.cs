using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EF01
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Refresh();

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void Refresh()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var emp = db.Employee.FirstOrDefault(a => a.Id == 1);
                Console.WriteLine($"Id :: {emp.Id}, Name :: {emp.Name}");
                Console.ReadLine();

                var objContext = db as IObjectContextAdapter;
                objContext.ObjectContext.Refresh(RefreshMode.StoreWins, emp);
                Console.WriteLine($"Id :: {emp.Id}, Name :: {emp.Name}");
            }
        }

        private static void ObjectIdentity()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var emp1 = db.Employee.FirstOrDefault(a => a.Id == 1);
                var emp2 = db.Employee.FirstOrDefault(a => a.Id == 1);

                if (emp1 == emp2)
                {
                    Console.WriteLine("is same");
                }
            }
        }

        private static void InsertData()
        {
            using (var db = new MyEFEntities())
            {
                var employee1 = new Employee { Name = "AA" };
                var employee2 = new Employee { Name = "BB" };
                var employee3 = new Employee { Name = "CC" };

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

        private static void UpdateData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var emp = db.Employee.Where(a => a.Id == 1).FirstOrDefault();
                emp.Name = "AA3";
                db.SaveChanges();
            }
        }
    }
}