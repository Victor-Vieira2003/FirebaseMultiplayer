using UnityEngine;

namespace Objetos
{
    public class Submisso :  MonoBehaviour
    {
        public string id;
        public string id_sala;
        public string nome;
        public Estado estado;
        public int? ultimo_comando;
        public float t_last_up;

        public Submisso(string id, string id_sala, string nome, Estado estado)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.ultimo_comando = null;
        }
    }
}