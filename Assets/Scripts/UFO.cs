using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    internal bool moveOnRight;
    [SerializeField] 
    private float speed = 5f;
    [SerializeField] 
    private float time = 5f;
    [SerializeField] 
    private int score = 50;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Destroy), time);

    }

    // Update is called once per frame
    void Update()
    {
        var shift = (moveOnRight ? speed : -speed);
        transform.Translate(shift * Time.deltaTime, 0, 0);
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("HeroBullet"))
        {
            var obj = GameObject.FindGameObjectWithTag("Game");
            obj.GetComponent<Game>().UpdateScore(score);
            Destroy();
        }
    }
}
