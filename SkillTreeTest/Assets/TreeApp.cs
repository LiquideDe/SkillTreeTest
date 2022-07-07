using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeApp : MonoBehaviour
{
    [SerializeField] private CircleShower shower;
    CircleWatcher circleWatcher;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        circleWatcher = new CircleWatcher(shower);
        circleWatcher.CreateTree();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
