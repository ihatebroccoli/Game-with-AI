using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체 Enemystat을 만듭니다. 안에는 Health나 Speed등을 넣습니다. 구조체를 inspector창에 보일수 있게 Attribute[System.Serializable]을 써줍니다.

public class Enemy : MonoBehaviour
{
    public bool isDead = false;
    //enemy스탯을 초기화 합니다.
    public float Health;
    private Animator E_animator;



    // Start is called before the first frame update
    void Start()
    {
        E_animator = GetComponent<Animator>();
    }
    
    public void TakeDamage(float damage)
    {
        if(isDead == true)
        {
            return;
        }
        //체력이 damage만큼 까지게 합니다.
        E_animator.SetTrigger("isHurt");
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            isDead = true;
            E_animator.SetTrigger("isDead");
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.tag = "DyingEnemy";
            Invoke("Death",2);
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
