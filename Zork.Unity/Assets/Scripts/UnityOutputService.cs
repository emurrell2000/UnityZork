using System;
using UnityEngine;
using Zork;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private int MaxEntries = 60;

    public UnityOutputService() => mEntries = new List<GameObject>();

    public void Clear()
    {
        mEntries.ForEach(entry => Destroy(entry));
    }

    public void Write(string value)
    {
        ParseAndWriteLine(value);
    }
    public void Write(object value)
    {
        Write(value.ToString());
    }

    public void WriteLine(string value)
    {
        ParseAndWriteLine(value);
    }
    public void WriteLine(object value)
    {
        WriteLine(value.ToString());
    }

    private void ParseAndWriteLine(string value)
    {
        string[] delimiters = { "\n" };

        var lines = value.Split(delimiters, StringSplitOptions.None);
        foreach (var line in lines)
        {
            if (mEntries.Count >= MaxEntries)
            {
                var entry = mEntries.First();
                Destroy(entry);
                mEntries.Remove(entry);
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                WriteNewLine();
            }
            else
            {
                WriteTextLine(line);
            }
        }
    }

    private void WriteNewLine()
    {

    }

    private void WriteTextLine(string value)
    {

    }

    private readonly List<GameObject> mEntries;
}
