using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain;
using Factura.Service;
using Microsoft.AspNetCore.Mvc;

namespace Factura.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostoController : Controller
    {
        private ICostoService costoService;
        public CostoController(ICostoService costoService)
        {
            this.costoService = costoService;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                costoService.GetAll()
            );
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(
                costoService.GetById(id)
            );
        }
        [HttpPost]
        public ActionResult Post([FromBody] Costo costo)
        {
            return Ok(
               costoService.Save(costo)
            );
        }
        [HttpPut]
        public ActionResult Put([FromBody] Costo costo)
        {
            return Ok(
                costoService.Update(costo)
            );
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                costoService.DeleteById(id)
            );
        }
    }
}