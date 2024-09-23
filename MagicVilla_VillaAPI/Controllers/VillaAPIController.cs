using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {

            return await Task.FromResult(Ok(VillaStore.villaDtos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return await Task.FromResult(BadRequest());
            }
            var villa = VillaStore.villaDtos.FirstOrDefault(x => x.Id == id);
            if(villa is null)
            {
                return await Task.FromResult(NotFound());
            }
            return await Task.FromResult(Ok(villa));
        }
    }
}
