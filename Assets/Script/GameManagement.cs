using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    [Header("Gorengan")]
    public List<GameObject> GorengansObject;

    [Header("Tool")]
    public List<GameObject> ToolObjects;
    public GameObject m_ToolObjectParent;

    [Header("Pembeli")]
    public GameObject PembeliParent;
    public GameObject ObjectPembeli;
    public GameObject OrderPlacement;

    [Header("Recipes")]
    public GameObject RecipesHolder;
    public GameObject RecipesPrefabs;

    [Header("UI Object")]
    public Text MoneyUI;
    public Text WarningTextUI;

    private Player _player;
    private Gorengan _activeGorengan;

    private static GameManagement _this;
    private static List<Gorengan> _listGorengans;
    private static List<CookingItem> _cookingItems;
    private static string _warningText;

    private void Awake()
    {
        _this = this;
        _listGorengans = Gorengan.GetListGorenganFromCSV();
        _cookingItems = CookingItem.GetListToolFromCSV();

        foreach (CookingItem c in _cookingItems)
        {
            Debug.Log("cooking item : " + c.ids);
        }
        
    }

    private void Start()
    {
        _player = new Player();
        _player.AddMoney(10000);
        DrawRecipes();
    }

    private void SetWarning()
    {
        WarningTextUI.text = _warningText;
    }

    public static void SetMoneyText(string amount)
    {
        _this.MoneyUI.text = amount;
    }


    public void DrawRecipes()
    {
        float width = 0f;
        for (int i = 0; i < _listGorengans.Count; i++)
        {
            if (_listGorengans[i] != null && _listGorengans[i].ids != "")
            {
                GameObject GorenganImage = GameManagement.FindGorengan(_listGorengans[i]);
                SpriteRenderer image = null;
                if(GorenganImage != null )
                {
                    Transform TGorenganImage = GorenganImage.transform.Find("gambar");
                    if(TGorenganImage != null)
                    {
                        image = TGorenganImage.GetComponent<SpriteRenderer>();
                    }
                }

                GameObject o_gorengan = Instantiate(RecipesPrefabs, RecipesHolder.transform.position, RecipesHolder.transform.rotation, RecipesHolder.transform);
                o_gorengan.GetComponent<RectTransform>().sizeDelta= new Vector3(1, 1, 1);
                o_gorengan.name = _listGorengans[i].ids;
                o_gorengan.GetComponent<RecipeManager>().buttonAction = _listGorengans[i].ids;
                o_gorengan.transform.Find("nama").GetComponent<Text>().text = _listGorengans[i].Name;
                o_gorengan.transform.Find("Jumlah").GetComponent<Text>().text = _listGorengans[i].TotalResult.ToString() + " X";
                o_gorengan.transform.Find("harga").GetComponent<Text>().text = _listGorengans[i].Cost.ToString();
                if(image != null)
                    o_gorengan.GetComponent<Image>().sprite = image.sprite;
                width += RecipesPrefabs.GetComponent<RectTransform>().sizeDelta.x;
            }
        }

        Debug.Log("width -> " + width);
        //RecipesHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 10);
        //Vector2 r_position = RecipesHolder.GetComponent<RectTransform>().anchoredPosition;
        //r_position.x = 0;
        //RecipesHolder.GetComponent<RectTransform>().anchoredPosition = r_position;

    }

    public static GameObject FindGorengan(Gorengan gorengan)
    {
        GameObject result = GameManagement.GorenganObjectList.Find(x => x.name == gorengan.ids);
        return result;
    }


    public static List<Gorengan> Gorengans
    {
        get
        {
            
            return _listGorengans;
        }
    }
    public static Gorengan ActiveGorengan
    {
        get
        {
            return _this._activeGorengan;
        }
        set
        {
            _this._activeGorengan = value;
        }
    }
    public static List<GameObject> GorenganObjectList
    {
        get
        {
            return _this.GorengansObject;
        }
    }
    public static GameObject OrderPlaceObject
    {
        get
        {
            return _this.OrderPlacement;
        }
    }
    public static List<CookingItem> CookingItems
    {
        get
        {
            return _cookingItems;
        }
    }
    public static GameObject ToolObjectParent
    {
        get
        {
            return _this.m_ToolObjectParent;
        }
    }
    public static Player Player
    {
        get
        {
            return _this._player;
        }
    }
    public static string WarningText
    {
        get
        {
            return _warningText;
        }
        set
        {
            _this.WarningTextUI.text = value;
        }
    }

}
