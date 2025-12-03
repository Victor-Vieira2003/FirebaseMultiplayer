using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase.Auth;
using JetBrains.Annotations;
using Objetos;
using Unity.VisualScripting;

public class DB_Manager : MonoBehaviour
{
    [SerializeField]
    private Parametrizador parametrizador;
    [SerializeField]
    private UI_Manager ui_manager;
    [SerializeField]
    private  SceneManager  sceneManager;
    
    private string id_usuario;

    private bool returnVerifyUserInRoom = false;
    [CanBeNull] private Master master = null;
    [CanBeNull]private Submisso submisso = null;
    
    //IDs das salas, Referencia a raiz do banco, Rotas
    #region Variaveis Firebase

        #region IDs Sala Betha

            private const string id_betha1 = "BE_PP_C1_01";
            private const string id_betha2 = "BE_PP_C2_01";
            private const string id_bethaJAU = "BE_JAU_01";
            private const string id_bethaGuaruja = "BE_GUA_01";

        #endregion

        #region IDs Arena

            private const string id_arenaPP =  "AN_PP_01";
            private const string id_arenaGuaruja =  "AN_GUA_01";
            private const string id_arenaJAU =  "AN_JAU_01";

        #endregion

        #region IDs LabMit

            private const string id_labmit1 = "LM_PP_01";
            private const string id_labmit2 = "LM_PP_02";
            private const string id_labmitC2 = "LM_PP_01_C2";
            private const string id_labmitJAU = "LM_JAU_01";
            private const string id_labmitGuaruja = "LM_GUA_01";

        #endregion
        
        private const string id_salaTeste = "TESTE_PP_01"; 
        
        
        
        //referencia ao banco
        private DatabaseReference reference;
        
        //Rotas de Gravação e Consulta
        private string rotaMaster;
        private string rotaSubmisso;
    #endregion
    async void Start()
    {
        await Task.Delay(10000);
        id_usuario = SystemInfo.deviceUniqueIdentifier;//captura o id unico da maquina para usar como chave  no banco
        reference = FirebaseDatabase.DefaultInstance.RootReference;//cria uma referencia para a raiz do banco
            
        Debug.Log(id_usuario);
        //VerifyUserInRoom(id_usuario, id_salaTeste);
        
        //verificando se maquina ja existe para poder realizar 
        bool clienteExistente = await VerifyFirebaseClient(id_usuario);
        if (clienteExistente)
        {
            ui_manager.telaCom_Registro.gameObject.SetActive(true);
            ui_manager.telaSem_Registro.gameObject.SetActive(false);
        }
        else
        {
            ui_manager.telaCom_Registro.gameObject.SetActive(false);
            ui_manager.telaSem_Registro.gameObject.SetActive(true);
        }
        
        //sceneManager.RE_LoadScene();
        //await Task.Delay(10000);
        //sceneManager.RE_LoadScene();
        Debug.Log("fim timer");
    }

    //Retorna a rota a ser utlizada com base no id da sala e no tipo de usuario
    private string GetFirebaseRoute(string id, UserType userType) 
    {
        if (userType == UserType.Master)//Define a rota de Gravação e Consulta do Master
        {
            rotaMaster = "Salas/" + id + "/master/ultimoComando";
            return rotaMaster;
        }
        else if (userType == UserType.Submisso)//Define a rota de gravação e consulta dos submissos
        {
            rotaSubmisso = "Salas/" + id + "/submissos/" + id_usuario + "ultimoComandoRecebido";
            return rotaSubmisso;
        }

        return null;
    }

    private async void VerifyUserInRoom(string userId, string roomId)
    {
        returnVerifyUserInRoom = await VerifyUserInRoomTask(userId, roomId);
    }

    public void NewRegistro()
    {
        string refIDsala = null;
        string campus = parametrizador.localidade + parametrizador.campus;
        Campus localizacao;
        Campus.TryParse(campus, out localizacao);
        string nameNewRoom = parametrizador.roomType.ToString() + parametrizador.localidade.ToString();
        
        #region Registro Sala

            if (localizacao == Campus.Guaruja)//Verificando Guaruja(Betha, Arena ou LabMit)
                {
                    if (parametrizador.roomType == RoomType.Arena)
                    {
                        NewFirebaseRoom(id_arenaGuaruja, localizacao, nameNewRoom);
                        refIDsala = id_arenaGuaruja;
                    }
                    else if (parametrizador.roomType == RoomType.Betha)
                    {
                        NewFirebaseRoom(id_bethaGuaruja, localizacao, nameNewRoom);
                        refIDsala = id_bethaGuaruja;
                    }
                    else if (parametrizador.roomType == RoomType.LabMit)
                    {
                        NewFirebaseRoom(id_labmitGuaruja, localizacao, nameNewRoom);
                        refIDsala = id_labmitGuaruja;
                    }
                }
            else if (localizacao == Campus.Jau)//Verificando JAU(betha, arena, labmit)
                {
                    if (parametrizador.roomType == RoomType.Arena)
                    {
                        NewFirebaseRoom(id_arenaJAU, localizacao, nameNewRoom);
                        refIDsala = id_arenaJAU;
                    }
                    else if (parametrizador.roomType == RoomType.Betha)
                    {
                        NewFirebaseRoom(id_bethaJAU, localizacao, nameNewRoom);
                        refIDsala = id_bethaJAU;
                    }
                    else if (parametrizador.roomType == RoomType.LabMit)
                    {
                        NewFirebaseRoom(id_labmitJAU, localizacao, nameNewRoom);
                        refIDsala = id_labmitJAU;
                    }
                }
            else if (localizacao == Campus.Presidente_Prudente_C1)//Verificando Presidente Prudente C1(betha, arena, labmit)
                {
                    if (parametrizador.roomType == RoomType.Arena)
                    {
                        NewFirebaseRoom(id_arenaPP, localizacao, nameNewRoom);
                        refIDsala = id_arenaPP;
                    }
                    else if (parametrizador.roomType == RoomType.Betha)
                    {
                        NewFirebaseRoom(id_betha1, localizacao, nameNewRoom);
                        refIDsala = id_betha1;
                    }
                    else if (parametrizador.roomType == RoomType.LabMit)
                    {
                        NewFirebaseRoom(id_labmit1, localizacao, nameNewRoom);
                        refIDsala = id_labmit1;
                    }
                    /*
                     * VERIFICAR O LABMIT 2
                     */
                }
            else if (localizacao == Campus.Presidente_Prudente_C2)//Verificando Presidente Prudente C2(betha, arena, labmit)
                {
                    if (parametrizador.roomType == RoomType.Betha)
                    {
                        NewFirebaseRoom(id_betha2, localizacao, nameNewRoom);
                        refIDsala = id_betha2;
                    }
                    else if (parametrizador.roomType == RoomType.LabMit)
                    {
                        NewFirebaseRoom(id_labmitC2, localizacao, nameNewRoom);
                        refIDsala = id_labmitC2;
                    }
                }

        #endregion

        #region Registro Usuario

            NewFirebaseClient(refIDsala);

        #endregion
        
        
        sceneManager.RE_LoadScene();
    }

    #region Tasks

        //Verifica se o usuario desejado existe na sala alvo
            private async Task<bool> VerifyUserInRoomTask(string userId, string roomId)
            {
                string pathMaster = "Salas/" + roomId + "/master/" + userId;
                string pathSubmisso = "Salas/" + roomId + "/submissos/" + userId;
                var REFMaster = FirebaseDatabase.DefaultInstance.GetReference(pathMaster);
                var REFSubmisso = FirebaseDatabase.DefaultInstance.GetReference(pathSubmisso);
        
                bool verificacaoMaster = true;
                bool verificacaoSubmisso  = true;
        
                var result = await GetSnapshot(REFMaster);
        
                if (!(result.Exists))
                {
                    Debug.Log("Nao eh um master de " + roomId);
                    verificacaoMaster = false;
                }
                result = await GetSnapshot(REFSubmisso);
                if (!(result.Exists))
                {
                    Debug.Log("Nao eh um submisso de " + roomId);
                    verificacaoSubmisso = false;
                }
        
                if (!verificacaoMaster &&  !verificacaoSubmisso)
                {
                    Debug.Log("Usuario nao existia" + roomId);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        
            //retorna a snapshot de uma "FireBaseReference"
            private async Task<DataSnapshot> GetSnapshot(DatabaseReference reference)
            {
                return await reference.GetValueAsync();
            }

            //verifica se  a maquina ja é um cliente
            private async Task<bool> VerifyFirebaseClient(string id_client)
            {
                var resultado =  await GetSnapshot(reference.Child("Clientes").Child(id_usuario));
                if (resultado.Exists)
                {
                    Debug.Log("o Cliente: " + id_usuario + " ja esta registrado");
                    return true;
                }

                return false;
            }

            #region Tasks de Apoio

                

            #endregion
    #endregion

    #region Metodos Criadores

        private async void NewFirebaseRoom(string id, Campus localizacao, string nameNewRoom)//cria sala 
        {
            //verificando se a sala ja existe
            var result = await GetSnapshot(reference.Child("Salas").Child(id));
            if (result.Exists)
            {
                Debug.Log("o ID: " + id + " ja esta em uso");
            }
            else
            {
                Sala room = new Sala(id, nameNewRoom, localizacao, Status.Ativo, 5f);
                string json = JsonUtility.ToJson(room);
                
                //criando no banco
                reference.Child("Salas").Child(id).SetRawJsonValueAsync(json);
                
                Debug.Log("Nova sala criada com o ID: " + id);
            }
        }

        //Verifica e caso nao haja, cria um novo usuario disponivel
        private async void NewFirebaseClient(string id_sala)
        {
            if (id_sala != null)
            {
                var resultado = await GetSnapshot(reference.Child("Clientes").Child(id_usuario));
                if (resultado.Exists)
                {
                    Debug.Log("o Cliente: " + id_usuario + " ja esta registrado");
                }
                else
                {
                    var nome = await GetSnapshot(reference.Child("Clientes"));
                
                    Usuario client = new Usuario(id_usuario, id_sala, nome.ChildrenCount.ToString(), Estado.Online, 5f);
                    string json = JsonUtility.ToJson(client);
                
                    reference.Child("Clientes").Child(id_usuario).SetRawJsonValueAsync(json);
                    Debug.Log("Novo cliente " + id_usuario);
                }
            }
        }

        public async void SetMaster()
        {
            string id = id_usuario;
            string id_sala = (await GetSnapshot(reference.Child("Clientes").Child(id).Child("id_sala"))).Value.ToString();
            string nome = (await  GetSnapshot(reference.Child("Clientes").Child(id).Child("nome"))).Value.ToString();
            Estado estado = (Estado) int.Parse((await GetSnapshot(reference.Child("Clientes").Child(id).Child("estado"))).Value.ToString());
            float t_last_up = float.Parse((await GetSnapshot(reference.Child("Clientes").Child(id).Child("t_last_up"))).Value.ToString());
            
            master = new Master(id,  id_sala, nome, estado, t_last_up);
            Debug.Log("Novo Master criado para " + id_sala);
        }

        public async void SetSubmisso()
        {
            string id = id_usuario;
            string id_sala = (await GetSnapshot(reference.Child("Clientes").Child(id).Child("id_sala"))).Value.ToString();
            string nome = (await GetSnapshot(reference.Child("Clientes").Child(id).Child("nome"))).Value.ToString();
            Estado estado = (Estado) int.Parse((await GetSnapshot(reference.Child("Clientes").Child(id).Child("estado"))).Value.ToString());
            float t_last_up = float.Parse((await GetSnapshot(reference.Child("Clientes").Child(id).Child("t_last_up"))).Value.ToString());

            submisso = new Submisso(id, id_sala, nome, estado, t_last_up);
            Debug.Log("Novo submisso criado para " + id_sala);
        }
        
    #endregion
}

/*
         public void CriarUsuario()//metodo que adiciona uma tupla de um usuario ao banco
        {
            Usuario usuario = new Usuario(this.nome.text, int.Parse(this.gold.text));
            string json = JsonUtility.ToJson(usuario);

            reference.Child("usuarios").Child(id_usuario).SetRawJsonValueAsync(json);
        }

        public async void GetUsuario()
        {
            await RetornoUsuario();
        }

        public async Task RetornoUsuario() //recupera o nome do usuario
        {
            var retorno = await reference.Child("usuarios").Child(id_usuario).Child("nome").GetValueAsync();
             Debug.Log(retorno.Value.ToString());
        }
        */
