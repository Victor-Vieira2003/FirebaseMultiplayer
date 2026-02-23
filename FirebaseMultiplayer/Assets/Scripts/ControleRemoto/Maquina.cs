using System.Net;
using JetBrains.Annotations;
using Objetos;
using UnityEngine;

namespace ControleRemoto
{
    [System.Serializable]
    public class Maquina
    {
        public string nome;
        public string ip;
        [CanBeNull] public string ultimoComando = null;
        public float pooling;
        public Estado status;


        public Maquina(string nome, string ip, float pooling)
        {
            this.nome = nome;
            this.ip = ip;
            this.pooling = pooling;
            this.status = Estado.Undefined;
        }

    }
}
