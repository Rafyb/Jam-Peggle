using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public Action<TypeBrick,EffectBrick> onDestroyBrick;
    public Action onDestroyBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
