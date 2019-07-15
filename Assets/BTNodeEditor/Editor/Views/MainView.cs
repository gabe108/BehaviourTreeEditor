﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTNE
{
    public class MainView : ViewBase
    {
        public MainView() : base("Main View")
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

                    }
                }
            }
        }
    }
}