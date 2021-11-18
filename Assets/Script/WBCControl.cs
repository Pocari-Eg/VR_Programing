using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBCControl : MonoBehaviour
{
    float CellHp = 3.0f;

    bool ishitPlayer = false;

    GameObject Player;
    [SerializeField]
    GameObject Renderer;
   public Animator m_animator;

    [SerializeField]
    float moveSpeed;
    int DieCheck = 0;
    int AttackCheck = 0;

    float AttackTime = 10.0f;

    public bool attackAreaIn = false;
    bool AttackMode = false;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_animator = this.gameObject.GetComponent<Animator>();
        transform.LookAt(Player.transform);
        transform.localRotation = transform.rotation;
     
    }
    private void Update()
    {

    

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
            Object.Destroy(this.gameObject);
           

        }
    }
    public void OnPointerEnter()
    {
        if (AttackMode == false)
        {
            Debug.Log("OnPointerEnter");
            Renderer.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
            m_animator.SetBool("Hit", true);
            ishitPlayer = true;
        }
        
    }
    public void OnPointerExit()
    {

        Debug.Log("OnPointerExit");
        Renderer.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
        m_animator.SetBool("Hit", false);

        ishitPlayer = false;

    }

    void Cell_hit()
    {
        if (CellHp < 0.0f)
        {
            m_animator.SetTrigger("Die");
            Player.GetComponent<PlayerData>().CellCountUp();


        }
        else if (ishitPlayer)
        {
            CellHp -= Time.deltaTime;
            Debug.Log(CellHp);
        }
        if (!ishitPlayer)
        {
            if (CellHp < 3.0f && CellHp > 0.0f)
            {
                CellHp += Time.deltaTime;
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
