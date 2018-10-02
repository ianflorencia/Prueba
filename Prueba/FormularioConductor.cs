using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    public partial class FormularioConductor : Form
    {
        public int _ConductorId;

        public FormularioConductor()
        {
            InitializeComponent();
            
        }

        private void FormularioConductor_Load(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void MaterialGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow selectedRow = MaterialGridView.Rows[e.RowIndex];

            Material material = new Material();


            material.materialId = Convert.ToInt16(selectedRow.Cells[0].Value);
            material.conductorId = _ConductorId;
            material.Descripcion = Convert.ToString(selectedRow.Cells[1].Value);
            material.fechaDeEntrega = Convert.ToDateTime(selectedRow.Cells[2].Value);
            material.fechaDeDevolucion = Convert.ToDateTime(selectedRow.Cells[3].Value);

            FormularioMaterial fMaterial = new FormularioMaterial();
            fMaterial._material = material;
            fMaterial.ShowDialog();
            MaterialGridView.Rows.Clear();
            RetrieveData();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormularioMaterial fMaterial = new FormularioMaterial();
            fMaterial._ConductorId = _ConductorId;
            fMaterial.ShowDialog();
            MaterialGridView.Rows.Clear();
            RetrieveData();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = MaterialGridView.Rows[MaterialGridView.CurrentCell.RowIndex];
            var contexto = new PruebaEntities1();
            int materialid = Convert.ToInt16(selectedRow.Cells[0].Value);
            var MaterialEliminar = contexto.Material.Where(material => material.materialId == materialid).First();
            contexto.Material.Remove(MaterialEliminar);
            contexto.SaveChanges();

            MaterialGridView.Rows.Clear();
            RetrieveData();
        }

        private void RetrieveData()
        {
            var contexto = new PruebaEntities1();
            
            var conductores = from Conductor in contexto.Conductor where _ConductorId.Equals(Conductor.conductorId) select Conductor;

            foreach (Conductor conductor in conductores)
            {
                tbNombre.Text = conductor.nombre;
                tbApellidos.Text = conductor.apellidos;
                tbDNI.Text = conductor.DNI;
                dtFecha.Text = Convert.ToString(conductor.fechaDeNacimiento);
                tbTipoCarnet.Text = conductor.tipoDeCarnet;
                foreach (Material material in conductor.Material)
                {

                    MaterialGridView.Rows.Add(
                        material.materialId.ToString(),
                        material.Descripcion.ToString(),
                        material.fechaDeEntrega.Value.ToShortDateString(),
                        material.fechaDeDevolucion.Value.ToShortDateString()

                    );
                }
            }

        }
    }
}
