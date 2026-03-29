using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public List<Vector3> blockPositions;
    public List<LetterStatus> letterStatuses;

    public static GameState GetCurrentState()
    {
        GameState gameStateToSave = new GameState();
        SavedElement[] elementsToSave = GameObject.FindObjectsByType<SavedElement>();
        gameStateToSave.blockPositions = new List<Vector3>();
        gameStateToSave.letterStatuses = new List<LetterStatus>();

        for (int i = 0; i < elementsToSave.Length; i++)
        {
            elementsToSave[i].saveIndex = i;
            gameStateToSave.blockPositions.Add(elementsToSave[i].transform.position);
            //gameStateToSave.letterStatuses.Add(elementsToSave[i].GetComponent<Letter>().status);
        }

        return gameStateToSave;
    }

    public void LoadGameState()
    {
        SavedElement[] elementsToLoad = GameObject.FindObjectsByType<SavedElement>();
        foreach (var element in elementsToLoad)
        {
            element.transform.position = blockPositions[element.saveIndex];
            //element.GetComponent<Letter>().status = letterStatuses[element.saveIndex];
        }
    }
}
