using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

class ListaDupla<Dado> : IDados<Dado>
                where Dado : IComparable<Dado>, IRegistro<Dado>, new()
{
    NoDuplo<Dado> primeiro, ultimo, atual;
    int quantosNos;

    public ListaDupla()
    {
        primeiro = ultimo = atual = null;
        quantosNos = 0; 
    }

    public Situacao SituacaoAtual { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int PosicaoAtual { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool EstaNoInicio => atual == primeiro;
    public bool EstaNoFim => atual == ultimo;
    public bool EstaVazio => quantosNos <= 0;         // (bool) Verificar se está vazia
    public int Tamanho => quantosNos;

    public void LerDados(string nomeArquivo)    // fará a leitura e armazenamento dos dados do arquivo cujo nome é passado por parâmetro
    {
        StreamReader leitor = new StreamReader(nomeArquivo);
        while (!leitor.EndOfStream)
        {
            Dado novoDado = new Dado();
            novoDado.LerRegistro(leitor);
            IncluirAposFim(novoDado);
        }
        leitor.Close();
    }
    public void GravarDados(string nomeArquivo)  // gravará sequencialmente, no arquivo cujo nome é passado por parâmetro, os dados armazenados na lista
    {
        throw new NotImplementedException();
    }
    public void PosicionarNoPrimeiro()        // Posicionar atual no primeiro nó para ser acessado
    {
        atual = primeiro;
    }
    public void RetrocederPosicao()        // Retroceder atual para o nó anterior para ser acessado
    {
        atual = atual.Anterior;
    }
    public void AvancarPosicao()
    {
        atual = atual.Prox;
    }
    public void PosicionarNoUltimo()        // posicionar atual no último nó para ser acessado
    {
        atual = ultimo;
    }
    public void PosicionarEm(int posicaoDesejada)
    {
        throw new NotImplementedException();
    }

    // (bool) Pesquisar Dado procurado em ordem crescente; a pesquisa
    // posicionará o ponteiro atual no nó procurado quando este
    // or encontrado ou, se não achar, no nó seguinte a local
    // onde deveria estar o nó procurado
    public bool Existe(Dado procurado, out int ondeEsta)
    {
        ondeEsta = -1;
        atual = null;
        if (!EstaVazio)
        {
            if (procurado.CompareTo(primeiro.Info) < 0)
                return false;
            else if (procurado.CompareTo(ultimo.Info) > 0)
            {
                return false;
            }
            atual = primeiro;
            bool achou = false;
            bool fim = false;
            while (!achou && !fim)
            {
                if (atual == null)
                    fim = true;
                else if (atual.Info.CompareTo(procurado) > 0)
                    fim = true;
                else if (atual.Info.CompareTo(procurado) == 0)
                    achou = true;
                else
                {
                    atual = atual.Prox;
                    ondeEsta++;
                }
            }
            if (!achou)
                ondeEsta = -1;
            return achou;
        }
        return false;
    }
    public bool Excluir(Dado dadoAExcluir)
    {
        throw new NotImplementedException();
    }
    public bool IncluirNoInicio(Dado novoValor)
    {
        var novoNo = new NoDuplo<Dado>(novoValor);
        if (EstaVazio)
            ultimo = novoNo;

        novoNo.Anterior = null;
        novoNo.Prox = primeiro;
        primeiro.Anterior = novoNo;
        primeiro = novoNo;
        quantosNos++;

        return true;
    }
    public bool IncluirAposFim(Dado novoValor)
    {
        var novoNo = new NoDuplo<Dado>(novoValor);
        if (EstaVazio)
            primeiro = novoNo;
        else
            ultimo.Prox = novoNo;

        novoNo.Prox = null;
        novoNo.Anterior = ultimo;
        ultimo = novoNo;
        ultimo.Prox = null;
        quantosNos++;

        return true;
    }
    public bool Incluir(Dado novoValor)         // (bool) Inserir nó com Dado em ordem crescente
    {
        throw new NotImplementedException();
    }
    public bool Incluir(Dado novoValor, int posicaoDeInclusao)  // inclui novo nó na posição indicada da lista
    {
        throw new NotImplementedException();
    }
    public Dado this[int indice]
    {
        get
        {
            if (indice < 0 || indice > quantosNos) throw new IndexOutOfRangeException();
            atual = primeiro;
            int idAtual = 0;
            while(atual != null)
            {
                atual = atual.Prox;
                if(idAtual < indice)
                    idAtual++;
            }
            return atual.Info;
        }
        set {
                if (indice < 0 || indice > quantosNos) throw new IndexOutOfRangeException();
                atual = primeiro;
                int idAtual = 0;
                while (atual != null)
                {
                    atual = atual.Prox;
                    if (idAtual < indice)
                        idAtual++;
                }
                atual.Info = value;
            }
        }
    public Dado DadoAtual()  // retorna o dado atualmente visitado
    {
        return atual.Info;
    }
    public void ExibirDados()   // lista os dados armazenados na lista em modo console
    {
        atual = primeiro;
        while(atual != null)
        {
            Console.WriteLine(atual.Info.ToString());
            atual = atual.Prox;
        }
    }
    public void ExibirDados(ListBox lista)  // lista os dados armazenados na lista no listbox passado como parâmetro
    {
        atual = primeiro;
        while (atual != null)
        {
            lista.Items.Add(atual.Info.ToString());
            atual = atual.Prox;
        }
    }
    public void ExibirDados(ComboBox lista) // lista os dados armazenados na lista no combobox passado como parâmetro
    {
        atual = primeiro;
        while(atual != null)
        {
            lista.Items.Add(atual.Info.ToString());
            atual = atual.Prox;
        }
    }
    public void ExibirDados(TextBox lista)
    {
        atual = primeiro;
        while (atual != null)
        {
            lista.Text = atual.Info.ToString();
            atual = atual.Prox;
        }
    }
    public void Ordenar()
    {
        throw new NotImplementedException();
    }
}