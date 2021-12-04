using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellControll : MonoBehaviour
{
    float CellHp = 5.0f;

    bool ishitPlayer = false;

    GameObject Player;
    [SerializeField]
    GameObject Renderer;
    Animator m_animator;

    int DieCheck = 0;

    [SerializeField]
    GameObject HpBar;
    [SerializeField]
    GameObject Guage;
    private void Start()
    {
        Guage = GameObject.FindGameObjectWithTag("Guage");
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
        HpBar.GetComponent<Image>().fillAmount = CellHp / 5f;

    }
    public void OnPointerEnter()
    {
        m_animator.SetBool("Hit", true);

        Debug.Log("OnPointerEnter");
        Renderer.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.42f, 1.0f, 0.0f);
        ishitPlayer = true;
        Guage.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
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

        

        }
        else  if (ishitPlayer)
        {
            CellHp -= Time.deltaTime*1.0f;
            Debug.Log(CellHp);
        }
        if (!ishitPlayer)
        {
            if (CellHp < 5.0f&&CellHp>0.0f)
            {
                CellHp += Time.deltaTime * 1.0f;
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

   public float GetCellHp()
    {
        return CellHp;
    }

}
