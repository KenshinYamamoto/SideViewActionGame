using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject
{
    [Header("���i�̃v���C���[�̃W�����v�����(�f�t�H���g:400f)")]
    public float playerJumpPower;

    [Header("�v���C���[�����񂾂Ƃ��̃W�����v�����(�f�t�H���g:400f)")]
    public float playerDeadJumpPower;

    [Header("�v���C���[���G�𓥂񂾎��̃W�����v�����(�f�t�H���g:400f)")]
    public float playerEnemyDownJumpPower;

    [Header("�v���C���[�̈ړ����x(�f�t�H���g:3f)")]
    public float playerSpeed;

    [Header("�G�̈ړ����x(�f�t�H���g:3f)")]
    public float enemySpeed;

    [Header("�A�C�e������������ɉ��Z����链�_(�f�t�H���g:100)")]
    public int getItemPoint;

    [Header("�G��|�����Ƃ��ɉ��Z����链�_(�f�t�H���g:500)")]
    public int enemyDownPoint;

    //MyScriptableObject���ۑ����Ă���ꏊ�̃p�X
    public const string PATH = "ParamsSO";

    //MyScriptableObject�̎���
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //���A�N�Z�X���Ƀ��[�h����
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
