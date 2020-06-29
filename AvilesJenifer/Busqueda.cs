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
    public partial class Busqueda : Form
    {
        Class1 objConexion = new Class1();
        public int _IdVehiculo;

        public Busqueda()
        {
            InitializeComponent();
        }

        private void Busqueda_Load(object sender, EventArgs e)
        {
            GrdBusquedaVehiculo.DataSource = objConexion.obtener_datos().Tables["tbl_vehiculo"].DefaultView;
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {

            if (GrdBusquedaVehiculo.RowCount > 0)
            {
                _IdVehiculo = int.Parse(GrdBusquedaVehiculo.CurrentRow.Cells["IdVehiculo"].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("NO hay datos que seleccionar", "Busqueda de Vehiculos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void filtrar_datos(String valor)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = GrdBusquedaVehiculo.DataSource;
            bs.Filter = "marca like '%" + valor + "%' or modelo like '%" + valor + "%' or year like '%" + valor + "%'";
            GrdBusquedaVehiculo.DataSource = bs;

        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar_datos(TxtBuscar.Text);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
