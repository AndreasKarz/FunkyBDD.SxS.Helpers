using System;
using System.Drawing;
using System.Globalization;

namespace SwissLife.SxS.Helpers
{
    public static class ColorHelpers
    {
        /// <summary>
        ///     Parse a color object from a string. 
        ///     Solves the problem that different browsers return different color values.
        /// </summary>
        /// <param name="cssColor">Color value in Hex, RGB or ARGB as string</param>
        /// <returns></returns>
        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                var left = cssColor.IndexOf('(');
                var right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                var noBrackets = cssColor.Substring(left + 1, right - left - 1);

                var parts = noBrackets.Split(',');

                var r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                var g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                var b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    var a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }
    }
}
