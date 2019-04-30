using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RecipeManager : MonoBehaviour {

    public string buttonAction;

	// Use this for initialization
	void Start () {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public void SelectRecipe()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);

        Gorengan gorengan = GameManagement.Gorengans.Find(x => x.ids == buttonAction);
        Player player = GameManagement.Player;

        if (gorengan != null && player.Money > gorengan.Cost)
        {
                Wajan.AddGorenganToWajan(gorengan);
                //GameManagement.Player.ReduceMoney(gorengan.Cost); --moved to wajan.cs supaya kalau wajannya penuh, duit ga kurang.
        }
        else if (gorengan != null && player.Money < gorengan.Cost)
        {
            GameManagement.WarningText = "Don't Have enough money to cook";
        }

    }

    private void TaskOnClick()
    {
        SelectRecipe();
    }
}
