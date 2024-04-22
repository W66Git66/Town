using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dog : MonoBehaviour
{
    [SerializeField]private TimeState _state;//��ʾ��ǰ���ﴦ�ڰ��컹�Ǻ�ҹ
    [SerializeField] private float _speed;//������ƶ��ٶ�

    public Transform player;

    [Header("Pathfinding")]
    private Seeker _seeker;
    private List<Vector3> _pathPoints;//·����
    private int _curIndex;//·���������
    [SerializeField]private float pathFindCooldown;//��Ѱ·������ȴ
    private float pathTimer = 0;//��ʱ��

    private void Awake()
    {
        _seeker=GetComponent<Seeker>();
    }

    private void AutoPath()
    {
        if (_pathPoints == null||_pathPoints.Count<0)
        {
            GetPathPoints(player.position);
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