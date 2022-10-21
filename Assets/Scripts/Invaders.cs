using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [SerializeField]
    private GameObject invader;
    [SerializeField]
    private GameObject bullet;
    [SerializeField] 
    private float muzzle = -1f;
    [SerializeField] 
    private Vector3 startPosition;
    [SerializeField] 
    private int columns = 8;
    [SerializeField] 
    private int rows = 3;
    [SerializeField] 
    private int step = 10;
    [SerializeField] 
    private float interval = 10;
    [SerializeField] 
    private float speed = 10;
    
    [SerializeField]
    public Sprite invader1;
    [SerializeField]
    public Sprite invader2;
    [SerializeField]
    public Sprite invader3;

    [SerializeField]
    public float score1 = 30;
    [SerializeField]
    public float score2 = 20;
    [SerializeField]
    public float score3 = 10;

    private float dist = 0f;
    
    private GameObject[,] _invaders;
    private bool[,] _isDestroy;
    private int[] _lastInvaders;
    private float[] _fireTime;
    private float[] _timers;

    private bool moveOnRight = true;
    
    [SerializeField] 
    private int minTime = 1;
    [SerializeField] 
    private int maxTime = 5;

    private int killed = 0;
    
    [SerializeField] 
    private int yLimit = 0;
    
    [SerializeField] 
    private float timeInc = 0.5f;
    

    

    
    // Start is called before the first frame update
    void Start()
    {
        _invaders = new GameObject[rows, columns];
        _isDestroy = new bool[rows, columns];
        _lastInvaders = new int[columns];
        _fireTime = new float[columns];
        _timers = new float[columns];
        for (int j = 0; j != columns; ++j)
        {
            _lastInvaders[j] = rows - 1;
            _timers[j] = 0f;
            _fireTime[j] = Random.Range(minTime, maxTime);
            for (int i = 0; i != rows; ++i)
            {
                _isDestroy[i, j] = false;
                var inv = Instantiate(invader, transform.position + new Vector3(j * interval, -i * interval, 0), Quaternion.identity);
                inv.transform.SetParent(transform);
                var spriteRenderer = inv.AddComponent<SpriteRenderer>();
                var comp = inv.GetComponent<Invader>();
                
                if (i < rows / 3)
                {
                    spriteRenderer.sprite = invader1;
                    comp.score = score1;
                }
                else if (i < 2 * rows / 3)
                {
                    spriteRenderer.sprite = invader2;
                    comp.score = score2;
                }
                else
                {
                    spriteRenderer.sprite = invader3;
                    comp.score = score3;
                }
                
                
                comp.column = j;
                comp.row = i;
                
                _invaders[i, j] = inv;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dist > step)
        {
            dist = 0f;
            moveOnRight = !moveOnRight;
            transform.Translate(0, -interval, 0);
            speed += timeInc;
        }
        else
        {
            float shift = speed * Time.deltaTime;
            dist += shift;
            shift = moveOnRight ? shift : -shift;
            transform.Translate(shift, 0, 0);
        }
        
        for (int j = 0; j != columns; ++j)
        {
            if (_lastInvaders[j] == -1)
            {
                continue;
            }

            if (_invaders[_lastInvaders[j], j].transform.position.y < yLimit)
            {
                var obj = GameObject.FindGameObjectWithTag("Game");
                obj.GetComponent<Game>().TriggerGameOver();
            }
            _timers[j] += Time.deltaTime;
            if (_timers[j] >= _fireTime[j])
            {
                Fire(_lastInvaders[j], j);
                _timers[j] = 0f;
                _fireTime[j] = Random.Range(minTime, maxTime);
                
            }
        }
        
    }

    void Fire(int i, int j)
    {
        Instantiate(bullet, _invaders[i,j].transform.position + new Vector3(0, muzzle, 0), Quaternion.identity);
    }

    internal void DestroyInvader(int row, int column)
    {
        ++killed;
        _isDestroy[row, column] = true;
        while (_lastInvaders[column] >= 0 && _isDestroy[_lastInvaders[column], column])
        {
            --_lastInvaders[column];
        }

        if (killed == rows * columns)
        {
            var obj = GameObject.FindGameObjectWithTag("Game");
            obj.GetComponent<Game>().TriggerGameOver(false);
        }
        
    }
}
