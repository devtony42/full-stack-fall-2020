using System.Collections.Generic;
using Emailer.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Emailer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _repo;
        private readonly ILogger<CustomerController> _logger;
        
        // private static readonly string[] Summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };
        //
        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //         {
        //             Date = DateTime.Now.AddDays(index),
        //             TemperatureC = rng.Next(-20, 55),
        //             Summary = Summaries[rng.Next(Summaries.Length)]
        //         })
        //         .ToArray();
        // }

        public CustomerController(IRepository<Customer> repo, ILogger<CustomerController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public List<Customer> Read()
        {
            var rval = _repo.Get();
            return rval;
        }

        [HttpGet]
        [Route("{id}")]
        public Customer Read(string id)
        {
            var rval = _repo.Get(id);
            return rval;
        }

        [HttpPost]
        public BoolResult Create(Customer record)
        {
            var rval = _repo.Insert(record).AsResult();
            return rval;
        }

        [HttpPut]
        public BoolResult Update(Customer record)
        {
            var rval = _repo.Update(record).AsResult();
            return rval;
        }

        [HttpDelete]
        [Route("{id}")]
        public BoolResult Delete(string id)
        {
            var rval = _repo.Delete(id).AsResult();
            return rval;
        }
    }
}