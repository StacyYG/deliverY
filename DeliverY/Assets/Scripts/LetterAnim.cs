using UnityEngine;

public class LetterAnim : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 6f;

    private SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.time * fps) % frames.Length;
        sr.sprite = frames[index];
    }
}
