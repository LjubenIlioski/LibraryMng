using LibraryMng.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMng.Repository
{
    public class MembersRepository: BaseRepository<Member>
    {
        public MembersRepository():base("MembersRepository.txt")
        {

        }
    }
}
