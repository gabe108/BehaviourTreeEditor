using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTNE
{
    [System.Serializable]
    public class PropertyView : ViewBase
    {
        public PropertyView() : base("Property View")
        {

        }

        public override void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e, NodeGraph _nodeGraph)
        {
            base.UpdateView(_editorRect, _percentageRect, _e, _nodeGraph);

            ProcessEvents(_e);
        }

        public override void ProcessEvents(Event _e)
        {
            base.ProcessEvents(_e);

            if (GetViewRect().Contains(_e.mousePosition))
            {

            }
        }
    }
}