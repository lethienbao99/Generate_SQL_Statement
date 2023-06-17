using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class FactoryParameters
    {
        //INSERT INTO [dbo].[System_SP_Parameters]([Order], [ParameterName], [ParameterType], [SPCode], [TypeSize])
        //VALUES(2, 'Username', 'NVARCHAR', 'GET_PROFILEQUIT', '50')


        public static void GenerateParameters(string input, string SpCode, bool? isGenerateParamInSP = false,bool? isGenerateEqualParamInSP = false)
        {
            input = input.Replace("@", "");
            input = input.Replace("(", " ");
            input = input.Replace(")", " ");
            input = input.Replace("=", " = ");
            string[] arrInput = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (isGenerateParamInSP != true && isGenerateEqualParamInSP != true)
                Console.WriteLine("----------------------------------Start System_SP_Parameters-------------------------------------");
            int i = 0;
            foreach (var itemArr in arrInput)
            {
                string[] statements = itemArr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (isGenerateParamInSP == true)
                {
                    //INSERT INTO @Parameters
                    //VALUES('EntityName', @EntityName)
                    Console.WriteLine($"INSERT INTO @Parameters");
                    Console.WriteLine($"VALUES('{statements[0]}', @{statements[0]})");
                    Console.WriteLine();
                }
                else if(isGenerateEqualParamInSP == true)
                {
                    //@EntityName=@EntityName,
                    Console.WriteLine($"@{statements[0]}=@{statements[0]},");
                }
                else
                {
                    i++;
                    if (statements.Count() == 4)
                    {
                        Console.WriteLine($"INSERT INTO [dbo].[System_SP_Parameters]([Order], [ParameterName], [ParameterType], [SPCode], [TypeSize])");
                        Console.WriteLine($"VALUES({i}, '{statements[0]}', '{statements[1]}', '{SpCode}', '')");
                        Console.WriteLine();
                    }
                    else if (statements.Count() == 5)
                    {
                        Console.WriteLine($"INSERT INTO [dbo].[System_SP_Parameters]([Order], [ParameterName], [ParameterType], [SPCode], [TypeSize])");
                        Console.WriteLine($"VALUES({i}, '{statements[0]}', '{statements[1]}', '{SpCode}', '{statements[2]}')");
                        Console.WriteLine();
                    }
                }


               
            }
            if (isGenerateParamInSP != true && isGenerateEqualParamInSP != true)
                Console.WriteLine("----------------------------------End System_SP_Parameters-------------------------------------");
        }

    }
}
