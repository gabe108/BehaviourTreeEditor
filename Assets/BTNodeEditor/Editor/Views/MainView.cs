using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{
    [System.Serializable]
    public class MainView : ViewBase
    {
        public MainView() : base("Main View")
        {

        }

        public override void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e, NodeGraph _nodeGraph)
        {
            base.UpdateView(_editorRect, _percentageRect, _e, _nodeGraph);
            
            if (m_currGraph != null)
            {
                m_currGraph.UpdateGraphView(_e, GetViewRect(), GetViewSkin());
            }

            ProcessEvents(_e);
        }

        public override void ProcessEvents(Event _e)
        {
            base.ProcessEvents(_e);

            if (GetViewRect().Contains(_e.mousePosition))
            {
                if(_e.button == 0)
                {
                    if(_e.type == EventType.MouseUp)
                    {

                    }

                    if(_e.type == EventType.MouseDown)
                    {

                    }

                    if(_e.type == EventType.MouseDrag)
                    {

                    }
                }

                if(_e.button == 1)
                {
                    if(_e.type == EventType.MouseDown)
                    {
                        m_mousePosition = _e.mousePosition;
                        ProcessContextMenu(_e);
                    }
                }
            }
        }

        private void ProcessContextMenu(Event e)
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("New Graph"), false, CallbackOnContextMenu, "0");
            menu.AddItem(new GUIContent("Load Graph"), false, CallbackOnContextMenu, "1");

            if(m_currGraph != null)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("UnLoad Graph"), false, CallbackOnContextMenu, "2");

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Add Node"), false, CallbackOnContextMenu, "3");
            }

            menu.ShowAsContext();
            e.Use();
        }

        private void CallbackOnContextMenu(object obj)
        {
            switch (obj.ToString())
            {
                case "0":
                    NodePopupWindow.InitPopupWindow();
                    break;
                case "1":
                    NodeUtils.LoadGraph();
                    break;
                case "2":
                    NodeUtils.UnloadGraph();
                    break;

                case "3":
                    NodeUtils.CreateNode(m_currGraph, NodeType.ADD_NODE, m_mousePosition);
                    break;

                default:
                    break;
            }
        }
    }
}