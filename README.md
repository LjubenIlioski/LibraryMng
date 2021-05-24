# LibraryMng
Online library management


An application that will help the employees of the library manage book stocks, manage members, rent books, track rented books and close book rents.

Application made in C# with N-tire architecture with no classic DB. Everything is saved intp TXT file fith JSON serilization

The requiremts of the application:

  Users of the application will be the employees of the library. 
  Every book has:
  Unique Id
  Title
  Number of copies
  
Books can be rented only by library members and each member can rent up to 3 books at a time. 
  Each member has: 
  Unique Id – which the member needs to give when renting a book
  Name
  Surname
  Email – there can not be two members with the same email address
  
The application should have the following functionalities: 
  Rent
  Input member Id (validate if there is a member with input Id and if the member can rent more books)
  Present available books for rent so the member can choose
  Based on the member’s choice mark the book as rented and assign it to the member
  
  Close rent	
  Input member Id (validate)
  Show the list of books that the user has rented
  Based on the member's input close the rent of the book and unassign it from the member
  
 Manage members

Show all members

Create new member

Delete existing member

Manage books

Show all books

Add new book (can not have multiple books with the same title)

Delete existing book

Print all rented books


