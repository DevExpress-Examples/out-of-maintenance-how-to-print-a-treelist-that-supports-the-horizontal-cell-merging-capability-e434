using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using System.Drawing;

namespace TreeListCellMerging
{
    public class MyTreeListViewInfo : TreeListViewInfo
    {
        public MyTreeListViewInfo(TreeList fTreeList)
            : base(fTreeList)
        {
        }

        protected override void CalcRowCellsInfo(RowInfo ri)
        {
            base.CalcRowCellsInfo(ri);
            if(!((MyTreeList)TreeList).OptionsView.AllowHorizontalMerge)
                return;

            if(TreeList.OptionsSelection.EnableAppearanceFocusedRow)
                TreeList.OptionsSelection.EnableAppearanceFocusedRow = false;

            for(int i = ri.Cells.Count - 1; i > 0; i--)
            {
                string prevDisplayText = GetCellDisplayText(ri, i - 1);
                string currDisplayText = GetCellDisplayText(ri, i);

                if(prevDisplayText == currDisplayText)
                {
                    CellInfo prevCell = ri.Cells[i - 1];
                    CellInfo currCell = ri.Cells[i];

                    AllowMergeRowCellsEventArgs args = new AllowMergeRowCellsEventArgs(ri, i, i - 1);
                    ((MyTreeList)TreeList).RaiseMergeRowCells(args);
                    if(!args.Merge)
                        continue;

                    for(int j = ri.Lines.Count - 1; j >= 0; j--)
                    {
                        LineInfo lineInfo = ri.Lines[j];
                        if(lineInfo.Appearance != PaintAppearance.VertLine)
                            continue;

                        if(lineInfo.Bounds.X == prevCell.Bounds.X + prevCell.Bounds.Width)
                        {
                            ri.Lines.RemoveAt(j);
                            break;
                        }
                    }
                    Rectangle mergedBounds = new Rectangle(prevCell.Bounds.X, prevCell.Bounds.Y, currCell.Bounds.Right - prevCell.Bounds.X, prevCell.Bounds.Height);
                    prevCell.SetBounds(mergedBounds, new System.Windows.Forms.Padding(0));
                    mergedBounds.Inflate(-1, -1);
                    prevCell.EditorViewInfo.Bounds = mergedBounds;

                    ri.Cells.RemoveRange(i - 1, 2);
                    ri.Cells.Insert(i - 1, prevCell);
                    base.UpdateCellInfo(prevCell, prevCell.RowInfo.Node);
                }
            }
        }

        protected string GetCellDisplayText(RowInfo ri, int cellIndex)
        {
            TreeListColumn col = ri.Cells[cellIndex].Column;
            return ri.Node.GetDisplayText(col);
        }

        protected string GetPrevNodeCellDisplayText(RowInfo ri, int cellIndex)
        {
            TreeListNode prevNode = ri.Node.PrevNode;
            if(prevNode == null)
                return string.Empty;

            TreeListColumn col = ri.Cells[cellIndex].Column;
            return prevNode.GetDisplayText(col);
        }
    }
}
