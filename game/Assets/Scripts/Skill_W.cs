using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_W : MonoBehaviour
{
    public GameObject player;
    private Animator            playerState;
    public Image W_cool;

    private Animator W_animator;
    float WCoolDown = 20;
    float WCoolDownCounter;
    bool WReady = true;

    
    //Aura
    Renderer colorRenderer;
    bool AuraState;
    bool player_is_dead;
    public GameObject Dasheff;
    ParticleSystem.MainModule DashColor;


    

    void Start()
    {
        AuraState = false;
        player_is_dead = false;
        

        colorRenderer = player.GetComponent<Renderer>();
        DashColor = Dasheff.GetComponent<ParticleSystem>().main;

        W_animator = GetComponent<Animator>();

        playerState = player.GetComponent<Animator>();

    }

   
    void SkillW_Off()
    {
        AuraState = false;
        W_animator.SetBool("AuraOn", false);
        colorRenderer.material.SetColor("_Color", Color.white);
        GameObject.Find("AttackRange").GetComponent<Attack>().HitDmg = 1.0f;
        DashColor.startColor = new Color(0, 40, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {

        W_cool.fillAmount = (WCoolDown - WCoolDownCounter) / WCoolDown;
        if (playerState.GetBool("Death"))
        {
            player_is_dead = true;
            SkillW_Off();
        }
        if (player_is_dead == false)
        {
            if(!player.GetComponent<Bandit>().PILSALING)
            { 
            //skill 2
                if (Input.GetKeyDown("w") && WReady == true)
                {
                    AuraState = true;
                    WReady = false;
                    WCoolDownCounter = WCoolDown;
                    GameObject.Find("AttackRange").GetComponent<Attack>().HitDmg = 10.0f;

                    colorRenderer.material.SetColor("_Color", Color.red);
                    DashColor.startColor = new Color(255, 0, 0);

                    W_animator.SetBool("AuraOn", true);
                    Invoke("SkillW_Off", 5);
                }
            }
            if (WReady == false)
            {
                WCoolDownCounter -= Time.deltaTime;
                if (WCoolDownCounter <= 0)
                {
                    WReady = true;
                }
            }

        }
        
    }
}
