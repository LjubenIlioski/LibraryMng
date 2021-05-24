using System;
using System.Collections.Generic;

namespace LibraryMng.Models
{
    public class Book : BaseEntity
    {

        public string Title { get; set; }
        public int NumberOfCopies { get; set; }
        public List<int> RentedToMembers { get; set; } = new List<int>(0);

        public void PrintInfo()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Book ID: {Id}\n Title: {Title.ToLower()}\n Number of copies: {NumberOfCopies}");
            if (RentedToMembers.Count > 0)
            {
                Console.WriteLine($" This Book is Rented to members Id: {string.Join(", ", RentedToMembers)}");
            }
            else if (RentedToMembers.Count == 0)
            {
                Console.WriteLine(" Book is not rented!");
            }
            Console.WriteLine("----------------------------------------------------");
        }

        public void PrintTitle()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Book ID: {Id}\n Title: {Title.ToLower()}\n Number of copies: {NumberOfCopies}");
            Console.WriteLine("----------------------------------------------------");
        }

        public void DecreaseQuantity()
        {
            NumberOfCopies -= 1;
        }

        public void IncreaseQuantity()
        {
            NumberOfCopies += 1;
        }
    }
}
