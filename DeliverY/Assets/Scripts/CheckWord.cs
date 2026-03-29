using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWord : MonoBehaviour
{
    public LetterStatus currentLetterStatus;
    Letter _letter;
    private SpriteRenderer _sr;
    
    // Start is called before the first frame update
    void Start()
    {
        _letter = GetComponent<Letter>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (_letter.horizontalWord=="PLAY" || _letter.verticalWord=="PLAY")
            {
                Services.LevelStatus = LevelStatus.StartFirstLevel;
                return;
            }
        }

        if (_letter.horizontalWord=="VICTORY" || _letter.verticalWord=="VICTORY")
        {
            Services.LevelStatus = LevelStatus.Victory;
            return;
        }
        
        if (_letter.horizontalWord=="REPLAY" || _letter.verticalWord=="REPLAY")
        {
            Services.LevelStatus = LevelStatus.Replay;
            return;
        }
        
        if (_letter.horizontalWord=="YOU" || _letter.verticalWord=="YOU")
        {
            // DO SOMETHING ELSE HERE
            GameObject.Find("BG").GetComponent<SpriteRenderer>().color =  Color.Lerp(Color.white, new Color(0.5f,0.9f,1f,1f), Mathf.PingPong(Time.time, 1));
            return;
        }

        bool isKeyWord = (_letter.horizontalWord == "KEY" || _letter.verticalWord == "KEY");
        if (isKeyWord && _letter.status!=LetterStatus.Key)
        {
            _sr.color = Color.yellow;
            _letter.block.moveable = true;
            _letter.status = LetterStatus.Key;
        }
        else if (!isKeyWord && _letter.status == LetterStatus.Key) 
        { 
            _sr.color = Color.white;
            _letter.status = LetterStatus.NA;
        }

        bool isLockWord= (_letter.horizontalWord == "LOCK" || _letter.verticalWord == "LOCK");

        bool canLock()
        {
            if(!isLockWord) return false;
            foreach (Letter l in _letter.vertical)
            {
                if (l.status != LetterStatus.NA && l.status!=LetterStatus.Lock)
                    return false;
            }
            foreach (Letter l in _letter.horizontal)
            {
                if (l.status != LetterStatus.NA && l.status != LetterStatus.Lock)
                    return false;
            }
            return true;
        }


        if (canLock() && _letter.status==LetterStatus.NA)
        {
            _letter.block.moveable = false;
            _sr.color = Color.gray;
            _letter.status = LetterStatus.Lock;
        }
        else if (!canLock() && _letter.status==LetterStatus.Lock) 
        {
            _letter.block.moveable = true;
            _sr.color = Color.white;
            _letter.status = LetterStatus.NA;
        }



        //if (_letter.horizontalWord == "FLY" || _letter.verticalWord == "FLY")
        //{
        //    currentWordStatus = WordStatus.Flying;
        //    Letter lastLetter = _letter.GetLetterAt(transform.position + 4 * Vector3.left);
        //    if (lastLetter)
        //    {
        //        lettersToFly = new List<Letter>();
        //        lettersToFly.Add(lastLetter);
        //        lastLetter.GetLeftLetter(lettersToFly);

        //        foreach (var letter in lettersToFly)
        //        {
        //            letter.isFly = true;
        //        }
        //    }
        //}
        // else
        // {
        //     currentWordStatus = WordStatus.Nothing;
        //     lettersToFly = new List<Letter>();
        //     
        // }


    }
    
}

public class WordManager
{
    
}

