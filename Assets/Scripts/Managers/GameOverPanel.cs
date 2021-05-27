using UnityEngine.UI;
using UnityEngine;

public class GameOverPanel : CommonUIPanel, IPanelWithListeners
{
    [SerializeField] private Text _yourScoreTxt;
    [SerializeField] private Text _highScoreTxt;

    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _menuBtn;

    public void AddListeners()
    {
        //TODO add listeners to ui elements
        _menuBtn.onClick.AddListener(() => GameManager.Instance.LoadScene("Menu"));
    }

    public override void Show(params object[] args)
    {
        base.Show(args);

        int score = (int)args[0];

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            _highScoreTxt.text = "New Best: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            _highScoreTxt.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
        }

        _yourScoreTxt.text = score.ToString();
    }
}
