using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [SerializeField] private AudioClip coinCollectClip;

    private void OnDisable()
    {
        AudioSource.PlayClipAtPoint(coinCollectClip, transform.position);
    }
}
