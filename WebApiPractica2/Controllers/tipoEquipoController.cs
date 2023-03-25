using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApiPractica2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipoEquipoController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public tipoEquipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto; ;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<tipo_equipo> listadoEstadoEq = (from e in _equiposContexto.tipo_equipo
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
            tipo_equipo? estadosEq = (from e in _equiposContexto.tipo_equipo
                                         where e.id_tipo_equipo == id
                                         select e).FirstOrDefault();

            if (estadosEq == null)
            {
                return NotFound();
            }
            return Ok(estadosEq);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarTipoEquipo([FromBody] tipo_equipo estadoEq)
        {

            try
            {
                _equiposContexto.tipo_equipo.Add(estadoEq);
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

        public IActionResult ActualizarTipoEquipo(int id, [FromBody] tipo_equipo equipoModificar)
        {
            tipo_equipo? equiposActual = (from e in _equiposContexto.tipo_equipo
                                             where e.id_tipo_equipo == id
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

        public IActionResult EliminarTipoEquipo(int id)
        {

            tipo_equipo? equipo = (from e in _equiposContexto.tipo_equipo
                                      where e.id_tipo_equipo == id
                                      select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            _equiposContexto.tipo_equipo.Attach(equipo);
            _equiposContexto.tipo_equipo.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}