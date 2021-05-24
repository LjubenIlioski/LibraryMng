using LibraryMng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryMng.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(string fileName)
        {
            Path = $"..\\..\\..\\..\\{fileName}";

            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }

            var result = File.ReadAllText(Path);
            var deserialzedList = JsonConvert.DeserializeObject<List<T>>(result);
            Data = deserialzedList;
        }

        protected string Path { get; set; }
        protected List<T> Data { get; set; }

        public void SaveChanges()
        {
            var serialzed = JsonConvert.SerializeObject(Data);
            File.WriteAllText(Path, serialzed);
        }

        public void Create(T entity)
        {
            entity.Id = GenerateId();
            Data.Add(entity);
        }

        public void Delete(T entity)
        {
            Data.Remove(entity);
        }

        public List<T> GetAll()
        {

            return Data;
        }

        public T GetById(int id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        public T GetFirstOrDefault(Func<T, bool> predicate)
        {
            return Data.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAllWhere(Func<T, bool> predicate)
        {
            return Data.Where(predicate);
        }

        private int GenerateId()
        {
            var newId = 0;

            if (Data.Count > 0)
            {
                newId = Data.Max(x => x.Id);
            }

            return newId + 1;
        }
    }
}
