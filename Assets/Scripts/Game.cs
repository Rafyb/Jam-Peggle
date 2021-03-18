using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public ScoreBoard scoreBoard;
    [HideInInspector] public TypeBrick active;

	public GameObject ball;
	public GameObject trampolino;
	GameObject ballPlayer;
	Rigidbody2D theRb;

	public float force;
	public float trampoSpeed;

	Vector3 mousePosition;
	Vector3 direction;
	Vector3 trampoDir;

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
		trampoDir = Vector3.left;
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
		Trampoline();
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

	public void Trampoline()
	{
		trampolino.transform.position += trampoDir * trampoSpeed * Time.deltaTime;
		if (trampolino.transform.position.x < -5.5f || trampolino.transform.position.x > 5.5f)
		{
			trampoDir = -trampoDir;
		}
		
	}
}
