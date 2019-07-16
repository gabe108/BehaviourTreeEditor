using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] private string m_graphName;
        #endregion

        #region GettersAndSetters
        public string GetGraphName() { return m_graphName; }

        public void SetGraphName(string _name) { m_graphName = _name; }
        #endregion

        private void OnEnable()
        {
            if (m_nodes == null)
                m_nodes = new List<BaseNode>();
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

        }

        #if UNITY_EDITOR
        public void UpdateGraphView(Event _e, Rect _viewRect)
        {
            if(m_nodes.Count > 0)
            {
                ProcessEvents(_e, _viewRect);
                foreach(BaseNode node in m_nodes)
                {
                    node.UpdateNodeGUI(_e, _viewRect);
                }
            }

            EditorUtility.SetDirty(this);
        }
        #endif

        public void ProcessEvents(Event _e, Rect _viewRect)
        {
            if (_viewRect.Contains(_e.mousePosition))
            {

            }
        }
    }
}