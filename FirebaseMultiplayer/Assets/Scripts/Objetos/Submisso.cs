using UnityEngine;

namespace Objetos
{
    public class Submisso : Usuario
    {
        public int? ultimo_comando;

        public Submisso(string id,  string id_sala, string nome, Estado estado, float t_last_up, int ultimo_comando)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = t_last_up;
            this.ultimo_comando = ultimo_comando;
            Debug.Log("Objeto Criado");
        }
        
        public Submisso(string id,  string id_sala, string nome, Estado estado, float t_last_up)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = t_last_up;
            this.ultimo_comando = 0;
            Debug.Log("Objeto Criado");
        }
        public Submisso()
        {
            this.ultimo_comando = 0;
            Debug.Log("Objeto Criado");
        }
    }
}