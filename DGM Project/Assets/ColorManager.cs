using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] public Renderer answerRenderer;
    [SerializeField] public Renderer questionRenderer;
    [SerializeField] Renderer gameMapBuffer;
    private Color colorHit;
    private Vector3 questionColorVector;

    // Start is called before the first frame update
    void Start()
    {
        GrabRandomColor();
    }

    private void GrabRandomColor()
    {
        float xUVCord = (float)(Random.Range(1, 100));
        float yUVCord = (float)(Random.Range(1, 100));
        xUVCord = xUVCord / 100f;
        yUVCord = yUVCord / 100f;
        //Debug.Log($"{xUVCord}, {yUVCord}");

        Texture2D texture2D = gameMapBuffer.material.mainTexture as Texture2D;
        questionRenderer.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
        questionColorVector = new Vector3(questionRenderer.material.color.r, questionRenderer.material.color.g, questionRenderer.material.color.b);
        //mouseToWorld.questionColorVector = new Vector3(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b);
        //print(texture2D.GetPixelBilinear(xUVCord, yUVCord));
    }

    public void HandleColorChoice(RaycastHit hit)
    {
        Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
        Vector2 pixelPos = hit.textureCoord;
        Texture2D texture2D = renderer.material.mainTexture as Texture2D;
        colorHit = texture2D.GetPixelBilinear(pixelPos.x, pixelPos.y);

        answerRenderer.material.color = colorHit;
        Vector3 answerColorVector = new Vector3(colorHit.r, colorHit.g, colorHit.b);
        //print($"Color key {questionColorVector} --- Answer {answerColorVector}");
        float questionVal = questionColorVector.x + questionColorVector.y + questionColorVector.z;
        float answerVal = answerColorVector.x + answerColorVector.y + questionColorVector.z;
        print(Mathf.Abs((int)(answerVal * 100) - (int)(questionVal * 100)));
        GrabRandomColor();
    }
}
