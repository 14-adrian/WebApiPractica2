﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica2.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto; ;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            //var listadoMarcas = (from n in _equiposContexto.marcas
            //                     select n).ToList();


            //var listadoEquipo = (from e in _equiposContexto.equipos
            //                     join m in listadoMarcas on e.marca_id equals m.id_marcas
            //                     join te in _equiposContexto.tipoEquipo on e.tipo_equipo_id equals te.tipoEquipoId
            //                     where m.estados == "D"
            //                     select new
            //                     {
            //                         e.id_equipos,
            //                         e.nombre,
            //                         e.descripcion,
            //                         e.tipo_equipo_id,
            //                         e.marca_id,
            //                         m.nombre_marca
            //                     }).ToList();

            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                 select e).ToList();

            if (listadoEquipo == null)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);

        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {

            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            equipos? equiposActual = (from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound(id);
            }

            equiposActual.nombre = equipoModificar.nombre;
            equiposActual.descripcion = equipoModificar.descripcion;
            equiposActual.marca_id = equipoModificar.marca_id;
            equiposActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equiposActual.anio_compra = equipoModificar.anio_compra;
            equiposActual.costo = equipoModificar.costo;

            _equiposContexto.Entry(equiposActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {

            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}
