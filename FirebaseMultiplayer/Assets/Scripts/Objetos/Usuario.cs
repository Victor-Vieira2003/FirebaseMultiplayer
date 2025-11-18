using UnityEngine;

namespace Objetos
{
    public class Usuario
    {
        public string id;
        public string id_sala;
        public string nome;
        public Estado estado;
        public float t_last_up;

        public Usuario(string id, string id_sala, string nome, Estado estado, float t_last_up)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = t_last_up;
        }
        public Usuario(string id, string id_sala, string nome, Estado estado)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.t_last_up = 0;
        }
        public Usuario()
        {
            this.id = null;
            this.id_sala = null;
            this.nome = null;
            this.estado = Estado.Undefined;
            this.t_last_up = 0;
        }
        
        public Usuario(string id, string id_sala, string nome, float t_last_up)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = Estado.Undefined;
            this.t_last_up = t_last_up;
        }
    }
}
