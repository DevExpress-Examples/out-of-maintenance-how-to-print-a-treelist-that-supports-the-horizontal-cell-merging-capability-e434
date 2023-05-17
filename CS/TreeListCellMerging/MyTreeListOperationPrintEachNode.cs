using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Printing;
using DevExpress.XtraTreeList.ViewInfo;
using System.Drawing;

namespace TreeListCellMerging
{
    public class MyTreeListOperationPrintEachNode : TreeListOperationPrintEachNode
    {
        CellInfo prevCell = null;
        bool printAllNodes;
        TreeList treeList;

        public MyTreeListOperationPrintEachNode(TreeList treeList, TreeListPrinter printer, TreeListViewInfo viewInfo, bool printTree, bool printImages, bool printCheckBoxes, bool printAllNodes)
            : base(treeList, printer, viewInfo, printTree, printImages, printCheckBoxes)
        {
            this.treeList = treeList;
            this.printAllNodes = printAllNodes;
        }

        protected override VisualBrick CreateCellBrick(CellInfo cell, DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            VisualBrick brick = base.CreateCellBrick(cell, node) as VisualBrick;

            int lastColumnIndex = treeList.Columns.Count - 1;
            int prevIndex = lastColumnIndex - 1;
            if(node.GetDisplayText(lastColumnIndex) == node.GetDisplayText(prevIndex))
            {
                if(cell.Column.AbsoluteIndex == lastColumnIndex)
                {
                    brick.Sides = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                    Rectangle rect = cell.EditorViewInfo.Bounds;
                    rect.X -= prevCell.EditorViewInfo.Bounds.Width;
                    rect.Width += prevCell.EditorViewInfo.Bounds.Width;
                    rect.Inflate(1, 1);
                    cell.SetBounds(rect, new System.Windows.Forms.Padding(0));
                }

                else if(cell.Column.AbsoluteIndex == lastColumnIndex - 1)
                {
                    brick.Sides = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                    (brick as TextBrick).HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
                    prevCell = cell;
                }
            }
            return brick;
        }

        public override bool NeedsVisitChildren(TreeListNode node) { return node.Expanded || printAllNodes; }
    }
}
