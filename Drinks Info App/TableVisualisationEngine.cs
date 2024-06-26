﻿using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;

namespace Drinks_Info_App
{
    public class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
        {
            Console.Clear();
            if (tableName == null)
            {
                tableName = "";
            }
            Console.WriteLine("\n\n");
            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
