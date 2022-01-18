using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void GetItem()
    {
        SoundObserver.soundObserver.PlaySE(SoundObserver.SE.GetItem);
        GameObserver.gameObserver.AddScore(100);
        Destroy(gameObject);
    }
}
