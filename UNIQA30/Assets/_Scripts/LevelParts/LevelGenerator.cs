using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> partPrefabs;
    public List<GameObject> parts = new List<GameObject>();

    public Transform lastPart;
    public Vector3 nextPartOffset;

    public void NextPart()
    {
        int randomPartNumber = Random.Range(0, partPrefabs.Count);
        GameObject newPart = Instantiate(partPrefabs[randomPartNumber], transform);
        newPart.transform.position = lastPart.position + nextPartOffset;
        lastPart = newPart.transform;
        parts.Add(newPart);
        if (parts.Count > 4)
        {
            Destroy(parts[0].gameObject);
            parts.RemoveAt(0);
        }
    }
}
