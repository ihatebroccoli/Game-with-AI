using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Skill_Q : MonoBehaviour
{
    public GameObject player;
    private Animator playerState;
    public Image Q_cool;

    private Animator Q_animator;
    //start point
    Vector2 Q_StartPoint;
    float sight;
    float OriginalDirection;

    float QCoolDown = 5;
    float QCoolDownCounter;
    bool OnShooting = false;
    bool QReady = true;
    public float QDmg;

    bool player_is_dead;


    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DyingEnemy")
        {
            GameObject.Find("TestEnemy").GetComponent<Enemy>().TakeDamage(QDmg);
            SkillQ_Off();
        }
    }


    void Start()
    {
        player_is_dead = false;

        Q_animator = GetComponent<Animator>();

        playerState = player.GetComponent<Animator>();

    }



    void SkillQ_Off()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        if (OnShooting == false)
        {
            return;
        }
        OnShooting = false;
        Q_animator.SetTrigger("Boom");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localScale.x < 0)
        {
            sight = 1.0f;
        }
        else
        {
            sight = -1.0f;
        }

        Q_cool.fillAmount = (QCoolDown - QCoolDownCounter) / QCoolDown;

        if (playerState.GetBool("Death"))
        {
            player_is_dead = true;
        }
        if (player_is_dead == false)
        {
            if (Input.GetKeyDown("q") && QReady == true && !player.GetComponent<Bandit>().PILSALING)
            {
                OnShooting = true;
                QReady = false;
                QCoolDownCounter = QCoolDown;

                //set start point and direction
                OriginalDirection = sight;
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1.0f);
                transform.localScale = new Vector3(sight * 2.0f, 2.0f, 1.0f);
                transform.GetChild(0).gameObject.SetActive(true);

                //Add velocity 

                GetComponent<Rigidbody2D>().AddForce(new Vector2(10.0f * sight, 0), ForceMode2D.Impulse);
                if (OnShooting)
                {
                    Invoke("SkillQ_Off", 2);
                }
            }


            if (QReady == false)
            {
                QCoolDownCounter -= Time.deltaTime;
                if (QCoolDownCounter <= 0)
                {
                    QReady = true;
                }
            }
        }

    }
}
