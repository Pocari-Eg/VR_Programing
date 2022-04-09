using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uv_Animaiton : MonoBehaviour
{
    [SerializeField]
    float speedX = 0.1f;
    [SerializeField]
    float speedY= 0.1f;

    float curX;
    float curY;
    // Start is called before the first frame update
    void Start()
    {
        curX = GetComponent<Renderer>().material.mainTextureOffset.x;
        curY = GetComponent<Renderer>().material.mainTextureOffset.y;


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        curX += Time.deltaTime * speedX;
        curY += Time.deltaTime * speedY;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));

    }
}
