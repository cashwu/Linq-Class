using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EF01
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InheritanceTable();

            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void InheritanceTable()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;
                db.Person.Add(new Person
                {
                    Id = 1,
                    Name = "p",
                });

                db.Person.Add(new Manager
                {
                    Id = 2,
                    Name = "m"
                });

                db.SaveChanges();
            }
        }

        private static void ComplexType()
        {
            using (var db = new MyEFEntities())
            {
                db.Departement.Add(new Departement
                {
                    Id = 1,
                    ComplexProperty = new DepartComplex
                    {
                        Name = "cc",
                        Version = new byte[] {1}
                    }
                });

            }
        }

        private static void CallSp()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var emp = db.GetEmployee(1);
                foreach (var item in emp)
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

        private static void GetConnectData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var result = from s in db.Department2
                    where s.Employee2.Any(a => a.Name == "AA")
                    select s;

                foreach (var item in result)
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

        private static void AddConnectData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var dep = new Department2
                {
                    Id = 1,
                    Name = "Marketing"
                };

                dep.Employee2.Add(new Employee2
                {
                    Name = "AA"
                });

                dep.Employee2.Add(new Employee2
                {
                    Name = "BB"
                });

                db.Department2.Add(dep);

                db.SaveChanges();
            }
        }

        private static void Local()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                db.Employee.Load();

                foreach (var item in db.Employee.Local.Where(a => a.Id == 1))
                {
                    Console.WriteLine($"Id :: {item.Id}, Name :: {item.Name}");
                }
            }
        }

        private static void AttachedWithFixException()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var item = new Employee
                {
                    Id = 1
                };

                db.Employee.Attach(item);
                item.Name = "Cash";

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"concurrynct exception object id :" +
                                      $" {((Employee)ex.Entries.FirstOrDefault().Entity).Id}");
                }
            }
        }

        private static void Attached()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var item = new Employee
                {
                    Id = 1
                };

                db.Employee.Attach(item);
                item.Name = "Cash";

                db.SaveChanges();
            }
        }

        private static void ProperytValue()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var emp = db.Employee.FirstOrDefault(a => a.Id == 1);
                emp.Name = "AA11";

                Console.WriteLine($"OriginalValue :: {db.Entry(emp).Property(a => a.Name).OriginalValue}, " +
                                  $"CurrentValue :: {db.Entry(emp).Property(a => a.Name).CurrentValue}");
            }
        }

        private static void ListChangeState()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var emp = db.Employee.FirstOrDefault(a => a.Id == 1);
                emp.Name = "AA11";

                foreach (var item in db.ChangeTracker.Entries<Employee>())
                {
                    Console.WriteLine($"Id :: {item.Entity.Id}, State :: {item.State}");
                }
            }
        }

        private static void Exception()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                //var dep1 = new Departement { Id = 1, Name = "AA" };
                //var dep2 = new Departement { Id = 2, Name = "BB" };

                //db.Departement.Add(dep1);
                //db.Departement.Add(dep2);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"concurrency error , object id : {((Departement)ex.Entries.FirstOrDefault().Entity).Id}");
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"insert records fail, message {ex.Message}, inner message {ex.InnerException?.InnerException?.Message}");
                }

                Console.WriteLine("done");
                Console.ReadLine();
            }
        }

        private static void UpdateRowversionData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = Console.WriteLine;

                var dep = db.Departement.FirstOrDefault(a => a.Id == 1);

                Console.ReadLine();

                //dep.Name = "AA11";
                db.SaveChanges();

                //Console.WriteLine($"Id :: {dep.Id}, Name :: {dep.Name}");
            }
        }

        private static void InsertRowversionData()
        {
            using (var db = new MyEFEntities())
            {
                db.Database.Log = log => Console.WriteLine(log);

                //var dep1 = new Departement { Id = 1, Name = "AA" };
                //var dep2 = new Departement { Id = 2, Name = "BB" };

                //db.Departement.Add(dep1);
                //db.Departement.Add(dep2);

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