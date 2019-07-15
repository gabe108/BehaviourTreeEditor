using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{
    public class ViewBase 
    {
        #region Variables
        private string m_viewTitle;
        private Rect m_viewRect;

        protected GUISkin m_viewSkin;        
        #endregion

        #region GettersAndSetters
        public string GetViewTitle() { return m_viewTitle; }
        public Rect GetViewRect() { return m_viewRect; }
        public GUISkin GetViewSkin() { return m_viewSkin; }

        public void SetViewRect(Rect _viewRect) { m_viewRect = _viewRect; }
        public void SetViewSkin() { m_viewSkin = Resources.Load("GUISkins/EditorSkins/BTNE Editor Skin") as GUISkin; }
        #endregion

        public ViewBase(string _title)
        {
            m_viewTitle = _title;
            SetViewSkin();
        }

        public virtual void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e)
        {
            m_viewRect = new Rect(_editorRect.x * _percentageRect.x,
                                  _editorRect.y * _percentageRect.y,
                                  _editorRect.width * _percentageRect.width,
                                  _editorRect.height * _percentageRect.height);

            GUI.Box(m_viewRect, m_viewTitle, GetViewSkin().GetStyle("BTNE Node Editor"));
        }

        public virtual void ProcessEvents(Event _e) { }
    }
}