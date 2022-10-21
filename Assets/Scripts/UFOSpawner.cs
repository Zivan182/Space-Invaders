using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    private float _spawnTime;
    private float _timer;
    
    [SerializeField] 
    private int minTime = 1;
    [SerializeField] 
    private int maxTime = 5;
    [SerializeField] 
    private GameObject UFO;
    [SerializeField] 
    private Vector3 leftPosition;
    [SerializeField] 
    private Vector3 rightPosition;

    private bool moveOnRight = true;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
        _spawnTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnTime)
        {
            _timer = 0f;
            _spawnTime = Random.Range(minTime, maxTime);
            moveOnRight = !this.moveOnRight;
            var startPosition = moveOnRight ? leftPosition : rightPosition;
            var obj = Instantiate(UFO, startPosition, Quaternion.identity);
            obj.GetComponent<UFO>().moveOnRight = moveOnRight;  
        }
    }
}
