﻿using Dominio;
using Dominio.Productos;
using LecturaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio.Usuarios;

namespace TPC_Equipo_5
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        List<Provincia> ListaProvincias;
        LecturaProvincia lecturaProvincias;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    
                }

            }
            catch (Exception ex)
            {

                Session["error"] = ex.Message;
                Response.Redirect("error.aspx", false);
            }
        }

        
        
        protected void Btn_CrearCuenta_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                try
                {
                    LecturaDatosUsuario lecturadatos = new LecturaDatosUsuario();
                    DatosUsuario datosUsuario = new DatosUsuario();
                    datosUsuario.nombre = Txt_Nombre.Text;
                    datosUsuario.apellido = Txt_Apellido.Text;                 
                    datosUsuario.email = Txt_Email.Text;                                  
                    lecturadatos.agregar(datosUsuario);
                    LecturaUsuario lecturaUsuario = new LecturaUsuario();
                    Usuario aux = new Usuario();
                    aux.usuario = Txt_Usuario.Text;
                    aux.password = Txt_Password.Text;
                    lecturaUsuario.agregar(aux, datosUsuario);

                    //actualizo la session de usuario
                    Response.Redirect("Ventana_Usuario.aspx", false);

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}