using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.IO;


namespace WebApplication1
{
    public class DataBase
    {
        
        public static SQLiteDataReader GetData()
        {
            SQLiteConnection myconnection = new SQLiteConnection("data Source= DemoDatenbank.db");
            string ConnectionString = ConfigurationManager.AppSettings["BloggingDatabase"];
            string query = "SELECT * FROM Personen";
            SQLiteCommand GetCommand = new SQLiteCommand(query, myconnection);
            SQLiteDataReader result = GetCommand.ExecuteReader();
            return result; 
        }
    }
}
 
  

        
