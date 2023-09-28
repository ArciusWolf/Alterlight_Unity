using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _progressBar;
    private float _target;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadLevel(string levelName)
    {
        _target = 0f;
        _progressBar.fillAmount = 0f;
        var level = SceneManager.LoadSceneAsync(levelName);
        level.allowSceneActivation = false;

        _loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(100);
            _progressBar.fillAmount = level.progress;

        } while (level.progress < 0.9f);

        level.allowSceneActivation = true;
        _loadingScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, Time.deltaTime * 3f);
    }
}
