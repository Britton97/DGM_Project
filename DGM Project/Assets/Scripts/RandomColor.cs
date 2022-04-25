using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    //INSTEAD OF MAKING A RANDOM COLOR JUST SAMPLE A RANDOM PIXEL FROM THE TEXTURE INSTEAD. THAT WILL MAKE IT SCALEABLE IN THE FUTURE
    Renderer myRenderer;
    [SerializeField] public MouseToWorld mouseToWorld;
    [SerializeField] Renderer gameMapBuffer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        GrabRandomColor();
        mouseToWorld.questionColorVector = new Vector3(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GrabRandomColor();
        }
    }

    private void GrabRandomColor()
    {
        float xUVCord = (float)(Random.Range(1, 100));
        float yUVCord = (float)(Random.Range(1, 100));
        xUVCord = xUVCord / 100f;
        yUVCord = yUVCord / 100f;
        //Debug.Log($"{xUVCord}, {yUVCord}");

        Texture2D texture2D = gameMapBuffer.material.mainTexture as Texture2D;
        myRenderer.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
        mouseToWorld.questionColorVector = new Vector3(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b);
        //print(texture2D.GetPixelBilinear(xUVCord, yUVCord));
    }
}