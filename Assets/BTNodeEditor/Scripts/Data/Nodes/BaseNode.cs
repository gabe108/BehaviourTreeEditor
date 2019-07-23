using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BTNE
{
    public enum NodeStates
    {
        FAILURE,
        SUCCESS,
        RUNNING,

        COUNT
    }

    public enum ConnectionType
    {
        OUTPUT,
        INPUT,

        COUNT
    }

    [System.Serializable]
    public abstract class BaseNode : ScriptableObject
    {
        #region Variables
        public List<NodeInput> m_inputs;
        public List<NodeOutput> m_outputs;

        [SerializeField] protected NodeStates m_nodeState;
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
        public void SetNodeRect(Rect _rect) { m_nodeRect = _rect; }
        public void SetParentGraph(NodeGraph _parentGraph) { m_parentGraph = _parentGraph; }

        public NodeType GetNodeType() { return m_nodeType; }
        public Rect GetNodeRect() { return m_nodeRect; }
        public NodeStates GetNodeState() { return m_nodeState; }
        #endregion

        #region Delegates
        public delegate NodeStates NodeReturn();
        #endregion

        public virtual void InitNode()
        {
            m_inputs = new List<NodeInput>();

            if (m_nodeType != NodeType.ROOT_NODE)
                m_inputs.Add(new NodeInput());

            m_outputs = new List<NodeOutput>();
            m_outputs.Add(new NodeOutput());
        }

        public abstract NodeStates Evaluate();

        public virtual void UpdateNode(Event _e, Rect _viewRect)
        {
            ProcessEvents(_e);
        }

        #if UNITY_EDITOR
        public virtual void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            NodeEditorWindow curWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            if (curWindow != null)
            {
                int index = curWindow.GetCurrentGraph().m_nodes.IndexOf(this);
                
                m_nodeRect = GUI.Window(index, m_nodeRect, DoMyWindow, m_nodeName);
            }

            ProcessEvents(Event.current);

            foreach (NodeInput input in m_inputs)
                input.UpdateGUI(this, curWindow);

            foreach (NodeOutput output in m_outputs)
                output.UpdateGUI(this, curWindow);

            EditorUtility.SetDirty(this);
        }

        private void DoMyWindow(int id)
        {            
            GUI.DragWindow();
        }
        #endif

        public void ProcessEvents(Event _e)
        {
            if (m_nodeRect.Contains(_e.mousePosition))
            {
                if (_e.type == EventType.Layout && _e.button == 1)
                {
                    ProcessContextMenu(_e);
                }
            }
        }

        private void ProcessContextMenu(Event _e)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Add Child"), false, CallbackOnContextMenu, "0");
            menu.AddItem(new GUIContent("Delete All Children"), false, CallbackOnContextMenu, "1");

            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Output"), false, CallbackOnContextMenu, "2");

            menu.ShowAsContext();
        }

        private void CallbackOnContextMenu(object obj)
        {
            string str = obj.ToString();
            switch (str)
            {
                case "0":
                    break;

                case "1":
                    break;

                case "2":
                    m_outputs.Add(new NodeOutput());
                    break;

                default:
                    break;
            }
        }
    }   
}