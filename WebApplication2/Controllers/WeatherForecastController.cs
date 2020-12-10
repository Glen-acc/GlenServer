﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WebApplication2.Startup;
using static WebApplication2.AccountModerator;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Wahrscheinlich für Exception Verantwortlich1
        // Durch neue Klasse CreateContext wenn möglich Konstruktor umgehen
        CreateContext Db;
        public WeatherForecastController(CreateContext context)
        {
            Db = context;
        }
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
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        // Hier kommen die Methoden bzgl. der SQlite DB
        [HttpGet]
        [Route("InserData/{Name}/{Alter}/{ID}")]
        public void InsertAccountModerators(string Name, int Alter, string ID)
        {
            Db.AccountModerators.AddRange(new AccountModerator[]{
                new AccountModerator { Name = $"{Name}", Alter = Alter, ID = $"{ID}" }, }
            );
            Db.SaveChanges();
        }
        [HttpGet]
        [Route("GetData")]
        public List<string> GetDataAccountModerators()
        {
            List<string> output = new List<string>();
            foreach(AccountModerator X in Db.AccountModerators)
            {
                output.Add(X.Name);
                output.Add(X.ID);
                output.Add(X.Alter.ToString());
            }
            return output; 
        } 
    }
}
