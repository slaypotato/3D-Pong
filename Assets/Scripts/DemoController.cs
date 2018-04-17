using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DemoController : MonoBehaviour {

	private int opponentScore = 0;
	private int playerScore = 0;
	private bool startGame = false; //Tells if the game is running
	private bool endGame = false;
	private bool victory = false;
	private bool menu = false;
	private AudioSource goal;

	public GameObject ball;
	public Button btn_pause;
	public Button btn_resume;
	public int maxScore;

	// Use this for initialization
	void Start()
	{

		btn_pause.onClick.AddListener (onPauseClick);
		btn_resume.onClick.AddListener (onResumeClick);
		btn_resume.gameObject.SetActive (false);
		goal = GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update()
	{
		if (menu) {
			btn_pause.gameObject.SetActive (false);
			btn_resume.gameObject.SetActive (true);
		} else {
			btn_pause.gameObject.SetActive (true);
			btn_resume.gameObject.SetActive (false);
		}
		isSound ();

	}

	public void setStartGameStatus(bool s)
	{
		startGame = s;
	}

	public bool getStartGameStatus()
	{
		return startGame;
	}

	public void IncreaseOpponentScore()
	{
		opponentScore++;
		goal.Play();
		VictoryCheck();
	}

	public void IncreasePlayerScore()
	{
		playerScore++;
		goal.Play();
		VictoryCheck();
	}

	public bool getEndGame()
	{
		return endGame;
	}

	public bool getVictory()
	{
		return victory;
	}

	private void VictoryCheck(){
		if (opponentScore == maxScore || playerScore == maxScore) {
			endGame = true;
			if (playerScore > opponentScore) {
				victory = true;
			} else {
				victory = false;
			}
		}
	}

	void onPauseClick()
	{
		Time.timeScale = 0;
		menu = true;
	}

	public bool getMenuStatus(){
		return menu;
	}

	void onResumeClick()
	{
		Time.timeScale = 1;
		menu = false;
	}

	void isSound(){
		if (PlayerPrefs.GetInt ("sound") == 0) {
			goal.volume = 0;
			ball.GetComponent<AudioSource> ().volume = 0;
		} else {
			goal.volume = 1;
			ball.GetComponent<AudioSource> ().volume = 1;
		}
	}	
}
