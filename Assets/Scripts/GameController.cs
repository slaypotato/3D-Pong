using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelDiff
{
	public GameObject easy, normal, hard;
}

[System.Serializable]
public class Goals
{
	public GameObject player, opponent;
}

public class GameController : MonoBehaviour {

    private int opponentScore = 0;
    private int playerScore = 0;
    private bool startGame = false; //Tells if the game is running
    private bool endGame = false;
    private bool victory = false;
	private bool menu = false;
    private AudioSource goal;

    public GameObject ball;
	public LevelDiff fieldElements = new LevelDiff ();
	public Goals goals = new Goals ();
	public Button btn_pause;
	public Button btn_resume;
    public int maxScore;

    public Text playerScoreTxt;
    public Text opponentScoreTxt;
    public Text clickTxt;

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
		isDiff ();
	}

    public void setStartGameStatus(bool s)
    {
        startGame = s;
        showClick(!s);
    }

    public void showClick (bool s)
    {
        if (!endGame)
        {
            clickTxt.enabled = s;
            //Debug.Log("Game Status: " + s);
        }
    }

    public bool getStartGameStatus()
    {
        return startGame;
    }

    public void IncreaseOpponentScore()
    {
        opponentScore++;
        opponentScoreTxt.text = "Opponent: "+opponentScore;
        goal.Play();
        VictoryCheck();
    }

    public void IncreasePlayerScore()
    {
        playerScore++;
        playerScoreTxt.text = "Your Score: " + playerScore;
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
			goal.volume = 0.5f;
			ball.GetComponent<AudioSource> ().volume = 0.5f;
		}
	}

	void isDiff(){
		toggleDiff(PlayerPrefs.GetInt("diff"));
	}

	void toggleDiff(int i){
		switch (i) {
		case 0:
			fieldElements.easy.SetActive (true);
			fieldElements.normal.SetActive (false);
			fieldElements.hard.SetActive (false);
			goals.player.transform.localScale = new Vector3(2f, 1, 1);
			goals.opponent.transform.localScale = new Vector3(2f, 1, 1);
			break;
		case 1:
			fieldElements.easy.SetActive (false);
			fieldElements.normal.SetActive (true);
			fieldElements.hard.SetActive (false);
			goals.player.transform.localScale = new Vector3(6.75f, 1, 1);
			goals.opponent.transform.localScale = new Vector3(6.75F, 1, 1);
			break;
		case 2:
			fieldElements.easy.SetActive (false);
			fieldElements.normal.SetActive (false);
			fieldElements.hard.SetActive (true);
			goals.player.transform.localScale = new Vector3(9f, 1, 1);
			goals.opponent.transform.localScale = new Vector3(9f, 1, 1);
			break;
		}
	}
}
