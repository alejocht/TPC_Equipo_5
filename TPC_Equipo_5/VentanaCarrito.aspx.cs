﻿using Dominio.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo_5
{
    public partial class VentanaCarrito : System.Web.UI.Page
    {
        public List<Producto> listaLecturaProductos;
        public Producto producto;
        public int indice = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (listaLecturaProductos == null)
                {
                    if (Session["listaArticulosEnCarrito"] != null)
                    {
                        listaLecturaProductos = (List<Producto>)Session["listaArticulosEnCarrito"];
                    }
                    else
                    {
                        listaLecturaProductos = new List<Producto>();
                    }

                }
                if (Session["ArticulosEnCarrito"] != null)
                {
                    producto = (Producto)Session["ArticulosEnCarrito"];
                    listaLecturaProductos.Add(producto);
                    Session.Add("listaArticulosEnCarrito", listaLecturaProductos);

                    Session["ArticulosEnCarrito"] = null;
                }

                repCarrito.DataSource = listaLecturaProductos;
                repCarrito.DataBind();

                decimal SubtotalCarrito = CalcularCarritoTotal(listaLecturaProductos);
                lblSubTotal.Text = "Subtotal: $" + SubtotalCarrito.ToString("F2");

                lblEnvio.Text = "Envío: $" + 5000.ToString("0.00"); ;
                lblTotalCompra.Text = "Total: $" + (SubtotalCarrito + 5000).ToString("0.00");
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int IdProducto = int.Parse(((Button)sender).CommandArgument);
            listaLecturaProductos = (List<Producto>)Session["listaArticulosEnCarrito"];

            List<Producto> nuevaLista = new List<Producto>();
            bool eliminado = false;

            foreach (var producto in listaLecturaProductos)
            {
                if (!eliminado && producto.id == IdProducto)
                {
                    eliminado = true;
                }
                else
                {
                    nuevaLista.Add(producto);
                }
            }

            listaLecturaProductos = nuevaLista;
            if (listaLecturaProductos.Count == 0)
            {
                Session["ArticulosEnCarrito"] = null;
                Session.Add("listaArticulosEnCarrito", listaLecturaProductos);
                Response.Redirect("default.aspx");
            }
            else
            {
                repCarrito.DataSource = listaLecturaProductos;
                repCarrito.DataBind();
                Session.Add("listaArticulosEnCarrito", listaLecturaProductos);
                decimal SubtotalCarrito = CalcularCarritoTotal(listaLecturaProductos);
                lblSubTotal.Text = "Subtotal: $" + SubtotalCarrito.ToString("F2");

                lblEnvio.Text = "Envío: $" + 5000.ToString("0.00"); ;
                lblTotalCompra.Text = "Total: $" + (SubtotalCarrito + 5000).ToString("0.00");

                site master = (site)Master;
                master.cantidadItems = listaLecturaProductos.Count().ToString();
            }
        }

        protected void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx", false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VentanaCompra.aspx", false);
        }

        private decimal CalcularCarritoTotal(List<Producto> productos)
        {
            decimal total = 0;

            foreach (var producto in productos)
            {
                total += (decimal)producto.precio;
            }

            return total;
        }
    }
}