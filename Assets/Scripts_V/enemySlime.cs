using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyAi;

public enum enemySlimeAnimationState
{
    Idle, Walk, Jump, Attack, Damage
}

public class enemySlime : MonoBehaviour
{
    public Face faces;
    public GameObject SmileBody;
    public enemySlimeAnimationState enemyCurrentState; 

    bool inCombat;
    bool dead; //flag to determine when this game object is destroyed
    private Material faceMaterial;
    public Animator animator;
    public GameObject enemy;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        SetFace(faces.WalkFace);
        enemyCurrentState = enemySlimeAnimationState.Walk;
        enemy = GameObject.Find("EnemyCharacter");
        player = GameObject.Find("PlayerCharacter");

    }

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    // Update is called once per frame
    void Update()
    {
        inCombat = enemy.GetComponent<Enemy>().GetInCombat();

        switch (enemyCurrentState)
        {
            /* case enemySlimeAnimationState.Idle:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                SetFace(faces.Idleface);
                break; */

            case enemySlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;
                SetFace(faces.WalkFace);
                break;


            case enemySlimeAnimationState.Jump:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");

                //Debug.Log("Jumping");
                break; 

            case enemySlimeAnimationState.Attack:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");

                // Debug.Log("Attacking");

                break;

            case enemySlimeAnimationState.Damage:

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
        if (player.GetComponent<MainPlayer>().isDead)
        {
            enemyCurrentState = enemySlimeAnimationState.Jump;
        }

        if (inCombat)
        {
            enemyCurrentState = enemySlimeAnimationState.Attack;
        }
        if (dead)
        {
            enemyCurrentState = enemySlimeAnimationState.Damage;
        }
        else
        {
            enemyCurrentState = enemySlimeAnimationState.Walk;
        }


    }

    public void AlertObservers(string message)
    {

        if (message.Equals("AnimationDamageEnded"))
        {
            //Debug.Log("DamageAnimationEnded");
            Destroy(gameObject);

        }

        /* if (message.Equals("AnimationAttackEnded"))
        {
            enemyCurrentState = enemySlimeAnimationState.Walk;
        } */

        /* if (message.Equals("AnimationJumpEnded"))
        {
            enemyCurrentState = enemySlimeAnimationState.Walk;
        } */
    }
}
