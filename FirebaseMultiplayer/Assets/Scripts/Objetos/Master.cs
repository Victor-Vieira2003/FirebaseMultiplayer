using UnityEngine;

namespace Objetos
{
    public class Master : Usuario
    {
        public int? comando;
        

        
        public Master(string id,  string id_sala, string nome, Estado estado, float t_last_up, int comando)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = t_last_up;
            this.comando = comando;
            Debug.Log("Objeto Criado");
        }
        public Master(string id,  string id_sala, string nome, Estado estado, float t_last_up)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = t_last_up;
            this.comando = 0;
            Debug.Log("Objeto Criado");
        }
        public Master()
        {
            this.comando = 0;
            Debug.Log("Objeto Criado");
        }
    }
}