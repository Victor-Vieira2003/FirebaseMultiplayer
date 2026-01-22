using UnityEngine;

namespace ControleRemoto
{
    public enum Permissoes
    {
        total,
        parcial,
        restrita,
        nula
    }
    public class Usuario
    {
        public string nome;
        public string localizacao;
        public Permissoes Permissao;
        public string chave;
        public int ra;

        public Usuario(string nome = "", string localizacao = "", Permissoes Permissao = Permissoes.nula, string chave = "", int ra = 0)
        {
            this.nome = nome;
            this.localizacao = localizacao;
            this.Permissao = Permissao;
            this.chave = chave;
            this.ra = ra;
        }

        void GerarChaveAleatorio()
        {
            string chaveTemp = "";
            // Verificacao nome, localizacao, permissao, chave e ra



            chaveTemp = nome + Permissao.ToString();
        }

    }
}

