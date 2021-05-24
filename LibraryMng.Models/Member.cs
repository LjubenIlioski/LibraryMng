using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMng.Models
{
    public class Member : BaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public List<int> RentedBooks { get; set; } = new List<int>();

        public void PrintInfo()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Member ID: {Id}\n Name: {Name}\n Surname: {Surname}\n Email: {EMail}");
            if (RentedBooks.Count == 0)
            {
                Console.WriteLine(" No rented books for this Member!");
            }
            else if (RentedBooks.Count > 0)
            {
                Console.WriteLine($" Rented books ID: {string.Join(",", RentedBooks)}");
            }
            Console.WriteLine("----------------------------------------------------");
        }
        
    }
}
