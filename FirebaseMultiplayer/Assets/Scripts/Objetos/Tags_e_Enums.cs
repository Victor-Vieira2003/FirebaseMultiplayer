using UnityEngine;

/*public class Tags_e_Enums : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/
namespace Objetos
{
    public enum Campus
    {
        undefined,
        Presidente_Prudente_C1,
        Presidente_Prudente_C2,
        Jau,
        Guaruja
    }

    public enum Status
    {
        Ativo,
        Inativo,
        Pendente,
        Cancelado,
        Manutencao
    }

    public enum Estado
    {
        Online,
        Offiline,
        Undefined
    }

    public enum UserType
    {
        Undefined,
        Master,
        Submisso
    }

    public enum RoomType
    {
        Unknown,
        Betha,
        Arena,
        LabMit
    }
}