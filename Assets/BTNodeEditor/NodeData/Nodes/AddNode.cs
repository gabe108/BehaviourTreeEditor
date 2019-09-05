using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BTNE
{
    [System.Serializable]
    public class AddNode : BaseNode
    {
        #region Variables
        public float m_nodeValue;

		#endregion

		#region GettersAndSetters
		#endregion

#if UNITY_EDITOR
		public override void InitNode()
        {
            base.InitNode();
            m_nodeType = NodeType.ADD_NODE;
            m_nodeRect = new Rect(50f, 50f, 150f, 150f);
        }
#endif

        public override NodeStates Evaluate()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateNode(Event _e, Rect _viewRect)
        {
            base.UpdateNode(_e, _viewRect);
        }

#if UNITY_EDITOR
		public override void UpdateNodeGUI(Event _e, Rect _viewRect, GUISkin _skin)
        {
            base.UpdateNodeGUI(_e, _viewRect, _skin);
        }
#endif
	}
}