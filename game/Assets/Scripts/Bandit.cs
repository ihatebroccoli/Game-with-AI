using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    SpriteRenderer spriteRenderer;


    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                isDash = false;
    private bool                isDamaged = false;
    
    public float player_HP;
    public float player_MP;

    //forDeath
    public GameObject Dead_backGround;
    public bool isDead;

    public bool PILSALING;

    //forDash
    public GameObject Dasheff;
    public float DashSpeed;
    public float defaultTime;
    private float DashTime;
    private float DashTime4effect;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            player_HP -= 5.0f;
            OnDamaged(collision.transform.position);
            
        }
        else if (collision.gameObject.layer == 14)
        {
            player_HP = 0;
        }
    }
    void AllowMove()
    {
        isDamaged = false;
    }
    void OnDamaged(Vector2 targetPos)
    {
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, 0);
        if(player_HP > 0)
        {
            gameObject.layer = 12;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            m_body2d.AddForce(new Vector2(dirc, 0.5f) * 5, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1.5f);
        }
        
        isDamaged = true;
        Invoke("AllowMove", 0.3f);


    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }



    void Update () {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        if(player_MP >= 100)
        {
            player_MP = 100;
        }
        if(player_HP <= 0)
        {
            Dead_backGround.SetActive(true);
            m_animator.SetBool("Death", true);
            isDead = true;
        }
        
        

        if(isDead == false && PILSALING == false)
        {
            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                Dasheff.transform.localScale = new Vector3(-1.1f, 1.0f, 1.0f);
            }
            else if (inputX < 0)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Dasheff.transform.localScale = new Vector3(1.1f, 1.0f, 1.0f);
            }

            if (isDamaged == false)
            {
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
            }


            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            // -- Handle Animations --
            //Death



            //Attack
            if (Input.GetKeyDown("a"))
            {
                m_animator.SetTrigger("Attack");
            }

            //Dash
            else if (Input.GetButtonDown("Fire3"))
            {
                Dasheff.SetActive(true);
                isDash = true;
            }

            //Jump
            else if (Input.GetKeyDown("s") && m_grounded && isDamaged == false)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                m_animator.SetInteger("AnimState", 2);

            //Combat Idle
            else if (m_combatIdle)
                m_animator.SetInteger("AnimState", 1);
            //Idle
            else
                m_animator.SetInteger("AnimState", 0);

            if (DashTime <= 0)
            {
                m_speed = 4.0f;
                if (isDash)
                {
                    DashTime4effect = defaultTime + 0.2f;
                    DashTime = defaultTime;
                }
            }

            else
            {
                DashTime -= Time.deltaTime;

                m_speed = DashSpeed;
            }
            DashTime4effect -= Time.deltaTime;
            isDash = false;
            if (DashTime4effect <= 0)
            {
                Dasheff.SetActive(false);
            }
        }
        

    }
    


}
