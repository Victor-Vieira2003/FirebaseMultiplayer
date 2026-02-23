using System;
using System.Text;
using System.Text.RegularExpressions;
using Objetos;
using Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ControleRemoto
{
    public class Laboratorio
    {
        public string nome;
        public string ip;
        public string descricao;
        [SerializeField]
        public Maquina[] maquinas;
        public Campus campus;
        public string KeyMaquina;

        private int index = 0;

        private void Start()
        {
          //  Debug.Log("iniciou");
            index = Random.Range(0, aleatorio.Length);
            string[] chaves = {};
            if (maquinas != null && maquinas.Length > 0)
            {
                KeyMaquina = GerarKeyMaquina(maquinas[0], chaves);
                if (KeyMaquina == "")
                {
                    Debug.Log("vazio");
                }
                Debug.Log(KeyMaquina);
                Debug.Log(ReverterChave(KeyMaquina));
            }
        }

        private static char[] aleatorio = new[] {'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',

            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',

            '0','1','2','3','4','5','6','7','8','9'
            
        }; // caracteres aleatorios
            
        
        public Laboratorio(string nome, Campus campus, string descricao, Maquina[] maquinas){
            this.nome = nome;
            this.descricao = descricao;
            this.maquinas = maquinas;
            this.campus = campus;
        }

        public Laboratorio(string nome, string descricao)
        {
            this.nome = nome;
            this.descricao = descricao;
        }

        public Laboratorio()
        {
            this.nome = "";
            this.descricao = "";
            this.maquinas = new Maquina[0];
            this.campus = new Campus();
        }

        public Laboratorio(string nome, Maquina[] maquinas, Campus campus)
        {
            this.nome = nome;
            this.maquinas = maquinas;
            this.campus = campus;
        }
        
        private string[] FormateKeyLab(Maquina maquina) //Divide a IP da maquina em dois
        {
            string ip = maquina.ip;

            int metade = ip.Length / 2;

            string[] vetKey = new string[2];

            vetKey[0] = ip.Substring(0, metade);
            vetKey[1] = ip.Substring(metade);

            return vetKey;

        }
        
        private string Ramdon( int range) //Cria uma ordem aleatoria se ja exister uma chave igual
        {
            string rChars = "";

            for (int i = 0; i < range; i++)
            {
                int rIndex = Random.Range(0, aleatorio.Length);
                rChars += aleatorio[rIndex];
            }

            return rChars;
        }

        public bool CheckKeyLab(string[] keyLab, string value) //Verifica se existe chave
        {
            foreach (var word in keyLab)
            {
                if (word == value)
                {
                    return true;
                }
            }
            return false;
        }

        public string GerarKeyMaquina(Maquina maquina, string[] keysExistentes) // Cria a chave dos laboratorios
        {
            string[] DivideIP = FormateKeyLab(maquina);

            string baseKey = DivideIP[0] + '-' + nome  + '-' +  DivideIP[1];
            string tempKey = baseKey;

            while (CheckKeyLab(keysExistentes, tempKey))
            {
                tempKey = baseKey + "||" + Ramdon(4);
            }
            return tempKey;
        }

        
        public static (string Id, string Nome)? ReverterChave(string chave)//retorna as informações usadas na chave (IP maquina e nome)
        {
            if(string.IsNullOrWhiteSpace(chave))
                return null;
            
            string estrutura =  chave;
            
            if (chave.Contains("||"))
            {
                var keySplit = chave.Split("||");

                if (keySplit.Length != 2) 
                    return null;
                estrutura = keySplit[0];
            }

            var parts = estrutura.Split('-');

            if (parts.Length < 3) 
                return null;

            string id1 = parts[0];
            string id2 = parts[parts.Length - 1];
            
            if(!int.TryParse(id1, out _) || !int.TryParse(id2, out _))
                return null;
            string idCompleto = id1 + id2;
            
            StringBuilder nameBuilder = new StringBuilder();

            for (int i = 1; i < parts.Length - 1; i++)
            {
                nameBuilder.Append(parts[i]);
                
                if (i < parts.Length - 2)
                    nameBuilder.Append("-");
            }
            string nome = nameBuilder.ToString();

            if (string.IsNullOrWhiteSpace(nome))
                return null;

            return (idCompleto, nome);
        }
        
    }
    
    
}