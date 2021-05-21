using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Original view
/// Will be rewrited
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Canvas _faderObj;
    [SerializeField] private Image _faderImg;
    [SerializeField] private float _fadeSpeed = .02f;

    public bool IsGameOver = false;

    private Color fadeTransparency = new Color(0, 0, 0, .04f);

    private string _currentSceneName = string.Empty;
    private AsyncOperation async;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        StartCoroutine(Load("Menu"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Load(sceneName));
        StartCoroutine(FadeOut(_faderObj, _faderImg));
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UnloadScene(string sceneName)
    {
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        _currentSceneName = scene.name;
        StartCoroutine(FadeIn(_faderObj, _faderImg));
    }

    //Iterate the fader transparency to 100%
    IEnumerator FadeOut(Canvas faderObject, Image fader)
    {
        faderObject.enabled = true;
        while (fader.color.a < 1)
        {
            fader.color += fadeTransparency;
            yield return new WaitForSeconds(_fadeSpeed);
        }
        ActivateScene(); //Activate the scene when the fade ends
    }

    // Iterate the fader transparency to 0%
    IEnumerator FadeIn(Canvas faderObject, Image fader)
    {
        while (fader.color.a > 0)
        {
            fader.color -= fadeTransparency;
            yield return new WaitForSeconds(_fadeSpeed);
        }
        faderObject.enabled = false;
    }

    // Begin loading a scene with a specified string asynchronously
    IEnumerator Load(string sceneName)
    {
        _currentSceneName = sceneName;
        async = SceneManager.LoadSceneAsync(_currentSceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        yield return async;

        isReturning = false;
    }

    // Allows the scene to change once it is loaded
    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
			// Quit the application
			Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private bool isReturning = false;

    public void ReturnToMenu()
    {
        if (isReturning)
        {
            return;
        }

        if (_currentSceneName != "Menu")
        {
            StopAllCoroutines();
            LoadScene("Menu");
            isReturning = true;
        }
    }

    //  [SerializeField] private GameObject[] _systemPrefabs;

    //  private List<GameObject> _instancedSystemPrefabs;
    //  private List<AsyncOperation> _loadOperations;

    //  private string _currentSceneName = string.Empty;

    //  private void Start()
    //  {
    //      DontDestroyOnLoad(gameObject);
    //      _loadOperations = new List<AsyncOperation>();
    //InstantiateSystemPrefabs();
    //LoadScene("Main");
    //  }

    //  private void InstantiateSystemPrefabs()
    //  {
    //GameObject prefabInstance;
    //      for (int i = 0; i < _systemPrefabs.Length; i++)
    //      {
    //	prefabInstance = Instantiate(_systemPrefabs[i]);
    //	_instancedSystemPrefabs.Add(prefabInstance);
    //      }
    //  }

    //  public void LoadScene(string sceneName)
    //  {
    //      _currentSceneName = sceneName;
    //      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_currentSceneName, LoadSceneMode.Additive);
    //      _loadOperations.Add(asyncOperation);
    //  }

    //  protected override void OnDestroy()
    //  {
    //base.OnDestroy();

    //      for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
    //      {
    //	Destroy(_instancedSystemPrefabs[i]);
    //      }

    //_instancedSystemPrefabs.Clear();
    //  }
}
