using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
