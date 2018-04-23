// Developer Express Code Central Example:
// How to add a horizontal cells merge capability to an XtraTreeList control
// 
// This sample shows how to add a horizontal cell merge capability to an
// XtraTreeList control.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2501

using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Printing;

namespace TreeListCellMerging
{
    public delegate void AllowMergeRowCellsEventHandler(object sender, AllowMergeRowCellsEventArgs e);
    public delegate void AllowMergeColumnCellsEventHandler(object sender, AllowMergeColumnCellsEventArgs e);

    public class MyTreeList : TreeList
    {
        public MyTreeList()
            : base() {
            this.FocusedColumnChanged += MyTreeList_FocusedColumnChanged;
            this.FocusedNodeChanged += MyTreeList_FocusedNodeChanged;
        }


        void MyTreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            MovementUpDownInMergeColumn(e);
        }

        public TreeListColumn _OldColumn;
        public TreeListColumnCollection MergedColumns;

        void MyTreeList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (e.OldColumn == null || e.Column == null) return;
            ReturnIfBeyondRightBorder(e);

            if (e.OldColumn.VisibleIndex < e.Column.VisibleIndex && this.Columns[e.Column.VisibleIndex + 1] != null)
                JumpIfInMergedCell(e, 1);
            if (e.OldColumn.VisibleIndex >  e.Column.VisibleIndex && this.Columns[e.Column.VisibleIndex - 1] != null)
                JumpIfInMergedCell(e, -1);
        }

        private void MovementUpDownInMergeColumn(FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode != null && (this.FocusedColumn.VisibleIndex - 1) > 0)
            {
                if (Equals(e.Node.GetValue(this.GetColumnByVisibleIndex(this.FocusedColumn.VisibleIndex)), 
                    e.Node.GetValue(this.GetColumnByVisibleIndex(this.FocusedColumn.VisibleIndex - 1))))
                    this.FocusedColumn = this.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex - 1);
            }
        }
        
        private void JumpIfInMergedCell(FocusedColumnChangedEventArgs e, int step)
        {
            TreeListColumn nextColumn = GetColumnByVisibleIndex(e.Column.VisibleIndex - 1);
            if (Equals(this.FocusedNode.GetValue(e.Column), this.FocusedNode.GetValue(nextColumn)))
            {
                this.FocusedColumn = this.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex + step);
            }
        }

        private void ReturnIfBeyondRightBorder(FocusedColumnChangedEventArgs e)
        {
                if (this.FocusedNode.GetValue(e.Column).ToString() == this.FocusedNode.GetValue(e.OldColumn).ToString()
                                && this.Columns[e.Column.VisibleIndex + 1] == null)
                { this.FocusedColumn = e.OldColumn; }

        }

        public override void ShowEditor()
        {
            RowInfo ri = this.FocusedRow;
            CellInfo cell = ri[this.FocusedColumn];
            if (cell == null)
            {
                if (this.FocusedColumn.VisibleIndex == 0)
                    return;

                this.FocusedColumn = this.VisibleColumns[this.FocusedColumn.VisibleIndex - 1];
                base.ShowEditor();
                return;
            }

            base.ShowEditor();
        }

        protected override TreeListViewInfo CreateViewInfo()
        {
            return new MyTreeListViewInfo(this);
        }

        protected override TreeListPrinter CreatePrinter() {
            return new MyPrinter(this);
        }

        public event AllowMergeRowCellsEventHandler AllowMergeRowCells;
        protected internal void RaiseMergeRowCells(AllowMergeRowCellsEventArgs args)
        {
            if (AllowMergeRowCells != null)
                AllowMergeRowCells(this, args);
        }

        protected override TreeListOptionsView CreateOptionsView()
        {
            return new MyTreeListOptionsView();
        }

        public new MyTreeListViewInfo ViewInfo
        {
            get
            {
                return (MyTreeListViewInfo)base.ViewInfo;
            }
        }

        public override bool CanShowEditor
        {
            get
            {
                return base.CanShowEditor;
            }
        }

        [Description("Provides access to the tree list's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new MyTreeListOptionsView OptionsView
        {
            get
            {
                return base.OptionsView as MyTreeListOptionsView;
            }
        }

    }
    
}