# FunkyBDD.SxS.Helpers
Various auxiliary classes for the Specflow xUnit Selenium (SxS) Framework. Makes working with Selenium, Specflow and the APOM model easier.

## ColorHelpers

### ParseColor(string cssColor)

Returns a C# Color object of the given color string. Recognizes hex, rgb and argb definitions.

```c#
using SwissLife.SxS.Helpers;

var color = ColorHelpers.ParseColor("#00FF00");
color = ColorHelpers.ParseColor("argb(1,255,0,0)")
```



## FileHelpers

### RemoveIllegalFileNameChars(string fileName)

As the name suggests.

```c#
using SwissLife.SxS.Helpers;

var fileName = FileHelpers.RemoveIllegalFileNameChars("*ç%&Zhj{//}test.png");
```



## SpecflowHelpers

### CompareTables

Compare two C# DataTable. Support Regex definitions in the first table, thats means, in the Gherkin code.

```c#
using SwissLife.SxS.Helpers;
            
var csvString = "head1,head2,head3\r\nval1,regex:val[0-9],val3";
var isTable = SpecflowHelpers.Csv2Table(csvString);
List<string> result = SpecflowHelpers.CompareTables(isTable, shouldTableFromSelenium);

```

### Csv2Table

Convert a CSV string to a DataTable. The frist line must be the headers and the delemiter must be a comma.

```
var csvString = "head1,head2,head3\r\nval1,val2,val3";
DataTable isTable = SpecflowHelpers.Csv2Table(csvString);
```

