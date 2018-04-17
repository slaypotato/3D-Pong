using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Difficults
{
	public GameObject easy, normal, hard;
	public Button forward, back;
}

[System.Serializable]
public class ToggleObject
{
	public Toggle toggle;
	public GameObject imgOn, imgOff;
}

public class OptionsControl : MonoBehaviour {

	public ToggleObject vibrate = new ToggleObject();
	public ToggleObject sound = new ToggleObject ();
	public Difficults dif = new Difficults ();

	public GameObject menu;
	public GameController gc;

	private int soundStatus;
	private int diffStatus; //0- easy, 1- normal, 2- hard

	// Use this for initialization
	void Start () {
		soundCheck();
		diffCheck ();
		if (soundStatus == 0) {
			sound.toggle.isOn = false;
		}
		toggleDiff (diffStatus);
		sound.toggle.onValueChanged.AddListener (onClickSound);
		dif.forward.onClick.AddListener (forwardClickDiff);
		dif.back.onClick.AddListener (backClickDiff);
		menu.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		bool menuStatus = gc.getMenuStatus ();
		//soundCheck ();
		if (menuStatus) {
			menu.SetActive (true);
		} else {
			menu.SetActive (false);
		}
		toggleStatus (sound);
		toggleStatus (vibrate);
		toggleDiff (diffStatus);
	}

	void toggleDiff(int i){
		switch (i) {
		case 0:
			dif.easy.SetActive (true);
			dif.normal.SetActive (false);
			dif.hard.SetActive (false);
			break;
		case 1:
			dif.easy.SetActive (false);
			dif.normal.SetActive (true);
			dif.hard.SetActive (false);
			break;
		case 2:
			dif.easy.SetActive (false);
			dif.normal.SetActive (false);
			dif.hard.SetActive (true);
			break;
		}
	}

	void onClickSound(bool isClick){
		if (soundStatus == 1){
			soundStatus = 0;
			PlayerPrefs.SetInt ("sound", 0);
			//Debug.Log ("Sound = 0, Clicked = "+isClick);
		} else {
			soundStatus = 1;
			PlayerPrefs.SetInt ("sound", 1);
			//Debug.Log ("Sound = 1, Clicked = "+isClick);
		}
	}

	void backClickDiff(){
		onClickDiff (-1);
	}

	void forwardClickDiff(){
		onClickDiff (1);
	}

	void onClickDiff(int i){
		diffStatus = diffStatus + i;
		if (diffStatus < 0) {
			diffStatus = 2;
		} else if (diffStatus > 2) {
			diffStatus = 0;
		}
		PlayerPrefs.SetInt ("diff", diffStatus);
		Debug.Log ("Current Difficult: "+diffStatus);
	}

	//UI status
	void toggleStatus(ToggleObject t){
		if (t.toggle.isOn) {
			t.imgOn.SetActive (true);
			t.imgOff.SetActive (false);
		} else {
			t.imgOn.SetActive (false);
			t.imgOff.SetActive (true);
		}
	}

	//Manipulates the Sound in the game start.
	void soundCheck(){
		if (PlayerPrefs.HasKey ("sound")) {
			soundStatus = PlayerPrefs.GetInt ("sound");
			//Debug.Log ("Currrent SoundStatus: "+soundStatus);
		} else {
			soundStatus = 1;
			PlayerPrefs.SetInt ("sound", soundStatus);
			//Debug.Log ("Creating SoundStatus: "+soundStatus);
		}
	}

	void diffCheck(){
		if(PlayerPrefs.HasKey("diff")){
			diffStatus = PlayerPrefs.GetInt("diff");
		} else {
			diffStatus = 0;
			PlayerPrefs.SetInt("diff",diffStatus);
		}
		Debug.Log("Current Difficult: "+diffStatus);
	}

}
