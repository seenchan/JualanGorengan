using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : MonoBehaviour {

    public int UnlockLevel = 1;
    public Usage UsageFor = Usage.cooking;

    public Tools tool;
    // Use this for initialization

    void Start () {
        CheckUnlockStatus();
        if(UsageFor == Usage.cooking)
            tool = new Wajan(gameObject);
        else
            tool = new Meja(gameObject);
	}

    private void OnMouseDown()
    {
        if (UsageFor == Usage.placement)
        {
            tool.OnObjectClick();
        }
    }

    public void CheckUnlockStatus()
    {
        CookingItem item = GameManagement.CookingItems.Find(x => x.UsageTool == UsageFor);

        Debug.Log("check unlock name : " + name);
        if (item != null)
        {
            if (UnlockLevel > item.Level)
            {
                GetComponent<SpriteRenderer>().color = Color.black;
                //this.gameObject.SetActive(false);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                //this.gameObject.SetActive(true);
            }
        }
    }

    public void Addlevel()
    {
        tool.Addlevel();
    }

    // Update is called once per frame
    void Update () {
        if(UsageFor == Usage.cooking)
        {
            if (tool.Status() == FryingStatus.frying)
            {
                //GetComponent<SpriteRenderer>().color = Color.red;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                tool.Cook();
            }
            else if (tool.Status() == FryingStatus.finish)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                //GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        
	}
}
