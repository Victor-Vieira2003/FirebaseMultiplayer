using System;
using System.Globalization;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase.Auth;
using Objetos;

public class DB_Manager : MonoBehaviour
{
    [SerializeField]
    private Parametrizador parametrizador;
    
    private string id_usuario;

    private bool returnVerifyUserInRoom = false;
    
    //IDs das salas, Referencia a raiz do banco, Rotas
    #region Variaveis Firebase

        #region IDs Sala Betha

            private const string id_betha1 = "BE_PP_C1_01";
            private const string id_betha2 = "BE_PP_C2_01";
            private const string id_bethaJAU = "BE_JAU_01";
            private const string id_bethaGuaruja = "BE_GUA_01";

        #endregion

        #region IDs Arena

            private const string id_arena =  "AN_PP_01";

        #endregion

        #region IDs LabMit

            private const string id_labmit1 = "LM_PP_01";
            private const string id_labmit2 = "LM_PP_02";
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
    void Start()
    {
        id_usuario = SystemInfo.deviceUniqueIdentifier;//captura o id unico da maquina para usar como chave  no banco
        reference = FirebaseDatabase.DefaultInstance.RootReference;//cria uma referencia para a raiz do banco

        Debug.Log(id_usuario);
        VerifyUserInRoom(id_usuario, id_salaTeste);
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

    #endregion

    #region Metodos Criadores

        private async void NewFirebaseRoom(string id)//cria sala 
        {
            //verificando se a sala ja existe
            var result = await GetSnapshot(reference.Child("Salas").Child(id));
            if (result.Exists)
            {
                Debug.Log("o ID: " + id + " ja esta em uso");
            }
            else
            {
                //TRATANDO OS DADOS
                string campus = parametrizador.localidade + parametrizador.campus;
                Campus localizacao;
                Campus.TryParse(campus, out localizacao);
                
                
                string nameNewRoom = parametrizador.roomType.ToString() + parametrizador.localidade.ToString();
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
            var resultado = await GetSnapshot(reference.Child("Clientes").Child(id_client));
            if (resultado.Exists)
            {
                Debug.Log("o Cliente: " + id_client + " ja esta registrado");
            }
            else
            {
                var
                Usuario client = new Usuario(id_usuario, id_sala, nome:victor, Estado.Undefined, );
            }
        }

        #endregion

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
}

