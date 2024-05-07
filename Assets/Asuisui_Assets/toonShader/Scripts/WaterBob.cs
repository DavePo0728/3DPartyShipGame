using UnityEngine;

public class WaterBob : MonoBehaviour
{
    [SerializeField]
    float height = 0.1f;

    [SerializeField]
    float period = 1;

    private Vector3 initialLocalPosition;
    private float offset;

    private void Awake()
    {
        initialLocalPosition = transform.localPosition;

        offset = 1 - (Random.value * 2);
    }

    private void Update()
    {
        transform.localPosition = initialLocalPosition - Vector3.up * Mathf.Sin((Time.time + offset) * period) * height;
    }
}
