using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class frmAgendaContatos : Form
    {
        private OperacaoEnum acao;

        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void frmAgendaContatos_Load(object sender, EventArgs e)
        {

        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarCancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            CarregarListaContatos();
            AlterarEstadoCampos(false);
        }

        private void AlterarBotoesSalvarCancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarBotoesSalvarCancelar(true);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarBotoesSalvarCancelar(true);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarBotoesSalvarCancelar(false);
            AlterarEstadoCampos(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato
            {
                Nome = txbNome.Text,
                Email = txbEmail.Text,
                NumeroTelefone = txbNumeroTelefone.Text
            };
            List<Contato> contatosList = new List<Contato>();
            foreach (Contato contatoLista in lbxContatos.Items)
            {
                contatosList.Add(contatoLista);
            }

            if (acao == OperacaoEnum.INCLUIR)
            {
                contatosList.Add(contato);
            }
            else
            {
                int indice = lbxContatos.SelectedIndex;
                contatosList.RemoveAt(indice);
                contatosList.Insert(indice, contato);
            }
            ManipuladorArquivos.EscreverArquivo(contatosList);
            CarregarListaContatos();
            AlterarBotoesSalvarCancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            LimparListaContatos();
            AlterarEstadoCampos(false);
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquivos.LerArquivo().ToArray());
            lbxContatos.SelectedIndex = 0;
        }

        private void LimparListaContatos()
        {
            txbNome.Text = "";
            txbEmail.Text = "";
            txbNumeroTelefone.Text = "";
        }

        private void AlterarEstadoCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbNumeroTelefone.Enabled = estado;
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbNumeroTelefone.Text = contato.NumeroTelefone;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Você tem certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes){
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contatoLista in lbxContatos.Items)
                {
                    contatosList.Add(contatoLista);
                }
                ManipuladorArquivos.EscreverArquivo(contatosList);
                CarregarListaContatos();
                LimparListaContatos();
            }
        }
    }
}
