using LibraryMng.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMng.Repository
{
    public class BookRepository:BaseRepository<Book>
    {
        public BookRepository():base("BookRepository.txt")
        {

        }
    }
}
