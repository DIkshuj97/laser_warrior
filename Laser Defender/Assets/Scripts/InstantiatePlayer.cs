using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }
}
