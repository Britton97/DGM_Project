using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] public Renderer answerRenderer;
    [SerializeField] public Renderer questionRenderer;
    [SerializeField] public List<Renderer> questionRenderHubs = new List<Renderer>();
    [SerializeField] public Renderer questionBottom;
    [SerializeField] Renderer gameMapBuffer;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Slider meter;
    private Color colorHit;
    private Vector3 questionColorVector;

    [SerializeField] RenderTexture renderText;

    bool changingValues = false;

    public float score = 0;
    private float lastTime;

    private int textureWidth = 1080;
    private bool waiting = false;

    [SerializeField] Material deadMaterial;


    // Start is called before the first frame update
    void Start()
    {
        GrabRandomColor();
        lastTime = Time.time;
    }

    private void Update()
    {
        float timePass = Time.time - lastTime;
        lastTime = Time.time;
        if (!changingValues)
        {
            score += timePass;
        }
        if (meter.value >= meter.maxValue)
        {
            lastTime = Time.time;
            score = 0;
            enemySpawner.SpawnRock();
            enemySpawner.SpawnRock();
            enemySpawner.SpawnRock();
        }
        meter.value = score;
        scoreText.text = $"{Time.time:n1}";
        if (!waiting && textureWidth > 64)
        {
            StartCoroutine(MinusOne(10));
            renderText.Release();
            renderText.width = textureWidth;
            renderText.height = textureWidth;
            renderText.Create();
        }

    }

    public int LoseLife()
    {

        int len = questionRenderHubs.Count;
        int rand = Random.Range(0, len);
        questionRenderHubs[rand].GetComponent<Renderer>().material = deadMaterial;
        questionRenderHubs.RemoveAt(rand);
        if(questionRenderHubs.Count == 1)
        {
            Debug.Log("Game Over");
            return -1;
        }
        return questionRenderHubs.Count - 1;
    }

    public int GetLifeCount()
    {
        return questionRenderHubs.Count;
    }

    private void GrabRandomColor()
    {
        float xUVCord = (float)(Random.Range(1, 100));
        float yUVCord = (float)(Random.Range(1, 100));
        xUVCord = xUVCord / 100f;
        yUVCord = yUVCord / 100f;
        //Debug.Log($"{xUVCord}, {yUVCord}");

        Texture2D texture2D = gameMapBuffer.material.mainTexture as Texture2D;

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
    }

    public void HandleColorChoice(RaycastHit hit)
    {
        Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
        Vector2 pixelPos = hit.textureCoord;
        Texture2D texture2D = renderer.material.mainTexture as Texture2D;
        colorHit = texture2D.GetPixelBilinear(pixelPos.x, pixelPos.y);

        Vector3 answerColorVector = new Vector3(colorHit.r, colorHit.g, colorHit.b);
        float questionVal = questionColorVector.x + questionColorVector.y + questionColorVector.z;
        float answerVal = answerColorVector.x + answerColorVector.y + questionColorVector.z;
        //new
        int qAbs = Mathf.Abs((int)(answerVal * 100));
        int aAbs = Mathf.Abs((int)(questionVal * 100));
        //new
        int num = qAbs - aAbs;
        Debug.Log($"question = {qAbs}\r\nAnswer = {aAbs}\r\nnum {num}");
        AddToMeter(num);
        GrabRandomColor();
    }

    private void AddToMeter(int adding)
    {
        StartCoroutine(AddingCoroutine(adding));
    }

    IEnumerator MinusOne(int passIn)
    {
        waiting = true;
        yield return new WaitForSeconds(1);
        textureWidth = textureWidth - passIn;
        waiting = false;
    }
    IEnumerator AddingCoroutine(float adding)
    {
        changingValues = true;
        if(adding <= 35)
        {
            float minus = -((20 - adding) * 4);
            adding = -Mathf.Abs(minus);
            Debug.Log($"adding = {adding}");
        }
        adding = adding / 10;
        float div = adding / 20;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.05f);
            if (div + meter.value >= meter.maxValue)
            {
                float val = (div + meter.value) - meter.maxValue;
                score = val;
                enemySpawner.SpawnRock();
            }
            else if (div + meter.value <= meter.minValue)
            {
                //float val = (div + meter.value) + meter.maxValue;
                float val = meter.minValue;
                score = val;
            }
            else
            {
                score += div;
            }
        }
        changingValues = false;

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
