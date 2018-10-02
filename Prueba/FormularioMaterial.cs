using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba
{
    public partial class FormularioMaterial : Form
    {

        public Material _material;
        public int _ConductorId;
        public FormularioMaterial()
        {
            InitializeComponent();
        }

        private void FormularioMaterial_Load(object sender, EventArgs e)
        {
            if (_material == null)
            {
                btnCrear.Text = "Crear";
                return;
            }
            tbDescripcion.Text = _material.Descripcion;
            dtFechaDeDevolucion.Text =  Convert.ToString(_material.fechaDeDevolucion);
            dtFechaDeEntrega.Text = Convert.ToString(_material.fechaDeEntrega);

            btnCrear.Text = "Modificar";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            var contexto = new PruebaEntities1();


            switch (btnCrear.Text)
            {
                case "Crear":
                    _material = new Material()
                    {
                        conductorId = _ConductorId,
                        Descripcion = tbDescripcion.Text,
                        fechaDeEntrega = Convert.ToDateTime(dtFechaDeEntrega.Text),
                        fechaDeDevolucion = Convert.ToDateTime(dtFechaDeDevolucion.Text)
                    };
                    contexto.Material.Add(_material);

                    break;
                case "Modificar":
                    var MaterialModificar = contexto.Material.Where(material => material.materialId == _material.materialId).First();
                    MaterialModificar.Descripcion = tbDescripcion.Text;
                    MaterialModificar.fechaDeEntrega = Convert.ToDateTime(dtFechaDeEntrega.Text);
                    MaterialModificar.fechaDeDevolucion = Convert.ToDateTime(dtFechaDeDevolucion.Text);
                    break;
            }
            contexto.SaveChanges();
            this.Close();
        }


    }
}
