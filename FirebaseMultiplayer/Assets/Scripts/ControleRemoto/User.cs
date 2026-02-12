using System;
using System.Text.RegularExpressions;
using Objetos;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ControleRemoto
{
    public class User: MonoBehaviour
    {
        public string id;
        public string[] id_sala;
        public string nome;
        public Estado estado;
        public float pooling;
        public PermissionsUsers permissions;
        public string keyUser;

        private const int Length = 15;
        private string RA = "";
        private  string name = "";
        private PermissionsUsers authorityLevel = PermissionsUsers.NoAcess;



        public string[] keys = {"Victor1234567890", "Gustavo1234567890", "Matheus1234567890", "Lucas1234567890", "Bruno1234567890", "Rafael1234567890", "Felipe1234567890", "Gabriel1234567890", "Enzo1234567890", "Leonardo1234567890"};

        private void Start()
        {
            ToKeyUser(keys);
        }

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

        public string ToKeyUser(string[] keys)
        {
            RA = id;
            name = nome;
            authorityLevel = permissions;
            
            this.id = "010101";
            this.nome = "Higor";
            this.permissions = PermissionsUsers.FullAccess;
            

            

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
            Debug.Log(keyUser);
            return "null";
        }
    }
}
