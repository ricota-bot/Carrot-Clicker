using System.Globalization;
using UnityEngine;

public enum IdleAbreviation
{
    k,
    M,
    B,
    T,
    q,
    Q,
    s,
    S,
    o,
    N,
    d,
    U,
    D,
    Td
}

public static class DoubleUtilities
{

    public static string ToIdleNotation(double value)
    {
        if (value < 1000)
            return value.ToString("F2");

        double temporaryValue = value;
        int abbreviationIndex = -1;

        while (temporaryValue > 1000)
        {
            temporaryValue /= 1000;
            abbreviationIndex++;
        }

        if (abbreviationIndex >= System.Enum.GetValues(typeof(IdleAbreviation)).Length)
            return ToScientificNotation(temporaryValue);

        string idleAbbreviation = System.Enum.GetValues(typeof(IdleAbreviation)).GetValue(abbreviationIndex).ToString();

        return temporaryValue.ToString("F2") + idleAbbreviation;
    }
    public static string ToScientificNotation(double value)
    {
        int exponent = 0;
        double temporaryValue = value;

        if (value < 10)
            return value.ToString("F2"); // UMA CASA DECIMAL

        while (temporaryValue > 10)
        {
            temporaryValue /= 10;
            exponent++;
        }

        return temporaryValue.ToString("F2") + "e" + exponent;
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
