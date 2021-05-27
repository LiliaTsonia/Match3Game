using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Fade _fadeScript;

    public bool IsGameOver = false;

    private string _currentSceneName = string.Empty;
    private AsyncOperation async;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        StartCoroutine(Load("Menu", false));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(UnloadScene(_currentSceneName));
        StartCoroutine(Load(sceneName));
    }

    public void ExitGame()
    {
#if UNITY_STANDALONE
			// Quit the application
			Application.Quit();
#endif

#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator Load(string sceneName, bool fade = true)
    {
        if (fade)
        {
            yield return StartCoroutine(_fadeScript.FadeOut());
        }
        
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return async;

        _currentSceneName = sceneName;

        if (fade)
        {
            StartCoroutine(_fadeScript.FadeIn());
        }
    }

    private IEnumerator UnloadScene(string sceneName)
    {
        async = SceneManager.UnloadSceneAsync(sceneName);
        yield return async;
    }
}
