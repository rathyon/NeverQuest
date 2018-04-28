
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float Tail1_maxPoint; // direira: 12.0f;
    public float Tail1_minPoint; // esquerda: -12.0f


    public int HP;
    public Text labelHP;
    public GameObject player, mob;


    public bool flag_change = false;

    public float speed;
    public float questAcceptTime;

    //private Rigidbody2D rb2d;
    private Vector3 playerPos;
    private bool timerActive;
    private bool slowed;
    private float slowPercentage;
    private float slowTimer;
    private float slowTimerMAX;

    SpriteRenderer spriteRenderer_mob;
    Rigidbody2D rb2d_mob;

    // Use this for initialization
    void Start()
    {
        spriteRenderer_mob = GetComponent<SpriteRenderer>();
        rb2d_mob = GetComponent<Rigidbody2D>();
    }

    int estado_antigo;
    // Update is called once per frame
    void Update()
    {

        float xx; int margem_lateral = 2, estado_actual;
        if (player.transform.position.x <= mob.transform.position.x)
        {
            xx = -0.25f; // para o lado esquerdo por exemplo
            estado_actual = 1;
        }
        else
        {
            xx = 0.25f; // para o lado direito , nesse caso
            estado_actual = 2;
        }

        if (estado_antigo != estado_actual) rb2d_mob.velocity = Vector3.zero;

        rb2d_mob.AddForce(new Vector2(xx, 0.0f));

        estado_antigo = estado_actual;

        //Wall1
        if ((mob.transform.position.x <= -12 || mob.transform.position.x >= 12) && mob.transform.position.y == -3.18f)//lado esquerdo
        {
            Vector3 aux = rb2d_mob.velocity;
            rb2d_mob.velocity = -aux;
            if (mob.transform.position.x < 0) margem_lateral = -margem_lateral;
            mob.transform.position = new Vector2(mob.transform.position.x - margem_lateral, 8.97f);
            rb2d_mob.AddForce(new Vector2(-xx, 0.0f));
        }
        //Wall2
        if ((mob.transform.position.x <= -12 || mob.transform.position.x >= 12) && mob.transform.position.y == 8.97f)//lado esquerdo
        {
            Vector3 aux = rb2d_mob.velocity;
            rb2d_mob.velocity = -aux;
            if (mob.transform.position.x < 0) margem_lateral = -margem_lateral;
            mob.transform.position = new Vector2(mob.transform.position.x - margem_lateral, -3.18f);
            rb2d_mob.AddForce(new Vector2(-xx, 0.0f));
        }
    }
    //private void Countdown()
    //{
    //    questAcceptTime -= Time.deltaTime;
    //}


}