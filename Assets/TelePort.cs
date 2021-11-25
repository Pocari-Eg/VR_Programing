using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class TelePort : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Marker;

    Vector3 oldpos;
    [SerializeField]
    float moveSpeed;
    private void Update()
    {
        
        if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
        {
               Player.GetComponent<ForceMove>().viewMode = 1;
            oldpos = Player.transform.position;
            Marker.SetActive(true);
            setPos();


        }
        else if (Google.XR.Cardboard.Api.IsTriggerHeldPressed || Input.GetMouseButton(0))
        {
      
            if(Vector3.Distance(oldpos, this.gameObject.transform.position)<20.0f)
            this.gameObject.transform.position += this.gameObject.transform.forward * (moveSpeed*Time.deltaTime);
            else
            {

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Marker.SetActive(false);
            Vector3 newPlayerPos = this.gameObject.transform.position;
            newPlayerPos.y = Player.transform.position.y;
            Player.transform.position = newPlayerPos;
            Debug.Log(Player.transform.position);
            Player.GetComponent<ForceMove>().debugpos();
        }
    }

    public void setPos()
    {

        this.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
        Vector3 newPos = this.gameObject.transform.position;
        newPos.y = 1.25f;
        this.gameObject.transform.position = newPos;


        this.gameObject.transform.rotation =new Quaternion( 0,Camera.main.transform.rotation.y,0, Camera.main.transform.rotation.w);
    }

    public Vector3 BackPos()
    {
        return oldpos;
    }
}
