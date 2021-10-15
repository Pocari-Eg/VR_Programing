using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_goPoint : MonoBehaviour
{
    public GameObject[] goPointList;

    public GameObject[] goPoints;
    public float rotSpeed = 2.0f;
    public float moveSpeed = 0.8f;
    private int curGoPointIdx = 0;
    private int numGoPoints = 0;
    private Transform curTransform;

    bool isGoing = true;
 
// Start is called before the first frame update
void Start()
    {
  
        goPointList = GameObject.FindGameObjectsWithTag("goPoint");
        curGoPointIdx = 0; // ���� GoPoint�� �ε���
        curTransform = GetComponent<Transform>(); // ���� Player�� Transform
        goPoints = GameObject.FindGameObjectsWithTag("goPoint"); // ������ GoPoints��
        numGoPoints = goPoints.Length; // GoPoints �� ����. 
    }
    void Update()
    {
        Debug.Log(" ������");
        if (isGoing)
        {
            MovePlayer();
        }
    }
   void MovePlayer()
    {
        // Player�� ������ ���� ����
        Vector3 goDirection = goPoints[curGoPointIdx].transform.position - curTransform.position;
        // Player�� �ٶ� ������ Rot���� ���ʹϾ��� ���ؼ� ����. 
        Quaternion goRoation = Quaternion.LookRotation(goDirection);
        // �ش� �������� ȸ��
        curTransform.rotation = Quaternion.Slerp(curTransform.rotation, goRoation, Time.deltaTime * rotSpeed);
        // �ش� �������� �̵�
        curTransform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goPoint"))
        {
            //curGoPointIdx++;
            //if (curGoPointIdx == numGoPoints)
            //{
            //    curGoPointIdx = 0;

            //}

            isGoing = false;
            Debug.Log("Trigger Enter : " + other.name);
        }
    }


}
