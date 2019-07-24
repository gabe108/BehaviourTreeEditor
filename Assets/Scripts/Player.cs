using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTNE;

public class Player : MonoBehaviour
{
    [SerializeField] private NodeGraph m_behaviourTree;

    // Start is called before the first frame update
    void Start()
    {
        m_behaviourTree.m_actor = gameObject;
        //m_behaviourTree.InitGraph();
    }

    // Update is called once per frame
    void Update()
    {
        m_behaviourTree.UpdateGraph();
    }
}
