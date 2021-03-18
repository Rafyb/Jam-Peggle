using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public ScoreBoard scoreBoard;
    [HideInInspector] public TypeBrick active;

	public GameObject ballPrefabB;
	public GameObject ballPrefabW;
	public GameObject trampolino;
	public Canvas MyCanvas;
	GameObject ballPlayer;
	Rigidbody2D theRb;

	public float force;
	public float trampoSpeed;
	[Range(0,6)]
	public float trampoRange;

	Vector3 mousePosition;
	Vector3 direction;
	Vector3 trampoDir;

	public int nbBall = 5;
	private List<GameObject> balls = new List<GameObject>();
	public Transform listBall;

	private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        active = TypeBrick.White;

		TypeBrick baseC = active;

		for (int i = 0; i < nbBall; i++)
        {
			Switch();
			GameObject go;
			if (active == TypeBrick.White) go = Instantiate(ballPrefabW, new Vector3(listBall.position.x, listBall.position.y + i , 0f), Quaternion.identity);
			else go = Instantiate(ballPrefabB, new Vector3(listBall.position.x, listBall.position.y + i , 0f), Quaternion.identity);
			balls.Add(go);
        }

		if (active != baseC) Switch();

		OnDestroyBall();
	}

	void InstantiateBall()
    {
		Switch();
		if(active == TypeBrick.White)	ballPlayer = Instantiate(ballPrefabW, this.transform.position, Quaternion.identity);
		if (active == TypeBrick.Black) ballPlayer = Instantiate(ballPrefabB, this.transform.position, Quaternion.identity);

		Ball b = ballPlayer.GetComponent<Ball>();
		b.onDestroyBall += OnDestroyBall;
		b.onDestroyBrick += OnDestroyBrick;
		theRb = ballPlayer.GetComponent<Rigidbody2D>();
		theRb.isKinematic = true;
		trampoDir = Vector3.left;
		
	}

    // Update is called once per frame
    void Update()
    {
		Plane p = new Plane(new Vector3(0, 0, 1), Vector3.zero);
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if(p.Raycast(r, out distance))
		{
			//Debug.Log(r.GetPoint(distance));
			Vector3 position = r.GetPoint(distance);
			if (Input.GetMouseButtonDown(0) && theRb.isKinematic == true)
			{
				direction = position - transform.position;
				theRb.isKinematic = false;
				theRb.AddForce((direction.normalized * force), ForceMode2D.Impulse);
				//Debug.Log(direction);
			}
		}

		Trampoline();
	}

    void OnDestroyBrick(EffectBrick effect)
    {

    }

	void OnDestroyBall()
	{
		if(balls.Count > 0)
        {
			InstantiateBall();
			GameObject ball = balls[0];
			balls.RemoveAt(0);
			ball.GetComponent<Ball>().Destroy();
		}
		
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

	public void Trampoline()
	{
		trampolino.transform.position += trampoDir * trampoSpeed * Time.deltaTime;
		if (trampolino.transform.position.x < -trampoRange || trampolino.transform.position.x > trampoRange)
		{
			trampoDir = -trampoDir;
		}
		
	}
}
