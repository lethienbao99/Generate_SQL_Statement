using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class FactoryTableLinks
    {
        private class DataTableLink
        {
            public string strField { get; set; }
            public string tableName { get; set; }
            public string tableNameJoin { get; set; }
            public string alias { get; set; }
            public string aliasTableJoin { get; set; }
            public string originColumnJoin { get; set; }
            public string ColumnJoin { get; set; }
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

        public static string[] GenerateTableLinks(string input, string spCode, string TableFrom, out List<DataTableOrigin> ListDataTableOrigin)
        {
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
                                    tableNames[tableNames.Count - 1] += "_" + alias;
                                }
                                else
                                {
                                    string alias = keywords[i + 2];
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
            PrintTableLinks(dicTable, tableNames, spCode, TableFrom, out ListDataTableOrigin);
            return tableNames.ToArray();
        }



        private static void PrintTableLinks(Dictionary<string, DataTableLink> data, List<string> TableNames, string spCode, string TableFrom, out List<DataTableOrigin> ListDataTableOrigin)
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
                else
                {
                    dataOrigin.tableName = $"{listItem[0]}_{listItem[1]}";
                    dataOrigin.alias = listItem[2];
                }
                listTableName.Add(dataOrigin);
            }
            ListDataTableOrigin = listTableName;

            int i = 1;
            Console.WriteLine("----------------------------------Start System_SP_TableLinks-------------------------------------");
            Console.WriteLine($"INSERT INTO [dbo].[System_SP_TableLinks]( [SPCode], [TableName], [TableAlias], [Order])");
            Console.WriteLine($"VALUES ('{spCode}', '{TableFrom.Split(".")[0]}', '{TableFrom.Split(".")[1]}', 0)");
            Console.WriteLine();

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
                

                Console.WriteLine($"INSERT INTO[dbo].[System_SP_TableLinks] ([SPCode], [TableName], [TableAlias], [Order], [JointTable], [JointTableColumn], [LinkedTableColumn], [JointType], ExtraConditions, CustomQuery)");
                Console.WriteLine($"VALUES('{spCode}', '{item.Key}', '{item.Value.alias}', {i}, '{JointTableColumn}', '{item.Value.originColumnJoin}', '{item.Value.ColumnJoin}', 'LEFT', NULL, 'select {item.Value.strField} FROM {item.Value.tableName} WHERE IsDelete IS NULL')");
                Console.WriteLine();
                i++;
            }
            Console.WriteLine("----------------------------------End System_SP_TableLinks-------------------------------------");
        }


    }
    
}
