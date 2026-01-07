using UnityEngine;

namespace Objetos
{
    public class Sala
    {
        public string id;
        public string nome;
        public Campus localizacao;
        public Status status;
        public float? t_last_up;

        public Sala(string id, string nome, Campus localizacao, Status status, float t_last_up)
        {
            this.id = id;
            this.nome = nome;
            this.localizacao = localizacao;
            this.status = status;
            this.t_last_up = t_last_up;
        }

    }
}