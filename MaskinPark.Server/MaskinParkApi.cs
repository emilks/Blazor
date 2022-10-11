using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using MaskinPark.Shared;
using MaskinPark.Server.Entities;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Linq;
using MaskinPark.Server.Extensions;
using System.Collections.Generic;

namespace MaskinPark.Server
{
    public static class MaskinParkApi
    {
        /*
        [FunctionName("MAskinTest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "maskinPark")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        */
        
        [FunctionName("Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "maskinPark")] HttpRequest req,
            [Table("machines", Connection = "AzureWebJobsStorage")] CloudTable table,
            //[Table("machines", Connection = "AzureWebJobsStorage")] IAsyncCollector<MachineTableEntity> table,
            ILogger log)
        {
            log.LogInformation("Get Item");

            var query = new TableQuery<MachineTableEntity>();
            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            var response = result.Select(Mapper.ToMachine).ToList();

            return new OkObjectResult(response);
        }

        [FunctionName("Create")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "maskinPark")] HttpRequest req,
            [Table("machines", Connection = "AzureWebJobsStorage")] CloudTable table,
            //[Table("machines", Connection = "AzureWebJobsStorage")] IAsyncCollector<MachineTableEntity> table,
            ILogger log)
        {
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createMachine = JsonConvert.DeserializeObject<CreateMachine>(requestBody);

            if (createMachine == null || string.IsNullOrWhiteSpace(createMachine.Name)) return new BadRequestResult();

            var machine = new Machine { 
                Name = createMachine.Name,
                Status = "Off",
                Data = "Test",
                DataTime = DateTime.Now
            };

            //await itemTable.AddAsync(item.ToTableEntity());

            var operation = TableOperation.Insert(machine.ToTableEntity());
            var res = await table.ExecuteAsync(operation);

            return new OkObjectResult(machine);
        }

        /*
        [FunctionName("Get")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "maskinPark")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Item");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var items = GetMachines();

            return new OkObjectResult(items);
        }
        */

        private static IEnumerable<Machine> GetMachines()
        {
            return new List<Machine>()
            {
                new Machine()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    Name = "Machine n1",
                    Status = "On",
                    Data = "test data",
                    DataTime = DateTime.Now
                },
                new Machine()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    Name = "Machine n2",
                    Status = "On",
                    Data = "test data",
                    DataTime = DateTime.Now
                },
                new Machine()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    Name = "Machine n3",
                    Status = "Off",
                    Data = "test data",
                    DataTime = DateTime.Now
                }
            };
        }
    }
}
