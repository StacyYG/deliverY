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
    }

    // Update is called once per frame
    void Update()
    {
        if (Services.LevelStatus == LevelStatus.StartFirstLevel)
        {
            SceneManager.LoadScene(1);
        }

        if (Services.LevelStatus == LevelStatus.Replay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Services.LevelStatus == LevelStatus.Victory)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
