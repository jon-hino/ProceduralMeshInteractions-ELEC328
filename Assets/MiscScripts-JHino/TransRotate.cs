using UnityEngine;

public class TransRotate : MonoBehaviour
{
    public float rotationspeed = 5f;
    float angle = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = angle + rotationspeed;
        transform.localEulerAngles = new Vector3(0, angle, 0);
    }
}
