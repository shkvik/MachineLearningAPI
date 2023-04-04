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

        [HttpPost("{id}")]
        public void Post([FromBody] string value, int id)
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
