using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteRedesignAdminAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteRedesignAdminAPI.Controllers
{
    [EnableCors]
    [Route("[controller]")]
    [ApiController]
    public class WebsiteRedesignController : ControllerBase
    {

        string jsonPath = Path.GetFullPath("Data/people.json");
        string imagesPath = Path.GetFullPath("Images/");

        [HttpGet]
        public List<Person> Get()
        {
            List<Person> people = new List<Person>();

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            foreach (var (value, i) in people.Select((value, i) => (value, i)))
            {
                value.id = i;
            }
            return people;
        }

        [HttpGet]
        [Route("GetImageFileNames")]
        public string[] GetImageFileNames()
        {
            DirectoryInfo di = new DirectoryInfo(imagesPath);
            FileInfo[] files = di.GetFiles("*.*");
            List<string> str = new List<string>();
            foreach (FileInfo file in files)
            {
                str.Add(file.Name);
            }
            return str.ToArray();

        }

        [HttpGet]
        [Route("{imgName}")]
        public IActionResult Get(string imgName)
        {
            string path = Path.GetFullPath("Images/" + imgName);
            return PhysicalFile(path, "image/jpeg");
        }

        [HttpPut] // edit
        public void Put([FromBody] Person person)
        {
            List<Person> people = new List<Person>();

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            foreach (var (value, i) in people.Select((value, i) => (value, i)))
            {
                value.id = i;
            }

            foreach (var (value, i) in people.Select((value, i) => (value, i)))
            {
                if (person.id == value.id)
                {
                    value.name = person.name;
                    value.description = person.description;
                    value.image = person.image;
                    value.team = person.team;
                }
            }

            using (StreamWriter w = new StreamWriter(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(w, people);
            }

        }

        [HttpPost] // add
        public void Post([FromBody] Person person)
        {
            List<Person> people = new List<Person>();

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            people.Add(person);

            using (StreamWriter w = new StreamWriter(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(w, people);
            }

        }

        [HttpDelete]
        public void Delete([FromBody] Person person)
        {
            List<Person> people = new List<Person>();

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            foreach (var (value, i) in people.Select((value, i) => (value, i)))
            {
                value.id = i;
            }

            foreach (var (value, i) in people.Select((value, i) => (value, i)))
            {
                if (person.id == value.id)
                {
                    people.Remove(value);
                    break;
                }
            }

            using (StreamWriter w = new StreamWriter(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(w, people);
            }
        }
    }
}
