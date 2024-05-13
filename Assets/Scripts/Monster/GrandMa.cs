using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GrandMa : MonoBehaviour
{
    [SerializeField] private float _speed;//������ƶ��ٶ�

    private EnemyStates curState;

    [Header("Ŀ��")]
    public Transform player;

    [Header("����Ѳ��")]
    public float IdleDuration = 2f; //����ʱ��
    public Transform[] patrolPoints;//Ѳ�ߵ�
    public int targetPointIndex = 0;//Ŀ�������

    [Header("�ƶ�׷��")]
    public float currentSpeed = 0;
    public Vector2 MovementInput { get; set; }

    public float chaseDistance = 10f;//׷������
    public float chaseTime=2f;


    [Header("Pathfinding")]
    private Seeker _seeker;
    private List<Vector3> _pathPoints;//·����
    private int _curIndex;//·���������
    [SerializeField] private float pathFindCooldown = 0.5f;//��Ѱ·������ȴ
    private float pathTimer = 0;//��ʱ��

    [Header("����")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//��ʾ���ͼ��

    private float Timer = 0;//������ʱ��

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;

    private float stopTime = 0f;//����ֹͣ�˶���ʱ��
    private float stopThreshold = 3f;//ֹͣ����ֵ

    private bool isPatrol = false;

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//��ײ�����
    }

    private void Start()
    {
        TransState(EnemyStates.Idle);
    }

    private void Update()
    {
        if (curState != EnemyStates.Death)
        {
            GetPlayerTransform();

            AutoPath();
        }

        StateMachine();
    }

    private void FixedUpdate()
    {
        if (curState == EnemyStates.Patrol)
        {
            Move(MovementInput);
        }
    }

    private void StateMachine()
    {
        switch (curState)
        {
            case EnemyStates.Idle:
                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("GrandMaJump")!=true)
                {
                     anim.Play("Idle");
                }
               
                MovementInput = Vector2.zero;
                rb.velocity = Vector2.zero;//����ʱ��Ҫ�ƶ�
                isPatrol = false;

                if (player != null)//�����Ҳ�Ϊ��
                {
                    Timer = 0;
                    TransState(EnemyStates.Chase);

                }
                else
                { //������Ϊ��,�ȴ�һ��ʱ���л���Ѳ��״̬
                    if (Timer <= IdleDuration)
                    {
                        Timer += Time.deltaTime;
                    }
                    else
                    {
                        Timer = 0;
                        TransState(EnemyStates.Patrol);
                    }
                }

                break;
            case EnemyStates.Chase:

               isPatrol = false;

                if (player == null)
                {
                    //��Χ���ֹͣ׷�����ص�����״̬
                    Timer = 0;
                    TransState(EnemyStates.Idle);           
                }
                else
                {
                    if(Timer<chaseTime)
                    {
                        Timer += Time.deltaTime;
                    }
                    else
                    {
                        Timer = 0;
                        Vector2 playerPosition = player.position;//��¼��ʱ��λ��
                        StartCoroutine(GrandMaJump(playerPosition));
                       
                       // transform.position = player.position;
                    }
                }

                break;
            case EnemyStates.Patrol:

                anim.Play("GrandMaMove");

                if (isPatrol == false)
                {
                    isPatrol = true;
                    GeneratePatrolPoint();
                }
                if (player != null)//�����Ҳ�Ϊ��
                {
                    TransState(EnemyStates.Chase);
                }

                //·�����б�Ϊ��ʱ������·������
                if (_pathPoints == null || _pathPoints.Count <= 0)
                {
                    //��������Ѳ�ߵ�
                    GeneratePatrolPoint();
                }
                else
                {
                    //�����˵��ﵱǰ·����ʱ����������currentIndex������·������
                    if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
                    {
                        _curIndex++;

                        //����Ѳ�ߵ�
                        if (_curIndex >= _pathPoints.Count)
                        {
                            Debug.Log("111");
                            TransState(EnemyStates.Idle);//�л�������״̬
                        }
                        else //δ����Ѳ�ߵ�
                        {
                            Vector2 direction = _pathPoints[_curIndex] - transform.position;
                            MovementInput = direction;  //�ƶ����򴫸�MovementInput
                        }
                    }
                    //else
                    //{//��ײ����

                    //    //���˸����ٶ�С�ڵ���Ĭ�ϵĵ�ǰ�ٶȣ����ҵ��˻�δ����Ѳ�ߵ�
                    //    if (rb.velocity.magnitude < currentSpeed && _curIndex < _pathPoints.Count)
                    //    {
                    //        if (rb.velocity.magnitude <= currentSpeed - 0.1f)
                    //        {
                    //            if (rb.velocity.magnitude <= 0.1f)//��������ٶ�С��0.1f,��Ѱ·��Χ��ĵ���
                    //            {
                    //                Vector2 direction = _pathPoints[_curIndex] - transform.position;
                    //                MovementInput = direction;  //�ƶ����򴫸�MovementInput
                    //            }
                    //            stopTime += Time.deltaTime;
                    //        }
                    //    }
                    //}

                    //ֹͣʱ�䵽
                    if (stopTime >= stopThreshold)
                    {
                        Debug.Log("�л�����״̬" + gameObject.name);
                        TransState(EnemyStates.Idle);//�л�Ϊ����״̬
                        stopTime = 0;
                    }

                }

                break;
            case EnemyStates.Death:

                anim.Play("null");
                MovementInput = Vector2.zero;
                rb.velocity = Vector2.zero;//����ʱ��Ҫ�ƶ�
                _pathPoints = null;
                isPatrol = false;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (curState != EnemyStates.Death && collision.CompareTag("���׷�"))
        {
            if (DataSaveManager.Instance.isDog == false)
            {
                DataSaveManager.Instance.isDog = true;
            }
            TransState(EnemyStates.Death);
        }
    }
    public void GetPlayerTransform()
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);

        if (chaseColliders.Length > 0)//�����׷����Χ��
        {
            curState = EnemyStates.Chase;
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
        if (player == null)
        {
            return;
        }

        pathTimer += Time.deltaTime;
        if (pathTimer >= pathFindCooldown)
        {
            GetPathPoints(player.position);
            pathTimer = 0;
        }
        if (_pathPoints == null || _pathPoints.Count <= 0 || _pathPoints.Count <= _curIndex)
        {
            GetPathPoints(player.position);
        }
        else if (_curIndex < _pathPoints.Count)
        {
            if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
            {

                _curIndex++;
                if (_curIndex >= _pathPoints.Count)
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
    public void Move(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0.1f && currentSpeed >= 0)
        {

            rb.velocity = movementInput.normalized * currentSpeed;
            //�������ҷ�ת
            if (Mathf.Abs(movementInput.x) < 1)
            {
                return;
            }
            if (movementInput.x > 0)//��
            {
                sr.flipX = false;
            }
            if (movementInput.x < 0)//��
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
        MovementInput = Vector2.zero;
    }

    public void GeneratePatrolPoint()
    {
        while (true)
        {
            //���ѡ��һ��Ѳ�ߵ�����
            int i = Random.Range(0, patrolPoints.Length);

            //�ų���ǰ����
            if (targetPointIndex != i)
            {
                targetPointIndex = i;
                break;//�˳���ѭ��
            }
        }

        //��Ѳ�ߵ������·���㺯��
        GetPathPoints(patrolPoints[targetPointIndex].position);

    }
    IEnumerator GrandMaJump(Vector2 position)
    {
        anim.Play("Idle");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("GrandMaJump");
        transform.position = position;
    }
}
