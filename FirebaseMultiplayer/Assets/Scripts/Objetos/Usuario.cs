using UnityEngine;

public class Usuario : MonoBehaviour
{
    public string nome;
    public int gold;

    public Usuario(string name, int gold)
    {
        this.nome = name;
        this.gold = gold;
    }
}
