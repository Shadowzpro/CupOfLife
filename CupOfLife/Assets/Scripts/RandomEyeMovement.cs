using UnityEngine;

public class RandomEyeMovement : MonoBehaviour
{
    [Range(0.2f, 1f)]
    public float speed = 1f;
    private float elapsed = 0.0f;

    private Vector3 orig;
    private Vector3 target;

    void Start()
    {
        orig = transform.eulerAngles;
        RandomiseTarget();
    }

    void Update()
    {
        elapsed += Time.deltaTime * speed;
        transform.eulerAngles = Vector3.Lerp(orig, target, elapsed);

        if(elapsed >= 1)
        {
            orig = target;
            elapsed = 0;
            RandomiseTarget();
        }
    }

    private void RandomiseTarget()
    {
        int targetRange1 = Random.Range(-45, 45);
        int targetRange2 = Random.Range(-45, 45);
        int targetRange3 = Random.Range(-45, 45);

        target = new Vector3(targetRange1, targetRange2, targetRange3);
    }
}