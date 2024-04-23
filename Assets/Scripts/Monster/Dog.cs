using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dog : MonoBehaviour
{
    //[SerializeField]private TimeState _state;//显示当前怪物处于白天还是黑夜
    [SerializeField] private float _speed;//怪物的移动速度

    public Transform player;

    [Header("Pathfinding")]
    private Seeker _seeker;
    private List<Vector3> _pathPoints;//路径点
    private int _curIndex;//路径点的索引
    [SerializeField]private float pathFindCooldown;//搜寻路径的冷却
    private float pathTimer = 0;//计时器

    private void Awake()
    {
        _seeker=GetComponent<Seeker>();
    }

    private void AutoPath()
    {
        pathTimer += Time.deltaTime;
        if(pathTimer > pathFindCooldown) 
        {
            GetPathPoints(player.position);
            pathTimer = 0;
        }
        if (_pathPoints == null||_pathPoints.Count<0)
        {
            GetPathPoints(player.position);
        }
        else if (Vector3.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
        {
            _curIndex++;
            if( _curIndex >= _pathPoints.Count )
            {
                GetPathPoints(player.position);
            }
        }
    }
    private void GetPathPoints(Vector3 target)
    {
        _curIndex = 0;

        _seeker.StartPath(transform.position, target, Path =>
        {
            _pathPoints = Path.vectorPath;
        });
    }

}
