using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvilesJenifer
{
    public partial class Form1 : Form
    {
        Class1 objConexion = new Class1();
        int posicion = 0;
        string accion = "nuevo";
        DataTable tbl = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        void actualizarDs()
        {
            tbl = objConexion.obtener_datos().Tables["tbl_vehiculo"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdVehiculo"] };
        }

        void mostrarDatos()
        {
            try
            {
                lblidvehiculo.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtmarca.Text = tbl.Rows[posicion].ItemArray[1].ToString();
                txtmodelo.Text = tbl.Rows[posicion].ItemArray[2].ToString();
                txtyear.Text = tbl.Rows[posicion].ItemArray[3].ToString();

                lblregistroxden.Text = (posicion + 1) + " de " + tbl.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "vehiculos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar_cajas();
            }
        }

        void limpiar_cajas()
        {
            txtmarca.Text = "";
            txtmodelo.Text = "";
            txtyear.Text = "";

        }

        void controles(Boolean valor)
        {
            pnlNavegacion.Enabled = valor;
            btnEliminar.Enabled = valor;
            btnBuscar.Enabled = valor;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {//boton de nuevo
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "nuevo";

                limpiar_cajas();
                controles(false);
            }
            else
            { //boton de guardar
                String[] valores = {
                    lblidvehiculo.Text,
                    txtmarca.Text,
                    txtmodelo.Text,
                    label.Text

                };
                objConexion.mantenimiento_datos(valores, accion);
                actualizarDs();
                posicion = tbl.Rows.Count - 1;
                mostrarDatos();

                controles(true);

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarDs();
            mostrarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")
            {//boton de modificar
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "modificar";

                controles(false);

                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";

            }
            else
            { //boton de cancelar
                controles(true);
                mostrarDatos();

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Modificar";
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esta seguro de eliminar a " + txtmarca.Text, "Registro de Vehiculos",
             MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidvehiculo.Text };
                objConexion.mantenimiento_datos(valores, "eliminar");

                actualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                mostrarDatos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda buscarVehi = new Busqueda();
            buscarVehi.ShowDialog();

            if (buscarVehi._IdVehiculo > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(buscarVehi._IdVehiculo));
                mostrarDatos();
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Primer Registro...", "Vehiculo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < tbl.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Ultimo Registro...", "Persona Entrevistada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            posicion = tbl.Rows.Count - 1;
            mostrarDatos();
        }
    }
    }

