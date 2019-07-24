using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BTNE
{
    [System.Serializable]
    public class ViewBase 
    {
        #region Variables
        [SerializeField] protected string m_viewTitle;

        [SerializeField] protected Rect m_viewRect;
        [SerializeField] protected NodeGraph m_currGraph;
        [SerializeField] protected GUISkin m_viewSkin;
        [SerializeField] protected Vector2 m_mousePosition;        
        #endregion

        #region GettersAndSetters
        public string GetViewTitle() { return m_viewTitle; }
        public Rect GetViewRect() { return m_viewRect; }
        public GUISkin GetViewSkin()
        {
            if (m_viewSkin == null)
                SetViewSkin();

            return m_viewSkin;
        }

        public void SetViewRect(Rect _viewRect) { m_viewRect = _viewRect; }
        public void SetViewSkin() { m_viewSkin = Resources.Load("GUISkins/EditorSkins/BTNE Editor Skin") as GUISkin; }
        #endregion

        public ViewBase(string _title)
        {
            m_viewTitle = _title;
        }

        public virtual void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e, NodeGraph _nodeGraph)
        {
            m_viewRect = new Rect(_editorRect.x * _percentageRect.x,
                                  _editorRect.y * _percentageRect.y,
                                  _editorRect.width * _percentageRect.width,
                                  _editorRect.height * _percentageRect.height);

            m_currGraph = _nodeGraph;

            if(m_currGraph != null)
            {
                m_viewTitle = m_currGraph.GetGraphName();
            }
            else
            {
                m_viewTitle = "No Graph";
            }

            GUI.Box(m_viewRect, m_viewTitle, GetViewSkin().GetStyle("BTNE Node Editor"));
        }

        protected virtual void DoMyWindow(int id)
        {
            
        }

        public virtual void ProcessEvents(Event _e)
        {

        }
    }
}