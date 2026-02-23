using System;
using System.Text;
using System.Text.RegularExpressions;
using Objetos;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private string RA = "";
        private  string name = "        ";
        private PermissionsUsers authorityLevel = PermissionsUsers.NoAcess;
        private char[] aleatorio = new[] {'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',

            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',

            '0','1','2','3','4','5','6','7','8','9'}; // caracteres aleatorios

        private Unity.Mathematics.Random r;

        private void Awake()
        {
            r = new Unity.Mathematics.Random((uint) UnityEngine.Random.Range(1, int.MaxValue));
        }

        private void Start()
        {
            string[] chaves = {"abcd", "efgh", "ijkl"};
            keyUser = ToKeyUser(chaves);
            Debug.Log(keyUser);
            Debug.Log(ReverseKey(keyUser));
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
            this.id = "";
            this.id_sala = null;
            this.nome = "";
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
            string baseKey = divideId[0] + '-' + nome + '-' + divideId[1] + '-' + authorityLevel.ToString();
            string tempKey =  baseKey;

            while (CheckId(keys, tempKey))
            {
                tempKey = baseKey + "||" + Ramdon(4);
            }
            return tempKey;
            
        }

        //divide a string pela metade e retorna um vetor com cada metade
        private string[] FormateId(string id)
        {
            char[] vetId = id.ToCharArray();
            int index = 0;

            string KeyUser = "";

            string[] vt = { "", "" };

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

        //retorna as informações usadas na chave (IP maquina, nome e nivel de permissão)
        public static (string Id, String nome, PermissionsUsers permission)? ReverseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            string estrutura = key;

            if (key.Contains("||"))
            {
                var keySplit = key.Split("||");
                
                if(keySplit.Length != 2)
                    return null;
                estrutura = keySplit[0];
            }
            
            var parts = estrutura.Split("-");
            
            if (parts.Length < 4)
                return null;
            
            string id1 = parts[0];
            string id2 = parts[parts.Length - 2];
            string permissaoStr =  parts[parts.Length - 1];
            
            if (!Enum.TryParse(permissaoStr, out PermissionsUsers permissao))
                return null;
            
            if(!int.TryParse(id1, out _) || !int.TryParse(id2, out _))
                return null;

            string idCOmpleto = id1 + id2;
            
            
            StringBuilder nameBUilder = new StringBuilder();

            for (int i = 1; i < parts.Length - 2; i++)
            {
                nameBUilder.Append(parts[i]);
                
                if(i<parts.Length-3)
                    nameBUilder.Append("-");
            }
            string nome = nameBUilder.ToString();
            
            if(string.IsNullOrWhiteSpace(nome))
                return null;
            
            return (idCOmpleto, nome, permissao);
        }
    }
}
