// Developer Express Code Central Example:
// How to add a horizontal cells merge capability to an XtraTreeList control
// 
// This sample shows how to add a horizontal cell merge capability to an
// XtraTreeList control.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2501

using System.ComponentModel;
using DevExpress.Utils.Controls;
using DevExpress.XtraTreeList;

namespace TreeListCellMerging
{
	public class MyTreeListOptionsView : TreeListOptionsView
	{
		private bool allowHorzMerge;

		public MyTreeListOptionsView()
			: base()
		{
			allowHorzMerge = false;
		}

		[DefaultValue(false)]
		public bool AllowHorizontalMerge
		{
			get { return allowHorzMerge; }
			set
			{
				if ( allowHorzMerge == value )
					return;
				
				allowHorzMerge = value;
			}
		}

		public override void Assign(BaseOptions options)
		{
			base.Assign(options);
			MyTreeListOptionsView optView = options as MyTreeListOptionsView;
			if ( optView == null )
				return;

			this.allowHorzMerge = optView.AllowHorizontalMerge;
		}
	}
}
