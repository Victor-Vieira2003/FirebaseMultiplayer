using System;
using System.Net;
using System.Threading.Tasks;
using ControleRemoto;
using Firebase.Database;
using Objetos;
using TMPro;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    #region Veriaveis

        #region Privates

            private DatabaseReference machine_reference;
            private DatabaseReference command_reference;

        #endregion

        #region Publics

            public Transform terminal;
            public GameObject comand;

        #endregion

    #endregion


    #region Unity Methods

        private void Start()
        {
            machine_reference = FirebaseDatabase.DefaultInstance.GetReference("Controle/Maquinas");
            VerifyUser();
            }

    #endregion

    

    #region Custom Methods

        private async void VerifyUser()
        {
            string ip = GetMyIPV4Adress();
            CreateTerminalRegistry("Verificando a Existencia do usuario: " + ip);

            var result = await machine_reference.Child(ip).GetValueAsync();
            if (!(result.Exists))//o usuario nao existe
            {
                CreateTerminalRegistry("Usuario não encontrado", Color.red);

                //criando o objeto desta maquina
                string name = Dns.GetHostName();
                int pooling = 20;
                Maquina my = new Maquina(name, IPAddress.Parse(ip), pooling);
                CreateTerminalRegistry("------------------------", Color.blue);
                CreateTerminalRegistry("Uma nova maquina foi denida");
                CreateTerminalRegistry("Nome: " +  name);
                CreateTerminalRegistry("IP: " + ip);
                CreateTerminalRegistry("Pooling: " + pooling);
                CreateTerminalRegistry("Estado: " + my.status);
                CreateTerminalRegistry("------------------------", Color.blue);
                
                //registrando no Firebase
                var path = machine_reference.Child(ip);
                await NewFirebaseRegistry(my, path.ToString());
            }
            else
            {
                CreateTerminalRegistry("Usuario Encontrado");
            }
        }

        private string GetMyIPV4Adress()
        {
            string nome = Dns.GetHostName();
            IPAddress[] adresses = Dns.GetHostAddresses(nome);
            string adress = adresses[1].ToString();
            return adress; 
        }

        private async Task NewFirebaseRegistry<T>( T obj, string path)
        {
            string json = JsonUtility.ToJson(obj);
            DatabaseReference tempRef =  FirebaseDatabase.DefaultInstance.GetReference(path);

            await tempRef.SetRawJsonValueAsync(json);
        }

        #region Terminal Methods

            public void CreateTerminalRegistry(string msg)
            {
                GameObject comandLine = Instantiate(comand,  terminal);
                GameObject cmd =  comandLine.transform.GetChild(0).gameObject;
               
                cmd.GetComponent<TextMeshProUGUI>().text = msg;
            }

            public void CreateTerminalRegistry(string msg, Color color)
            {
                GameObject comandLine = Instantiate(comand,  terminal);
                GameObject cmd =  comandLine.transform.GetChild(0).gameObject;
               
                cmd.GetComponent<TextMeshProUGUI>().text = msg;
                
                //Color
                cmd.GetComponent<TextMeshProUGUI>().color = color;
            }

            #endregion

    #endregion
}
