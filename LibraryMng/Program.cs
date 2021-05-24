using LibraryMng.Common;
using LibraryMng.Services;
using System;
using System.IO;

namespace LibraryMng
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Welcome to our Library ) ");
            Console.WriteLine("---------------------------");
            var libraryService = new LibraryServices();
            var shouldContinue = "";

            do
            {
                try
                {
                    Console.WriteLine("---------------------------");
                    Console.WriteLine("Please choose one of the following options");
                    Console.WriteLine(" 1.Rent\n 2.Close rent\n 3.Manage members\n 4.Manage book \n 0. Exit this menu");

                    var userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            libraryService.RentBook();
                            break;
                        case "2":
                            libraryService.CloseRent();
                            break;
                        case "3":
                            ShowManageMemberMenu(libraryService);
                            break;
                        case "4":
                            ShowManageBookMenu(libraryService);
                            break;

                        case "0":
                            
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    }
                }
                catch (LibraryException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    File.AppendAllText("Log.txt", $"{DateTime.Now}. {ex.Message} {ex.StackTrace}");
                    Console.WriteLine($"Date of error: {DateTime.Now} An error has ocured. Please try again later.");
                }

                Console.WriteLine("Would you like to continue. Enter no to exit");
                shouldContinue = Console.ReadLine().Trim().ToLower();

            } while (shouldContinue != "no");
        }

        private static void ShowManageMemberMenu(LibraryServices libraryService)
        {
            var shouldContinue = "";
            while (shouldContinue != "no")
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Please choose the following options:");
                Console.WriteLine(" 1.Show all members\n 2.Create new member\n 3.Delete existing member \n 0. Exit this menu");
                var memberMenuInput = Console.ReadLine();
                switch (memberMenuInput)
                {
                    case "1":
                        libraryService.ShowAllMembers();
                        break;
                    case "2":
                        libraryService.CreateNewMember();
                        break;
                    case "3":
                        libraryService.DeleteMember();
                        break;
                    case "0":
                        shouldContinue = "no";
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
                if (memberMenuInput != "0") { 
                Console.WriteLine("Would you like to continue in this menu. Enter no to exit");
                shouldContinue = Console.ReadLine().Trim().ToLower();
                }
            }
        }

        private static void ShowManageBookMenu(LibraryServices libraryService)
        {
            var shouldContinue = "";
            while (shouldContinue != "no")
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Please choose the following options:");
                Console.WriteLine(" 1.Show all books\n 2.Add new book\n 3.Delete existing book\n 4.Print all rented books\n 5.Edit number of copies \n 0. Exit this menu");
                var bookMenuInput = Console.ReadLine();

                switch (bookMenuInput)
                {
                    case "1":
                        libraryService.ShowAllBooks();
                        break;
                    case "2":
                        libraryService.AddBook();
                        break;
                    case "3":
                        libraryService.RemoveBook();
                        break;
                    case "4":
                        libraryService.PrintRentedBooks();
                        break;
                    case "5":
                        libraryService.EditQuantity();
                        break;
                    case "0":
                        shouldContinue = "no";
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
                if (bookMenuInput != "0")
                {
                    Console.WriteLine("Would you like to continue in this menu. Enter no to exit");
                    shouldContinue = Console.ReadLine().Trim().ToLower();
                }
            }
        }
    }
}