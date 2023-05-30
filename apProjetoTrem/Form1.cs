using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            cidades.SituacaoAtual = Situacao.navegando;
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
                cidades.SituacaoAtual = Situacao.navegando;

                VerificarBotoes();
            }
        }

        private void VerificarBotoes()
        {
            if(cidades.SituacaoAtual == Situacao.navegando)
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

                btnProcurar.Enabled = true;
                btnNovo.Enabled = true;
                btnSalvar.Enabled = true;
                btnExcluir.Enabled = true;
                btnCancelar.Enabled = false;
            }
            else if(cidades.SituacaoAtual == Situacao.incluindo)
            {
                btnAnterior.Enabled = false;
                btnUltimo.Enabled = false;
                btnProx.Enabled = false;
                btnInicio.Enabled = false;

                btnProcurar.Enabled = false;
                btnSalvar.Enabled = false;
                btnExcluir.Enabled = false;

                btnAnterior.Enabled = false;
                btnInicio.Enabled = false;
                btnProx.Enabled = false;
                btnUltimo.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else if(cidades.SituacaoAtual == Situacao.pesquisando)
            {
                btnAnterior.Enabled = false;
                btnUltimo.Enabled = false;
                btnProx.Enabled = false;
                btnInicio.Enabled = false;

                btnProcurar.Enabled = true;
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                btnExcluir.Enabled = false;
                btnCancelar.Enabled = true;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            cidades.SituacaoAtual = Situacao.navegando;
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
            cidades.SituacaoAtual = Situacao.navegando;
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
            cidades.SituacaoAtual = Situacao.navegando;
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
            cidades.SituacaoAtual = Situacao.pesquisando;
            try
            {
                string cidadeAProcurar = txtNome.Text;
                int pos;
                cidades.Existe(new Cidade(txtNome.Text, 0, 0),out pos);
                cidades.PosicionarEm(pos);
                Cidade cid = cidades.DadoAtual();
                txtCodigo.Text = cidades.PosicaoAtual.ToString();
                txtNome.Text = cid.Nome;
                txtCoordX.Text = cid.X.ToString();
                txtCoordY.Text = cid.Y.ToString();

                VerificarBotoes();
            }
            catch
            {
                MessageBox.Show("Não foi possível localizar cidade", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            cidades.SituacaoAtual = Situacao.incluindo;
            VerificarBotoes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cidades.SituacaoAtual = Situacao.navegando;
            VerificarBotoes();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            cidades.SituacaoAtual = Situacao.editando;
            cidades.Existe(new Cidade(txtNome.Text, 0, 0), out int pos);
            if (pos <= -1)
            {
                MessageBox.Show("Cidade inexistente, digite um nome válido!", "Cidade Inexistente");
            }
            else
            {
                cidades.PosicionarEm(pos);
                try
                {
                    double X = double.Parse(txtCoordX.Text);
                    double Y = double.Parse(txtCoordY.Text);
                    cidades.DadoAtual().X = X;
                    cidades.DadoAtual().Y = Y;
                }catch(Exception ex) 
                {
                    MessageBox.Show("Digite valores válidos!", "Valores inválidos!");
                }
                pbMapa.Refresh();
                cidades.SituacaoAtual = Situacao.navegando;

                lsbCidades.Items.Clear();
                lsbCidades.Items.Add("Nome                X        Y");
                cidades.ExibirDados(lsbCidades);

                VerificarBotoes();
            }

        }

        private void pbMapa_Click(object sender, EventArgs e)
        {
            if (cidades.SituacaoAtual == Situacao.incluindo)
            {
                double largura = pbMapa.Bounds.Width;
                double altura = pbMapa.Bounds.Height;
                double xClicado = ((MouseEventArgs)e).X;
                double yClicado = ((MouseEventArgs)e).Y;

                double porcentagemX = Math.Round(xClicado / largura, 3);
                double porcentagemY = Math.Round(yClicado / altura, 3);

                if (cidades.Incluir(new Cidade(txtNome.Text, porcentagemX, porcentagemY)))
                    MessageBox.Show("Cidade incluida com exito", "Sucesso");
                else
                    MessageBox.Show("Falha ao incluir cidade", "Falha");
                cidades.ExibirDados(lsbCidades);

                cidades.Existe(new Cidade(txtNome.Text, 0, 0), out int pos);
                cidades.PosicionarEm(pos);
                Cidade cid = cidades.DadoAtual();
                txtCodigo.Text = cidades.PosicaoAtual.ToString();
                txtNome.Text = cid.Nome;
                txtCoordX.Text = cid.X.ToString();
                txtCoordY.Text = cid.Y.ToString();

                lsbCidades.Items.Clear();
                lsbCidades.Items.Add("Nome                X        Y");
                cidades.ExibirDados(lsbCidades);

                pbMapa.Refresh();
            }
            if (cidades.SituacaoAtual == Situacao.pesquisando)
            {
                int esp = (pbMapa.Bounds.Width / 150)/2;
                double raio = double.Parse(esp.ToString()) / 100;

                double largura = pbMapa.Bounds.Width;
                double altura = pbMapa.Bounds.Height;
                double xClicado = ((MouseEventArgs)e).X;
                double yClicado = ((MouseEventArgs)e).Y;

                double porcentagemX = Math.Round(xClicado / largura, 3);
                double porcentagemY = Math.Round(yClicado / altura, 3);

                cidades.PosicionarNoPrimeiro();
                bool achou = false;
                while(!achou)
                {
                    while (!cidades.EstaNoFim)
                    {
                        double distX = Math.Abs(porcentagemX - cidades.DadoAtual().X);
                        double distY = Math.Abs(porcentagemY - cidades.DadoAtual().Y);
                        if (distX <= raio
                            && distY <= raio)
                        {
                            achou = true;
                            break;
                        }
                        else
                            cidades.AvancarPosicao();
                    }
                    if (cidades.PosicaoAtual == cidades.Tamanho-1)
                        break;
                }
                if (!achou)
                    MessageBox.Show("Não foi encontrado uma cidade nessa posição", "Falha ao encontrar cidade");
                if (achou)
                {
                    Cidade cid = cidades.DadoAtual();
                    txtCodigo.Text = cidades.PosicaoAtual.ToString();
                    txtNome.Text = cid.Nome;
                    txtCoordX.Text = cid.X.ToString();
                    txtCoordY.Text = cid.Y.ToString();
                    MessageBox.Show("Cidade encontrada", "Cidade encontrada");
                }
            }
            cidades.SituacaoAtual = Situacao.navegando;
            VerificarBotoes();
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int esp = pbMapa.Bounds.Width / 150;
            Pen pen = new Pen(Color.Black, esp);
            cidades.PosicionarNoPrimeiro();
            while(!cidades.EstaNoFim)
            {
                Cidade cidadeAtual = cidades.DadoAtual();
                float pX = pbMapa.Bounds.Width * float.Parse(cidadeAtual.X.ToString());
                float pY = pbMapa.Bounds.Height * float.Parse(cidadeAtual.Y.ToString());
                g.DrawEllipse(pen, pX-esp/2, pY-esp/2, esp, esp);
                cidades.AvancarPosicao();
            }
            Cidade cidade = cidades.DadoAtual();
            float X = pbMapa.Bounds.Width * float.Parse(cidade.X.ToString());
            float Y = pbMapa.Bounds.Height * float.Parse(cidade.Y.ToString());
            g.DrawEllipse(pen, X - esp / 2, Y - esp / 2, esp, esp);
            cidades.PosicionarNoPrimeiro();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(cidades.Excluir(new Cidade(txtNome.Text, 0, 0)))
            {
                Cidade cid = cidades.DadoAtual();
                txtCodigo.Text = cidades.PosicaoAtual.ToString();
                txtNome.Text = cid.Nome;
                txtCoordX.Text = cid.X.ToString();
                txtCoordY.Text = cid.Y.ToString();

                lsbCidades.Items.Clear();
                lsbCidades.Items.Add("Nome                X        Y");
                cidades.ExibirDados(lsbCidades);

                MessageBox.Show("Cidade excluida com exito", "Sucesso");

                cidades.PosicionarNoPrimeiro();
            }
            else
            {
                MessageBox.Show("Falha ao excluir cidade", "Falha");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cidades.GravarDados(dlgAbrir.FileName);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
