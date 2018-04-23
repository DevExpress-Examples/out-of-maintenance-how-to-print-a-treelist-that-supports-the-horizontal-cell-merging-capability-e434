using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.XtraTreeList.Printing;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraTreeList.Nodes;

namespace TreeListCellMerging {
    public class MyTreeListOperationPrintEachNode : TreeListOperationPrintEachNode {

        TreeList treeList;
        bool printAllNodes;
        public MyTreeListOperationPrintEachNode(TreeList treeList, TreeListPrinter printer, TreeListViewInfo viewInfo, bool printTree, bool printImages, bool printCheckBoxes, bool printAllNodes)         
            : base(treeList, printer, viewInfo, printTree, printImages, printCheckBoxes) {
            this.treeList = treeList;
            this.printAllNodes = printAllNodes;
        }

        CellInfo prevCell = null;

        public override bool NeedsVisitChildren(TreeListNode node) { return node.Expanded || printAllNodes; }      


        protected override DevExpress.XtraPrinting.IBrick CreateCellBrick(CellInfo cell, DevExpress.XtraTreeList.Nodes.TreeListNode node) {
            IVisualBrick brick = base.CreateCellBrick(cell, node) as IVisualBrick;
           
            int lastColumnIndex = treeList.Columns.Count - 1;
            int prevIndex = lastColumnIndex - 1;
          
            if (node.GetDisplayText(lastColumnIndex) == node.GetDisplayText(prevIndex)) {
                if (cell.Column.AbsoluteIndex == lastColumnIndex) {
                    brick.Sides = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                    Rectangle rect = cell.EditorViewInfo.Bounds;
                    rect.X -= prevCell.EditorViewInfo.Bounds.Width;
                    rect.Width += prevCell.EditorViewInfo.Bounds.Width;
                    cell.EditorViewInfo.Bounds = rect;

                }

                else if (cell.Column.AbsoluteIndex == lastColumnIndex - 1) {
                    brick.Sides = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                    (brick as TextBrick).HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
                    prevCell = cell;
                }
            }
            return brick;
        }
    }
}
