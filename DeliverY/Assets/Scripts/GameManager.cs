using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<GameState> savedStates;
    public static float inputInterval = 0.18f;
    public static LetterStatus overallCurrentLetterStatus = LetterStatus.Nothing;

    [SerializeField] private GameObject bg;
    [SerializeField] private float duration = 0.8f;
    [SerializeField] private Vector3 startScale = new Vector3(3.1f, 3.1f, 3.1f);
    [SerializeField] private Vector3 endScale = new Vector3(0f, 0f, 0f);

    private bool isLoading = false;

    private void Awake()
    {
        Services.EventManager = new EventManager();
        Services.Players = FindObjectsOfType<PlayerControl>().ToList();
        
        //automatically sync letter colliders whenever Transform.Position change, important for checking words during the same frame
        Physics2D.autoSyncTransforms = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        Services.LevelStatus = LevelStatus.Nothing;
        savedStates = new List<GameState>();
        SaveGameState();
        bg.GetComponent<Transform>().localScale = startScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Services.LevelStatus == LevelStatus.StartFirstLevel)
        {
            if (!isLoading)
            {
                isLoading = true;
                StartCoroutine(PlayAnimationAndLoadNextLevel());
            }
        }

        if (Services.LevelStatus == LevelStatus.Replay)
        {
            if (!isLoading)
            {
                isLoading = true;
                StartCoroutine(PlayAnimationAndReload());
            }
        }

        if (Services.LevelStatus == LevelStatus.Victory)
        {
            if (!isLoading) {
                isLoading = true;
                StartCoroutine(PlayAnimationAndLoadNextLevel());
            }
        }
    }

    private IEnumerator PlayAnimationAndLoadNextLevel()
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            bg.GetComponent<Transform>().localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }
        bg.GetComponent<Transform>().localScale = endScale;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private IEnumerator PlayAnimationAndReload()
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            bg.GetComponent<Transform>().localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }
        bg.GetComponent<Transform>().localScale = endScale;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void SaveGameState()
    {
        savedStates.Add(GameState.GetCurrentState());
    }

    public static void UndoMove()
    {
        if (savedStates.Count > 1)
        {
            savedStates[savedStates.Count - 2].LoadGameState();
            savedStates.RemoveAt(savedStates.Count-1);
        }
    }

    public LetterStatus GetOverallWordStatus()
    {
        for (int i = 0; i < Services.Players.Count; i++)
        {
            if (Services.Players[i].GetComponent<CheckWord>().currentLetterStatus==LetterStatus.Flying)
            {
                return LetterStatus.Flying;
            }
        }

        return LetterStatus.Nothing;
    }
}
