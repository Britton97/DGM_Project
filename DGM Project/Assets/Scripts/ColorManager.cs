using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] public Renderer answerRenderer;
    [SerializeField] public Renderer questionRenderer;
    [SerializeField] public List<Renderer> questionRenderHubs = new List<Renderer>();
    [SerializeField] public Renderer questionBottom;
    [SerializeField] Renderer gameMapBuffer;
    [SerializeField] public TextMeshPro scoreText;
    [SerializeField] private EnemySpawner enemySpawner;
    private Color colorHit;
    private Vector3 questionColorVector;

    public float score = 0;
    private float lastTime;


    // Start is called before the first frame update
    void Start()
    {
        GrabRandomColor();
        lastTime = Time.time;
    }

    private void Update()
    {
        score = Time.time - lastTime;
        if(score > 3)
        {
            lastTime = Time.time;
            score = 0;
            enemySpawner.SpawnRock();
        }
        scoreText.text = $"{score:n1}";
    }

    private void GrabRandomColor()
    {
        float xUVCord = (float)(Random.Range(1, 100));
        float yUVCord = (float)(Random.Range(1, 100));
        xUVCord = xUVCord / 100f;
        yUVCord = yUVCord / 100f;
        //Debug.Log($"{xUVCord}, {yUVCord}");

        Texture2D texture2D = gameMapBuffer.material.mainTexture as Texture2D;
        /*
        questionRenderer.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
        questionRenderer.material.EnableKeyword("_EMISSION");
        questionRenderer.material.SetColor("_EmissionColor", texture2D.GetPixelBilinear(xUVCord, yUVCord) * 15.0f);
        */

        foreach(Renderer child in questionRenderHubs)
        {
            child.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
            child.material.EnableKeyword("_EMISSION");
            child.material.SetColor("_EmissionColor", texture2D.GetPixelBilinear(xUVCord, yUVCord) * 10.0f);
        }

        questionBottom.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
        questionBottom.material.EnableKeyword("_EMISSION");
        questionBottom.material.SetColor("_EmissionColor", texture2D.GetPixelBilinear(xUVCord, yUVCord) * 10.0f);

        questionColorVector = new Vector3(questionBottom.material.color.r, questionBottom.material.color.g, questionBottom.material.color.b);
        //mouseToWorld.questionColorVector = new Vector3(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b);
        //print(texture2D.GetPixelBilinear(xUVCord, yUVCord));
    }

    public void HandleColorChoice(RaycastHit hit)
    {
        Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
        Vector2 pixelPos = hit.textureCoord;
        Texture2D texture2D = renderer.material.mainTexture as Texture2D;
        colorHit = texture2D.GetPixelBilinear(pixelPos.x, pixelPos.y);

        //answerRenderer.material.color = colorHit;
        Vector3 answerColorVector = new Vector3(colorHit.r, colorHit.g, colorHit.b);
        //print($"Color key {questionColorVector} --- Answer {answerColorVector}");
        float questionVal = questionColorVector.x + questionColorVector.y + questionColorVector.z;
        float answerVal = answerColorVector.x + answerColorVector.y + questionColorVector.z;
        int num = Mathf.Abs((int)(answerVal * 100) - (int)(questionVal * 100));
        GrabRandomColor();
    }

    public void MakeRockRandomColor(Renderer rockMat)
    {
        float xUVCord = (float)(Random.Range(1, 100));
        float yUVCord = (float)(Random.Range(1, 100));
        xUVCord = xUVCord / 100f;
        yUVCord = yUVCord / 100f;

        Texture2D texture2D = gameMapBuffer.material.mainTexture as Texture2D;
        rockMat.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);

        rockMat.material.color = texture2D.GetPixelBilinear(xUVCord, yUVCord);
        rockMat.material.EnableKeyword("_EMISSION");
        rockMat.material.SetColor("_EmissionColor", texture2D.GetPixelBilinear(xUVCord, yUVCord));
    }
}
