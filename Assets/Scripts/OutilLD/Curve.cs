using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector2 gizmosPosition;
    private List<Vector2> positions;

    public GameObject brickPrefab;

    [Header("Taux ( entre 0 et 1")]
    public float black = 0.5f;
    public float white = 0.5f;

    private void OnDrawGizmos()
    {
        positions = new List<Vector2>();

        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
            positions.Add(gizmosPosition);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

    }

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();

        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
            positions.Add(gizmosPosition);
        }

        foreach (Vector2 pos in positions)
        {
            GameObject go = Instantiate(brickPrefab, new Vector3(pos.x, pos.y, 0f),Quaternion.identity);
            float rnd = Random.Range(0f, 1f);
            if (rnd >= black) 
            {
                go.GetComponent<Bricks>().type = TypeBrick.Black;
            }
            else 
            { 
                go.GetComponent<Bricks>().type = TypeBrick.White;
            }
        }
    }
}
