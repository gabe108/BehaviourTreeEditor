using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTNE;

public class Agent : MonoBehaviour
{
    [SerializeField] private NodeGraph m_behaviourTree;
	[HideInInspector] public bool m_hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        m_behaviourTree.m_actor = gameObject;
		m_behaviourTree.m_active = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (m_behaviourTree.m_active)
		{
			m_behaviourTree.m_player = this;
			m_behaviourTree.UpdateGraph();
		}
    }
}
