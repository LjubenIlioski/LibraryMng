using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMng.Common.Validation
{
    public static class InputReader
    {
        public static string StringReader()
        {
            var userInput = Console.ReadLine().Trim();
            var flag = 0;
            while (userInput.Length < 2)
            {
                Console.WriteLine($"The Input must be at least 2 characters You have {3 - flag} atempts left");
                Console.WriteLine("Please Insert Input Again or n to exit this menu ");
                userInput = Console.ReadLine().Trim();

                flag++;
                if (flag >= 3 || userInput == "n")
                {
                    throw new LibraryException("Invalid Registration input");
                }
            }

            return userInput;
        }
        public static int IntReader()
        {
            bool isParsed = int.TryParse(Console.ReadLine(), out int result);

            if (isParsed && result >= 0)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid Input Do you like to try again. Input must be a positive integer Enter yes to try again");
                var userInput = Console.ReadLine();
                if (userInput == "yes")
                {
                    result = IntReader();
                }
                else
                {
                    throw new LibraryException("Invalid input");
                }
            }


            return result;
        }
    }


}
