using System.Globalization;
using UnityEngine;

public static class DoubleUtilities
{
    public static string ToScientificNotation(double value)
    {
        int exponent = 0;
        double temporaryValue = value;

        if (value < 10)
            return value.ToString("F1"); // UMA CASA DECIMAL

        while (temporaryValue > 10)
        {
            temporaryValue /= 10;
            exponent++;
        }

        return temporaryValue.ToString("F1") + "e" + exponent;
    }

    public static string ToCustomScientificNotation(double value)
    {
        if (value < Mathf.Pow(10, 12))
            return ToSeparatedThousands(value);

        return ToScientificNotation(value);
    }

    public static string ToSeparatedThousands(double value)
    {
        NumberFormatInfo nfi = new NumberFormatInfo();

        nfi.NumberGroupSeparator = " ";
        nfi.NumberDecimalSeparator = ".";

        return value.ToString("N", nfi);
    }
}
