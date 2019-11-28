using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace SwissLife.SxS.Helpers
{
    public static class SpecflowHelpers
    {
        private const string RegexPrefix = "regex:";

        /// <summary>
        ///     Convert a CSV string to a DataTable
        ///     First must be the headers
        ///     Delemiter is comma ,
        /// </summary>
        /// <param name="csv">CSV string</param>
        /// <returns></returns>
        public static DataTable Csv2Table(string csv = "")
        {
            DataTable resultTable = new DataTable();

            string[] rows = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < rows.Length; i++)
            {
                string[] cols = rows[i].Split(',');

                if (i == 0)
                {
                    foreach (string value in cols)
                    {
                        resultTable.Columns.Add(value.Trim());
                    }
                }
                else
                {
                    DataRow row = resultTable.NewRow();
                    for (int j = 0; j < cols.Length; j++)
                    {
                        row[j] = cols[j].Trim();
                    }
                    resultTable.Rows.Add(row);
                }
            }

            return resultTable;
        }

        /// <summary>
        ///     Compares two data table. 
        ///     Supports Regex placeholder inside the shouldData.
        /// </summary>
        /// <param name="shouldData">DataTable with the expexted data. Supports Regex placeholder</param>
        /// <param name="isData">DataTable with the results from the APOM</param>
        /// <returns>The result as list. If count = 0 then the tables are identical.</returns>
        public static List<string> CompareTables(DataTable shouldData, DataTable isData)
        {
            var result = new List<string>();

            /* Validate count of columns */
            if(shouldData.Columns.Count > isData.Columns.Count)
            {
                result.Add("Table <shouldData> contains more columns then table <isData>!");
                return result;
            } 
            else if (shouldData.Columns.Count < isData.Columns.Count)
            {
                result.Add("Table <isData> contains more columns then table <shouldData>!");
                return result;
            }

            /* Compare Columns */
            for (var i = 0; i < shouldData.Columns.Count; i++)
            {
                var shouldValue = shouldData.Columns[i].ColumnName;
                var isValue = isData.Columns[i].ColumnName;
                if (shouldValue != isValue)
                {
                    result.Add($"Col {(i + 1)} | {shouldValue} != {isValue}");
                }
            }

            /* Compare data columns */
            try
            {
                for (var i = 0; i < shouldData.Rows.Count; i++)
                {
                    for (var j = 0; j < shouldData.Columns.Count; j++)
                    {
                        var shouldValue = shouldData.Rows[i][j].ToString().Replace("{nl}", "\r\n");
                        var isValue = isData.Rows[i][j].ToString();

                        if (!ValuesMatch(shouldValue, isValue))
                        {
                            result.Add($"Row {(i + 1):D3} / Col {(j + 1):D3} | {shouldValue} != {isValue}");
                        }
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                result.Add("Table <isData> displays LESS rows than expected!");
            }
            if (shouldData.Rows.Count < isData.Rows.Count)
            {
                result.Add("Table <isData> displays MORE rows than expected!");
            }

            return result;
        }

        private static bool ValuesMatch(string expected, string value)
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
