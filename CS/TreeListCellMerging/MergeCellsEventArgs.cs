using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using System;

namespace TreeListCellMerging
{
    public class AllowMergeRowCellsEventArgs : EventArgs
    {
        private string currentCellDisplayText;
        private TreeListColumn currentColumn;
        private bool merge;
        private TreeListNode node;
        private string previousCellDisplayText;
        private TreeListColumn previousColumn;

        public AllowMergeRowCellsEventArgs(RowInfo rowInfo, int currCellIndex, int prevCellIndex)
        {
            node = rowInfo.Node;
            currentColumn = rowInfo.Cells[currCellIndex].Column;
            previousColumn = rowInfo.Cells[prevCellIndex].Column;
            currentCellDisplayText = node.GetDisplayText(currentColumn);
            previousCellDisplayText = node.GetDisplayText(previousColumn);
            merge = true;
        }

        public string CurrentCellDisplayText {
            get {
                return currentCellDisplayText;
            }
        }

        public TreeListColumn CurrentColumn {
            get {
                return currentColumn;
            }
        }

        public bool Merge {
            get {
                return merge;
            }
            set {
                merge = value;
            }
        }

        public TreeListNode Node {
            get {
                return node;
            }
        }

        public string PreviousCellDisplayText {
            get {
                return previousCellDisplayText;
            }
        }

        public TreeListColumn PreviousColumn {
            get {
                return previousColumn;
            }
        }
    }

    public class AllowMergeColumnCellsEventArgs : EventArgs
    {
        private TreeListColumn column;
        private string currCellVisibleText;
        private TreeListNode currNode;
        private bool merge;
        private string prevCellVisibleText;
        private TreeListNode prevNode;

        public AllowMergeColumnCellsEventArgs(RowInfo rowInfo, int cellIndex)
        {
            currNode = rowInfo.Node;
            prevNode = rowInfo.Node.PrevNode;
            column = rowInfo.Cells[cellIndex].Column;
            currCellVisibleText = currNode.GetDisplayText(column);
            prevCellVisibleText = prevNode.GetDisplayText(column);
            merge = true;
        }

        public TreeListColumn Column {
            get {
                return column;
            }
        }

        public string CurrCellVisibleText {
            get {
                return currCellVisibleText;
            }
        }

        public TreeListNode CurrNode {
            get {
                return currNode;
            }
        }

        public bool Merge {
            get {
                return merge;
            }
            set {
                merge = value;
            }
        }

        public string PrevCellVisibleText {
            get {
                return prevCellVisibleText;
            }
        }

        public TreeListNode PrevNode {
            get {
                return prevNode;
            }
        }
    }
}
