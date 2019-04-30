using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private float _money = 0;

    private string[] units = {"", "Ribu", "Juta"};

    public Player()
    {
        GameManagement.SetMoneyText(ParseMoneytoText(_money));
    }

    public void AddMoney(float amount)
    {
        _money += amount;
        GameManagement.SetMoneyText(ParseMoneytoText(_money));
    }

    public void ReduceMoney(float amount)
    {
        _money -= amount;
        GameManagement.SetMoneyText(ParseMoneytoText(_money));
    }

    public float Money
    {
        get{ return _money; }
    }

    private string ParseMoneytoText(float money, int marker = 0)
    {
        string result = "";
        int p = marker + 1;
        int mark = 1;
        for (int i = 0; i < p; i++)
        {
            mark *= 1000;
        }
        float changed_money = money / mark;
        //Debug.Log("changed_money : " + mark);
        if (money >= mark && marker < (units.Length - 1))
        {
            result = ParseMoneytoText(money, marker + 1);
        }
        else
        {
            changed_money *= 1000;
            changed_money = (float)System.Math.Round((double)changed_money, 1);
            result = changed_money.ToString() + " " + units[marker];
        }

        return result;
    }
}
