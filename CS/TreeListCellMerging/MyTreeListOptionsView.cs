using DevExpress.Utils.Controls;
using DevExpress.XtraTreeList;
using System.ComponentModel;

namespace TreeListCellMerging
{
    public class MyTreeListOptionsView : TreeListOptionsView
    {
        private bool allowHorzMerge;

        public MyTreeListOptionsView(TreeList treeList)
            : base(treeList)
        {
            allowHorzMerge = false;
        }

        public override void Assign(BaseOptions options)
        {
            base.Assign(options);
            MyTreeListOptionsView optView = options as MyTreeListOptionsView;
            if(optView == null)
                return;

            this.allowHorzMerge = optView.AllowHorizontalMerge;
        }

        [DefaultValue(false)]
        public bool AllowHorizontalMerge {
            get { return allowHorzMerge; }
            set {
                if(allowHorzMerge == value)
                    return;

                allowHorzMerge = value;
            }
        }
    }
}
