using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public void GetItem()
    {
        EffectObserver.effectObserver.GenerateEffect(transform, EffectObserver.Effect.GetItem);
        SoundObserver.soundObserver.PlaySE(SoundObserver.SE.GetItem);
        GameObserver.gameObserver.AddScore(ParamsSO.Entity.getItemPoint);
        Destroy(gameObject);
    }
}
