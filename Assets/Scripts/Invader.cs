using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int row;
    public int column;
    public float score;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Destroy()
    {
        var obj = GameObject.FindGameObjectWithTag("Invaders");
        obj.GetComponent<Invaders>().DestroyInvader(row, column);
        
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
