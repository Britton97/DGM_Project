using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToWorld : MonoBehaviour
{
    public float rayDistance;
    public Color testColor;
    public Renderer answerCube;

    public Color questionColor;
    public Vector3 questionColorVector;
    private Vector3 answerColorVector;

    public LayerMask mapLayer;
    [SerializeField] public string layerName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance) && hit.transform.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
            Vector2 pixelPos = hit.textureCoord;
            Texture2D texture2D = renderer.material.mainTexture as Texture2D;
            testColor = texture2D.GetPixelBilinear(pixelPos.x, pixelPos.y);

            answerCube.material.color = testColor;
            answerColorVector = new Vector3(testColor.r, testColor.g, testColor.b);
            //print($"Color key {questionColorVector} --- Answer {answerColorVector}");
            float questionVal = questionColorVector.x + questionColorVector.y + questionColorVector.z;
            float answerVal = answerColorVector.x + answerColorVector.y + questionColorVector.z;
            print(Mathf.Abs((int)(answerVal * 100) - (int)(questionVal * 100)));
        }
    }
}
