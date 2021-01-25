using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skill_E : MonoBehaviour
{
    public GameObject animation_object;

    public float PilsalDmg;
    public Image PilsalImage;



    AudioSource clipsound;
    public AudioClip SoundEff;
    // Start is called before the first frame update
    void Start()
    {
        clipsound = gameObject.AddComponent<AudioSource>(); 
        clipsound.clip = SoundEff;
        clipsound.mute = false;
        clipsound.loop = false;
        clipsound.playOnAwake = false;
    }

    void PILSAL()
    {
        GameObject.Find("TestEnemy").GetComponent<Enemy>().TakeDamage(PilsalDmg);
        clipsound.Play();
    }
    void PILSALOff()
    {
        animation_object.SetActive(false);
        GetComponent<Bandit>().PILSALING = false;
    }

    // Update is called once per frame
    void Update()
    {
        PilsalImage.fillAmount = (GetComponent<Bandit>().player_MP) / 100;
        if (Input.GetKeyDown("e") && !GetComponent<Bandit>().isDead && GetComponent<Bandit>().player_MP == 100)
        {
            animation_object.SetActive(true);
            GetComponent<Bandit>().player_MP = 0;
            GetComponent<Bandit>().PILSALING = true;
            Invoke("PILSAL", 2.4f);
            Invoke("PILSALOff", 4.50f);
        }
    }
}
