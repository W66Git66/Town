using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="monsterData",menuName = "ScriptableObject/newMonsterData")]
public class MonsterDataSO : ScriptableObject
{

    public float speed;//怪物的移动速度 

    public Sprite daySprite;//怪物白天的形象
    public Sprite nightSprite;//怪物晚上的形象

    public AnimationClip anim;//怪物的动画状态机
}
