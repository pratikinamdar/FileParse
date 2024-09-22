using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FileParsing.Models;
using System.Runtime.CompilerServices;
using PersonalElectionMgmt.Models;

namespace FileParsing.Controllers
{
    public class DataController : ControllerBase
    {
        private Db db;
  
        private readonly List<Data> data;

        private string filePath = "C:\\Users\\prinamda\\source\\repos\\FileParsing\\FileParsing\\test.json";
        public DataController(IConfiguration config)
        {
            data = LoadDataSetFromFile(filePath);
            db = new Db(config.GetConnectionString("constr"));
        }
        private List<Data> LoadDataSetFromFile(string file)
        {
            using var streamReader = new StreamReader(filePath);
            var json = streamReader.ReadToEnd();
            if (json != null)
                return JsonSerializer.Deserialize<List<Data>>(json);
            else throw new Exception("json is null");
        }
        [HttpGet("{Id}")]
    public Data GetLargestId(int Id)
    {
            if (data != null)
            {
                PriorityQueue<Data, int> pq = new PriorityQueue<Data, int>(data.Count);



                foreach (var item in data)
                {
                    pq.Enqueue(item, item.Id);

                }
                 pq.Dequeue();
                return pq.Dequeue();

                    throw new Exception("Record not found");
            }
            else
                throw new Exception("Dataset is empty");

     }

        // getting data from user and storing it in json file
        [HttpPost("User")]
        public void Post()
        {
                string json = JsonSerializer.Serialize(data);
                System.IO.File.WriteAllText(filePath, json);
                db.Data.AddRange(data);
                db.SaveChanges();
        }

        [HttpDelete("DeleteUser")]

        public void Delete(int Id)
        {
            var da = (from d in db.Data
                     select d).ToList();
            db.Data.RemoveRange(da);
            db.SaveChanges();
        }


        [HttpGet("Data", Name = "GetAllData")]
        public IEnumerable<Data> GetAllData()
        {
            var r = ( from i in db.Data
                      select i).ToList();
            return r;
        }
    }

}



