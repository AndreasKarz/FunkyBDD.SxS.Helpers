using SwissLife.SxS.Helpers;
using System.Data;
using Xunit;

namespace Helpers.Test
{
    public class SpecflowHelpersTests
    {
        private readonly DataTable shouldTable;

        public SpecflowHelpersTests()
        {
            shouldTable = new DataTable();
            shouldTable.Columns.Add("head1");
            shouldTable.Columns.Add("head2");
            shouldTable.Columns.Add("head3");
            DataRow row = shouldTable.NewRow();
            row["head1"] = "val1";
            row["head2"] = "val2";
            row["head3"] = "val3";
            shouldTable.Rows.Add(row);
        }

        [Fact]
        public void Csv2Table_with_identic_table()
        {
            // Arrange
            var csvString = "head1,head2,head3\r\nval1,val2,val3";
            
            // Act
            var isTable = SpecflowHelpers.Csv2Table(csvString);

            //Assert
            Assert.Empty(SpecflowHelpers.CompareTables(isTable, shouldTable));
        }

        [Fact]
        public void Csv_2_Table_from_file_readed()
        {
            // Arrange
            var csvString = System.IO.File.ReadAllText(@"goodData.csv");

            // Act
            var isTable = SpecflowHelpers.Csv2Table(csvString);

            //Assert
            Assert.Empty(SpecflowHelpers.CompareTables(isTable, shouldTable));
        }

        [Fact]
        public void Compare_Tables_with_regex()
        {
            // Arrange
            var csvString = "head1,head2,head3\r\nval1,regex:val[0-9],val3";

            // Act
            var isTable = SpecflowHelpers.Csv2Table(csvString);

            //Assert
            Assert.Empty(SpecflowHelpers.CompareTables(isTable, shouldTable));
        }
    }
}
