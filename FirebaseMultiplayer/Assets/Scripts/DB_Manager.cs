using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class DB_Manager : MonoBehaviour
{
    public TMP_InputField nome;
    public TMP_InputField gold;
    
    private string id_usuario;

    private DatabaseReference reference;
    private void Start()
    {
        id_usuario = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

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
         Debug.Log(retorno.ToString());
    }
}
