using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Original view
/// Will be rewrited
/// </summary>
/// 
public class GUIManager : MonoBehaviour {
	public static GUIManager instance;

	public GameObject gameOverPanel;
	public Text yourScoreTxt;
	public Text highScoreTxt;

	public Text scoreTxt;
	public Text moveCounterTxt;

	private int score;

	void Awake() {
		instance = GetComponent<GUIManager>();
	}

	// Show the game over panel
	public void GameOver() {
		GameManager.Instance.IsGameOver = true;

		gameOverPanel.SetActive(true);

		if (score > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", score);
			highScoreTxt.text = "New Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		} else {
			highScoreTxt.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		yourScoreTxt.text = score.ToString();
	}

}
