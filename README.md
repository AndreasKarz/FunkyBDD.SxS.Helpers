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

Compare a Specflow table with a C# DataTable. Support Regex definitions in the Specflow table, thats means, in the Gherkin code.

```c#
using SwissLife.SxS.Helpers;

List<string> result = SpecflowHelpers.CompareTables(table, _page.Structure);
```



