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

[SelectionBase]
public class Bricks : MonoBehaviour
{
    public TypeBrick type;
    public EffectBrick effect;

    private void OnCollisionExit2D(Collision2D col)
    {
        if (Game.Instance.active != type && type != TypeBrick.Grey) return;
        Ball ball;
        if (col.gameObject.TryGetComponent<Ball>(out ball))
        {
            ball.onDestroyBrick?.Invoke(type,effect);
            Destroy();
        }
        //
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }


}
