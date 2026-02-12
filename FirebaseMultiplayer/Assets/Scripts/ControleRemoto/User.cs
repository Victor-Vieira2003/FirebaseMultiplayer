using UnityEngine;

namespace ControleRemoto
{
    public class User
    {
        public string id;
        public string[] id_sala;
        public string nome;
        public Estado estado;
        public float pooling;
        public PermissionsUsers permissions;
        public string keyUser;

        private const int Length = 15;

        public User(string id, string[] id_sala, string nome, Estado estado, float pooling, PermissionsUsers permissions)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.pooling = pooling;
            this.permissions = permissions;
        }
        public User(string id, string[] id_sala, string nome, Estado estado, PermissionsUsers permissions)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.pooling = 0;
            this.permissions = permissions;
        }
        public User()
        {
            this.id = null;
            this.id_sala = null;
            this.nome = null;
            this.estado = Estado.Undefined;
            this.pooling = 0;
            this.permissions = PermissionsUsers.NoAcess;
        }
        
        public User(string id, string[] id_sala, string nome, float pooling)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = Estado.Undefined;
            this.pooling = pooling;
        }

        public User(string id, string nome, PermissionsUsers permissions)
        {
            this.id = id;
            this.id_sala = null;
            this.nome = nome;
            this.estado = Estado.Undefined;
            this.pooling = 0;
            this.permissions = permissions;
        }

        public bool TextoValido(string texto)
        {
            return Regex.IsMatch(texto, @"^[a-zA-Z0-9 _-]+$");
        }

        private string ToKeyUser(string[] keys)
        {
            const string id = this.id;
            const string nome = this.nome;
            const PermissionsUsers permissions = this.permissions;

            string KeyUser = "";

            for (int i = 0; i < Length; i++)
            {
                if (TextoValido(id))
                {
                    KeyUser += id[Random.Range(0, id.Length)];
                }
                else
                {
                    // fazer a biblioteca para substituição do caractere invalido por um caractere pre-definido
                }

                if (TextoValido(nome))
                {
                    KeyUser += nome[Random.Range(0, nome.Length)];
                }
                else
                {
                    // fazer a biblioteca para substituição do caractere invalido por um caractere pre-definido
                }
            }
            keyUser = KeyUser + permissions.ToString();
        }
    }
}
