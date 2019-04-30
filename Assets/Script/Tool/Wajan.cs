using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wajan : Tools{

    private FryingStatus _FryingStatus = FryingStatus.empty;

    private Gorengan _friyingGorengan;
    float t_time = 0;

    public Wajan(GameObject ThisObject) : base(ThisObject) 
    {
        _friyingGorengan = null;
    }

    public override void Cook()
    {
        if (_FryingStatus == FryingStatus.frying)
        {
            t_time += Time.deltaTime;

            float percentTime = t_time / _friyingGorengan.CookingTime;
            if (!ThisObject.transform.Find("timer").gameObject.activeSelf)
                ThisObject.transform.Find("timer").gameObject.SetActive(true);
            ThisObject.transform.Find("timer").GetChild(0).GetComponent<Slider>().value = 1 - percentTime ;
            if (t_time > _friyingGorengan.CookingTime)
            {
                Finish();
                ThisObject.transform.Find("timer").gameObject.SetActive(false);
            }
        }
    }

    public void Finish()
    {
        _FryingStatus = FryingStatus.finish;
        t_time = 0f;

        Transform toolA = GameManagement.ToolObjectParent.transform;
        ToolAction t_empty = null;
        
        foreach(Transform t in toolA)
        {
            ToolAction ta = t.GetComponent<ToolAction>();
            if(ta.UsageFor == Usage.placement)
            {
                if(t_empty == null && ta.tool.CheckGorengan() == null)
                {
                    t_empty = ta;
                }

                if(ta.tool.CheckGorengan() != null && ta.tool.CheckGorengan().ids == _friyingGorengan.ids)
                {
                    t_empty = ta;
                    break;
                }
            }
        }
        if (t_empty != null)
            t_empty.tool.AddGorengan(_friyingGorengan);

        _FryingStatus = FryingStatus.empty;
        _friyingGorengan = null;
        
    }

    public override void Addlevel()
    {
        Debug.Log("Add level wajan");
        CookingItem item = GameManagement.CookingItems.Find(x => x.UsageTool == Usage.cooking);
        Debug.Log("Add level Wajan Level : " + item.Level + ", Max Level : " + item.MaxLevel);
        if (GameManagement.Player.Money < item.Price || item.Level >= item.MaxLevel)
        {
            if(GameManagement.Player.Money < item.Price)
            {
                GameManagement.WarningText = "Not Enough Money To Upgrade";
            }
            else if (item.Level >= item.MaxLevel)
            {
                GameManagement.WarningText = "Already MAX Level";
            }
            return;
        }

        item.Level++;
        GameObject obj = GameManagement.ToolObjectParent.gameObject;
        GameManagement.Player.ReduceMoney(item.Price);
        foreach (Transform t in obj.transform)
        {
            ToolAction action = t.GetComponent<ToolAction>();
            if (action.UsageFor == Usage.cooking)
                action.CheckUnlockStatus();
        }
    }

    public override void Use(Gorengan gorengan)
    {
        _FryingStatus = FryingStatus.frying;
        _friyingGorengan = gorengan;
    }

    public override FryingStatus Status()
    {
        return _FryingStatus;
    }

    public static void AddGorenganToWajan(Gorengan gorengan)
    {
        bool isAvailable = false;
        CookingItem item = GameManagement.CookingItems.Find(x => x.UsageTool == Usage.cooking);
        foreach (Transform obj in GameManagement.ToolObjectParent.transform)
        {
            ToolAction tool_a = obj.GetComponent<ToolAction>();
            if (item.Level >= tool_a.UnlockLevel && tool_a.UsageFor == Usage.cooking)
            {
                Wajan tool = tool_a.tool as Wajan;
                Debug.Log("wajan level : " + item.Level + ", requirement : " + tool_a.UnlockLevel + ", status empty : " + tool.Status());
                if (tool != null && tool.Status() == FryingStatus.empty)
                {
                    GameManagement.Player.ReduceMoney(gorengan.Cost);
                    tool.Use(gorengan);
                    isAvailable = true;
                    break;
                }
            }
        }

        if (!isAvailable)
        {
            GameManagement.WarningText = "Wajan is Full";
        }
            
    }
}
