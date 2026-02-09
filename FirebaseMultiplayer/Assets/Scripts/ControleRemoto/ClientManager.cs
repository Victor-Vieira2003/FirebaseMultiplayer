using System;
using System.Net;
using System.Threading.Tasks;
using ControleRemoto;
using Firebase.Database;
using Firebase.Analytics;
using Objetos;
using TMPro;
using UnityEngine;

namespace ControleRemoto
{
    public class ClientManager : MonoBehaviour
    {
        #region Variaveis
    
            #region Privates
    
                private DatabaseReference machine_reference;
                private DatabaseReference command_reference;

                private string ip;
                private Maquina thisMachine;
                
                private BashCompiler bashCompiler =  new BashCompiler();
            #endregion
    
            #region Publics
    
                public Transform terminal;
                public GameObject comand;
    
            #endregion
    
        #endregion
    
    
        #region Unity Methods
    
            private void Start()
            {
                //Criando a conexao
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                machine_reference = FirebaseDatabase.DefaultInstance.GetReference("Controle/Maquinas");
                VerifyOrCreateUser();
                
                //Configurando recebimento de comandos
                command_reference = FirebaseDatabase.DefaultInstance.GetReference("Controle/Maquinas/" + ip + "/ultimoComando");
                command_reference.ValueChanged += RefreshControll;
            }
    
        #endregion
    
        
    
        #region Custom Methods
    
            //Verifica se existe e cria, caso nao haja
            private async void VerifyOrCreateUser()
            {
                ip = GetMyIPV4Adress();
                CreateTerminalRegistry("Verificando a Existencia do usuario: " + ip);
    
                var result = await machine_reference.Child(ip).GetValueAsync();
                if (!(result.Exists))//o usuario nao existe
                {
                    CreateTerminalRegistry("Usuario não encontrado", Color.red);
    
                    //criando o objeto desta maquina
                    string name = Dns.GetHostName();
                    int pooling = 20;
                    string originalIP = ip.Replace("-", ".");
                    Maquina my = new Maquina(name, originalIP, pooling);
                    CreateTerminalRegistry("------------------------", Color.blue);
                    CreateTerminalRegistry("Uma nova maquina foi definida");
                    CreateTerminalRegistry("Nome: " +  name);
                    CreateTerminalRegistry("IP: " + ip);
                    CreateTerminalRegistry("Pooling: " + pooling);
                    CreateTerminalRegistry("Estado: " + my.status);
                    CreateTerminalRegistry("------------------------", Color.blue);
                    
                    //registrando no Firebase
                    var path = machine_reference;
                    Debug.Log(path.ToString());
                    await NewFirebaseRegistry(my, path.Child(ip));
                }
                else
                {
                    CreateTerminalRegistry("Usuario Encontrado");
                    
                    //criando o objeto da maquina com base no firebase
                    DataSnapshot machineSnapShot = await GetFirebaseRegistry(machine_reference.Child(ip));
                    string name = machineSnapShot.Child("nome").Value.ToString();
                    string recoveredIP = machineSnapShot.Child("ip").Value.ToString();
                    float pooling = float.Parse(machineSnapShot.Child("pooling").Value.ToString());
                    Estado status = (Estado)(int.Parse(machineSnapShot.Child("status").Value.ToString()));
                    thisMachine = new Maquina(
                        name,
                        recoveredIP,
                        pooling
                        );
                    
                    CreateTerminalRegistry("------------------------", Color.blue);
                    CreateTerminalRegistry("Novo Registro Recuperado");
                    CreateTerminalRegistry("Nome: " +  name);
                    CreateTerminalRegistry("IP: " + recoveredIP);
                    CreateTerminalRegistry("Pooling: " + pooling);
                    CreateTerminalRegistry("Estado: " + status);
                    CreateTerminalRegistry("------------------------", Color.blue);
                    
                }
            }
    
            private string GetMyIPV4Adress()
            {
                string nome = Dns.GetHostName();
                IPAddress[] adresses = Dns.GetHostAddresses(nome);
                string adress = adresses[1].MapToIPv4().ToString();
                adress = adress.Replace(".", "-");
                return adress; 
            }
            
            //Metodo de Controle atualizado a cada alteração
            async void RefreshControll(object sender, ValueChangedEventArgs args)
            {
                if (args.DatabaseError != null)
                {
                    Debug.LogError("Erro do Firebase: " + args.DatabaseError.Message);
                    return;
                }

                // Os dados mais recentes estão disponíveis em args.Snapshot
                DataSnapshot snapshot = args.Snapshot;

                if (snapshot.Exists)
                {
                    DataSnapshot instruction = await GetFirebaseRegistry(command_reference);
                    bashCompiler.CompileThisInstruction(instruction.Value.ToString());
                }
            }

            void OnDestroy()
            {
                if (command_reference != null)
                {
                    command_reference.ValueChanged -= RefreshControll;
                    Debug.Log("Listener de Realtime Database removido.");

                    command_reference.SetValueAsync("");

                }
            }

            #region Task

                private async Task NewFirebaseRegistry<T>( T obj, DatabaseReference path)
                            {
                                string json = JsonUtility.ToJson(obj);
                                //DatabaseReference tempRef =  FirebaseDatabase.DefaultInstance.GetReference(path);
                                
                                bool sucess = (path.SetRawJsonValueAsync(json)).IsFaulted;
                                if (!sucess)
                                {
                                    CreateTerminalRegistry(("Novo Registro Criado em: " + path), Color.yellow);
                                }
                            }
                
                private async Task<DataSnapshot> GetFirebaseRegistry(DatabaseReference path)
                            {
                                DataSnapshot snapshot;
                                snapshot = await path.GetValueAsync();
                                return snapshot;
                            }

            #endregion
            
    
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

                public void CleanTerminal()
                {
                    for (int i = 0; i < terminal.childCount; i++)
                    {
                        Destroy(terminal.GetChild(i).gameObject);
                    }
                }
    
                #endregion
    
        #endregion
    }
}


