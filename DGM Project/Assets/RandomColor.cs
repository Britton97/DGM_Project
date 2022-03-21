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

    /*
    private Color RandomColor255()
    {
        int r = Random.Range(0, 255);
        int g = Random.Range(0, 255);
        int b = Random.Range(0, 255);

        Debug.Log($"{r},{g},{b}");
        Color newColor = new Color(r / 255f, g / 255f, b / 255f, 1);
        myRenderer.material.color = newColor;
        return newColor;
    }
    */
}

//GARBAGE CODE BUT MIGHT COME IN HANDY LATER

//myRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
//mouseToWorld.question = myRenderer.material.color;
//Color newColor = RandomColor255();
//print($"{newColor.r * 255},{newColor.g * 255}, {newColor.b * 255}");
//Vector3 t = new Vector3 (newColor.r * 255,newColor.g * 255, newColor.b * 255);
//mouseToWorld.colorKey = t;
//mouseToWorld.colorKey = new Vector3(myRenderer.material.color.r * 255, myRenderer.material.color.g * 255, myRenderer.material.color.b * 255);
//mouseToWorld.colorKey = new Vector3((int)(myRenderer.material.color.r * 100), (int)(myRenderer.material.color.g * 100), (int)(myRenderer.material.color.b * 100));
//print(new Vector3(myRenderer.material.color.r * 255, myRenderer.material.color.g * 255, myRenderer.material.color.b * 255));