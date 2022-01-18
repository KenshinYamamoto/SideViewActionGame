using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObserver : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceSE;
    [SerializeField] AudioClip[] audioClips;

    public enum SE
    {
        GetItem,
        Jump,
        EnemyDown,
        GameOver,
        GameClear,
        Max
    };
    
    public static SoundObserver soundObserver;
    private void Awake()
    {
        if(soundObserver == null)
        {
            soundObserver = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySE(SE se)
    {
        int index = (int)se;
        audioSourceSE.PlayOneShot(audioClips[index]);
    }
}
