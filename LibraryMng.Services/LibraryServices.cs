using LibraryMng.Common;
using LibraryMng.Common.Validation;
using LibraryMng.Models;
using LibraryMng.Repository;
using System;
using System.Collections.Generic;

namespace LibraryMng.Services
{
    public class LibraryServices
    {
        public LibraryServices()
        {
            _bookRepository = new BookRepository();
            _membersRepository = new MembersRepository();
        }

        private BookRepository _bookRepository { get; set; }
        private MembersRepository _membersRepository { get; set; }


        public void CreateNewMember()
        {
            Console.WriteLine("Please enter member Name:");
            var memberName = InputReader.StringReader();
            Console.WriteLine("Please enter member Surname:");
            var memberSurname = InputReader.StringReader();
            Console.WriteLine("Please enter member E-mail:");
            var memberEmail = InputReader.StringReader().ToLower();

            var member = _membersRepository.GetFirstOrDefault(x => x.EMail == memberEmail);
            if (member != null)
            {
                throw new LibraryException($"Member with this E-mail ({member.EMail} already exist!");
            }

            var createMember = new Member()
            {
                Name = memberName,
                Surname = memberSurname,
                EMail = memberEmail
            };
            _membersRepository.Create(createMember);
            _membersRepository.SaveChanges();
            Console.WriteLine($"Member created successfully! Unique ID is {createMember.Id} !");
        }

        public void DeleteMember()
        {
            ShowAllMembers();
            Console.WriteLine("Please enter member ID:");
            var memberId = InputReader.IntReader();

            var member = _membersRepository.GetFirstOrDefault(x => x.Id == memberId && x.RentedBooks.Count == 0);
            if (member == null)
            {
                throw new LibraryException("Member not found! Or member haven't returned books!");
            }
            _membersRepository.Delete(member);
            _membersRepository.SaveChanges();
            Console.WriteLine($"Member with ID: {memberId} successfully removed!");
        }

        public void ShowAllMembers()
        {
            var allMembers = _membersRepository.GetAll();
            allMembers.ForEach(x => x.PrintInfo());
        }

        public void AddBook()
        {
            Console.WriteLine("Please enter book Title:");
            var bookTitle = InputReader.StringReader(); ;
            var checkBook = _bookRepository.GetFirstOrDefault(x => x.Title == bookTitle);
            if (checkBook == null)
            {
                throw new LibraryException("Book already exist!");
            }
            Console.WriteLine("Please enter number of copies:");
            var bookCopies = InputReader.IntReader();

            var createBook = new Book()
            {
                Title = bookTitle,
                NumberOfCopies = bookCopies
            };
            _bookRepository.Create(createBook);
            _bookRepository.SaveChanges();
            Console.WriteLine($"Book with ID {createBook.Id} successfully added!");
        }

        public void RemoveBook()
        {
            ShowAllBooks();
            Console.WriteLine("Please enter Book ID:");
            var bookID = InputReader.IntReader();

            var book = _bookRepository.GetFirstOrDefault(x => x.Id == bookID && x.RentedToMembers.Count == 0);
            if (book == null)
            {
                throw new LibraryException("Book not found! Or book is still rented to someone!");
            }
            _bookRepository.Delete(book);
            _bookRepository.SaveChanges();
            Console.WriteLine($" Book with ID: {bookID} successfully removed!");
        }

        public void PrintRentedBooks()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Rented Books:");
            Console.WriteLine("----------------------------------------------");
            var rentedBooks = _bookRepository.GetAllWhere(x => x.RentedToMembers.Count != 0);

            foreach (var book in rentedBooks)
            {
                book.PrintInfo();
            }
            Console.WriteLine("----------------------------------------------");

        }

        public void ShowAllBooks()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Rented Books:");
            Console.WriteLine("----------------------------------------------");
            var allBooks = _bookRepository.GetAll();

            foreach (var book in allBooks)
            {
                book.PrintInfo();
            }

            Console.WriteLine("----------------------------------------------");
        }

        public void ShowAllAveilableBooks()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Available Books:");
            Console.WriteLine("----------------------------------------------");

            var booksToRent = _bookRepository.GetAllWhere(x => x.NumberOfCopies > 0);

            foreach (var book in booksToRent)
            {
                book.PrintInfo();
            }
            Console.WriteLine("----------------------------------------------");
        }

        public void EditQuantity()
        {
            ShowAllBooks();
            Console.WriteLine("Please enter Book ID:");
            var bookID = InputReader.IntReader();

            var book = _bookRepository.GetFirstOrDefault(x => x.Id == bookID);

            if (book == null)
            {
                throw new LibraryException("Book not found!");
            }
            Console.WriteLine("Enter new quantity:");

            var bookNewQuantity = InputReader.IntReader();

            book.NumberOfCopies = bookNewQuantity;
            _bookRepository.SaveChanges();
            Console.WriteLine($"Quantity for the book : {book.Title} changed successfully!");

        }

        public void RentBook()
        {
            ShowAllMembers();
            Console.WriteLine("Please enter member Id:");
            var memberId = InputReader.IntReader();
           
            var theMember = _membersRepository.GetFirstOrDefault(x => x.Id == memberId && x.RentedBooks.Count < 3);
            if (theMember == null)
            {
                throw new LibraryException($"Member with ID {memberId}not found!(Or member has more than 3 rented books!)");
            }

            ShowAllAveilableBooks();

            Console.WriteLine("Please choose book ID from above:");
            var bookId = InputReader.IntReader();

            var theBook = _bookRepository.GetFirstOrDefault(x => x.Id == bookId);

           
            if (theBook == null)
            {
                throw new LibraryException("Book not found!");
            }
            else if (theBook.RentedToMembers.Contains(theMember.Id))
            {
                throw new LibraryException("Book is already rented to this member!");
            }
            else if (theBook.NumberOfCopies <= 0)
            {
                throw new LibraryException("Book is out of stock!");
            }
            theBook.RentedToMembers.Add(theMember.Id);
            theBook.DecreaseQuantity();
            theMember.RentedBooks.Add(theBook.Id);
            _bookRepository.SaveChanges();
            _membersRepository.SaveChanges();
            Console.WriteLine("Book is rented successfully!");
        }

        public void CloseRent()
        {
            ShowAllMembers();
            Console.WriteLine("Please enter member Id:");
            var memberId = InputReader.IntReader();
         
            var theMember = _membersRepository.GetFirstOrDefault(x => x.Id == memberId && x.RentedBooks.Count != 0);
            if (theMember == null)
            {
                throw new LibraryException("Member not found!(Or member doesn't have rents!");
            }
            Console.WriteLine($" Rented books ID: {string.Join(",", theMember.RentedBooks)}");

            PrintRentedBooks(theMember.RentedBooks);

            Console.WriteLine("Please enter book ID:");
            var bookId = InputReader.IntReader();
         
            var theBook = _bookRepository.GetFirstOrDefault(x => x.Id == bookId && x.RentedToMembers.Contains(theMember.Id));
            if (theBook == null)
            {
                throw new LibraryException("Book not found!");
            }
            theBook.RentedToMembers.Remove(theMember.Id);
            theMember.RentedBooks.Remove(theBook.Id);
            theBook.IncreaseQuantity();
            _bookRepository.SaveChanges();
            _membersRepository.SaveChanges();
            Console.WriteLine("Rent successfully closed!");
        }

        private void PrintRentedBooks(List<int> rentedBooks)
        {
            foreach (var bookId in rentedBooks)
            {
                var bookToPring = _bookRepository.GetById(bookId);
                bookToPring.PrintTitle();

            }
        }
    }
}
