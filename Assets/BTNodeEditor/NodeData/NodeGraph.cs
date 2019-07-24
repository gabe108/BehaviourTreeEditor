using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace BTNE
{
    [System.Serializable]
    public class NodeGraph : ScriptableObject
    {
        #region Variables
        public List<BaseNode> m_nodes;
        public BaseNodeIO m_connectionFrom;
        public List<Connection> m_connections;
        public GameObject m_actor;

        [SerializeField] private BaseNode m_selectedNode;
        [SerializeField] private string m_graphName;
        [SerializeField] private bool m_isMakingConnection;
        [SerializeField] private Vector3 m_connectionStart;
        [SerializeField] private ConnectionType m_connectorType;
        #endregion

        #region GettersAndSetters
        public string GetGraphName() { return m_graphName; }
        public BaseNode GetSelectedNode() { return m_selectedNode; }
        public bool GetIsMakingConnection() { return m_isMakingConnection; }
        public Vector3 GetConnectionStart() { return m_connectionStart; }
        public ConnectionType GetConnectionType() { return m_connectorType; }

        public void SetIsMakingConnection(bool _isMakingConnection) { m_isMakingConnection = _isMakingConnection; }
        public void SetConnectionStart(Vector3 _connectionStart) { m_connectionStart = _connectionStart; }
        public void SetConnectionType(ConnectionType _connectorType) { m_connectorType = _connectorType; }
        public void SetGraphName(string _name) { m_graphName = _name; }
        #endregion

        private void OnEnable()
        {
            if (m_nodes == null)
                m_nodes = new List<BaseNode>();

            if (m_connections == null)
                m_connections = new List<Connection>();
        }

        public void InitGraph()
        {
            if(m_nodes.Count > 0)
            {
                foreach(BaseNode node in m_nodes)
                {
                    node.InitNode();
                }
            }
        }

        public void UpdateGraph()
        {
            if(m_nodes.Count > 0)
                m_nodes[0].Evaluate();
        }

        #if UNITY_EDITOR
        public void UpdateGraphView(Event _e, Rect _viewRect, GUISkin _viewSkin)
        {
            if(m_nodes.Count > 0)
            {
                ProcessEvents(_e, _viewRect);
                NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
                
                if (curWindow != null)
                {
                    curWindow.BeginWindows();
                    foreach (BaseNode node in m_nodes)
                    {
                        node.UpdateNodeGUI(_e, _viewRect, _viewSkin);
                    }
                    curWindow.EndWindows();
                }
            }

            if (m_isMakingConnection)
                Handles.DrawLine(m_connectionStart, _e.mousePosition);

            if (m_connections.Count > 0)
            {
                foreach (Connection con in m_connections)
                    Handles.DrawLine(con.output.m_IORect.center, con.input.m_IORect.center);
            }

            EditorUtility.SetDirty(this);
        }
        #endif

        public void ProcessEvents(Event _e, Rect _viewRect)
        {

        }
    }

    [System.Serializable]
    public class Connection : ScriptableObject
    {
        public NodeOutput output;
        public NodeInput input;
    }
}