using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Meja : Tools
{
    Gorengan _gorengan;
    int total;
    GameObject ChildGorengan;


    public Meja(GameObject Obj) : base(Obj)
    {
        _gorengan = null;
        total = 0;
        ChildGorengan = null;
    }

    public override void Addlevel()
    {
        
        CookingItem item = GameManagement.CookingItems.Find(x => x.UsageTool == Usage.placement);
        Debug.Log("Add level Meja Level : " + item.Level + ", Max Level : " + item.MaxLevel);

        if (GameManagement.Player.Money < item.Price || item.Level >= item.MaxLevel)
        {
            if (GameManagement.Player.Money < item.Price || item.Level >= item.MaxLevel)
            {
                if (GameManagement.Player.Money < item.Price)
                {
                    GameManagement.WarningText = "Not Enough Money To Upgrade";
                }
                else if (item.Level >= item.MaxLevel)
                {
                    GameManagement.WarningText = "Already MAX Level";
                }
                return;
            }
        }

        item.Level++;
        GameManagement.Player.ReduceMoney(item.Price);
        GameObject obj = GameManagement.ToolObjectParent.gameObject;
        foreach (Transform t in obj.transform)
        {
            ToolAction action = t.GetComponent<ToolAction>();
            if (action.UsageFor == Usage.placement)
                action.CheckUnlockStatus();
        }
    }

    public override void AddGorengan(Gorengan gorengan)
    {
        CookingItem item = GameManagement.CookingItems.Find(x => x.UsageTool == Usage.placement);
        ToolAction t_action = ThisObject.GetComponent<ToolAction>();
        if (t_action != null && item != null)
        {
            if (item.Level < t_action.UnlockLevel)
            {
                // can't use wajan because level
                return;
            }

            if (_gorengan == null)
            {
                _gorengan = gorengan;
                GameObject g_object = GameManagement.GorenganObjectList.Find(x => x.name == gorengan.ids);
                ChildGorengan = MonoBehaviour.Instantiate(g_object, ThisObject.transform.position, ThisObject.transform.rotation, ThisObject.transform);
                ChildGorengan.transform.GetChild(0).Find("GorenganName").GetComponent<Text>().text = _gorengan.Name;
            }
            this.total += gorengan.TotalResult;
            ChildGorengan.transform.GetChild(0).Find("GorenganNumber").GetComponent<Text>().text = this.total.ToString();
        }
    }

    public override void OnObjectClick()
    {
        if(total > 0)
        {
            if (_gorengan != null)
                _gorengan = null;

            MonoBehaviour.Destroy(ChildGorengan);
            ChildGorengan = null;
            total = 0;
        }

    }
    public override void MinGorengan(int total)
    {
        this.total -= total;
        if(this.total == 0)
        {
            MonoBehaviour.Destroy(ChildGorengan);
            _gorengan = null;
        }
        ChildGorengan.transform.GetChild(0).Find("GorenganNumber").GetComponent<Text>().text = this.total.ToString();

    }

    public override Gorengan CheckGorengan()
    {
        return _gorengan;
    }

    public Gorengan Gorengan
    {
        get
        {
            return _gorengan;
        }
    }

    public override int GetTotal()
    {
        return total;
    }

    

}
