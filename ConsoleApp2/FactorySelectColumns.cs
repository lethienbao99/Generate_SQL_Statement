using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.FactoryTableLinks;

namespace ConsoleApp2
{
    public static class FactorySelectColumns
    {

        public static void GenerateSelectColumns(string input, List<DataTableOrigin> dataTableOrigins, string SpCode)
        {
            var listColumns = new List<SelectColumnEntity>();
            input = input.Replace("\"", "");
            input = input.Replace(" AS ", " as ");

            string[] statements = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            statements = statements.Distinct().ToArray();
            foreach (string statement in statements)
            {
                var splitStatement = statement.Split(" ").ToList();
                if(splitStatement.Count == 1)
                {
                    var colum = new SelectColumnEntity();
                    colum.OriginColumn = splitStatement[0].Split(".")[1];
                    colum.AliasColumn = splitStatement[0].Split(".")[1];
                    colum.Alias = splitStatement[0].Split(".")[0];
                    var tableLink = dataTableOrigins.Where(s => s.alias == colum.Alias).FirstOrDefault();
                    if (tableLink != null)
                    {
                        if (tableLink.IsOrigin == true)
                            colum.TableLinks = $"{tableLink.tableName}";
                        else
                            colum.TableLinks = $"{tableLink.tableName}_{colum.Alias}";
                    }
                    listColumns.Add(colum);
                }
                else if(splitStatement.Count == 3)
                {
                    var colum = new SelectColumnEntity();
                    colum.OriginColumn = splitStatement[0].Split(".")[1];
                    colum.AliasColumn = splitStatement[2];
                    colum.Alias = splitStatement[0].Split(".")[0];
                    var tableLink = dataTableOrigins.Where(s => s.alias == colum.Alias).FirstOrDefault();
                    if(tableLink != null)
                    {
                        if(tableLink.IsOrigin == true)
                            colum.TableLinks = $"{tableLink.tableName}";
                        else
                            colum.TableLinks = $"{tableLink.tableName}_{colum.Alias}";
                    }
                    listColumns.Add(colum);
                }
            }
            PrintSelectColumns(listColumns, SpCode);

        }


        private static void PrintSelectColumns (List<SelectColumnEntity> listColumns, string SpCode)
        {
            Console.WriteLine("----------------------------------Start System_SP_SelectColumns-------------------------------------");
            foreach (var item in listColumns)
            {
                Console.WriteLine($"INSERT INTO[dbo].[System_SP_SelectColumns] ([SPCode], [TableName], [ColumnName], [ColumnAlias])");
                Console.WriteLine($"VALUES('{SpCode}', '{item.TableLinks}', '{item.OriginColumn}', '{item.AliasColumn}')");
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------End System_SP_SelectColumns-------------------------------------");

        }



        public class SelectColumnEntity
        {
            public string OriginColumn { get; set; }
            public string AliasColumn { get; set; }
            public string Alias { get; set; }
            public string TableLinks { get; set; }
        }


    }
}
