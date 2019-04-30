using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pembeli{

    public int patientPoint = 100;
    private List<Order> orders = null;
    private int _maxOrder = 2;
    private float _waitingTime = 15.0f;

    public void Orders()
    {
        orders = GetRandomOrder(_maxOrder);
        int minValue = 2;
        int maxValue = 8;
        int i = 0;

        if (GameManagement.OrderPlaceObject.transform.childCount > 0)
        {
            foreach (Transform g in GameManagement.OrderPlaceObject.transform)
            {
                MonoBehaviour.Destroy(g.gameObject);
            }
        }

        foreach (Order o in orders)
        {
            Debug.Log("order -> " + o.item.Name);
            float randomValue = Random.value;
            int range = maxValue - minValue;
            int randomV = Mathf.RoundToInt(randomValue * range);
            randomV = randomV + minValue;
            o.total = randomV;
            //o.total = 3;

            GameObject g = GameManagement.GorenganObjectList.Find(x => x.name == o.item.ids);
            GameObject h = MonoBehaviour.Instantiate(g, GameManagement.OrderPlaceObject.transform.position, GameManagement.OrderPlaceObject.transform.rotation, GameManagement.OrderPlaceObject.transform);
            Vector2 h_pos = h.transform.position;
            h_pos.x = h_pos.x - i;
            h.transform.position = h_pos;
            i++;

            h.transform.GetChild(0).Find("GorenganName").GetComponent<Text>().text = o.item.Name;
            h.transform.GetChild(0).Find("GorenganNumber").GetComponent<Text>().text = o.total.ToString();
        }
    }

    public List<Order> GetOrder
    {
        get
        {
            return orders;
        }
    }

    public int MaxOrder
    {
        get
        {
            return _maxOrder;
        }
        set
        {
            if(value > 0)
                _maxOrder = value;
        }
    }

    public float WaitingTime
    {
        get
        {
            return _waitingTime;
        }
    }

    private int randomOrderCount()
    {
        return 0;
    }


    private List<Order> GetRandomOrder(int count)
    {
        List<Order> choosenGorengans = new List<Order>();
        List<Gorengan> currentGorengans = GameManagement.Gorengans;

        Gorengan t_last_choosen_gorengan = null;
        for (int i = 0; i < count; i++)
        {
            if (t_last_choosen_gorengan != null)
                currentGorengans = currentGorengans.FindAll(x => x.Name != t_last_choosen_gorengan.Name);

            t_last_choosen_gorengan = GetRandomGorengan(currentGorengans);

            Order t_order = new Order();
            t_order.item = t_last_choosen_gorengan;
            choosenGorengans.Add(t_order);
        }

        return choosenGorengans;
    }

    private Gorengan GetRandomGorengan(List<Gorengan> gorengans)
    {
        float randomValue = Random.value;
        float sum_of_weight = 0;
        List<Gorengan> c_gorengan = gorengans;
        foreach (Gorengan gorengan in c_gorengan)
        {
            sum_of_weight += gorengan.RandomWeight;
        }

        randomValue = randomValue * sum_of_weight;
        Gorengan choosenGorengan = null;
        for (int i = 0; i < c_gorengan.Count; i++)
        {
            Gorengan gorengan = c_gorengan[i];
            if (randomValue < gorengan.RandomWeight)
            {
                choosenGorengan = gorengan;
                break;
            }
            randomValue -= gorengan.RandomWeight;
        }

        return choosenGorengan;
    }
}
