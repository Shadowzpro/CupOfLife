using UnityEngine;

public class EyeTracker : MonoBehaviour
{
    private Arm arm;

    void Start()
    {
        arm = FindObjectOfType<Arm>();
    }

    void Update()
    {
        if (arm != null)
        {
            transform.LookAt(arm.transform);
        }
    }
}