using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public RectTransform telaCom_Registro;
    public RectTransform telaSem_Registro;
    public RectTransform telaAplicacao;
    
    public Sprite[] registro;

    public void SetSprite(string i_sprite)
    {
        telaAplicacao.gameObject.SetActive(true);
        //telaCom_Registro.GetComponent<Image>().color = Color.clear;
        //telaSem_Registro.GetComponent<Image>().color = Color.clear;
        
        for (int i = 0; i < registro.Length; i++)
        {
            if (registro[i].name == i_sprite)
            {
                telaAplicacao.GetComponent<Image>().sprite = registro[i];
            }
        }
    }
}
