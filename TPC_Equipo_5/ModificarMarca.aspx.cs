﻿using Dominio.Productos;
using LecturaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_5
{
    public partial class ModificarMarca : System.Web.UI.Page
    {
        Marca seleccionada = new Marca();
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                LecturaMarca lecturaMarca = new LecturaMarca();
                int id = int.Parse(Request.QueryString["id"].ToString());
                seleccionada = lecturaMarca.listar(id);
                if (!IsPostBack)
                {
                    txtNombre.Text = seleccionada.nombre;
                    ckbActivo.Checked = seleccionada.estado;
                }
            } 
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("marcasAdmin.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                LecturaMarca lecturaMarca = new LecturaMarca();
                seleccionada.nombre = txtNombre.Text;
                seleccionada.estado = ckbActivo.Checked;
                lecturaMarca.modificar(seleccionada);
                Response.Redirect("marcasAdmin.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LecturaMarca lecturaMarca = new LecturaMarca();
                lecturaMarca.eliminarFisica(seleccionada);
                Response.Redirect("marcasAdmin.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }
    }
}