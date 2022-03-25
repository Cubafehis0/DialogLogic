// See https://aka.ms/new-console-template for more information
using ExpressionAnalyser;
using System.Collections.Generic;
using UnityEngine;

public class Program
{
    List<string> list = new List<string>()
{
    //"1+1",
    //"4*5",
    "3 && 5",
    "-3",
    "+3",
    "!2",
    "-4*-5",
    "-3*3",
    "-(3*5)",
    "-(-23*13)+a",
    "-(22)",
    "10/2",
    "10%3",
    "0 && 2 || 3",
    "1 || 2 && 0 ",
    "!1 || 0",
    "!1 && 1",
    "!0 && !0",
    "3>3",
    "3<=2",
    "2==1",
    "1!=4",
    "2>=3",
    "1<s"
};

    public void Test()
    {

        KeyValuePair<string, string> pair = new KeyValuePair<string, string>();
        Debug.Log(pair.Key == null);
        foreach (var l in list)
        {
            Debug.Log(ExpressionParser.AnalayseExpression(l).Value);
        }
    }

}

