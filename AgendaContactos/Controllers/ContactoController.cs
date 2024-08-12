using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration; //Ocupar configuracion del proyecto en este controlador, para conectarse a la bd
using AgendaContactos.Models;
//Paquetes para establecer la conexión con la bd
using System.Data.SqlClient;
using System.Data;

namespace AgendaContactos.Controllers
{
    public class ContactoController : Controller
    {
        //Variable que permite establecer el string de conexion llamado cadena
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> olista = new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {
            olista = new List<Contacto>();
            //En oconexion se guarda la conexion
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                //Para que lea los datos de la tabla
                SqlCommand cmd = new SqlCommand("SELECT * FROM Contacto", oconexion);
                //Para que se ejecute
                cmd.CommandType = CommandType.Text;

                oconexion.Open();

                //Lectura de los datos de la tabla
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();
                        nuevoContacto.IDContacto = Convert.ToInt32(dr["IDContacto"]);
                        nuevoContacto.Nombre = dr["Nombre"].ToString();
                        nuevoContacto.Apellido = dr["Apellido"].ToString();
                        nuevoContacto.Telefono = dr["Telefono"].ToString();
                        nuevoContacto.Correo = dr["Correo"].ToString();

                        olista.Add(nuevoContacto);
                    }
                }
            }

            return View(olista);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Contacto ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                //Se coloca el procedimiento almacenado que ya está preconfigurado para agregar datos
                SqlCommand cmd = new SqlCommand("sp_Crear", oconexion);
                cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", ocontacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", ocontacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", ocontacto.Correo);
                //Para que se ejecute el procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpGet]
        public ActionResult Editar(int? idcontacto)
        {
            if (idcontacto == null)
            {
                return RedirectToAction("Inicio", "Contacto");
            }

            //LLama al modelo donde se hace un filtro para el id del registro que se ha seleccionado
            Contacto ocontacto = olista.Where(c => c.IDContacto == idcontacto).FirstOrDefault();

            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Editar(Contacto ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                //Se coloca el procedimiento almacenado que ya está preconfigurado para agregar datos
                SqlCommand cmd = new SqlCommand("sp_Actualizar", oconexion);
                cmd.Parameters.AddWithValue("IDContacto", ocontacto.IDContacto);
                cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", ocontacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", ocontacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", ocontacto.Correo);
                //Para que se ejecute el procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpGet]
        public ActionResult Eliminar(int? idcontacto)
        {
            if (idcontacto == null)
            {
                return RedirectToAction("Inicio", "Contacto");
            }

            //LLama al modelo donde se hace un filtro para el id del registro que se ha seleccionado
            Contacto ocontacto = olista.Where(c => c.IDContacto == idcontacto).FirstOrDefault();

            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Eliminar(string IDContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                //Se coloca el procedimiento almacenado que ya está preconfigurado para agregar datos
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("IDContacto", IDContacto);
                //Para que se ejecute el procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }


    }
}