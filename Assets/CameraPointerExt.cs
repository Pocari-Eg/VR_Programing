using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerExt : MonoBehaviour
{
    private const float _maxDistance = 20;
    private GameObject _gazedAtObject = null;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))

        {



            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.

               _gazedAtObject?.SendMessage("OnUIPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                Debug.Log(_gazedAtObject);
                _gazedAtObject.SendMessage("OnUIPointerEnter");

            }
            else
            {
                // No GameObject detected in front of the camera.


                _gazedAtObject?.SendMessage("OnUIPointerExit");

                _gazedAtObject = null;
            }

            // Checks for screen touches.
            if (Google.XR.Cardboard.Api.IsTriggerPressed)
            {
                _gazedAtObject?.SendMessage("OnPointerClick");
            }
        }
    }
}
