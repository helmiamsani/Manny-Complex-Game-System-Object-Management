using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector3 spawnPoint
    {
        get
        {
            return Random.insideUnitSphere * 5f;
        }
    }
}
