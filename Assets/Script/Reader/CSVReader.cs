using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class CSVReader{

    public TextAsset CSVFile;

    private string[,] grid;
    private int totalRow = 0;
    private int totalColumn = 0;


	// Use this for initialization
	public CSVReader (string path) {
        TextAsset textRead = Resources.Load(path) as TextAsset;
        grid = getLine(textRead.text);
    }

    private string[,] getLine(string text)
    {
        string[] explodeText = text.Split("\n"[0]);
        totalRow = explodeText.Length;

        int MaxWidth = 0;
        for (int i = 0; i < totalRow; i++)
        {
            string[] checkRow = SplitCsvLine(explodeText[i]);
            MaxWidth = Mathf.Max(MaxWidth, checkRow.Length);
        }
        totalColumn = MaxWidth;
        string[,] result = new string[MaxWidth + 1, totalRow + 1];
        for(int y = 0; y< totalRow; y++)
        {
            string[] checkRow = SplitCsvLine(explodeText[y]);
            for (int x = 0; x < checkRow.Length; x++)
            {
                result[x, y] = checkRow[x];
                result[x, y] = result[x, y].Replace("\"\"", "\"");
            }
        }
        return result;
    }

    public int getTotalRow
    {
        get
        {
            return totalRow;
        }
    }

    public int getTotalColumn
    {
        get
        {
            return totalColumn;
        }
    }

    public string[,] getdata
    {
        get
        {
            return grid;
        }
    }

    static public string Getpath(string fileName)
    {
        return Application.dataPath + fileName;
    }

    static public string GetStreamingAssetPath(string fileName)
    {
        return Getpath("/StreamingAssets/" + fileName);
    }

    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}
