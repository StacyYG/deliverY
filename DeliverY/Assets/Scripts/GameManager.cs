using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameState> savedStates;
    public static float inputInterval = 0.25f;
    public static WordStatus overallCurrentWordStatus = WordStatus.Nothing;
    private void Awake()
    {
        Services.EventManager = new EventManager();
        Services.Players = FindObjectsOfType<PlayerControl>().ToList();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        savedStates = new List<GameState>();
        SaveGameState();
    }

    // Update is called once per frame
    void Update()
    {
        //overallCurrentWordStatus = GetOverallWordStatus();
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

    public WordStatus GetOverallWordStatus()
    {
        for (int i = 0; i < Services.Players.Count; i++)
        {
            if (Services.Players[i].GetComponent<CheckWord>().currentWordStatus==WordStatus.Flying)
            {
                return WordStatus.Flying;
            }
        }

        return WordStatus.Nothing;
    }
}
