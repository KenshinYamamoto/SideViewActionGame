using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObserver : MonoBehaviour
{
    [SerializeField] GameObject[] effectPrefabs;

    public static EffectObserver effectObserver;
    private void Awake()
    {
        effectObserver = this;
    }

    public enum Effect
    {
        EnemyDown,
        GetItem,
        Max
    };

    public void GenerateEffect(Transform trans,Effect effect)
    {
        int index = (int)effect;
        Instantiate(effectPrefabs[index], trans.position, trans.rotation);
    }
}
