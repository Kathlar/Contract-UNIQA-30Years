using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerPoints points { get; private set; }
    public Animator animator { get; private set; }

    public float moveSpeed = 5;
    
    protected float horizontalValue;
    protected bool spaceButton;

    protected bool started;
    protected bool lost = false;

    protected virtual void Awake()
    {
        points = GetComponent<PlayerPoints>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollectInput();
    }

    private void CollectInput()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        spaceButton = Input.GetKeyDown(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        started = true;
    }

    public virtual void Lose()
    {
        if (lost) return;
        lost = true;
        GameManager.SaveScore(points.numberOfPoints);
        GameManager.Lose();
        if(animator)
        {
            animator.SetInteger("Action", 2);
            animator.SetTrigger("GetHitTrigger");
            animator.SetBool("Stunned", true);
        }
        GetComponent<AudioSource>().Play();
    }
}
