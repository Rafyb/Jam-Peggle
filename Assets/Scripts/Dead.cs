using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball;
        if (collision.gameObject.TryGetComponent<Ball>(out ball))
        {
            ball.onDestroyBall?.Invoke();
            ball.Destroy();
        }
    }
}
