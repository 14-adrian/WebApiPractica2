using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApiPractica2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public marcasController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto; ;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<marcas> listadoEstadoEq = (from e in _equiposContexto.marcas
                                                    select e).ToList();

            if (listadoEstadoEq.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEstadoEq);

        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            marcas? estadosEq = (from e in _equiposContexto.marcas
                                         where e.id_marcas == id
                                         select e).FirstOrDefault();

            if (estadosEq == null)
            {
                return NotFound();
            }
            return Ok(estadosEq);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEstadoEquipo([FromBody] marcas estadoEq)
        {

            try
            {
                _equiposContexto.marcas.Add(estadoEq);
                _equiposContexto.SaveChanges();
                return Ok(estadoEq);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEstadoEquipo(int id, [FromBody] marcas equipoModificar)
        {
            marcas? equiposActual = (from e in _equiposContexto.marcas
                                             where e.id_marcas == id
                                             select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound(id);
            }
            equiposActual.nombre_marca = equipoModificar.nombre_marca;
            equiposActual.estados = equipoModificar.estados;

            _equiposContexto.Entry(equiposActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEstadoEquipo(int id)
        {

            marcas? equipo = (from e in _equiposContexto.marcas
                                      where e.id_marcas == id
                                      select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            _equiposContexto.marcas.Attach(equipo);
            _equiposContexto.marcas.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}