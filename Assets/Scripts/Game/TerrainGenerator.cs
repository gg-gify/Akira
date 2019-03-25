using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int width = 12;
    [SerializeField] private int height = 12;
    [SerializeField] private Material materialOne;
    [SerializeField] private Material materialTwo;

    private bool inOnPlayMode = false;
    private float widthModifyer = 0;
    private float heightModifyer = 0;

    private void Awake()
    {
        SetModifyers();
        Material currentMaterial = materialOne;
        Vector3 cubeSize = new Vector3(1, 0.5f, 1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                bool odd = (i + j) % 2 == 0;
                Vector3 position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);
                position.x += i - width / 2;
                position.z += j - height / 2;

                if (odd)
                    currentMaterial = materialOne;
                else
                    currentMaterial = materialTwo;

                GameObject currentCube = InstantiateCube(position, currentMaterial);
                currentCube.transform.SetParent(transform);
            }
        }

        BoxCollider bc = gameObject.AddComponent<BoxCollider>();
        bc.size = new Vector3(width, 0.5f, height);
        bc.center = new Vector3(-widthModifyer, 0, -heightModifyer);

        float widthCenter = width / 2 + 0.75f;
        float heightCenter = height / 2 + 0.75f;

        // left wall
        BoxCollider left_bc = gameObject.AddComponent<BoxCollider>();
        left_bc.center = new Vector3(-widthCenter, 2, -heightModifyer);
        left_bc.size = new Vector3(0.5f, 3.5f, height);

        // right wall
        BoxCollider right_bc = gameObject.AddComponent<BoxCollider>();
        right_bc.center = new Vector3(widthCenter - ((width % 2 == 0) ? 1f:0), 2, -heightModifyer);
        right_bc.size = new Vector3(0.5f, 3.5f, height);

        // bottom wall
        BoxCollider bottom_bc = gameObject.AddComponent<BoxCollider>();
        bottom_bc.center = new Vector3(-widthModifyer, 2, -heightCenter);
        bottom_bc.size = new Vector3(width, 3.5f, 0.5f);

        // top wall
        BoxCollider top_bc = gameObject.AddComponent<BoxCollider>();
        top_bc.center = new Vector3(-widthModifyer, 2, heightCenter - ((height % 2 ==0)?1f:0));
        top_bc.size = new Vector3(width, 3.5f, 0.5f);

        inOnPlayMode = true;
    }

    private GameObject InstantiateCube(Vector3 position, Material material)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(1, 0.5f, 1);
        Destroy(cube.GetComponent<BoxCollider>());
        cube.transform.position = position;
        cube.GetComponent<MeshRenderer>().material = material;
        return cube;
    }

    private void SetModifyers()
    {
        widthModifyer = ((width % 2 == 0) ? 0.5f : 0);
        heightModifyer = ((height % 2 == 0) ? 0.5f : 0);
    }

    private void OnDrawGizmos()
    {
        if (materialOne != null && materialTwo != null && !inOnPlayMode)
        {
            SetModifyers();
            Vector3 cubeSize = new Vector3(1, 0.5f, 1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bool odd = (i + j) % 2 == 0;
                    Vector3 position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);
                    position.x += i - width / 2;
                    position.z += j - height / 2;

                    if (odd)
                        Gizmos.color = materialOne.color;
                    else
                        Gizmos.color = materialTwo.color;
                    Gizmos.DrawCube(position, cubeSize);
                }
            }
        }
        else
        {
            Debug.LogWarning("Terrain materials not set!");
        }
    }
}
