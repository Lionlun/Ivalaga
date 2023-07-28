using System.Collections;
using UnityEngine;

public class PaperPlaneMovementPattern : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ObjectMovementPattern());
    }

    private void Update()
    {
        Vector2 v = rb.velocity;
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10f*Time.deltaTime);
    }

    private IEnumerator ObjectMovementPattern()
    {
        while(true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(1f, 5f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(4f);
        }
    }
}
