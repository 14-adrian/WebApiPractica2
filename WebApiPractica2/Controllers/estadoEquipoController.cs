using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApiPractica2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadoEquipoController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public estadoEquipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto; ;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<estados_equipo> listadoEstadoEq = (from e in _equiposContexto.estados_equipo
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
            estados_equipo? estadosEq = (from e in _equiposContexto.estados_equipo
                               where e.id_estados_equipo == id
                               select e).FirstOrDefault();

            if (estadosEq == null)
            {
                return NotFound();
            }
            return Ok(estadosEq);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEstadoEquipo([FromBody] estados_equipo estadoEq)
        {

            try
            {
                _equiposContexto.estados_equipo.Add(estadoEq);
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

        public IActionResult ActualizarEstadoEquipo(int id, [FromBody] estados_equipo equipoModificar)
        {
            estados_equipo? equiposActual = (from e in _equiposContexto.estados_equipo
                                      where e.id_estados_equipo == id
                                      select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound(id);
            }
            equiposActual.descripcion = equipoModificar.descripcion;
            equiposActual.estado = equipoModificar.estado;

            _equiposContexto.Entry(equiposActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEstadoEquipo(int id)
        {

            estados_equipo? equipo = (from e in _equiposContexto.estados_equipo
                               where e.id_estados_equipo == id
                               select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            _equiposContexto.estados_equipo.Attach(equipo);
            _equiposContexto.estados_equipo.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}
