using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMP : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject Boss;
    bool BossDead = false;
    public Image PlayerHP_Display;
    public Image PlayerMP_Display;

    public Image BossHP_Display;
    public Text BossHP_Per_Display;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (BossDead)
        {
            Boss = null;
            BossHP_Per_Display.text = "0%";
        }
        else
        {
            BossDead = Boss.GetComponent<Enemy>().isDead;
            BossHP_Display.fillAmount = Boss.GetComponent<Enemy>().Health / 300;
            BossHP_Per_Display.text = (int)((Boss.GetComponent<Enemy>().Health / 300) * 100) + "%";
        }
        PlayerHP_Display.fillAmount = player.GetComponent<Bandit>().player_HP / 100;
        PlayerMP_Display.fillAmount = player.GetComponent<Bandit>().player_MP / 100;
        
    }
}
