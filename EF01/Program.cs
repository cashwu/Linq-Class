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
            UpdateRowversionData();

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void UpdateRowversionData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var dep = db.Departement.FirstOrDefault(a => a.Id == 1);

                Console.ReadLine();

                dep.Name = "AA11";
                db.SaveChanges();

                Console.WriteLine($"Id :: {dep.Id}, Name :: {dep.Name}");
            }
        }

        private static void InsertRowversionData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var dep1 = new Departement { Id = 1, Name = "AA" };
                var dep2 = new Departement { Id = 2, Name = "BB" };

                db.Departement.Add(dep1);
                db.Departement.Add(dep2);

                db.SaveChanges();

                Console.WriteLine("done");
                Console.ReadLine();
            }
        }

        private static void Fixed()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var emp = db.Employee.FirstOrDefault(a => a.Id == 1);

                Console.ReadLine();

                emp.Name = "AA11";
                db.SaveChanges();

                Console.WriteLine($"Id :: {emp.Id}, Name :: {emp.Name}");
            }
        }

        private static void Pager()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                foreach (var item in GetPager(db.Employee.OrderBy(a => a.Id), 2, 1))
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

        public static IQueryable<T> GetPager<T>(IQueryable<T> source, int pageSize, int page)
        {
            return source.Skip(pageSize * page).Take(pageSize); 
        }

        private static void Refresh()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                var emp = db.Employee.FirstOrDefault(a => a.Id == 1);
                Console.WriteLine($"Id :: {emp.Id}, Name :: {emp.Name}");
                Console.ReadLine();

                emp = db.Employee.FirstOrDefault(a => a.Id == 1);
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