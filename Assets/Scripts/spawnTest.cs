using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTest : MonoBehaviour
{
    private GameObject man;
    [SerializeField] private GameObject robotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            man = Instantiate(robotPrefab, new Vector3(0, 5, 0), new Quaternion(0, 0, 0, 0));
            man.transform.parent = transform;
            man.transform.localPosition = new Vector3(0, 0, 0);
        }
        
    }
}
