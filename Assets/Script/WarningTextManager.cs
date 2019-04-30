using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningTextManager : MonoBehaviour {

    public Text _text;
    private float timer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_text.text != "")
        {
            timer = Time.deltaTime + timer;
            if (timer > 3)
            {
                _text.text = "";
                timer = 0f;
            }
        }
	}
}
