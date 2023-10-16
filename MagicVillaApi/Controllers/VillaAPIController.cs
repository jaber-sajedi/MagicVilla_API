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
        #region  Get

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }



        [HttpGet("{id:int}", Name = "GetVilla")]
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

        #endregion

        #region Create


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Bad Request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//Not Found
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {

            if (VillaStore.villaList.FirstOrDefault(c => c.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.TryAddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        #endregion

        #region Delete

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla(int id)
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
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        #endregion

        #region Update

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto==null || id!=villaDto.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(c => c.Id == id);
            villa.Name=villaDto.Name;
            villa.Sqft=villaDto.Sqft;
            villa.Occupency=villaDto.Occupency;

            return NoContent();
        }
        #endregion

    }

     


}
