using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    private GameAudio() { }
    private static GameAudio instance;

    public static GameAudio Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameAudio>();
            }
            return instance;
        }
    }


    public AudioSource correct;
    public AudioSource wrong;

    public void Correct()
    {
        correct.Play();
    }
    public void Wrong()
    {
        wrong.Play();
    }
}
