using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [SerializeField] private AudioClip coinCollectClip;

    public void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(coinCollectClip, transform.position);
        gameObject.SetActive(false);
    }
}
