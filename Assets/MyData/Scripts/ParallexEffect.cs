using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    [SerializeField] private float parallexSpeed;

    private Transform camTransform;
    private Vector3 oldCamPos;
    private float spriteWidth;
    private PlayerInputController controller;

    private void Start()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        spriteWidth = sprite.texture.width / sprite.pixelsPerUnit;
        controller = PlayerInputController.Instance;
        camTransform = Camera.main.transform;
        oldCamPos = camTransform.localPosition;
        Debug.Log(spriteWidth + " for " + transform.name);
    }

    private void LateUpdate()
    {
        if (camTransform.localPosition != oldCamPos)
        {
            var delta = camTransform.localPosition.x - oldCamPos.x; // 11 - 10 = 1 || 10 - 11 = -1
            Vector3 targetPos = new Vector3(delta, transform.localPosition.y);
            transform.localPosition -= Vector3.Lerp(transform.localPosition, targetPos, parallexSpeed * Time.deltaTime);
            //transform.localPosition += targetPos * parallexSpeed;

            if (Mathf.Abs(transform.localPosition.x) - spriteWidth > 0)
            {
                transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
            }
            oldCamPos = camTransform.localPosition;
        }
    }
}
