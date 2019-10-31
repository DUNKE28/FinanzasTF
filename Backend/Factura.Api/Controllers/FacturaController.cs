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
    public class FacturaController : Controller
    {
        private IFacturaService facturaService;
        public FacturaController(IFacturaService facturaService)
        {
            this.facturaService = facturaService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                facturaService.GetAll()
            );
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(
                facturaService.GetById(id)
            );
        }
        [HttpPost]
        public ActionResult Post([FromBody] Factu factura)
        {
            return Ok(
               facturaService.Save(factura)
            );
        }
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Factu factura)
        {
            return Ok(
                facturaService.Update(factura)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                facturaService.DeleteById(id)
            );
        }
    }
}