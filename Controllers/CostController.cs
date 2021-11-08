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

        [HttpGet]
        public async Task<IEnumerable<Data>> Get()
        {
            List<Data> dataSet = new List<Data>();
            const int passengerCount = 2;

            //Have your api call in try/catch block.
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
                            double totalPrice = maxPassengers * pricePerPassenger;
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
            /*   if (dataSet.Count!= 0)
               {
                   return dataSet;
               }
               else if (dataSet.Count == 0)
               {
                   List<Data> dataSet2 = new List<Data>();
                   const string message = "No Cars available";
                   dataSet2.Add(new Data(message));
                   return dataSet2;
               }*/

            return dataSet;
        }





      /*  [HttpGet]
        public async Task<IEnumerable<Data>> Get()
        {
            List<Data> dataSet = new List<Data>();
            const int passengerCount = 2;

            //Have your api call in try/catch block.
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
                            double totalPrice = maxPassengers * pricePerPassenger;
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
         *//*   if (dataSet.Count!= 0)
            {
                return dataSet;
            }
            else if (dataSet.Count == 0)
            {
                List<Data> dataSet2 = new List<Data>();
                const string message = "No Cars available";
                dataSet2.Add(new Data(message));
                return dataSet2;
            }*//*

            return dataSet;
        }*/
    }
}
