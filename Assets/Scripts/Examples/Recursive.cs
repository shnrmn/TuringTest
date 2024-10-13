using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveAllChildren(Transform parent, bool active)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(active);
            SetActiveAllChildren(child, active);
        }
    }
}
