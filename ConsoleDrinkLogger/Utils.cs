using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Validator
    {
        public static bool IsIdValid(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            foreach (char c in input)
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }

        public static bool IsStringValid(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != '/' && c != ' ') return false;
            }

            return true;
        }
    }

    internal class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, /*[AllowNull]*/ string? tableName) where T : class 
        {
            Console.Clear();
            if (tableName == null) tableName = "Default table name";
            if(tableData == null)
            {
                Console.WriteLine("TableData is null, table will be empty.");
            }

            Console.WriteLine("\n");

            ConsoleTableBuilder.From(tableData)
                .WithColumn(tableName)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();

            Console.WriteLine("\n");
        }
    }

}
