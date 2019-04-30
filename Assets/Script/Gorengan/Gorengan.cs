using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gorengan {

    public string ids = null;

    private string _name = null;
    private float _cookingTime = 0;
    private float _randomWeight = 0.0f;
    private int _price = 0;
    private int _totalResult;
    private int _cost;

    public static List<Gorengan> GetListGorenganFromCSV()
    {
        CSVReader reader = new CSVReader("config/Gorengan");
        List<Gorengan> gorengs = new List<Gorengan>();

        for(int y = 1; y < reader.getTotalRow; y++)
        {
            if (reader.getdata[0, y] != null)
            {
                
                Gorengan go = new Gorengan();
                go.ids = reader.getdata[0, y];
                go._name = reader.getdata[1, y];
                float.TryParse(reader.getdata[2, y], out go._cookingTime);
                Int32.TryParse(reader.getdata[3, y], out go._price);
                float.TryParse(reader.getdata[4, y], out go._randomWeight);
                Int32.TryParse(reader.getdata[5, y], out go._totalResult);
                Int32.TryParse(reader.getdata[8, y], out go._cost);
                gorengs.Add(go);
            }
        }
        return gorengs;
    }

    public Gorengan GetGorengan()
    {
        if(GameManagement.Gorengans.Count > 0)
        {
            return GameManagement.Gorengans.Find(x => x.ids == ids);
        }
        else
        {
            return null;
        }
    }



    public string Name
    {
        get
        {
            return _name;
        }
    }

    public float CookingTime
    {
        get
        {
            return _cookingTime;
        }
    }

    public float RandomWeight
    {
        get
        {
            return _randomWeight;
        }
        set
        {
            _randomWeight = value;
        }
    }

    public float Price
    {
        get
        {
            return _price;
        }
    }

    public int TotalResult
    {
        get
        {
            return _totalResult;
        }
    }
    public int Cost
    {
        get
        {
            return _cost;
        }
    }
}
