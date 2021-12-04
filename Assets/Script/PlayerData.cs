using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    [SerializeField]
    GameObject HpBar;
    
    [SerializeField]
    int Cell_Count;
    // Start is called before the first frame update
    private void Awake()
    {
     
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.GetComponent<Image>().fillAmount =((float) Cell_Count) / 30f;
    }
 

 
    //colider

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            CellCountDown(1);

        }
        else if (other.gameObject.tag == "Lava")
        {
            CellCountDown(1);
            this.gameObject.transform.position = GameObject.FindGameObjectWithTag("TP").GetComponent<TelePort>().BackPos();  

        }
        if (other.gameObject.tag=="Goal")
        {
            Debug.Log("Å¬¸®¾î");
            other.gameObject.SetActive(false);
            this.GetComponent<ForceMove>().viewMode = 1;
            BonusStageManager.instance.startUiOn();

          
        }
    
    }

    //get set
    public  void SetCellCount(int m) { Cell_Count = m; }
    public int GetCellCount() { return Cell_Count; }

    public void CellCountUp() { Cell_Count++; }
    public void CellCountDown(int n) { Cell_Count-=n;  if (Cell_Count < 0) Cell_Count = 0; }

    public void Jump()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, 3.0f, 3.0f);
    }


}
