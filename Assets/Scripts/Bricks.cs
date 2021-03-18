using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBrick
{
    Black, White, Grey
}

public enum EffectBrick
{
    None, Bonus
}


public class Bricks : MonoBehaviour
{
    public TypeBrick type;
    public EffectBrick effect;

    private void OnCollisionEnter(Collision col)
    {
        if (Game.Instance.active != type && type != TypeBrick.Grey) return;

        Ball ball;
        if (col.gameObject.TryGetComponent<Ball>(out ball))
        {
            ball.onDestroyBrick?.Invoke(effect);
            Destroy();
        }
        //
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }


}
