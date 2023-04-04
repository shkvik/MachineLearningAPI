using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestNetApi.MachineLearning;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class trainController : ControllerBase
    {

        [HttpGet("{id}")]
        public TrainStatus Get(int id)
        {
            return TrainingHandler.GetTrainStatus(id);
        }

        [HttpGet("train/{id}")]
        public async Task GetTrain(int id)
        {
            string url = "http://localhost:8081/getData/";
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            try
            {
                var ts = JsonConvert.DeserializeObject<TimeSerias>(value);
                if (ts != null)
                    TrainingHandler.TrainConnection(ts);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
