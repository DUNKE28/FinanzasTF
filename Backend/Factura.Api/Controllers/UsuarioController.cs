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
    public class UsuarioController : Controller
    {
        private IUsuarioService usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                usuarioService.GetAll()
            );
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(
                usuarioService.GetById(id)
            );
        }
        [HttpPost]
        public ActionResult Post([FromBody] Usuario user)
        {
            return Ok(
               usuarioService.Save(user)
            );
        }
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Usuario user)
        {
            return Ok(
                usuarioService.Update(user)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                usuarioService.DeleteById(id)
            );
        }

        [HttpGet("{username}/{password}")]
        public ActionResult Login(string username, string password)
        {
            return Ok(
                usuarioService.Login(username,password)
            );
        }

        [HttpGet("username/{RUC}")]
        public ActionResult GetByRUC(long RUC)
        {
            return Ok(
                usuarioService.GetByRUC(RUC)
            );
        }

    }
}