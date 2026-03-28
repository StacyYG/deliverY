using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWord : MonoBehaviour
{
    public LetterStatus currentLetterStatus;
    Letter _letter;
    bool isLocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _letter = GetComponent<Letter>();
        
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

        bool isLockWord= (_letter.horizontalWord == "LOCK" || _letter.verticalWord == "LOCK");
        if (isLockWord && !isLocked)
        {
            _letter.block.moveable = false;
            _letter.GetComponent<SpriteRenderer>().color = Color.gray;
            isLocked = true;
        }
        else if (!isLockWord && isLocked) 
        {
            _letter.block.moveable = true;
            _letter.GetComponent<SpriteRenderer>().color = Color.white;
            isLocked = false;
        }


        if (_letter.horizontalWord == "KEY" || _letter.verticalWord == "KEY")
        {

            return;
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

