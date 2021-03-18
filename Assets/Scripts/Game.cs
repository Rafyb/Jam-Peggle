using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public ScoreBoard scoreBoard;
    [HideInInspector] public TypeBrick active;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        active = TypeBrick.White;
    }

    // Update is called once per frame
    void Update()
    {
        
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
