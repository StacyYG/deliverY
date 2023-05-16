using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWord : MonoBehaviour
{
    Letter _letter;
    public List<Letter> lettersToFly;
    public WordStatus currentWordStatus;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _letter = GetComponent<Letter>();
        lettersToFly = new List<Letter>();
        currentWordStatus = WordStatus.Nothing;

    }

    // Update is called once per frame
    void Update()
    {
        if (_letter.word1=="VICTORY" || _letter.word2=="VICTORY")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        if (_letter.word1=="REPLAY" || _letter.word2=="REPLAY")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (_letter.word1=="KEY" || _letter.word2=="KEY")
        {
            
            return;
        }
        // if (_letter.word1 == "FLY")
        // {
        //     currentWordStatus = WordStatus.Flying;
        //     Letter lastLetter = _letter.GetLetterAt(transform.position + 4 * Vector3.left);
        //     if (lastLetter)
        //     {
        //         lettersToFly = new List<Letter>();
        //         lettersToFly.Add(lastLetter);
        //         lastLetter.GetLeftLetter(lettersToFly);
        //         
        //         foreach (var letter in lettersToFly)
        //         {
        //             letter.isFly = true;
        //         }
        //     }
        // }
        // else
        // {
        //     currentWordStatus = WordStatus.Nothing;
        //     lettersToFly = new List<Letter>();
        //     
        // }
        
        if (_letter.word1=="PLAY" || _letter.word2=="PLAY")
        {
            SceneManager.LoadScene(1);
            return;
        }

        if (_letter.word1=="YOU" || _letter.word2=="YOU")
        {
            GameObject.Find("BG").GetComponent<SpriteRenderer>().color =  Color.Lerp(Color.white, new Color(0.5f,0.9f,1f,1f), Mathf.PingPong(Time.time, 1));
        }
    }
    
}

public enum WordStatus
{
    Nothing, Flying
}
