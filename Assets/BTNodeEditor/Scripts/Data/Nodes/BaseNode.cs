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
    public class BaseNode : ScriptableObject
    {
        #region Variables
        [SerializeField] protected string m_nodeName = "New Node";
        [SerializeField] protected NodeGraph m_parentGraph;
        [SerializeField] protected GUISkin m_nodeSkin;
        [SerializeField] protected Rect m_nodeRect = new Rect(0, 0, 0, 0);
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
        public virtual void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            ProcessEvents(_e, _viewRect);

            NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            if (curWindow != null)
            {
                int index = curWindow.GetCurrentGraph().m_nodes.IndexOf(this);
                
                m_nodeRect = GUI.Window(index, m_nodeRect, DoMyWindow, m_nodeName);
            }

            EditorUtility.SetDirty(this);
        }

        private void DoMyWindow(int id)
        {            
            GUI.DragWindow();
        }
        #endif

        public void ProcessEvents(Event _e, Rect _viewRect)
        {
           
        }
    }
}