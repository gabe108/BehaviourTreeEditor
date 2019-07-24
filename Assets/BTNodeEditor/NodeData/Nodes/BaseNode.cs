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
        public string m_details;

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

        public string GetNodeName() { return m_nodeName; }
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
            {
                NodeInput input = new NodeInput();
                input.m_holderNode = this;
                
                m_inputs.Add(input);
            }

            m_outputs = new List<NodeOutput>();
            NodeOutput output = new NodeOutput();
            output.m_holderNode = this;

            m_outputs.Add(output);
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

                if(index == 0)
                    GUI.color = Color.blue;
                else
                    GUI.color = Color.white;

                m_nodeRect = GUI.Window(index, m_nodeRect, DoMyWindow, m_nodeName);
            }

            ProcessEvents(Event.current);

            GUI.color = Color.white;
            foreach (NodeInput input in m_inputs)
            {
                if(input != null)
                    input.UpdateGUI(this, curWindow);
            }
            foreach (NodeOutput output in m_outputs)
            {
                if(output != null)
                    output.UpdateGUI(this, curWindow);
            }
            EditorUtility.SetDirty(this);
        }

        private void DoMyWindow(int id)
        {
            //m_parentGraph.SetSelectedNode(this);
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                if (m_parentGraph != null)
                    m_parentGraph.SetSelectedNode(this);
            }
            GUI.DragWindow();
        }
        #endif

        public void ProcessEvents(Event _e)
        {
            if (m_nodeRect.Contains(_e.mousePosition))
            {
                NodeGraph graph = (EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow).GetCurrentGraph();
                if (_e.type == EventType.Layout && _e.button == 1)
                {                    
                    if (graph != null)
                        graph.SetIsMakingConnection(false);

                    ProcessContextMenu(_e);
                }
            }
        }

        private void ProcessContextMenu(Event _e)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Delete Node"), false, CallbackOnContextMenu, "0");
            menu.AddItem(new GUIContent("Mark this as Root Node"), false, CallbackOnContextMenu, "1");

            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Output"), false, CallbackOnContextMenu, "2");

            menu.ShowAsContext();
        }

        private void CallbackOnContextMenu(object obj)
        {
            string str = obj.ToString();
            List<BaseNode> nodes;
            int index = 0;
            switch (str)
            {
                case "0":
                    nodes = m_parentGraph.m_nodes;
                    index = nodes.IndexOf(this);
                    //if(nodes[index].)
                    m_parentGraph.m_nodes.RemoveAt(index);
                    break;

                case "1":
                    nodes = m_parentGraph.m_nodes;
                    index = nodes.IndexOf(this);
                    BaseNode tmp = nodes[0];
                    nodes[0] = nodes[index];
                    nodes[index] = tmp;
                    m_parentGraph.m_nodes = nodes;
                    break;

                case "2":
                    NodeOutput output = new NodeOutput();
                    output.m_holderNode = this;

                    m_outputs.Add(output);
                    break;

                default:
                    break;
            }
        }

        public void mDebug()
        {
            Debug.Log(m_details);
        }
    }   
}