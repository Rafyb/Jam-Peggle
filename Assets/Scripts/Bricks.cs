using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBrick
{
    Black, White, Orange
}


public class Bricks : MonoBehaviour
{
    public TypeBrick type;

    private void OnCollisionEnter(Collision col)
    {
        Ball ball;
        if (col.gameObject.TryGetComponent<Ball>(out ball))
        {
            ball.onDestroyBrick?.Invoke(type);
        }
        
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }


}
