﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public ScoreBoard scoreBoard;
    [HideInInspector] public TypeBrick active;

	public GameObject ballPrefabB;
	public GameObject ballPrefabW;
	public GameObject trampolino;
	public GameObject leBRAS;
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

	public int nbNoir = 1;
	public int nbBlanc = 1;

	private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
		scoreBoard.addBall += AddBall;
		scoreBoard.UpdateUI();

        active = TypeBrick.White;

		TypeBrick baseC = active;

		for (int i = 0; i < nbBall; i++)
        {
			Switch();
			GameObject go;
			if (active == TypeBrick.White) go = Instantiate(ballPrefabW, new Vector3(listBall.position.x, listBall.position.y + i , listBall.position.z), Quaternion.identity);
			else go = Instantiate(ballPrefabB, new Vector3(listBall.position.x, listBall.position.y + i , listBall.position.z), Quaternion.identity);
			balls.Add(go);
        }

		if (active != baseC) Switch();

		OnDestroyBall();


	}

	public void AddBall()
    {
		GameObject go;
		if (active == TypeBrick.White) go = Instantiate(ballPrefabW, new Vector3(listBall.position.x, listBall.position.y, 0f), Quaternion.identity);
		else go = Instantiate(ballPrefabB, new Vector3(listBall.position.x, listBall.position.y, 0f), Quaternion.identity);
		balls.Add(go);
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
			mousePosition = r.GetPoint(distance);
			if (Input.GetMouseButtonDown(0) && theRb.isKinematic == true)
			{
				scoreBoard.ActiveBras(false);
				direction = mousePosition - transform.position;
				theRb.isKinematic = false;
				theRb.AddForce((direction.normalized * force), ForceMode2D.Impulse);
				//Debug.Log(direction);
			}
		}

		Trampoline();

		//TEST de rotation du bras mais pas concluant :(

		/*Vector3 temp = transform.transform.eulerAngles;
		temp.z = Mathf.Atan2(mousePosition.normalized.y, mousePosition.normalized.x);
		leBRAS.transform.rotation = Quaternion.Euler(temp);*/

		leBRAS.transform.Rotate(new Vector3(0,0,Mathf.Atan2(mousePosition.normalized.x, mousePosition.normalized.y)));
		//Debug.Log(Mathf.Atan2(mousePosition.normalized.x, mousePosition.normalized.y));
	}

    void OnDestroyBrick(TypeBrick type, EffectBrick effect)
    {
		if (type == TypeBrick.White) nbBlanc--;
		if (type == TypeBrick.Black) nbNoir--;
		if (nbNoir == 0 || nbBlanc == 0) Fini(true);
	}

	public void Fini(bool win)
    {
		if (win)
		{
			if(SceneManager.GetActiveScene().buildIndex == 6) SceneManager.LoadScene(0);
			else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

		}
		else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	void OnDestroyBall()
	{
		if(balls.Count > 0)
        {
			scoreBoard.ActiveBras(true);
			InstantiateBall();
			GameObject ball = balls[0];
			balls.RemoveAt(0);
			ball.GetComponent<Ball>().Destroy();
		} else
        {
			if (nbNoir == 0 || nbBlanc == 0) Fini(true);
			else Fini(false);
		}
		
	}

	public void Boing()
    {
		Debug.Log("boing");
		scoreBoard.AddJauge3(1);
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
