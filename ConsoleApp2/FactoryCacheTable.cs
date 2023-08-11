using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class FactoryCacheTable
    {
        public class DataTableLink
        {
            public string strField { get; set; }
            public string tableName { get; set; }
            public string tableNameJoin { get; set; }
            public string alias { get; set; }
            public string aliasTableJoin { get; set; }
            public string originColumnJoin { get; set; }
            public string ColumnJoin { get; set; }
            public string ColumnCondition { get; set; }

        }
        public class DataTableOrigin
        {
            public string alias { get; set; }
            public string tableName { get; set; }
            public bool? IsOrigin { get; set; }
        }

        private static string CaseInsenstiveReplace(this string originalString, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue,
               RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(originalString, newValue);
        }

        public static string[] GenerateTableLinks(string input, string spCode, string TableFrom, out List<DataTableOrigin> ListDataTableOrigin, out Dictionary<string, DataTableLink> dataReturn)
        {
            input = input.CaseInsenstiveReplace("WHERE", "where");
            input = input.CaseInsenstiveReplace("LEFT", "left");
            input = input.CaseInsenstiveReplace("JOIN", "join");
            input = input.CaseInsenstiveReplace("SELECT", "select");
            input = input.CaseInsenstiveReplace("FROM", "from");
            input = input.CaseInsenstiveReplace(" ON ", " on ");
            input = input.Replace("=", " = ");
            input = input.Replace("DBO.", "");
            input = input.Replace("dbo.", "");
            input = input.Replace("[", "");
            input = input.Replace("]", "");
            input = input.Replace("\"", "");

            List<string> tableNames = new List<string>();
            var listTableName = new List<DataTableOrigin>();
            listTableName.Add(new() { tableName = TableFrom.Split(".")[0], alias = TableFrom.Split(".")[0], IsOrigin = true });
            var dicTable = new Dictionary<string, DataTableLink>();

            string[] statements = input.Split(new string[] { "left" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (string statement in statements)
            {

              /*  if (statement.IndexOf("Cat_DelegateCompany") > 0)
                {
                    Console.WriteLine();
                }*/

                string[] keywords = statement.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                for (int i = 0; i < keywords.Length; i++)
                {
                    if (keywords[i].ToLower() == "from")
                    {
                        if (i + 1 < keywords.Length)
                        {
                            string tableName = keywords[i + 1];
                            if (tableName.StartsWith("\"") && tableName.EndsWith("\""))
                            {
                                tableName = tableName.Substring(1, tableName.Length - 2);
                            }
                            tableNames.Add(tableName);
                            if (keywords.Length > i + 2 && keywords[i + 2].ToLower() != "on")
                            {
                                if (keywords[i + 2].ToLower() == "with" || keywords[i + 2].ToLower() == "nolock")
                                {
                                    string alias = keywords[i + 4];
                                    if (string.IsNullOrEmpty(alias) || alias == "where")
                                    {
                                        alias = keywords[Array.IndexOf(keywords, "on") - 1];
                                    }
                                    tableNames[tableNames.Count - 1] += "_" + alias;
                                }
                                else
                                {
                                    string alias = keywords[i + 2];
                                    if(string.IsNullOrEmpty(alias) || alias == "where")
                                    {
                                        alias = keywords[Array.IndexOf(keywords, "on") - 1];
                                    }
                                    tableNames[tableNames.Count - 1] += "_" + alias;
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Invalid input format.");
                        }
                    }
                    else if (keywords[i] == "join" && keywords[i + 1] != "select")
                    {
                        tableNames.Add($"{keywords[i + 1]}_{keywords[i + 2]}");
                    }

                }

                if (Array.IndexOf(keywords, "select") == 1 && Array.IndexOf(keywords, "from") > 1)
                {
                    string arrFields = string.Empty;
                    for (int i = Array.IndexOf(keywords, "select") + 1; i < Array.IndexOf(keywords, "from"); i++)
                    {
                        arrFields += $",{keywords[i].ToString()}";
                    }
                    arrFields = arrFields.Substring(1);

                    string tableName = keywords[Array.IndexOf(keywords, "from") + 1];

                    string alias = keywords[Array.IndexOf(keywords, "on") - 1];

                    if (tableName.StartsWith("\"") && tableName.EndsWith("\""))
                        tableName = tableName.Substring(1, tableName.Length - 2);
                    var table = tableNames.Where(s => s == $"{tableName}_{alias}").FirstOrDefault();
                    var data = new DataTableLink();
                    data.tableName = tableName;
                    data.alias = alias;
                    data.strField = arrFields;
                    string aliasTableJoin = string.Empty;
                    string field1 = keywords[Array.IndexOf(keywords, "=") - 1];
                    var listField1 = field1.Split(".").ToList();
                    if (listField1[0] == data.alias)
                    {
                        if (listField1[1].StartsWith("\"") && listField1[1].EndsWith("\""))
                        {
                            listField1[1] = listField1[1].Substring(1, listField1[1].Length - 2);
                        }
                        data.originColumnJoin = listField1[1];
                        if (data.originColumnJoin.Contains("\r"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\r", "");
                        if (data.originColumnJoin.Contains("\n"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\n", "");
                        if (data.originColumnJoin.Contains("\t"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\t", "");

                    }
                    else
                    {
                        aliasTableJoin = listField1[0];
                        if (listField1[1].StartsWith("\"") && listField1[1].EndsWith("\""))
                        {
                            listField1[1] = listField1[1].Substring(1, listField1[1].Length - 2);
                        }
                        data.ColumnJoin = listField1[1];
                        if (data.ColumnJoin.Contains("\r"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\r", "");
                        if (data.ColumnJoin.Contains("\n"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\n", "");
                        if (data.ColumnJoin.Contains("\t"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\t", "");
                    }

                    string field2 = keywords[Array.IndexOf(keywords, "=") + 1];
                    var listField2 = field2.Split(".").ToList();
                    if (listField2[0] == data.alias)
                    {
                        if (listField2[1].StartsWith("\"") && listField2[1].EndsWith("\""))
                        {
                            listField2[1] = listField2[1].Substring(1, listField2[1].Length - 2);
                        }
                        data.originColumnJoin = listField2[1];
                        if (data.originColumnJoin.Contains("\r"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\r", "");
                        if (data.originColumnJoin.Contains("\n"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\n", "");
                        if (data.originColumnJoin.Contains("\t"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\t", "");

                    }
                    else
                    {
                        aliasTableJoin = listField2[0];
                        if (listField2[1].StartsWith("\"") && listField2[1].EndsWith("\""))
                        {
                            listField2[1] = listField2[1].Substring(1, listField2[1].Length - 2);
                        }
                        data.ColumnJoin = listField2[1];
                        if (data.ColumnJoin.Contains("\r"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\r", "");
                        if (data.ColumnJoin.Contains("\n"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\n", "");
                        if (data.ColumnJoin.Contains("\t"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\t", "");
                    }
                    if (!string.IsNullOrEmpty(aliasTableJoin))
                        data.aliasTableJoin = aliasTableJoin;

                    dicTable.Add($"{tableName}_{alias}", data);
                }
                else
                {
                    var data = new DataTableLink();
                    data.tableName = keywords[1];
                    data.alias = keywords[2];
                    data.strField = "*";

                    string aliasTableJoin = string.Empty;
                    string field1 = keywords[Array.IndexOf(keywords, "=") - 1];

                    var listField1 = field1.Split(".").ToList();
                    if (listField1[0] == data.alias)
                    {
                        if (listField1[1].StartsWith("\"") && listField1[1].EndsWith("\""))
                        {
                            listField1[1] = listField1[1].Substring(1, listField1[1].Length - 2);
                        }
                        data.originColumnJoin = listField1[1];
                        if (data.originColumnJoin.Contains("\r"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\r", "");
                        if (data.originColumnJoin.Contains("\n"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\n", "");
                        if (data.originColumnJoin.Contains("\t"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\t", "");

                    }
                    else
                    {
                        aliasTableJoin = listField1[0];
                        if (listField1[1].StartsWith("\"") && listField1[1].EndsWith("\""))
                        {
                            listField1[1] = listField1[1].Substring(1, listField1[1].Length - 2);
                        }
                        data.ColumnJoin = listField1[1];
                        if (data.ColumnJoin.Contains("\r"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\r", "");
                        if (data.ColumnJoin.Contains("\n"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\n", "");
                        if (data.ColumnJoin.Contains("\t"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\t", "");
                    }

                    string field2 = keywords[Array.IndexOf(keywords, "=") + 1];
                    var listField2 = field2.Split(".").ToList();
                    if (listField2[0] == data.alias)
                    {
                        if (listField2[1].StartsWith("\"") && listField2[1].EndsWith("\""))
                        {
                            listField2[1] = listField2[1].Substring(1, listField2[1].Length - 2);
                        }
                        data.originColumnJoin = listField2[1];
                        if (data.originColumnJoin.Contains("\r"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\r", "");
                        if (data.originColumnJoin.Contains("\n"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\n", "");
                        if (data.originColumnJoin.Contains("\t"))
                            data.originColumnJoin = data.originColumnJoin.Replace("\t", "");


                    }
                    else
                    {
                        aliasTableJoin = listField2[0];
                        if (listField2[1].StartsWith("\"") && listField2[1].EndsWith("\""))
                        {
                            listField2[1] = listField2[1].Substring(1, listField2[1].Length - 2);
                        }
                        data.ColumnJoin = listField2[1];
                        if (data.ColumnJoin.Contains("\r"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\r", "");
                        if (data.ColumnJoin.Contains("\n"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\n", "");
                        if (data.ColumnJoin.Contains("\t"))
                            data.ColumnJoin = data.ColumnJoin.Replace("\t", "");
                    }
                    if (!string.IsNullOrEmpty(aliasTableJoin))
                        data.aliasTableJoin = aliasTableJoin;

                    dicTable.Add($"{keywords[1]}_{keywords[2]}", data);

                }
            }
            PrintTableLinks(dicTable, tableNames, TableFrom, out ListDataTableOrigin, out dataReturn);
            return tableNames.ToArray();
        }

        private static void PrintTableLinks(Dictionary<string, DataTableLink> data, List<string> TableNames, string TableFrom, out List<DataTableOrigin> ListDataTableOrigin, out Dictionary<string, DataTableLink> dataReturn)
        {
            var listTableName = new List<DataTableOrigin>();
            listTableName.Add(new() { tableName = TableFrom.Split(".")[0], alias = TableFrom.Split(".")[1], IsOrigin = true });
            foreach (var item in TableNames)
            {

                var listItem = item.Split("_").ToList();
                var dataOrigin = new DataTableOrigin();

                if (item.Contains("#"))
                {
                    dataOrigin.tableName = $"{listItem[0]}";
                    dataOrigin.alias = listItem[1];
                }
                else if(listItem.Count() == 4)
                {
                    dataOrigin.tableName = $"{listItem[0]}_{listItem[1]}_{listItem[2]}";
                    dataOrigin.alias = listItem[3];
                }
                else
                {
                    dataOrigin.tableName = $"{listItem[0]}_{listItem[1]}";
                    dataOrigin.alias = listItem[2];
                }
                listTableName.Add(dataOrigin);
            }
            ListDataTableOrigin = listTableName.Where(s => s.tableName.StartsWith("Cat_")).ToList();
            data = data.Where(s => s.Value.tableName.StartsWith("Cat_")).ToDictionary(s => s.Key, s => s.Value);
            var dataObject = new List<DataTableSQL>();

            foreach (var item in data)
            {
                var alisaTableJoinFormData = item.Value.aliasTableJoin;
                string JointTableColumn = string.Empty;
                var tableJoin = listTableName.FirstOrDefault(s => s.alias == alisaTableJoinFormData);
                if (tableJoin != null)
                {
                    item.Value.tableNameJoin = tableJoin.tableName;
                    if (tableJoin.IsOrigin == true)
                        JointTableColumn = item.Value.tableNameJoin;
                    else
                        JointTableColumn = $"{item.Value.tableNameJoin}_{item.Value.aliasTableJoin}";
                }
                item.Value.ColumnCondition = $"{item.Value.aliasTableJoin}_{item.Value.ColumnJoin}";

                dataObject.Add(new DataTableSQL() { aliasTableJoin = item.Value.aliasTableJoin, ColumnJoin = item.Value.ColumnJoin, ColumnCondition = item.Value.ColumnCondition });
            }

            dataObject = dataObject.GroupBy(p => new { p.aliasTableJoin, p.ColumnJoin, p.ColumnCondition })
              .Select(g => g.First())
              .ToList();
            foreach(var item in dataObject)
            {
                //Console.WriteLine($"{item.aliasTableJoin}.{item.ColumnJoin} as {item.ColumnCondition},");
                Console.WriteLine($"public Guid? {item.ColumnCondition} {{ get; set; }}");
            }
            dataReturn = data;
        }


        class DataTableSQL
        {
            public string aliasTableJoin { get; set; }
            public string ColumnJoin { get; set; }
            public string ColumnCondition { get; set; }
        }


        public static void GenerateSelectColumns(string input, List<DataTableOrigin> dataTableOrigins, Dictionary<string, DataTableLink> dataReturn, string entity)
        {
            var listColumns = new List<SelectColumnEntity>();
            input = input.Replace("\"", "");
            input = input.Replace(" AS ", " as ");

            string[] statements = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            statements = statements.Distinct().ToArray();
            foreach (string statement in statements)
            {
                /*if (statement == "Delegate.NoDecision as MoreInfoNoDecision")
                {
                    Console.WriteLine();
                }*/


                var splitStatement = statement.Split(" ").ToList();
                if (splitStatement.Count == 1)
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
                    else
                        continue;
                    listColumns.Add(colum);
                }
                else if (splitStatement.Count == 3)
                {
                    var colum = new SelectColumnEntity();
                    colum.OriginColumn = splitStatement[0].Split(".")[1];
                    colum.AliasColumn = splitStatement[2];
                    colum.Alias = splitStatement[0].Split(".")[0];
                    var tableLink = dataTableOrigins.Where(s => s.alias == colum.Alias).FirstOrDefault();
                    if (tableLink != null)
                    {
                        if (tableLink.IsOrigin == true)
                            colum.TableLinks = $"{tableLink.tableName}";
                        else
                            colum.TableLinks = $"{tableLink.tableName}_{colum.Alias}";
                    }
                    else
                        continue;
                    listColumns.Add(colum);
                }
            }
            PrintSelectColumns(listColumns, dataReturn, entity);

        }


        private static void PrintSelectColumns(List<SelectColumnEntity> listColumns, Dictionary<string, DataTableLink> dataReturn, string entity)
        {
            foreach (var item in dataReturn)
            {

              /*  if (item.Value.ColumnCondition == "hpmf_DelegateCompanyID")
                {
                    Console.WriteLine("STOP");
                }*/

                if (!dataReturn.Values.Select(s => s.alias).Contains(item.Value.aliasTableJoin))
                {
                    var listColumnNames = listColumns.Where(s => s.TableLinks == item.Key).ToList();
                    Console.WriteLine($"if({entity}.{item.Value.ColumnCondition} != null)");
                    Console.WriteLine("{");
                    string tableName = item.Value.tableName;

                    if(tableName.IndexOf("_Translate") > 0)
                    {
                        Console.WriteLine($"var entity = list{tableName}.FirstOrDefault(s => s.OriginID == {entity}.{item.Value.ColumnCondition});");
                    }
                    else
                        Console.WriteLine($"var entity = list{tableName}.FirstOrDefault(s => s.ID == {entity}.{item.Value.ColumnCondition});");

                    foreach (var column in listColumnNames)
                    {
                        Console.WriteLine($"{entity}.{column.AliasColumn} = entity != null ? entity.{column.OriginColumn} : null;");
                    }
                    var listTableChildren = dataReturn.Where(s => s.Value.aliasTableJoin == item.Value.alias).ToList();
                    if (listTableChildren.Count > 0)
                    {
                        for (int i = 0; i < listTableChildren.Count; i++)
                        {
                            var itemChild = listTableChildren[i];

                            var listColumnNamesChild = listColumns.Where(s => s.TableLinks == itemChild.Key).ToList();

                            if(listColumnNamesChild.Count() > 0)
                            {
                                var columID = listColumnNames.FirstOrDefault();
                                Console.WriteLine($"{entity}.{itemChild.Value.ColumnCondition} = entity != null ? entity.{itemChild.Value.ColumnJoin} : null;");

                                Console.WriteLine($"if({entity}.{itemChild.Value.ColumnCondition} != null)");
                                Console.WriteLine("{");
                                string tableNameChild = itemChild.Value.tableName;

                                if (tableName.IndexOf("_Translate") > 0)
                                    Console.WriteLine($"var entityChild{i} = list{tableNameChild}.FirstOrDefault(s => s.OriginID == {entity}.{itemChild.Value.ColumnCondition});");
                                else
                                    Console.WriteLine($"var entityChild{i} = list{tableNameChild}.FirstOrDefault(s => s.ID == {entity}.{itemChild.Value.ColumnCondition});");
                                foreach (var column in listColumnNamesChild)
                                {
                                    Console.WriteLine($"{entity}.{column.AliasColumn} = entityChild{i} != null ? entityChild{i}.{column.OriginColumn} : null;");
                                }
                                Console.WriteLine("}");
                            }
                            
                        }
                    }
                    Console.WriteLine("}");
                }
                Console.WriteLine();
            }
            Console.ReadLine();


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
