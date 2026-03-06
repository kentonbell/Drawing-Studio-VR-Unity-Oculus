using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
public class AppleScript : MonoBehaviour
{
    public MeshFilter filter;
    private Vector3[] originalVertices, mVertices, mNormals;
    private Color32[] mColors;

    public string appleType = "Red"; // Use "Red", "Yellow", or "Green"
    private int hitsCount;
    public int hitsToDestroy = 10;

    public bool fallSporatic = false;

    void Start()
    {
        Debug.LogError("Apple script started");

        filter = GetComponent<MeshFilter>();
        if (filter == null || filter.mesh == null)
        {
            Debug.LogError("MeshFilter or mesh is missing!");
            return;
        }

        //filter.mesh = Instantiate(filter.mesh);

        originalVertices = filter.mesh.vertices;
        mVertices = filter.mesh.vertices;
        mNormals = filter.mesh.normals;
        mColors = new Color32[mVertices.Length];

     

        //for (int i = 0; i < mColors.Length; i++)
        //    mColors[i] = new Color32(0, 0, 0, 0);


        

        for (int i = 0; i < mVertices.Length; i++)
        {
            // Transparent black = nothing visible in second material
            mColors[i] = new Color32(0, 0, 0, 0);
        }
        //filter.mesh.colors32 = mColors;



        filter.mesh.colors32 = mColors;
        filter.mesh.RecalculateNormals();


        if (!fallSporatic)
        {
            StartCoroutine(ReduceVelocityOverTime(27f));
        }

    }

    public void DeformAtPoint(Vector3 hitPoint)
    {

        Debug.LogError("Trying to deform");
        List<int> affected = new List<int>();
        float deformRadius = appleType == "Red" ? 0.2f : appleType == "Yellow" ? 0.1f : 0f; //nice little ternary

        if (deformRadius == 0f)
            return; // green apples are stiff

        for (int i = 0; i < mVertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(mVertices[i]);
            if (Vector3.Distance(worldVertex, hitPoint) <= deformRadius)
            {
                affected.Add(i);
            }
        }


        //for (int i = 0; i < deformedList.Count; i++)
        //{
        //    int index = deformedList[i];
        //    mVertices[index] = mVertices[index] - mNormals[index]; // deform
        //    mColors[index] = new Color32(255, 255, 255, 255); // opaque white for deform
        //}
       


        foreach (int index in affected)
        {
            mVertices[index] -= mNormals[index] * 0.01f;
            mColors[index] = new Color32(255, 255, 255, 255);
        }

        filter.mesh.vertices = mVertices;
        filter.mesh.colors32 = mColors;
        filter.mesh.RecalculateNormals();


        //////optional for eating the whole apple.
        hitsCount++;
        if (hitsCount >= hitsToDestroy)
        {
            Destroy(gameObject);
        }/////////

        Debug.LogError("Deformed");
    }


   

    void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.LogError("Bird pecking physciall!!!");
            // Apply deformation at the contact point
            ContactPoint contact = collision.contacts[0];
            DeformAtPoint(contact.point);
            // Optionally, count hits and destroy after several pecks
            hitsCount++;
            if (hitsCount >= hitsToDestroy)
            {
                Destroy(gameObject);
            }
        }


        if (fallSporatic)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                StartCoroutine(ReduceVelocityOverTime(3f));
            }
        }
    }

    private IEnumerator ReduceVelocityOverTime(float duration)
    {
        Debug.LogError("Reducing velocity over time!");
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 initialVelocity = rb.velocity;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rb.velocity = Vector3.Lerp(initialVelocity, Vector3.zero, t);
            yield return null;
        }

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        Debug.LogError("Reduced velocity over time!!!!!");
    }
}