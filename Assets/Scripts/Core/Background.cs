using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float backgroundSpeed;
    private float smoothSpeed = 0.125f;
    float targetY;

    // Update is called once per frame
    void LateUpdate()
    {
        //Background movement
        float targetX = player.position.x * backgroundSpeed + 2.5f;
        if (player.position.y > 6f)
            targetY = player.position.y - (player.position.y * 0.1f);
        else
            targetY = 2.57f;

        Vector3 desired = new Vector3(
            targetX,
            targetY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed);
    }

}

