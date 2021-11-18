using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControll : MonoBehaviour
{
    float CellHp = 5.0f;

    bool ishitPlayer = false;

    GameObject Player;
    [SerializeField]
    GameObject Renderer;
    Animator m_animator;

    int DieCheck = 0;
   

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_animator = this.gameObject.GetComponent<Animator>();
        transform.LookAt(Player.transform);
     
    }
    private void Update()
    {

        Cell_hit();

            if (DieCheck == 1)
            {
                Player.GetComponent<PlayerData>().CellCountUp();
                Object.Destroy(this.gameObject);
                

            }


    }
    public void OnPointerEnter()
    {
        Debug.Log("OnPointerEnter");
        Renderer.GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
        m_animator.SetBool("Hit", true);
        ishitPlayer = true;
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

        

        }
        else  if (ishitPlayer)
        {
            CellHp -= Time.deltaTime;
            Debug.Log(CellHp);
        }
        if (!ishitPlayer)
        {
            if (CellHp < 5.0f&&CellHp>0.0f)
            {
                CellHp += Time.deltaTime;
            }
            else if (CellHp > 5.0f)
            {
                CellHp = 5.0f;
            }
        }
    }

  

    void DieAniEnd(int i)
    {
        DieCheck = i;
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            Object.Destroy(this.gameObject);
        }
    }



}
