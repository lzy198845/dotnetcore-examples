using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OvOv.Core.Domain;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly TagService _tagService;
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] Tag tag)
        {
           await _tagService.UpdateAsync(tag);
        }
        
        [HttpPost]
        public async Task CreateAsync([FromBody] Tag tag)
        {
          await _tagService.CreateAsync(tag);
        }
    }


}