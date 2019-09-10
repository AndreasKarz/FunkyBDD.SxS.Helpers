using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SwissLife.SxS.Helpers
{
    public static class SpecflowHelpers
    {
        private const string RegexPrefix = "regex:";

        /// <summary>
        ///     Compares a SpecFlow data table with a DotNet data table. 
        ///     Supports Regex placeholder inside the SpecFlow data table.
        /// </summary>
        /// <param name="sTable">The SpecFlow data table from the Gherkin code</param>
        /// <param name="tTable">The DotNet data table</param>
        /// <returns>The result as list. If count = 0 then the tables are identical.</returns>
        public static List<string> CompareTables(Table sTable, DataTable tTable)
        {
            var result = new List<string>();

            try
            {
                for (var i = 0; i < sTable.Rows.Count; i++)
                {
                    for (var j = 0; j < sTable.Header.Count; j++)
                    {
                        var expectedValue = sTable.Rows[i].ElementAt(j).Value;
                        expectedValue = expectedValue.Replace("{nl}", "\r\n");
                        var value = tTable.Rows[i][j].ToString();

                        if (!valuesMatch(expectedValue, value))
                        {
                            result.Add($"Row {(i + 1):D3} / Col {(j + 1):D3} | {expectedValue} != {value}");
                        }
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                result.Add("Tabelle zeigt WENIGER Zeilen an als erwartet!");
            }

            if (sTable.Rows.Count < tTable.Rows.Count)
            {
                result.Add("Tabelle zeigt MEHR Zeilen an als erwartet!");
            }

            return result;
        }

        private static bool valuesMatch(string expected, string value)
        {
            if (expected.StartsWith(RegexPrefix))
            {
                return Regex.Match(
                    value, expected.Substring(RegexPrefix.Length), RegexOptions.IgnoreCase
                ).Success;
            }

            return string.Equals(expected, value, StringComparison.InvariantCulture);
        }

    }
}
