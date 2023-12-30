using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] chunks = null;

    private void OnEnable()
    {
        foreach (var chunk in chunks)
        {
            chunk.SetActive(false);
        }
        byte random = (byte)Random.Range(0, chunks.Length);
        chunks[random].SetActive(true);
        Debug.Log(chunks[random].activeInHierarchy);
        Debug.Log(random);
    }
}
