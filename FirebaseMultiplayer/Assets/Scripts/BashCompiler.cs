using System;
using UnityEngine;

public class BashCompiler : MonoBehaviour
{
    //definição dos indicadores iniciais
    private string[] IndicadoresDeInicio = new string[]{"@"};
    
    //definição dos indicadores de termino da instrrução
    private string[] IndicadoresDeTermino = new string[]{"./"};
    
    //definição dos comandos e qual ou quais os tipos de parametros o mesmo aceita
    private BashBasicComand[] Comandos = new BashBasicComand[]
    {
        new BashBasicComand("open", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("off",  new tipoDoParametro[] { tipoDoParametro.undefined, tipoDoParametro._int }),
        new BashBasicComand("url", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("msg", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("kill", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("lock", new tipoDoParametro[] { tipoDoParametro.undefined })
    };

    public bool CompileThisInstruction(string comando)
    {
        string[] validateInitialSimbols;
        try
        {
            validateInitialSimbols = comando.Split('>');
            //verifica se o comando é valido
            foreach (var initialChar in IndicadoresDeInicio)
            {
                if ( validateInitialSimbols[0].ToCharArray()[0].ToString() == initialChar)
                {//verificando o carracter inicial
                    foreach (var finalChars in IndicadoresDeTermino)
                    {
                        //verificando o caracter final
                        var characters = validateInitialSimbols[1].ToCharArray();
                        string lastSimbols = characters[characters.Length - 2].ToString() + characters[characters.Length - 1].ToString();
                        if (lastSimbols == finalChars)
                        {
                            //Exatraindo e Concatenando o comando
                            char[] comandCHAR = validateInitialSimbols[0].ToCharArray();
                            string comand = "";
                            for (int i = 1; i < comandCHAR.Length; i++)
                            {
                                comand += comandCHAR[i].ToString();
                            }

                            //verificando se o comando é valido
                            foreach (var token in Comandos)
                            {
                                if (comand == token.comando)
                                {
                                    //Extraindo o parametro para verificação
                                    string parametro = validateInitialSimbols[1].ToString().Trim('/').Trim('.');
                                    //TRAVADO NESSA PARTE
                                }
                            }
                            return false;
                        }
                    }
                    return false;
                }
            }
            Debug.Log("fim do try");
            return false;
        }
        catch (Exception e)
        {
            Debug.Log("Execeção encontrada: " + e.Message);
            return false;
        }

        
    }
    
}
public class BashBasicComand
{
    public string comando;
    public tipoDoParametro[] tipo;

    public BashBasicComand(string comando, tipoDoParametro[] tipo)
    {
        this.comando = comando;
        this.tipo = tipo;
    }
}

public enum tipoDoParametro
{
    undefined,
    _int,
    _float,
    _char,
    _bool,
}