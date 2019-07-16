using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BTNE
{
    [System.Serializable]
    public class NodeEditorWindow : EditorWindow
    {
        #region Variables
        [SerializeField] private NodeEditorWindow m_mainWindow;
        [SerializeField] private PropertyView m_propertyView;
        [SerializeField] private MainView m_mainView;
        [SerializeField] private NodeGraph m_currGraph;

        private static float m_viewPercentage = 0.75f;
        #endregion

        #region GettersAndSetters
        public NodeGraph GetCurrentGraph() { return m_currGraph; }

        public void SetCurrentGraph(NodeGraph _graph) { m_currGraph = _graph; }
        #endregion

        public void InitEditorWindow()
        {
            CreateViews();
        }
        
        // When you open the window
        private void OnEnable()
        {
            
        }

        // When we close the window
        private void OnDestroy()
        {
            
        }

        // 60-70 frames per second
        private void Update()
        {
            
        }

        // Twice every update
        private void OnGUI()
        {
            if (m_propertyView == null || m_mainView == null)
            {
                CreateViews();
                return;
            }

            Event e = Event.current;
            ProcessEvents(e);

            m_mainView.UpdateView(position, new Rect(0f, 0f, m_viewPercentage, 1f), e, m_currGraph);
            m_propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
                                      new Rect(m_viewPercentage, 0f, 1f - m_viewPercentage, 1f), e, m_currGraph);
            
            Repaint();
            EditorUtility.SetDirty(this);
        }

        private void ProcessEvents(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow)
                m_viewPercentage -= 0.01f;
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow)
                m_viewPercentage += 0.01f;
        }

        private void CreateViews()
        {
            if (m_mainWindow != null)
            {
                m_mainView = new MainView();
                m_propertyView = new PropertyView();
            }
            else
            {
                m_mainWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            }
        }
    }
}
