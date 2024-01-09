using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    [SerializeField] private bool isLeftScroll;
    [SerializeField] private float parallexSpeed;

    private float spriteWidth;
    private PlayerInputController playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        var sprite = GetComponent<SpriteRenderer>().sprite;
        spriteWidth = sprite.texture.width / sprite.pixelsPerUnit;

        Debug.Log(spriteWidth);

        if (isLeftScroll )
        {
            parallexSpeed = -parallexSpeed;
        }
    }

    private void Update()
    {
        transform.localPosition += (playerInput.Horizontal * parallexSpeed * Time.deltaTime * Vector3.right);

        if(Mathf.Abs(transform.localPosition.x) - spriteWidth > 0)
        {
            transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
