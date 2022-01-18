using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("普段のプレイヤーのジャンプする力(デフォルト:400f)")]
    public float playerJumpPower;

    [Header("プレイヤーが死んだときのジャンプする力(デフォルト:400f)")]
    public float playerDeadJumpPower;

    [Header("プレイヤーが敵を踏んだ時のジャンプする力(デフォルト:400f)")]
    public float playerEnemyDownJumpPower;

    [Header("プレイヤーの移動速度(デフォルト:3f)")]
    public float playerSpeed;

    [Header("敵の移動速度(デフォルト:3f)")]
    public float enemySpeed;

    [Header("アイテムを取った時に加算される得点(デフォルト:100)")]
    public int getItemPoint;

    [Header("敵を倒したときに加算される得点(デフォルト:500)")]
    public int enemyDownPoint;

    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
