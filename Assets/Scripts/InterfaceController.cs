using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InterfaceController : MonoBehaviour {

    public Button startBtn;
    public Button optionBtn;
	public Button shareBtn;

    public GameObject menu;
    public GameObject option;


	// Use this for initialization

	void Start () {
		option.SetActive (false);
        startBtn.onClick.AddListener(onStartClick);
        optionBtn.onClick.AddListener(onOptionClick);
    }
	
    void onStartClick()
    {
        //Application.LoadLevel("Stage_1");
        SceneManager.LoadScene("Stage_1");
    }

    void onOptionClick()
    {
		option.SetActive(true);
    }
}
