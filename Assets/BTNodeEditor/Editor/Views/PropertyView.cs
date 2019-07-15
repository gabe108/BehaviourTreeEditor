using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    public class PropertyView : ViewBase
    {
        public PropertyView() : base("Property View")
        {

        }

        public override void UpdateView(Rect _editorRect, Rect _percentageRect, Event _e)
        {
            base.UpdateView(_editorRect, _percentageRect, _e);

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