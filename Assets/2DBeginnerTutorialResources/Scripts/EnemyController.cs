using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    public AudioClip hitClip;
    public AudioClip robotwalkClip;
    
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    public bool broken; 

    public ParticleSystem smokeEffect;
   
    AudioSource audioSource;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();   
        timer = changeTime;
        animator = GetComponent<Animator>();
        
    } 

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        {
            //remember ! inverse the test, so if broken is true !broken will be false and return won't be executed.
        if(!broken)
        {
            return;
        }
        }
    }

   void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if(!broken)
        {
            return;
        }
        
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 1);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 1);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            player.PlaySound(hitClip);
            
        }
         
        
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;

        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
     public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

    }
}
