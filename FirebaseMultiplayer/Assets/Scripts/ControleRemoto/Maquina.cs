using System.Net;
using JetBrains.Annotations;
using Objetos;
using UnityEngine;

namespace ControleRemoto
{
    public class Maquina
    {
        public string nome;
        public IPAddress ip;
        [CanBeNull] public string ultimoComando = null;
        public float pooling;
        public Estado status;

        public Maquina(string nome, IPAddress ip, float pooling)
        {
            this.nome = nome;
            this.ip = ip;
            this.pooling = pooling;
            this.status = Estado.Undefined;
        }

    }
}
