using System;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class BashCompiler
{
    //definição dos indicadores iniciais
    private string[] IndicadoresDeInicio = new string[]{"@"};
    
    //definição dos indicadores de termino da instrrução
    private string[] IndicadoresDeTermino = new string[]{"./"};
    
    //definição dos comandos e qual ou quais os tipos de parametros o mesmo aceita
    private BashBasicComand[] Comandos = new BashBasicComand[]
    {
        new BashBasicComand("open", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("off",  new tipoDoParametro[] { tipoDoParametro.undefined, tipoDoParametro._int }, metodo: ShutDown),
        new BashBasicComand("url", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("msg", new tipoDoParametro[] { tipoDoParametro._char }, metodo: SendMessage),
        new BashBasicComand("kill", new tipoDoParametro[] { tipoDoParametro._char }),
        new BashBasicComand("lock", new tipoDoParametro[] { tipoDoParametro.undefined })
    };
    
    private static PhysicalExecuterCommand executer = new PhysicalExecuterCommand();
    
    //Variaveis de uso do compilador
    //public static string message, tittle;

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
                                    if (parametro!= "")//caso o parametro nao seja vazio
                                    {
                                        //TRAVADO NESSA PARTE
                                        /*
                                         * VALIDAÇÃO DE TIPO
                                         * NAO FUNCIONAL E TEMPORARIAMENTE
                                         * IGNORADO
                                         */
                                        string[] args = parametro.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                        token.metodo(args);
                                    }
                                    else
                                    {
                                        
                                    }
                                    
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

    #region Metodos de Instrucao

        private static void ShutDown(string[] args)
        {
            Debug.Log("desligando");
        }

        private static void SendMessage(string[] args)
        {
            string message = "";
            foreach (var word in args)
            {
                message += (" " + word);
            }
            string body = "@echo off\npowershell -WindowStyle Hidden -Command \"(New-Object -ComObject WScript.Shell).Popup('MENSSAGEM', 0, 'Aviso do Administrador do Sistema', 64)\"\n";
            string message_popUP = body.Replace("MENSSAGEM", message);
            //message_popUP = message_popUP.Replace("TITULO", tittle);
            executer.OverWrite(message_popUP);
            executer.ExecuteIt();
            Debug.Log("concluido");
        }

    #endregion
    
    
}

#region Classes e Definicoes

    public class BashBasicComand
    {
        public string comando;
        public tipoDoParametro[] tipo;
        public Action<string[]> metodo;

        public BashBasicComand(string comando, tipoDoParametro[] tipo)
        {
            this.comando = comando;
            this.tipo = tipo;
        }

        public BashBasicComand(string comando, tipoDoParametro[] tipo, Action<string[]> metodo)
        {
            this.comando = comando;
            this.tipo = tipo;
            this.metodo = metodo;
        }
    }

    public class PhysicalExecuterCommand
    {
        private string physicalExecuter = Path.GetTempPath().ToString() + "\\BashCompilerFile.bat";

        public void OverWrite(string data)
        {
            File.WriteAllText(physicalExecuter, data);
        }

        public void ExecuteIt()
        {
            Process.Start(physicalExecuter);
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

#endregion
