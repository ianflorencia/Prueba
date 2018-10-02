using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    public partial class FormularioPrincipal : Form
    {

        public FormularioPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PruebaEntities1 db = new PruebaEntities1();
            db.Conductor.Load();
            CondutctorGridView.DataSource = db.Conductor.Local.ToBindingList();
            CondutctorGridView.Columns["conductorId"].Visible = false;
            CondutctorGridView.Columns["Material"].Visible = false;
            CondutctorGridView.Columns["fechaDeNacimiento"].HeaderText = "Fecha de nacimiento";
            CondutctorGridView.Columns["tipoDeCarnet"].HeaderText = "Tipo de carnet";
        }

        private void CondutctorGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            DataGridViewRow selectedRow = CondutctorGridView.Rows[e.RowIndex];

            Console.WriteLine("pulsado");
            Console.WriteLine(selectedRow.Cells[0].Value);


            FormularioConductor fConductores = new FormularioConductor();
            fConductores._ConductorId = Convert.ToInt16(selectedRow.Cells[0].Value);
            fConductores.ShowDialog();
        }
    }
}
