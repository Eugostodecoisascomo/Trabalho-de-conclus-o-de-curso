using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject aim;
    private Vector2 dir;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float offset = 2;

    void FixedUpdate()
    {
        dir = (aim.transform.position + Vector3.up * offset)  - transform.position;
        transform.position += new Vector3(dir.x, dir.y, 0) * Time.deltaTime * speed;
    }
}
