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
            cidades.PosicionarNoPrimeiro();
            Cidade cid = cidades.DadoAtual();
            txtCodigo.Text = cidades.PosicaoAtual.ToString();
            txtNome.Text = cid.Nome;
            txtCoordX.Text = cid.X.ToString();
            txtCoordY.Text = cid.Y.ToString();

            VerificarBotoes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cidades = new ListaDupla<Cidade>();
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                cidades.LerDados(dlgAbrir.FileName);
                lsbCidades.Items.Clear();
                lsbCidades.Items.Add("Nome                X        Y");
                cidades.ExibirDados(lsbCidades);

                cidades.PosicionarNoPrimeiro();
                Cidade cid = cidades.DadoAtual();
                txtCodigo.Text = cidades.PosicaoAtual.ToString();
                txtNome.Text = cid.Nome;
                txtCoordX.Text = cid.X.ToString();
                txtCoordY.Text = cid.Y.ToString();

                VerificarBotoes();
            }
        }

        private void VerificarBotoes()
        {
            if (cidades.EstaNoInicio)
            {
                btnAnterior.Enabled = false;
                btnInicio.Enabled = false;
            }
            else
            {
                btnAnterior.Enabled = true;
                btnInicio.Enabled = true;
            }
            if (cidades.EstaNoFim)
            {
                btnProx.Enabled = false;
                btnUltimo.Enabled = false;
            }
            else
            {
                btnProx.Enabled = true;
                btnUltimo.Enabled = true;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            cidades.RetrocederPosicao();
            Cidade cid = cidades.DadoAtual();
            txtCodigo.Text = cidades.PosicaoAtual.ToString();
            txtNome.Text = cid.Nome;
            txtCoordX.Text = cid.X.ToString();
            txtCoordY.Text = cid.Y.ToString();

            VerificarBotoes();
        }

        private void btnProx_Click(object sender, EventArgs e)
        {
            cidades.AvancarPosicao();
            Cidade cid = cidades.DadoAtual();
            txtCodigo.Text = cidades.PosicaoAtual.ToString();
            txtNome.Text = cid.Nome;
            txtCoordX.Text = cid.X.ToString();
            txtCoordY.Text = cid.Y.ToString();

            VerificarBotoes();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            cidades.PosicionarNoUltimo();
            Cidade cid = cidades.DadoAtual();
            txtCodigo.Text = cidades.PosicaoAtual.ToString();
            txtNome.Text = cid.Nome;
            txtCoordX.Text = cid.X.ToString();
            txtCoordY.Text = cid.Y.ToString();

            VerificarBotoes();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            try
            {
                int posicaoAProcurar = int.Parse(txtCodigo.Text);
                cidades.PosicionarEm(posicaoAProcurar);
                Cidade cid = cidades.DadoAtual();
                txtCodigo.Text = cidades.PosicaoAtual.ToString();
                txtNome.Text = cid.Nome;
                txtCoordX.Text = cid.X.ToString();
                txtCoordY.Text = cid.Y.ToString();

                VerificarBotoes();
            }
            catch
            {
                MessageBox.Show("Digite um código válido!", "Código inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
