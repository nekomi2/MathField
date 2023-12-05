using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomSlimeAnimationState
{
    Idle, Walk, Jump, Attack, Damage
}

public class playerSlime : MonoBehaviour
{
    public Face faces;
    public GameObject SmileBody;
    public CustomSlimeAnimationState currentState;

    internal bool inCombat;
    internal bool dead; //flag to determine when this game object is destroyed
    private Material faceMaterial;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        // Debug.Log(faceMaterial.ToString());
        SetFace(faces.Idleface);
        currentState = CustomSlimeAnimationState.Idle;

    }

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CustomSlimeAnimationState.Idle:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                SetFace(faces.Idleface);
                break;

            case CustomSlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;
                SetFace(faces.WalkFace);
                break;


            case CustomSlimeAnimationState.Jump:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");

                //Debug.Log("Jumping");
                break;

            case CustomSlimeAnimationState.Attack:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");

                // Debug.Log("Attacking");

                break;
            case CustomSlimeAnimationState.Damage:

                // Do nothing when animtion is playing
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                     || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return;

                animator.SetTrigger("Damage");
                animator.SetInteger("DamageType", 2);
                SetFace(faces.damageFace);

                //Debug.Log("Take Damage");
                break;

        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("Speed", 1.0f);
            currentState = CustomSlimeAnimationState.Walk;
            // Debug.Log("Pressed w");
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("Speed", 1.0f);
            currentState = CustomSlimeAnimationState.Walk;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, -0.5f, 0.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, 0.5f, 0.0f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            currentState = CustomSlimeAnimationState.Jump;
            // Debug.Log("Pressed space");
        }
        if (!Input.anyKey)
        {
            currentState = CustomSlimeAnimationState.Idle;
            animator.SetFloat("Speed", 0.0f);
        }
        if (inCombat)
        {
            currentState = CustomSlimeAnimationState.Attack;
        }
        if (dead)
        {
            currentState = CustomSlimeAnimationState.Damage;
        }
    }

    // Animation Event
    public void AlertObservers(string message)
    {

        if (message.Equals("AnimationDamageEnded"))
        {
            Debug.Log("DamageAnimationEnded");
            Destroy(gameObject);

        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = CustomSlimeAnimationState.Idle;
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = CustomSlimeAnimationState.Idle;
        }
    }
}
