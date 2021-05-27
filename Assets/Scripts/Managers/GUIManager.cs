using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	[SerializeField] private CommonUIPanel _gameOverPanel;

	[SerializeField] private Text _scoreTxt;
	[SerializeField] private Text _moveCounterTxt;

	private int _score;

	public void ShowGameOverPanel()
    {
        _gameOverPanel.Show(_score);
	}
}
