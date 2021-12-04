using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WBCControl : MonoBehaviour
{
    [SerializeField]
    GameObject HpBar;
    float CellHp = 3.0f;

    bool ishitPlayer = false;

    GameObject Player;
    [SerializeField]
    GameObject Renderer;
    [SerializeField]
    GameObject Guage;
    public Animator m_animator;

    [SerializeField]
    float moveSpeed;
    int DieCheck = 0;
    int AttackCheck = 0;

    float AttackTime = 10.0f;

    public bool attackAreaIn = false;
    bool AttackMode = false;

    bool HitDead = false;
    private void Start()
    {
        Guage = GameObject.FindGameObjectWithTag("Guage");
        Player = GameObject.FindGameObjectWithTag("Player");
        m_animator = this.gameObject.GetComponent<Animator>();
        transform.LookAt(Player.transform);
        transform.localRotation = transform.rotation;
     
    }
    private void Update()
    {

        HpBar.GetComponent<Image>().fillAmount = CellHp / 3f;

        //  Debug.Log(AttackTime);

        if (AttackTime >= 0.0f)
        {

            if (AttackMode == false)
            {
                AttackTime -= Time.deltaTime;
                Cell_hit();
            }
        }
        else
        {
            AttackMode = true;
            Renderer.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
            m_animator.SetBool("Hit", false);

            ishitPlayer = false;
            if (attackAreaIn == false)
            {
                m_animator.SetBool("Move", true);
                MoveLookAt();
            }
            else
            {
              m_animator.SetBool("Move", false);
            m_animator.SetTrigger("Attack");
            }
        }

        if(AttackCheck == 1)
        {
            m_animator.SetTrigger("Die");
           
        }

        if (DieCheck == 1)
        {

            if (HitDead == true)
            {
                Player.GetComponent<PlayerData>().CellCountUp();
            }
                Object.Destroy(this.gameObject);
           

        }
    }
    public void OnPointerEnter()
    {
        if (AttackMode == false)
        {
            m_animator.SetBool("Hit", true);
            Debug.Log("OnPointerEnter");
            Renderer.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.42f, 1.0f, 0.0f);
            ishitPlayer = true;
            Guage.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        
    }
    public void OnPointerExit()
    {

        m_animator.SetBool("Hit", false);

        Debug.Log("OnPointerExit");
        Renderer.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;

        ishitPlayer = false;
        Guage.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    void Cell_hit()
    {
        if (CellHp < 0.0f)
        {
            m_animator.SetTrigger("Die");
            HitDead = true;


        }
        else if (ishitPlayer)
        {
            CellHp -= Time.deltaTime * 1.0f;
            Debug.Log(CellHp);
        }
        if (!ishitPlayer)
        {
            if (CellHp < 3.0f && CellHp > 0.0f)
            {
                CellHp += Time.deltaTime * 1.0f;
            }
            else if (CellHp > 3.0f)
            {
                CellHp = 3.0f;
            }
        }
    }



    void DieAniEnd(int i)
    {
        DieCheck = i;
    }
    void AttakcAniEnd(int i)
    {
        Player.GetComponent<PlayerData>().CellCountDown(2);
        AttackCheck = i;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            Object.Destroy(this.gameObject);
        }
    }

    void MoveLookAt()
    {

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("플레이어 충돌");

            attackAreaIn = true;

        }
    }
    private void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어 충돌");

            attackAreaIn = true;

        }
    }
}
