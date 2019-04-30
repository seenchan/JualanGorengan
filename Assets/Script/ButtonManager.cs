using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour {

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
        GameManagement.ActiveGorengan = gorengan;
    }

    private void TaskOnClick()
    {
        SelectRecipe();
    }
}
