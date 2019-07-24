using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BTNE
{
    [System.Serializable]
    public class BaseNodeIO : ScriptableObject
    {
        public BaseNode m_holderNode;
        public bool m_isOccupied = false;
        public Rect m_IORect;
        public BaseNodeIO m_connectedTo;
        public ConnectionType m_type;

        public BaseNodeIO()
        {

        }

        public virtual void UpdateGUI(BaseNode _node, NodeEditorWindow _curWindow)
        {
            if (GUI.Button(m_IORect, new GUIContent("")))
            {
                NodeGraph graph = _curWindow.GetCurrentGraph();
                Event e = Event.current;
                if(e.button == 1)
                {
                    graph.SetIsMakingConnection(false);
                    graph.m_connectionFrom = null;
                    ProcessContextMenu(e, graph);
                    return;
                }

                if(graph.GetIsMakingConnection() && graph.GetConnectionType() != m_type)
                {
                    if(m_connectedTo != null)
                    {
                        m_connectedTo.m_isOccupied = false;
                        m_connectedTo.m_connectedTo = null;
                    }

                    Debug.Log("Connection made");
                    m_isOccupied = true;
                    m_connectedTo = graph.m_connectionFrom;
                    m_connectedTo.m_isOccupied = true;

                    m_connectedTo.m_isOccupied = true;
                    m_connectedTo.m_connectedTo = this;

                    graph.SetIsMakingConnection(false);
                    graph.m_connectionFrom = null;
                }
                else if (!_curWindow.GetCurrentGraph().GetIsMakingConnection())
                {
                    _curWindow.GetCurrentGraph().SetIsMakingConnection(true);
                    _curWindow.GetCurrentGraph().SetConnectionType(m_type);
                    _curWindow.GetCurrentGraph().SetConnectionStart(m_IORect.center);
                    _curWindow.GetCurrentGraph().m_connectionFrom = this;
                }

                AssetDatabase.AddObjectToAsset(this, m_holderNode);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void ProcessContextMenu(Event e, NodeGraph _currGraph)
        {
            GenericMenu menu = new GenericMenu();

            if (m_type == ConnectionType.OUTPUT)
                menu.AddItem(new GUIContent("Delete Output"), false, CallbackOnContextMenu, "1");

            menu.AddItem(new GUIContent("Delete Connection"), false, CallbackOnContextMenu, "0");
                        
            menu.ShowAsContext();
            e.Use();
        }

        private void CallbackOnContextMenu(object obj)
        {
            string str = obj.ToString();

            switch (str)
            {
                case "0":
                    DeleteConnections();
                    break;
                case "1":
                    int index = m_holderNode.m_outputs.IndexOf((NodeOutput)this);
                    m_holderNode.m_outputs.RemoveAt(index);
                    DeleteConnections();
                    break;

                default:
                    break;
            }

            AssetDatabase.AddObjectToAsset(this, m_holderNode);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void DeleteConnections()
        {
            if (m_connectedTo != null)
            {
                m_connectedTo.m_isOccupied = false;
                m_connectedTo.m_connectedTo = null;
                m_isOccupied = false;
                m_connectedTo = null;
            }
        }
    }
}
