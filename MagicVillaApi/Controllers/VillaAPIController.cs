﻿using MagicVillaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaApi.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController: ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetEnumerable()
        {
            return new List<Villa>
            {
            new Villa {Id = 1,Name = "Poll View"},
            new Villa {Id = 2,Name = "Beach View"}
            };
        }
    }
}