using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Vector3 playerPos;
    public List<Vector3> blockPositions;

    public static GameState GetCurrentState()
    {
        GameState gameStateToSave = new GameState();
        SavedElement[] elementsToSave = GameObject.FindObjectsOfType<SavedElement>();
        gameStateToSave.blockPositions = new List<Vector3>();

        for (int i = 0; i < elementsToSave.Length; i++)
        {
            elementsToSave[i].saveIndex = i;
            gameStateToSave.blockPositions.Add(elementsToSave[i].transform.position);
        }

        return gameStateToSave;
    }

    public void LoadGameState()
    {
        SavedElement[] elementsToLoad = GameObject.FindObjectsOfType<SavedElement>();
        foreach (var element in elementsToLoad)
        {
            element.transform.position = blockPositions[element.saveIndex];
            
        }
    }
}
