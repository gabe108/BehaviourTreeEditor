using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BTNE
{
    [System.Serializable]
    public class BaseNode : ScriptableObject
    {
        #region Variables
        [SerializeField] protected string m_nodeName = "New Node";

        [SerializeField] protected NodeGraph m_parentGraph;
        [SerializeField] protected GUISkin m_nodeSkin;
        [SerializeField] protected Rect m_nodeRect;
        [SerializeField] protected NodeType m_nodeType;
        #endregion

        #region GettersAndSetters
        public void SetNodeRect(int _x, int _y, int _width, int _height)
        {
            m_nodeRect.x = _x;
            m_nodeRect.y = _y;
            m_nodeRect.width = _width;
            m_nodeRect.height = _height;
        }

        public void SetParentGraph(NodeGraph _parentGraph) { m_parentGraph = _parentGraph; }

        public NodeType GetNodeType() { return m_nodeType; }
        public Rect GetNodeRect() { return m_nodeRect; }
        #endregion

        public virtual void InitNode()
        {

        }

        public virtual void UpdateNode(Event _e, Rect _viewRect)
        {
            ProcessEvents(_e, _viewRect);
        }

        #if UNITY_EDITOR
        public virtual void UpdateNodeGUI(Event _e, Rect _viewRect)
        {
            ProcessEvents(_e, _viewRect);

            GUI.Box(m_nodeRect, m_nodeName);

            EditorUtility.SetDirty(this);
        }
#endif

        public void ProcessEvents(Event _e, Rect _viewRect)
        {
            if (_e.type == EventType.MouseDrag)
            {
                if (_viewRect.Contains(_e.mousePosition))
                {
                    if (m_nodeRect.Contains(_e.mousePosition))
                    {
                        m_nodeRect.x += _e.delta.x;
                        m_nodeRect.y += _e.delta.y;
                    }
                }
            }        
        }
    }
}