using System.Collections;
using UnityEngine;

public class CameraPointerExt : MonoBehaviour
{
    private const float _maxDistance = 100;
    private GameObject _gazedAtObject = null;


    int layerMask;


    /// <summary>
    /// Update is called once per frame.

    /// </summary>
public void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("UI");
    }
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, layerMask)) 
        {



            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                Debug.Log(hit.transform.gameObject);
                // New GameObject.

                _gazedAtObject?.SendMessage("OnUIPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                Debug.Log(_gazedAtObject.name);
                _gazedAtObject.SendMessage("OnUIPointerEnter");
            }
            }
            else
            {
            // No GameObject detected in front of the camera.

           
                _gazedAtObject?.SendMessage("OnUIPointerExit");

                _gazedAtObject = null;
            }

            // Checks for screen touches.
     
        }
    }

