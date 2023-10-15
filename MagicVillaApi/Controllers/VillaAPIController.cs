using MagicVillaApi.Data;
using MagicVillaApi.Models;
using MagicVillaApi.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaApi.Controllers
{
    //[Route("api/Controller")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(200)]//ok
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Not Found
        //  [ProducesResponseType(404)]//Not Found
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Bad Request
          // [ProducesResponseType(400)]//Bad Request
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Bad Request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//Not Found
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            if (villaDto==null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);

            return Ok(villaDto);
        }

    }

     


}
