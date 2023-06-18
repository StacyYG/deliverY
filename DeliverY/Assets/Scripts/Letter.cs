using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public bool isFly, isLocked;

    CheckWord _checkWord;
    Block _block;
    public List<Letter> horizontal, vertical;
    public string horizontalWord, verticalWord;
    
    // Start is called before the first frame update
    void Start()
    {
        _block = GetComponent<Block>();
        MakeWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            _block.moveable = false;
        }
    }

    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector2.zero);

        return hit.collider;
    }
    
    Letter GetLetterAt(Vector3 position)
    {
        Letter letterToReturn;
        var collider = GetColliderAt(position);
        if (collider)
        {
            if (collider.TryGetComponent<Letter>(out letterToReturn))
            {
                return letterToReturn;
            }
        }
        return null;
    }

    void GetLettersInDirection(List<Letter> letters, Vector3 direction)
    {
        Letter letter = GetLetterAt(transform.position + direction);
        if (letter)
        {
            letters.Add(letter);
            letter.GetLettersInDirection(letters, direction);
        }
    }
    
    public void MakeWord()
    {
        horizontal = new List<Letter>();
        vertical = new List<Letter>();
        
        var horizontalBefore = new List<Letter>();
        var horizontalAfter = new List<Letter>();
        var verticalBefore = new List<Letter>();
        var verticalAfter = new List<Letter>();
        
        horizontalWord = "";
        verticalWord = "";
        
        GetLettersInDirection(horizontalBefore, Vector3.left);
        GetLettersInDirection(horizontalAfter, Vector3.right);
        GetLettersInDirection(verticalBefore, Vector3.up);
        GetLettersInDirection(verticalAfter, Vector3.down);

        for (int i = 0; i < horizontalBefore.Count; i++)
        {
            horizontalWord = horizontalWord.Insert(0, horizontalBefore[i].name);
            horizontal.Add(horizontalBefore[i]);
        }
        horizontalWord += name;
        horizontal.Add(this);
        for (int i = 0; i < horizontalAfter.Count; i++)
        {
            horizontalWord += horizontalAfter[i].name;
            horizontal.Add(horizontalAfter[i]);
        }
        
        for (int i = 0; i < verticalBefore.Count; i++)
        {
            verticalWord = verticalWord.Insert(0, verticalBefore[i].name);
            vertical.Add(verticalBefore[i]);
        }   
        verticalWord += name;
        vertical.Add(this);
        for (int i = 0; i < verticalAfter.Count; i++)
        {
            verticalWord += verticalAfter[i].name;
            vertical.Add(verticalAfter[i]);
        }
    }
}

public enum LetterStatus
{
    Nothing, Flying, Locked
}

public enum LevelStatus
{
    Nothing, StartFirstLevel, Replay, Victory
}
