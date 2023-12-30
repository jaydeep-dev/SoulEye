using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private Transform startPos = null;
    [SerializeField] private Transform targetPos = null;
    private bool isReached = false;

    private void FixedUpdate()
    {
        if (transform.position == startPos.position)
            isReached = false;
        else if (transform.position == targetPos.position)
            isReached = true;

        if (isReached)
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.fixedDeltaTime);
        else if (!isReached)
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.fixedDeltaTime);
    }
}