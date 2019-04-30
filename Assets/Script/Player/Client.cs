using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour {

    public Slider patientTimer;
    private Pembeli pembeli;
    float timeLeft = 0.0f;

    // Use this for initialization
    void Start () {
        pembeli = new Pembeli();
        pembeli.Orders();
    }

    // Update is called once per frame
    void Update() {
        //if(Input.GetMouseButtonUp(0))
        //      {
        //          Debug.Log("pembeli Clicked");
        //          GiveOrder();
        //      }
        if (timeLeft <= 0.0f) {
            timeLeft = pembeli.WaitingTime;
        } else{
            timeLeft -= Time.deltaTime;
            patientTimer.value = timeLeft/pembeli.WaitingTime;
            //Debug.Log(timeLeft);
            if (timeLeft < 0)
            {
                pembeli.Orders();
                GameManagement.WarningText = "Customer is Angry. There is a 200 Coins Penalty!";
                GameManagement.Player.ReduceMoney(200);
            }
        }
        //Debug.Log("here lies" + timeLeft);
    }

    public void OnMouseDown()
    {
        GiveOrder();
    }

    public void GiveOrder()
    {
        List<Order> orders = pembeli.GetOrder;
        bool ifEnough = isOrderEnough(orders);
        if (ifEnough)
        {
            GameManagement.WarningText = "Thanks for the Gorengan! Delicious!";
            GiveOrdertoPembeli(orders);
            timeLeft = pembeli.WaitingTime;
        }
        else
        {
            GameManagement.WarningText = "I need all of my Gorengan!";
        }
    }

    private void GiveOrdertoPembeli(List<Order> orders)
    {
        float totalPrice = 0;
        foreach (Order o in orders)
        {
            foreach (Transform obj in GameManagement.ToolObjectParent.transform)
            {
                ToolAction tool = obj.GetComponent<ToolAction>();

                if (tool.UsageFor == Usage.placement)
                {
                    if (tool.tool.CheckGorengan() != null && tool.tool.CheckGorengan().ids == o.item.ids && tool.tool.GetTotal() >= o.total)
                    {
                        tool.tool.MinGorengan(o.total);
                        totalPrice += (o.item.Price * o.total);
                    }
                }
            }
        }
        GameManagement.Player.AddMoney(totalPrice);
        pembeli.Orders();
    }

    private bool isOrderEnough(List<Order> orders)
    {
        bool ifEnough = true;
        foreach (Order o in orders)
        {
            bool isFoundOrder = false;
            foreach (Transform obj in GameManagement.ToolObjectParent.transform)
            {
                ToolAction tool = obj.GetComponent<ToolAction>();

                if (tool.UsageFor == Usage.placement)
                {
                    if (tool.tool.CheckGorengan() != null && tool.tool.CheckGorengan().ids == o.item.ids && tool.tool.GetTotal() >= o.total)
                    {
                        isFoundOrder = true;
                    }
                }
            }
            if (isFoundOrder == false)
            {
                Debug.Log("is not enough");
                ifEnough = false;
                break;
            }
        }

        return ifEnough;
    }

}
