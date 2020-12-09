using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Count)],
                Moderator = WetterLeute[rng.Next(WetterLeute.Count)]
            })
            .ToArray();
        }
        private static List<string> summaries = new List<string>()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"

        };
        [HttpGet]
        [Route("AddSuammry")]
        public List<string> AddSummary(string S)
        {
            if (!summaries.Contains(S))   // Alternaitve: summaries.Add(S); summaries = summaries.Distinct().ToList();   
                summaries.Add(S);
            return summaries;
        }
        [HttpGet]
        [Route("RemoveSummaries/{v}")]
        public List<string> RemoveSummaries(string v)
        {
            summaries.Remove(v);
            return summaries;
        }
        [HttpGet]
        [Route("UpdateSummaries")]
        public List<string> UpdateSummaries(string OldValue, string NewValue)
        {
            /*int Position = summaries.IndexOf(OldValue);
            summaries.Remove(OldValue);
            summaries.Insert(Position, NewValue);
            return summaries;
            */
            summaries = summaries.ConvertAll(s => (s == OldValue) ? NewValue : s);
            return summaries;
        }
        [HttpGet]
        [Route("SummariesVorschalg")]
        public List<string> SummariesVorschalg(string Suche)
        {
            List<string> Result = summaries.FindAll(s => s.StartsWith(Suche));
            return (Result.Count != 0) ? Result : new List<string> { "Geht nicht!" };
            /*if (Result.Count == 0) {
                Result.Add("Keine bekannte Vorgabe!");
            }
            return Result;
        }
            */
        }
        List<Account> WetterLeute = new List<Account> { new Account { Alter = 26, Name = "Hans", ID = "256" }, new Account { ID = "721", Name = "Joscha", Alter = 77 } };
        public Account ChangeName(string ID, String NewName)
        {
            WetterLeute.ForEach(s => { if (s.ID == ID) s.Name = NewName; });
            return WetterLeute.Find(v => v.ID == ID);
        }
        /*[HttpGet] 
        [Route("GetData")]
        public  SQLiteDataReader GetData()
         {
            SQLiteConnection myconnection = new SQLiteConnection("data Source= DemoDatenbank.db");
            string ConnectionString = ConfigurationManager.AppSettings["DemoDatenbank.dp"]; //Notwendig?
            myconnection.Open();
            string query = "SELECT * FROM Personen";
            SQLiteCommand GetCommand = new SQLiteCommand(query, myconnection);
            SQLiteDataReader result = GetCommand.ExecuteReader();
            myconnection.Close();
            return result;
        }  

        [HttpGet]
        [Route("InsertData")]
        public void InsetData(string table,string listofvalues, string values)
        {
            SQLiteConnection addingconnection = new SQLiteConnection("data Source= DemoDatenbank.db");
            string ConnectionString = ConfigurationManager.AppSettings["DemoDatenbank.dp"];
            addingconnection.Open();
            string queryInser = $"INSERT INTO {table}({listofvalues}) VALUES {values}";
            SQLiteCommand GetCommand = new SQLiteCommand(queryInser, addingconnection);
            SQLiteDataReader result = GetCommand.ExecuteReader();
            addingconnection.Close();
        }
        */

        [HttpGet]
        [Route("GetData")]
        public static List<string> GetData()
        {
            List<string> output = new List<string>();
             SQLiteConnection myconnection = new SQLiteConnection("data Source= TestDB.db");
             myconnection.Open();
            if (myconnection.State == System.Data.ConnectionState.Open)
            {
                SQLiteCommand query = myconnection.CreateCommand();
                query.CommandText = "SELECT * FROM Personen";
                SQLiteDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(reader.GetString(0));
                    output.Add(reader.GetString(1));
                    output.Add(reader.GetString(2));

                }
                return output;
            }
            else {
                return output; }

        }
        [HttpGet]
        [Route("InsertData/{ID}/{Name}/{Alter}")]
        public static void InsertData(string ID, string Name, string Alter)
        {
            SQLiteConnection myconnection = new SQLiteConnection("data Source=TestDB.db");
            myconnection.Open();
            if(myconnection.State == System.Data.ConnectionState.Open)
            {
                SQLiteCommand query = myconnection.CreateCommand();
                query.CommandText = $"INSERT INTO Personen Values ({ID},{Name},{Alter});";
                query.ExecuteReader();
                myconnection.Close();
            }
        }
    }
}
