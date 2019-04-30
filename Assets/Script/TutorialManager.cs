using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    private int curTutor=0;

	// Use this for initialization
	void Start () {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextTutorial()
    {
        gameObject.transform.GetChild(curTutor).gameObject.SetActive(false);
        curTutor++;
        gameObject.transform.GetChild(curTutor).gameObject.SetActive(true);
        if (curTutor == 4)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
