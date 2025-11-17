using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
    .
using Objetos;

public class DB_Manager : MonoBehaviour
{
    public GameObject Controller;
    private string id_usuario;

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
        
        //referencia ao banco
        private DatabaseReference reference;
        
        //Rotas de Gravação e Consulta
        private string rotaMaster;
        private string rotaSubmisso;
    #endregion
    private void Start()
    {
        id_usuario = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
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
    */

    private string GetFirebaseRoute(string id, UserType userType)
    {
        if (userType == UserType.Master)//Define a rota de Gravação e Consulta do Master
        {
            string rota = "Salas/" + id + "/master/ultimoComando";
            return rota;
        }
        else if (userType == UserType.Submisso)//Define a rota de gravação e consulta dos submissos
        {
            string rota = "Salas/" + id + "/submissos/" + id_usuario + "ultimoComandoRecebido";
            return rota;
        }

        return null;
    }

    public async Task RetornoUsuario() //recupera o nome do usuario
    {
        var retorno = await reference.Child("usuarios").Child(id_usuario).Child("nome").GetValueAsync();
         Debug.Log(retorno.Value.ToString());
    }
}

