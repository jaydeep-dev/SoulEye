using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mobileInputUI;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void Start()
    {
        bool isPhone = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
        mobileInputUI.SetActive(isPhone);
    }

    private void Update()
    {
        if (inputActions.UI.Pause.WasPressedThisFrame())
        {
            bool isPaused = !pauseMenu.activeInHierarchy;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    public void PlayClicked()
    {
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        LevelHandler.Instance.ReloadLevel();
    }

    public void HomeBTN()
    {
        LevelHandler.Instance.LoadLevel(Scenes.MainMenu);
    }
}