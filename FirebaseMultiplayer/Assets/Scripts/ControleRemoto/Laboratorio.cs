using UnityEditor;

namespace ControleRemoto
{
    public class Laboratorio
    {
        public string nome;
        public string id;
        public string descricao;
        public Maquina[] maquinas;
        
        public Laboratorio(string nome, string id, string descricao, Maquina[] maquinas){
            this.nome = nome;
            this.id = id;
            this.descricao = descricao;
            this.maquinas = maquinas;
        }

        public Laboratorio(string nome, string id, string descricao)
        {
            this.nome = nome;
            this.id = id;
            this.descricao = descricao;
        }
        
        
        
    }
    
    
}