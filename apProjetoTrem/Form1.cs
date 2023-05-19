using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apProjetoTrem
{
    public partial class Form1 : Form
    {
        ListaDupla<Cidade> cidades;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAlternar_Click(object sender, EventArgs e)
        {
            gbCaminhos.Visible = true;
        }

        private void btnCidades_Click(object sender, EventArgs e)
        {
            gbCaminhos.Visible = false;
            gbCidades.Visible = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            gbCaminhos.Visible = true;
            MessageBox.Show(gbCaminhos.Visible.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cidades = new ListaDupla<Cidade>();
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                cidades.LerDados(dlgAbrir.FileName);
                lsbCidades.Items.Clear();
                lsbCidades.Items.Add("Nome                  X       Y");
                cidades.ExibirDados(lsbCidades);
            }
        }
    }
}
