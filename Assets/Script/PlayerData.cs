using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    
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
        
    }
 

 
    //colider

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            CellCountDown(1);

        }
   if(other.gameObject.tag=="Goal")
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
}
