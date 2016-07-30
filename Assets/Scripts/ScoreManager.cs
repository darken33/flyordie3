using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Interface RestServicesConstants
public class RestServicesConstants {

	// Autorisation Key
	public const string KEY = "349304fb9411b5a286dedc1d1d689ddf6";
	// Starage service URL
	public const string STORAGE_SERVICE_URL = "http://darken33.net/applications/services/flyordie3/score_storage.php";
	// Get service URL
	public const string GET_SERVICE_URL = "http://darken33.net/applications/services/flyordie3/score_service.php";

}

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * ScoreManager - Shows scores on the Scores scene
 * 
 * GNU General Public License
 */
[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour {

	// Text for scores (Canvas)
	public Text bestDate;
	public Text bestScore;
	public Text todayDate;
	public Text todayScore;
	public Text lastDate;
	public Text lastScore;

	// Text for scores (CanvasVR)
	public TextMesh bestDateVR;
	public TextMesh bestScoreVR;
	public TextMesh todayDateVR;
	public TextMesh todayScoreVR;
	public TextMesh lastDateVR;
	public TextMesh lastScoreVR;

	// private values
	private Scoring playerBestGame;
	private Scoring playerTodayGame;
	private Scoring playerLastGame;
	private ScoringWeb theBestScore;
	private ScoringWeb theTodayScore;
	private int today;

	/**
	 * Start() - Call during initialization
	 */ 
	void Start () {
		// Load Player scores
		if (PlayerPrefs.HasKey("bestGame")) {
			playerBestGame = JsonUtility.FromJson<Scoring>(PlayerPrefs.GetString("bestGame"));
		}
		if (PlayerPrefs.HasKey("todayGame")) {
			playerTodayGame = JsonUtility.FromJson<Scoring>(PlayerPrefs.GetString("todayGame"));
			string date = ""+System.DateTime.Now.Year+(System.DateTime.Now.Month < 10 ? "0":"")+System.DateTime.Now.Month+(System.DateTime.Now.Day < 10 ? "0":"")+System.DateTime.Now.Day;
			today = int.Parse (date);
			if (playerTodayGame != null && playerTodayGame.date != today) {
				playerTodayGame = null;
			}
		}
		if (PlayerPrefs.HasKey("lastGame")) {
			playerLastGame = JsonUtility.FromJson<Scoring>(PlayerPrefs.GetString("lastGame"));
		}
		// The Best Score
		theBestScore  = new ScoringWeb();
		theBestScore.date="-";
		theBestScore.score="loading...";
		theBestScore.rank="loading...";
		StartCoroutine(GetBestScore());
		// The Score of the day
		theTodayScore  = new ScoringWeb();
		theTodayScore.date="-";
		theTodayScore.score="loading...";
		theTodayScore.rank="loading...";
		StartCoroutine(GetBestScoreOfTheDay());
	}
	
	/**
	 * Update() : is called once per frame
	 */ 
	void Update () {
		// Canvas
		bestDate.text = "World Record ("+theBestScore.date+") :\nPersonal Record (" + (playerBestGame != null ? GetDateString(playerBestGame.date.ToString()) : "-")+") :\nYour Rank :";				
		bestScore.text = ""+theBestScore.score+"\n"+(playerBestGame != null ? ""+playerBestGame.score : "-")+"\n"+theBestScore.rank;
		todayDate.text = "World Record :\nPersonal Record :\nYour Rank :";				
		todayScore.text = ""+theTodayScore.score+"\n"+(playerTodayGame != null ? ""+playerTodayGame.score : "-")+"\n"+theTodayScore.rank;
		lastDate.text = (playerLastGame != null ? ""+GetDateString(playerLastGame.date.ToString()) : "-")+" :";				
		lastScore.text = (playerLastGame != null ? ""+playerLastGame.score : "-");
		// CanvasVR
		bestDateVR.text = "World Record ("+theBestScore.date+") :\nPersonal Record (" + (playerBestGame != null ? GetDateString(playerBestGame.date.ToString()) : "-")+") :\nYour Rank :";				
		bestScoreVR.text = ""+theBestScore.score+"\n"+(playerBestGame != null ? ""+playerBestGame.score : "-")+"\n"+theBestScore.rank;
		todayDateVR.text = "World Record :\nPersonal Record :\nYour Rank :";				
		todayScoreVR.text = ""+theTodayScore.score+"\n"+(playerTodayGame != null ? ""+playerTodayGame.score : "-")+"\n"+theTodayScore.rank;
		lastDateVR.text = (playerLastGame != null ? ""+GetDateString(playerLastGame.date.ToString()) : "-")+" :";				
		lastScoreVR.text = (playerLastGame != null ? ""+playerLastGame.score : "-");
	}

	/**
	 * GetDateString() - return date mm/dd/yyyy
	 */ 
	private string GetDateString(string date) {
		if (date.Length < 8) {
			return date;
		}
		string dateRaw = date;
		string dateText = dateRaw.Remove (6).Substring (4) + "/" + dateRaw.Substring (6) + "/" + dateRaw.Remove (4);
		return dateText;
	}

	/**
	 * GetBestScore() - Get the best score from server
	 */ 
	IEnumerator GetBestScore() {
		WWW get_service = new WWW (RestServicesConstants.GET_SERVICE_URL+"?key="+RestServicesConstants.KEY+"&score="+playerBestGame.score);
		yield return get_service;
		if (get_service.error == null) {
			string result2 = get_service.text;
			string[] res = result2.Split ('#');
			theBestScore = new ScoringWeb ();
			theBestScore.date = GetDateString(res [0].Substring (1));
			theBestScore.score = res [1];
			theBestScore.rank = res [2];
		} else {
			Debug.Log (get_service.error);
			theBestScore = new ScoringWeb ();
			theBestScore.date = "-";
			theBestScore.score = "-";
			theBestScore.rank = "-";
		}
	}

	/**
	 * GetBestScore() - Get the best score from server
	 */ 
	IEnumerator GetBestScoreOfTheDay() {
		WWW get_service = new WWW (RestServicesConstants.GET_SERVICE_URL+"?key="+RestServicesConstants.KEY+(playerTodayGame != null ? "&score="+playerTodayGame.score : "")+"&date="+today);
		yield return get_service;
		if (get_service.error == null) {
			string result2 = get_service.text;
			string[] res = result2.Split ('#');
			theTodayScore = new ScoringWeb ();
			theTodayScore.date = GetDateString(res [0]);
			theTodayScore.score= res [1];
			theTodayScore.rank= res [2];
		} else {
			Debug.Log (get_service.error);
			theTodayScore = new ScoringWeb ();
			theTodayScore.date = "-";
			theTodayScore.score = "-";
			theTodayScore.rank = "-";
		}
	}
}
