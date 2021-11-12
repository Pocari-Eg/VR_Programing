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
        InitPlayer();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitPlayer()
    {
        Cell_Count = 15;

    }

 
    //colider

 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Trap")
        {
            CellCountDown(1);

        }
    }

    //get set
    public  void SetCellCount(int m) { Cell_Count = m; }
    public int GetCellCount() { return Cell_Count; }

    public void CellCountUp() { Cell_Count++; }
    public void CellCountDown(int n) { Cell_Count-=n;  if (Cell_Count < 0) Cell_Count = 0; }
}
