using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class CsvSample
{
    // Use this for initialization 
    public static List<string[]> Set(TextAsset csvFile)
    {
        List<string[]> csvDatas = new List<string[]>();
        string[] lines = csvFile.text.Replace("\r\n", "\n").Split("\n"[0]);
        foreach (var line in lines)
        {
            if (line == "") { continue; }
            csvDatas.Add(line.Split(','));
        }
        return csvDatas;
    }
    public static List<string[]> Set(string csvFile) {
        List<string[]> csvDatas = new List<string[]>();
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;
        string[] lines = csvFile.Replace("\r\n", "\n").Split("\n"[0]);
        foreach (var line in lines)
        {
            if (line == "") { continue; }
            csvDatas.Add(line.Split(new char[1] { ' ' }, option));

        }
        return csvDatas;
    }
}