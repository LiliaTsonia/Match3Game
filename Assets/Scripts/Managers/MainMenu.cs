using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _exitBtn;

    private void Start()
    {
        _playBtn.onClick.AddListener(() => GameManager.Instance.LoadScene("Game"));
        //_exitBtn.onClick.AddListener();
    }

}
