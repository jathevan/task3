using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using task3.Models;

namespace task3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CostController : ControllerBase
    {
        private readonly ILogger<CostController> _logger;

        public CostController(ILogger<CostController> logger)
        {
            _logger = logger;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{passengerCount}")]
        public async Task<IEnumerable<Data>> Get(int passengerCount)
        {
            List<Data> dataSet = new List<Data>();

            try
            {
                //Now we will have our using directives which would have a HttpClient
                using HttpClient client = new HttpClient();
                //Now get your response from the client from get request to baseurl.
                //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                using HttpResponseMessage res = await client.GetAsync("https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest").ConfigureAwait(false);
                //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                using HttpContent content = res.Content;
                //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                string data = await content.ReadAsStringAsync().ConfigureAwait(false);
                //If the data is not null, parse the data to a C# object
                if (data != null)
                {
                    //Parse your data into a object.
                    var dataObj = JObject.Parse(data);

                    int count = dataObj["listings"].Count();

                    for (int i = 0; i < count; i++)
                    {
                        var name = dataObj["listings"][i]["name"].Value<string>();
                        double pricePerPassenger = dataObj["listings"][i]["pricePerPassenger"].Value<double>();
                        int maxPassengers = dataObj["listings"][i]["vehicleType"]["maxPassengers"].Value<int>();
                        if (maxPassengers == passengerCount)
                        {
                            //Calculate the total price & total price should be 2 decimal value
                            double totalPrice = Math.Round(maxPassengers * pricePerPassenger, 2);
                            dataSet.Add(new Data(name, totalPrice, maxPassengers));
                        }
                    }
                }
                else
                {
                    //If data is null log it into console.
                    Console.WriteLine("Data is null!");
                }
            }
            //Catch any exceptions and log it into the console.
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            // Sort the total price list
            List<Data> SortedList = dataSet.OrderBy(o => o.TotalPrice).ToList();
            return SortedList;
        }
    }
}