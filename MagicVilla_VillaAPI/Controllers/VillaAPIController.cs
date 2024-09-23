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

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaDto villaDto) 
        {
            if(VillaStore.villaDtos.FirstOrDefault(x => x.Name.ToLower() == villaDto.Name.ToLower()) is not null)
            {
                ModelState.AddModelError("NameError", "Name must be unique.");
                return await Task.FromResult(BadRequest(ModelState));
            }

            if (villaDto is null) 
            {
                return await Task.FromResult(BadRequest());
            }

            if(villaDto.Id > 0)
            {
                return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError));
            }

            villaDto.Id = VillaStore.villaDtos.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaDtos.Add(villaDto);

            return await Task.FromResult(CreatedAtRoute("GetVilla",new {id = villaDto.Id}, villaDto));
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return await Task.FromResult(BadRequest());
            }
            var villa = VillaStore.villaDtos.FirstOrDefault(x => x.Id == id);
            if (villa is null)
            {
                return await Task.FromResult(NotFound());
            }

            VillaStore.villaDtos.Remove(villa);
            return await Task.FromResult(NoContent());
        }
    }
}
