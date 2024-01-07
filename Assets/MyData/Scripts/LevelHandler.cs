using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance { get; private set; }

    [SerializeField] private List<Scenes> levelsList;

    [SerializeField] private AnimationClip fadeInClip;
    [SerializeField] private AnimationClip fadeOutClip;

    private Animator animator;
    private Coroutine loadingLevelRoutine;

    private Scenes currentLevel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        currentLevel = levelsList[0];
        DontDestroyOnLoad(gameObject);

        animator = GetComponentInChildren<Animator>();
    }

    public void ReloadLevel()
    {
        StartLoadingLevel();
    }


    public void LoadNextLevel()
    {
        var nextLevel = (((int)currentLevel) + 1) % levelsList.Count;
        currentLevel = (Scenes)nextLevel;
        StartLoadingLevel();
    }

    public void LoadLevel(Scenes level)
    {
        currentLevel = level;
        StartLoadingLevel();
    }

    private void StartLoadingLevel()
    {
        if (loadingLevelRoutine == null)
            loadingLevelRoutine = StartCoroutine(AnimAndLoad());
    }
    private IEnumerator AnimAndLoad()
    {
        yield return new WaitForSeconds(1f);

        animator.Play(fadeInClip.name);

        yield return new WaitForSeconds(fadeInClip.length);

        GameManager.LoadLevel(currentLevel);

        animator.Play(fadeOutClip.name);
        loadingLevelRoutine = null;
    }
}
