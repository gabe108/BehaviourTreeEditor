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
        public float zoomScale = 1.0f;
        public Vector2 vanishingPoint = new Vector2(0, 21);
        private Vector2 _zoomCoordsOrigin;

        public MainView() : base("Main View")
        {

        }

        public override void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e, NodeGraph _nodeGraph)
        {
            base.UpdateView(_editorRect, _percentageRect, _e, _nodeGraph);

            PanningAndZoomUtils.Begin(zoomScale, m_viewRect);

            if (m_currGraph != null)
            {
                m_currGraph.UpdateGraphView(_e, GetViewRect(), GetViewSkin());
            }
            
            PanningAndZoomUtils.End();

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

                if (_e.type == EventType.MouseDrag &&
                   (_e.button == 0 && _e.modifiers == EventModifiers.Alt) ||
                    _e.button == 2)
                {
                    Vector2 delta = _e.delta;

                    foreach(BaseNode node in m_currGraph.m_nodes)
                    {
                        Rect temp = node.GetNodeRect();
                        temp.x += delta.x;
                        temp.y += delta.y;
                        node.SetNodeRect(temp);
                    }
                }

                if (_e.type == EventType.ScrollWheel)
                {
                    zoomScale += _e.delta.x * 0.01f;
                    zoomScale += _e.delta.y * 0.01f;
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
                menu.AddItem(new GUIContent("Add Node/Selector"), false, CallbackOnContextMenu, "3");
                menu.AddItem(new GUIContent("Add Node/Sequence"), false, CallbackOnContextMenu, "4");
                menu.AddItem(new GUIContent("Add Node/Invertor"), false, CallbackOnContextMenu, "5");
                menu.AddItem(new GUIContent("Add Node/Action Node"), false, CallbackOnContextMenu, "6");
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
                    NodeUtils.CreateNode(m_currGraph, NodeType.SELECTOR_NODE, m_mousePosition);
                    break;

                case "4":
                    NodeUtils.CreateNode(m_currGraph, NodeType.SEQUENCE_NODE, m_mousePosition);
                    break;

                case "5":
                    NodeUtils.CreateNode(m_currGraph, NodeType.INVERTER_NODE, m_mousePosition);
                    break;

                case "6":
                    NodeUtils.CreateNode(m_currGraph, NodeType.ACTION_NODE, m_mousePosition);
                    break;

                default:
                    break;
            }
        }
    }
}