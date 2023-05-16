using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string word1, word2;

    public bool isFly;

    private CheckWord _checkWord;
    
    // Start is called before the first frame update
    void Start()
    {
        word1 = word2 = gameObject.name;
        MakeWord();
    }

    // Update is called once per frame
    void Update()
    {
        MakeWord();
        if (GameManager.overallCurrentWordStatus != WordStatus.Flying)
        {
            isFly = false;
        }
    }
    void MakeWord()
    {
        var left = GetLetterAt(transform.position + Vector3.left);
        var up = GetLetterAt(transform.position + Vector3.up);
        if (left)
        {
            word1 = left.word1 + gameObject.name;
        }
        else
        {
            word1 = gameObject.name;
        }

        if (up)
        {
            word2 = up.word2 + gameObject.name;
        }
        else
        {
            word2 = gameObject.name;
        }

    }
    public Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position, .3f, Vector2.zero);

        return hit.collider;
    }
    public IEnumerator UpdateWord()
    {
        yield return new WaitForFixedUpdate();
        MakeWord();
    }

    public Letter GetLetterAt(Vector3 position)
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

    public Letter GetLeftLetter(List<Letter> letters)
    {
        Letter leftLetter = GetLetterAt(transform.position + Vector3.left);
        if (leftLetter)
        {
            letters.Add(leftLetter);
            leftLetter.GetLeftLetter(letters);
        }

        return leftLetter;
    }
}
