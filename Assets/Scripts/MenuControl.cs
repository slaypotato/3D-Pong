using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class EndMenuButtons
{
	public Button restartBtn, returnBtn, optionBtn;
}

public class MenuControl : MonoBehaviour {

	public EndMenuButtons emb = new EndMenuButtons ();

	public GameController gc;

	public GameObject menu;
	public GameObject win_banner, lose_banner;

	private bool status = true;

	void SetBanner(bool result){
		if (result) {
			win_banner.SetActive (true);
			lose_banner.SetActive (false);
		} else {
			win_banner.SetActive (false);
			lose_banner.SetActive (true);
		}
	}

	// Use this for initialization
	void Start () {
		emb.restartBtn.onClick.AddListener(onRestartClick);
		emb.returnBtn.onClick.AddListener(onReturnClick);
		emb.optionBtn.onClick.AddListener(onOptionClick);

		menu.SetActive (false);
	}

	void Update(){
		bool end = gc.getEndGame();
		if (end) {
			this.SetBanner (gc.getVictory ());
			menu.SetActive (true);
		}
	}

	//Listeners
	void onRestartClick()
	{
		//Application.LoadLevel("Stage_1");
		SceneManager.LoadScene("Stage_1");
	}

	void onReturnClick()
	{
		//Application.LoadLevel("Stage_0");
		SceneManager.LoadScene("Stage_0");
	}

	void onOptionClick()
	{
		menu.SetActive(!status);
	}
}
