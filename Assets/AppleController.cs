using UnityEngine;
using System.Collections.Generic;

public class AppleController : MonoBehaviour
{

    public int appleCount = 50;
    public float radius = 20f;
    //public float strength = 10f;

    //public AudioSource explosion;
    //public ParticleSystem MassiveImpact;

    public GameObject applePrefabOne;
    public GameObject applePrefabTwo;
    public GameObject applePrefabThree;
    //public GameObject applePrefabFour;
    //public GameObject applePrefabFive;
    //public GameObject applePrefabSix;
    //public GameObject applePrefabSeven;

    private List<GameObject> appleList = new List<GameObject>();
    private Transform parent;
    private int index = -1;

    private List<GameObject> prefabList = new List<GameObject>();


    public float appleSpacingNoiseVariance = 1.0f;

    public float appleScale = 1f; // size multiplier
    private bool randomizeScale = false; //if you want to turn off scale entirely
    public float scaleVariance = 1f;

    


    //public float explosionSize = 2f;

    void Start()
    {
        parent = transform;

        // Add all prefabs to list
        prefabList.Add(applePrefabOne);
        prefabList.Add(applePrefabTwo);
        prefabList.Add(applePrefabThree);
        //prefabList.Add(applePrefabFour);
        //prefabList.Add(applePrefabFive);
        //prefabList.Add(applePrefabSix);
        //prefabList.Add(applePrefabSeven);

        Spawnapples();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    if (appleList.Count > 0)
        //    {
        //        index = Random.Range(0, appleList.Count);
        //        GameObject apple = appleList[index];

        //        Rigidbody rb = apple.GetComponent<Rigidbody>();
        //        if (rb != null) //always check
        //        {
        //            rb.isKinematic = false; // NOW it's affected by physics
        //        }

        //        Debug.Log($"Pulling apple index: {index}");
        //    }
        //}

        //if (index != -1 && index < appleList.Count)
        //{
        //    GameObject apple = appleList[index];
        //    if (apple != null)
        //    {
        //        Rigidbody rb = apple.GetComponent<Rigidbody>();
        //        if (rb != null && !rb.isKinematic)
        //        {
        //            Vector3 direction = (transform.position - apple.transform.position).normalized;
        //            rb.AddForce(direction * strength * 10f, ForceMode.Acceleration);
        //        }
        //    }
        //}
    }

    void Spawnapples()
    {
        float angleStep = 360f / appleCount;

        for (int i = 0; i < appleCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = 0;

            Vector3 noise = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.01f, 0.01f), //not much change in y
            Random.Range(-1f, 1f)
            ) * appleSpacingNoiseVariance;

            Vector3 position = new Vector3(x, y, z) + noise;

            GameObject prefab = prefabList[Random.Range(0, prefabList.Count)];
            GameObject apple = Instantiate(prefab, parent);
            apple.transform.localPosition = position;

            if (randomizeScale)
            {
                float variation = Random.Range(-scaleVariance, scaleVariance);
                float scale = appleScale + variation;
                apple.transform.localScale = Vector3.one * Mathf.Max(scale, 0.1f); // prevent scale < 0
            }
            else
            {
                //apple.transform.localScale = Vector3.one * appleScale;
            }

            Rigidbody rb = apple.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.useGravity = false;
                //rb.isKinematic = true;
            }

            appleList.Add(apple); //and then repeat this 50 times!
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (index == -1 || appleList.Count == 0) return; //always assume the worst :)

    //    GameObject collided = collision.gameObject;

    //    if (collided == appleList[index]) //instead of compare tag as "apple"
    //    {
    //        if (explosion != null) explosion.Play();

    //        if (MassiveImpact != null) // just to make sure it exists
    //        {
    //            Vector3 contactPoint = collision.contacts[0].point;
    //            Quaternion rotation = Quaternion.identity;

    //            ParticleSystem impact = Instantiate(MassiveImpact, contactPoint, rotation, transform); // Parent to Earth
    //            impact.transform.localScale = Vector3.one * explosionSize;
    //            impact.Play();
    //            Destroy(impact.gameObject, 3f);
    //        }

    //        Destroy(collided);
    //        appleList.RemoveAt(index);
    //        index = -1;
    //    }
    //}

    //private void OnCollisionEnter2(Collision collision) //used for old apple explosion
    //{
    // Color randomlySelectedColor = GetRandomColor();
    // GetComponent<Renderer>().material.color = randomlySelectedColor;
    // ParticleSystem anim = Instantiate(MassiveImpact, collision.contacts[0].point, Quaternion.identity);
    // Destroy(anim.gameObject, 3f);
    //}


}