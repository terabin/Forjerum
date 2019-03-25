using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

//*********************************************************************************************
// O método de extensões atual simplesmente usa o Invoke para chamar determinado método
// que possua o mesmo nome de outro método determinado fora da extensão, logo, devemos entender 
// que esse não é um método override real, e sim simulado já que é impossível usar o override 
// em métodos estatícos.
//*********************************************************************************************
namespace __Forjerum.Extensions
{
    class ExtensionApp
    {
        //Valores de gerenciamento
        public static object OVERRIDE = false;
        public static object EXTEND = null;

        //*************************************************************
        // CRIAR UMA LISTA COM APENAS OS TYPES RELEVANTES
        // O objeto assembly e o types devem ser instanciados e 
        // tratados como um objeto estático, afinal, ELE NÃO MUDA 
        // os types que usaremos.
        //*************************************************************
        public static List<Type> types = new List<Type>(Assembly.GetExecutingAssembly().GetTypes());
        public static string nameSpace = "FORJERUM.Extensions";

        public static void instanceTypes()
        {
            List<Type> temp_types = new List<Type>();
            foreach (Type type in types)
            {
                if (type.Namespace != nameSpace)
                {
                    temp_types.Add(type);
                }
            }
            foreach (Type type in temp_types)
            {
                types.Remove(type);
            }
        }

        //*************************************************************
        // Método extendMyApp é responsável por receber parâmetros de
        // TODO o Core do projeto e processa-los com base nas extensões,
        // ele também funciona dentro de cada Thread.
        //************************************************************
        public static object extendMyApp(params object[] args)
        {
            //Strings para comparação
            string method_name = args[0].ToString();

            foreach (Type type in types)
            {
                // Obtemos apenas os métodos estáticos
                MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);

                // Classifica os métodos pelo nome
                Array.Sort(methodInfos,
                       delegate(MethodInfo methodInfo1, MethodInfo methodInfo2)
                       { return methodInfo1.Name.CompareTo(methodInfo2.Name); });

                // Obter o nome dos métodos com base no methodInfos
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    //Verifica se alguma extensão pede alguma alteração nesse comando
                    //através da existência de nomes repetidos.
                    if (methodInfo.Name == method_name)
                    {
                        //Armazena os dados dos parâmetros,
                        //não que seja realmente necessário 
                        //mas é uma boa questão de organização
                        ParameterInfo[] parameters = methodInfo.GetParameters();

                        //Parâmetros a serem enviados
                        //Caso o valor a ser enviado seja nulo esse
                        //objeto é ignorado :/
                        object[] parametersArray = new object[] { args };

                        //Instância da classe em que executaremos, 
                        //podemos chamar esse objeto de "contexto" ;)
                        object classInstance = Activator.CreateInstance(type, null);

                        //Enviar com base nos parâmetros necessários para a execução do objeto.
                        object result = methodInfo.Invoke(classInstance, parameters.Length == 0 ? null : parametersArray);

                        //OVERRIDE não permite que outras extensões sejam executadas nesse método
                        //deve ser usado apenas em necessidade.
                        if (result != EXTEND) { return result; }
                    }
                }
            }

            //Caso nada exista, retornamos um valor nulo de qualquer forma
            return null;
        }
    }
}
