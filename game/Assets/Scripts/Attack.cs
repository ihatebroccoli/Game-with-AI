using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform pos;
    public Vector2 boxSize;
    public GameObject player;
    public float HitDmg = 1.0f;
    public float AngerRate;
    
    AudioSource hit;
    AudioSource nonhit;

    public AudioClip n_hit;
    public AudioClip y_hit;


    void Start()
    {

        nonhit = gameObject.AddComponent<AudioSource>();
        hit = gameObject.AddComponent<AudioSource>();
        hit.clip = y_hit;
        hit.mute = false;
        hit.loop = false;
        hit.playOnAwake = false;

        nonhit.clip = n_hit;
        nonhit.mute = false;
        nonhit.loop = false;
        nonhit.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        if (Input.GetKeyDown("a") && !player.GetComponent<Bandit>().isDead && !player.GetComponent<Bandit>().PILSALING)
        {
            if (!nonhit.isPlaying)
            {
                nonhit.Play();
            }
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Enemy")
                    if (!hit.isPlaying)
                    {
                        hit.Play();
                        GameObject.Find("TestEnemy").GetComponent<Enemy>().TakeDamage(HitDmg);
                        player.GetComponent<Bandit>().player_MP += (AngerRate * HitDmg);
                    }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
