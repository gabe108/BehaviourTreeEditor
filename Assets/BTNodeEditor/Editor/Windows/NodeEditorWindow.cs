using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BTNE
{ 
    public class NodeEditorWindow : EditorWindow
    {
        #region Variables
        private static NodeEditorWindow m_mainWindow;
        private static PropertyView m_propertyView;
        private static MainView m_mainView;

        private static float m_viewPercentage = 0.75f;        
        #endregion

        #region GettersAndSetters
        #endregion

        public static void InitEditorWindow()
        {
            m_mainWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
            m_mainWindow.titleContent = new GUIContent("BTNE");

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

            m_mainView.UpdateView(position, new Rect(0f, 0f, m_viewPercentage, 1f), e);
            m_propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
                                      new Rect(m_viewPercentage, 0f, 1f - m_viewPercentage, 1f), e);

            Repaint();
        }

        private static void ProcessEvents(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow)
                m_viewPercentage -= 0.01f;
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow)
                m_viewPercentage += 0.01f;
        }

        private static void CreateViews()
        {
            if (m_mainWindow != null)
            {
                m_mainView = new MainView();
                m_propertyView = new PropertyView();
            }
            else
            {
                m_mainWindow = EditorWindow.GetWindow<NodeEditorWindow>() as NodeEditorWindow;
                m_mainWindow.titleContent = new GUIContent("BTNE");

                m_mainView = new MainView();
                m_propertyView = new PropertyView();
            }
        }
    }
}
