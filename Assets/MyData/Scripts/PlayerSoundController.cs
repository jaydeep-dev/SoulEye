using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dieClip;

    private PlayerInputController inputController;
    private PlayerMovement movementController;

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        movementController = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (inputController.IsJumped && movementController.IsGrounded)
            AudioSource.PlayClipAtPoint(jumpClip, transform.position, .3f);
    }

    private void OnDisable()
    {
        AudioSource.PlayClipAtPoint(dieClip, transform.position);
    }
}
