using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingItem {

    public string ids;
    public int Level = 1;
    public List<Gorengan> StillCooking;

    private string _name;
    private int _price;
    private Usage _usage;
    private int _maxLevel;


    public static List<CookingItem> GetListToolFromCSV()
    {
        CSVReader reader = new CSVReader("config/Upgrade");
        List<CookingItem> gorengs = new List<CookingItem>();

        for (int y = 1; y < reader.getTotalRow; y++)
        {
            if (reader.getdata[0, y] != null)
            {

                CookingItem go = new CookingItem();
                go.ids = reader.getdata[0, y];
                go._name = reader.getdata[1, y];
                Int32.TryParse(reader.getdata[2, y], out go._price);
                switch(reader.getdata[3, y])
                {
                    case "cooking":
                        go._usage = Usage.cooking;
                        break;
                    case "placement":
                        go._usage = Usage.placement;
                        break;
                    default:
                        break;

                }
                Int32.TryParse(reader.getdata[4, y], out go._maxLevel);
                gorengs.Add(go);
            }
        }
        return gorengs;
    }

    public string Name
    {
        get
        {
            return _name;
        }
    }
    public int Price
    {
        get
        {
            return _price;
        }
    }
    public Usage UsageTool
    {
        get
        {
            return _usage ;
        }
    }
    public int MaxLevel
    {
        get
        {
            return _maxLevel;
        }
    }
}
