using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public ScoreBoard scoreBoard;
    [HideInInspector] public TypeBrick active;

	public GameObject ball;
	GameObject ballPlayer;
	Rigidbody2D theRb;

	public float force;

	Vector3 mousePosition;
	Vector3 direction;

	private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        active = TypeBrick.White;
		ballPlayer = Instantiate(ball, this.transform.position, Quaternion.identity);
		theRb = ballPlayer.GetComponent<Rigidbody2D>();
		theRb.isKinematic = true;
	}

    // Update is called once per frame
    void Update()
    {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


		if (Input.GetMouseButtonDown(0))
		{
			direction = transform.position - mousePosition;
			direction.z = 0.0f;
			theRb.isKinematic = false;
			theRb.AddForce(-(direction.normalized * force), ForceMode2D.Impulse);
			Debug.Log(direction);
		}
	}

    void OnDestroyBrick(EffectBrick effect)
    {

    }

    public void Switch()
    {
        if(active == TypeBrick.White)
        {
            active = TypeBrick.Black;
        }
        else
        {
            active = TypeBrick.White;
        }
    }
}
