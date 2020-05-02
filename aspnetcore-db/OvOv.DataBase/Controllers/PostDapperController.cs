using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostDapperController : ControllerBase
    {
        private readonly AppDb _db;
        public PostDapperController(AppDb db)
        {
            _db = db;
        }

        // GET api/async
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (_db)
            {
                return new OkObjectResult("");
            }
        }
        // GET api/async/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            using (_db)
            {
                return new OkObjectResult("");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Post post)
        {
            using (_db)
            {
                await _db.Connection.OpenAsync();
                _db.Connection.Execute(@"INSERT INTO `Post` (`Title`, `Content`) VALUES (@title, @content);", post);
                return new OkObjectResult(post);
            }
        }
    }
}