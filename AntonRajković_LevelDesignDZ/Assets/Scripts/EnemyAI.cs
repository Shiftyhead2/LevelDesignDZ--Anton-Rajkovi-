using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    NavMeshAgent myAgent;
    Animator myAnimator;
    [Header("Enemy Info")]
    public Transform Target;
    public float AttackTimeStart;
    float AttackTime;
    public float RunDistance;
    public float WalkSpeed;
    public float RunSpeed;
    Health healthScript;
    public float Damage;

    
    AudioSource MyAudioSource;
    [Header("Enemy Audio")]
    public float AudioResetStart;
    float AudioReset;
    public AudioClip[] ZombieGroans;
    public AudioClip[] ZombieAttack;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        MyAudioSource = GetComponent<AudioSource>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        if(Target != null)
        {
            healthScript = Target.GetComponent<Health>();
        }
        AttackTime = AttackTimeStart;
        AudioReset = AudioResetStart;
        myAgent.speed = WalkSpeed;
    }

    private void Update()
    {
        if (Target != null)
        {
            if (myAgent.enabled != false)
            {
                myAgent.SetDestination(Target.position);

                if (Vector3.Distance(this.gameObject.transform.position, Target.position) > myAgent.stoppingDistance)
                {
                    myAnimator.SetBool("IsWalking", true);
                }
                else if (Vector3.Distance(this.gameObject.transform.position, Target.position) <= myAgent.stoppingDistance)
                {
                    AttackTime -= Time.deltaTime;
                    myAnimator.SetBool("IsWalking", false);
                    if (AttackTime <= 0f)
                    {
                        //Debug.Log("Attacking!");
                        myAnimator.SetTrigger("Attack");
                        healthScript.TakeDamage(Damage);
                        AttackTime = AttackTimeStart;
                        MyAudioSource.clip = ZombieAttack[Random.Range(0, ZombieAttack.Length)];
                        MyAudioSource.Play();
                    }
                }

                if(Vector3.Distance(this.gameObject.transform.position, Target.position) <= RunDistance)
                {
                    myAgent.speed = RunSpeed;
                }
                else
                {
                    myAgent.speed = WalkSpeed;
                }

                AudioReset -= Time.deltaTime;
                if (AudioReset <= 0f && !MyAudioSource.isPlaying)
                {
                    MyAudioSource.clip = ZombieGroans[Random.Range(0, ZombieGroans.Length)];
                    MyAudioSource.Play();
                    AudioReset = AudioResetStart;
                }
               
            }
        }
        else
        {
            Debug.LogWarning("There is nothing for me to chase. What do I do? I guess I will just stand here.");
            myAnimator.SetBool("IsWalking", false);
        }
    }
}
