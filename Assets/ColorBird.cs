using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;

public class ColorBird : MonoBehaviour
{
    private Vector3[] mVertices;
    private Vector3[] mNormals;
    MeshFilter filter;
    private Color32[] mColors;

    // Start is called before the first frame update


    void Start()
    {
        filter = GetComponent<MeshFilter>();

        if (filter == null || filter.mesh == null)
        {
            Debug.LogError("MeshFilter or mesh is missing!");
            return;
        }

        filter.mesh = Instantiate(filter.mesh);



        mVertices = filter.mesh.vertices;
        mNormals = filter.mesh.normals;
        mColors = new Color32[mVertices.Length];

        Debug.Log("Assigned " + mColors.Length + " vertex colors");

        for (int i = 0; i < mVertices.Length; i++)
        {
            Vector3 mCol = new Vector3(Mathf.Abs(mNormals[i][0]), Mathf.Abs(mNormals[i][1]), Mathf.Abs(mNormals[i][2]));
            mCol.Normalize();
            Color32 myColor = new Color32((byte)(mCol[0] * 255), (byte)(mCol[1] * 255), (byte)(mCol[2] * 255), (byte)(1.0f * 255));
            mColors[i] = myColor;
        }
        filter.mesh.colors32 = mColors;
        filter.mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {




    }
}