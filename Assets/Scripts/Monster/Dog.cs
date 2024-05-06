using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dog : MonoBehaviour
{
    [SerializeField] private float _speed;//������ƶ��ٶ�

    private EnemyStates curState;

    [Header("Ŀ��")]
    public Transform player;

    [Header("�ƶ�׷��")]
    public float currentSpeed = 0;
    public Vector3 direction { get; private set; }

    public float chaseDistance = 3f;//׷������
    public float attackDistance = 0.8f;//��������

    [Header("Pathfinding")]
    private Seeker _seeker;
    [SerializeField]private List<Vector3> _pathPoints;//·����
    private int _curIndex;//·���������
    [SerializeField]private float pathFindCooldown=0.5f;//��Ѱ·������ȴ
    private float pathTimer = 0;//��ʱ��

    [Header("����")]
    public float meleeAttackDamage;//��ս�����˺�
    public bool isAttack = true;
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//��ʾ���ͼ��
    public float AttackCooldownDuration = 2f;//��ȴʱ��

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;

    Dictionary<EnemyStates, IState> states = new Dictionary<EnemyStates, IState>();
    private void Awake()
    {
        _seeker=GetComponent<Seeker>();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//��ײ�����
    }

    private void Start()
    {
        TransState(EnemyStates.Chase);
    }

    private void Update()
    {
        if(player==null)
        {
            return;
        }
        float distance = Vector2.Distance(player.position, transform.position);
        AutoPath();
        Debug.Log(_curIndex);
        direction = _pathPoints[_curIndex] - transform.position;
        StateMachine();
    }

    private void FixedUpdate()
    {
        if(curState==EnemyStates.Chase)
        {
            Move(direction);
        }
    }

    private void StateMachine()
    {
        switch(curState)
        {
            case EnemyStates.Idle :
                break;
            case EnemyStates.Chase:
                break;
            case EnemyStates.Attack:
                break;
            case EnemyStates.Patrol:
                break;
            case EnemyStates.Death:
                break;
        }    
    }
        
    public void GetPlayerTransform()
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);

        if (chaseColliders.Length > 0)//�����׷����Χ��
        {
            player = chaseColliders[0].transform;//��ȡ��ҵ�Transform
            distance = Vector2.Distance(player.position, transform.position);
        }
        else
        {
            player = null;//�����׷����Χ��
        }
    }
    #region �Զ�Ѱ·
    private void AutoPath()
    {

        pathTimer += Time.deltaTime;
        if(pathTimer >= pathFindCooldown) 
        {
            GetPathPoints(player.position);
            pathTimer = 0;
        }
        if (_pathPoints == null||_pathPoints.Count<=0||_pathPoints.Count <= _curIndex)
        {
            GetPathPoints(player.position);
        }
        else if(_curIndex<_pathPoints.Count)
        {
            if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
            {

                _curIndex++;
                if( _curIndex >= _pathPoints.Count )
                {
                GetPathPoints(player.position);
                }
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

    #endregion

    #region �ƶ�
    public void Move(Vector2 MovementInput)
    {
        if (MovementInput.magnitude > 0.1f && currentSpeed >= 0)
        {            
           
            rb.velocity = MovementInput.normalized * currentSpeed;
            //�������ҷ�ת
            if (MovementInput.x < 0)//��
            {
                sr.flipX = false;
            }
            if (MovementInput.x > 0)//��
            {
                sr.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    #endregion

    private void TransState(EnemyStates states)
    {
        curState = states;
    }
}
