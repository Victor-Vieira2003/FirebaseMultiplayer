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
        private char[] aleatorio = new[] {'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',

            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',

            '0','1','2','3','4','5','6','7','8','9'}; // caracteres aleatorios
        private Unity.Mathematics.Random  r = new Unity.Mathematics.Random();

        private void Start()
        {
            
        }

        public User(string id, string[] id_sala, string nome, Estado estado, float pooling, PermissionsUsers permissions, string[] keyUsers)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.pooling = pooling;
            this.permissions = permissions;
            this.keyUser = ToKeyUser(keyUsers);
        }
        public User(string id, string[] id_sala, string nome, Estado estado, PermissionsUsers permissions, string[] keyUsers)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = estado;
            this.pooling = 0;
            this.permissions = permissions;
            this.keyUser = ToKeyUser(keyUsers);
        }
        public User()
        {
            this.id = null;
            this.id_sala = null;
            this.nome = null;
            this.estado = Estado.Undefined;
            this.pooling = 0;
            this.permissions = PermissionsUsers.NoAcess;
            this.keyUser = "null";
        }
        
        public User(string id, string[] id_sala, string nome, float pooling, string[] keyUsers)
        {
            this.id = id;
            this.id_sala = id_sala;
            this.nome = nome;
            this.estado = Estado.Undefined;
            this.pooling = pooling;
            this.keyUser = ToKeyUser(keyUsers);
        }

        public User(string id, string nome, PermissionsUsers permissions, string[] keyUsers)
        {
            this.id = id;
            this.id_sala = null;
            this.nome = nome;
            this.estado = Estado.Undefined;
            this.pooling = 0;
            this.permissions = permissions;
            this.keyUser = ToKeyUser(keyUsers);
        }

        //verifica se existe a chave 
        public bool CheckId(string[] key, string value)
        {
            foreach (var word in key)
            {
                if(value == word)
                { 
                    return true;
                }
            }
            return false;
        }

        //cria a chave
        public string ToKeyUser(string[] keys)
        {
            RA = id;
            name = nome;
            authorityLevel = permissions;
            
            string[] divideId = FormateId(id);
            string tempKey = divideId[0] + nome + divideId[1];
            
            bool check = CheckId(keys, tempKey);
            if (!check)
            {
                return tempKey;
            }
            else
            {
                tempKey += "-" + Ramdon(4);
                
                return tempKey;
            }
        }

        //divide a string pela metade e retorna um vetor com cada metade
        private string[] FormateId(string id)
        {
            char[] vetId = id.ToCharArray();
            int index = 0;

            string KeyUser = "";

            string[] vt = new string[2];

            for (index = 0; index < (vetId.Length / 2); index++)
            {
                vt[0] += vetId[index];
            }

            for (index = index; index < vetId.Length; index++)
            {
                vt[1] += vetId[index];
            }
            return vt;
        }

        //cria uma sequencia aleatoria caso ja exista uma chave
        private string Ramdon( int range)
        {
            string rChars = "";

            for (int i = 0; i < range; i++)
            {
                int rIndex = r.NextInt(aleatorio.Length);
                rChars += aleatorio[rIndex].ToString();
            }
            

            return rChars;
        }
        
    }
}
