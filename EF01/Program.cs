using System;

namespace EF01
{
    internal class Program
    {
        private static void Main(string[] args)
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
    }
}