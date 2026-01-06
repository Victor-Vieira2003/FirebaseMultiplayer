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
        new BashBasicComand("open", new tipoDoParametro[] { tipoDoParametro.character }),
        new BashBasicComand("off",  new tipoDoParametro[] { tipoDoParametro.undefined, tipoDoParametro.integer }),
        new BashBasicComand("url", new tipoDoParametro[] { tipoDoParametro.character }),
        new BashBasicComand("msg", new tipoDoParametro[] { tipoDoParametro.character }),
        new BashBasicComand("kill", new tipoDoParametro[] { tipoDoParametro.character }),
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
                        var characters = validateInitialSimbols[1].ToCharArray();
                        string lastSimbols = characters[characters.Length - 2].ToString() + characters[characters.Length - 1].ToString();
                        if (lastSimbols == finalChars)
                        {//verificando o caracter final
                            //CONTINUA DAQUI PORRA
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
        }
    }
    
}
public class BashBasicComand
{
    private string comando;
    private tipoDoParametro[] tipo;

    public BashBasicComand(string comando, tipoDoParametro[] tipo)
    {
        this.comando = comando;
        this.tipo = tipo;
    }
}

public enum tipoDoParametro
{
    undefined,
    integer,
    floating,
    character,
    boolean,
}