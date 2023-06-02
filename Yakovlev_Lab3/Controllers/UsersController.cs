using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TspuWebLabs.Contracts;
using TspuWebLabs.Data;
using TspuWebLabs.Repositories;

namespace TspuWebLabs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersRepositoryInMemory usersRepository;

        public UsersController(IUsersRepositoryInMemory usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int? id)
        {
            if (id == null) return Ok(usersRepository.GetData());
            else
            {
                if (usersRepository.TryGet(id.Value, out User user) == true)
                    return Ok(user);
                else return BadRequest($"User ID = {id.Value} not found");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUserContract addUserContract)
        {
            usersRepository.Add(addUserContract.Name, addUserContract.Login, addUserContract.Password);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditUserContract editUserContract)
        {
            if (usersRepository.TryEdit(editUserContract.Id, editUserContract.Name, editUserContract.Login) == true)
                return Ok();
            else return BadRequest($"User ID = {editUserContract.Id} not found");

        }
    }
}
