// Developer Express Code Central Example:
// How to add a horizontal cells merge capability to an XtraTreeList control
// 
// This sample shows how to add a horizontal cell merge capability to an
// XtraTreeList control.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2501

using System;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace TreeListCellMerging
{
	public class AllowMergeRowCellsEventArgs : EventArgs
	{
		private TreeListNode node;
		private TreeListColumn currentColumn;
		private TreeListColumn previousColumn;
		private string currentCellDisplayText;
		private string previousCellDisplayText;
		private bool merge;

		public AllowMergeRowCellsEventArgs(RowInfo rowInfo, int currCellIndex, int prevCellIndex)
		{
			node = rowInfo.Node;
			currentColumn = ((CellInfo)rowInfo.Cells[currCellIndex]).Column;
			previousColumn = ((CellInfo)rowInfo.Cells[prevCellIndex]).Column;
			currentCellDisplayText = node.GetDisplayText(currentColumn);
			previousCellDisplayText = node.GetDisplayText(previousColumn);
			merge = true;
		}

		public TreeListNode Node
		{
			get
			{
				return node;
			}
		}

		public TreeListColumn CurrentColumn
		{
			get
			{
				return currentColumn;
			}
		}

		public TreeListColumn PreviousColumn
		{
			get
			{
				return previousColumn;
			}
		}

		public string CurrentCellDisplayText
		{
			get
			{
				return currentCellDisplayText;
			}
		}

		public string PreviousCellDisplayText
		{
			get
			{
				return previousCellDisplayText;
			}
		}

		public bool Merge
		{
			get
			{
				return merge;
			}
			set
			{
				merge = value;
			}
		}
	}

	public class AllowMergeColumnCellsEventArgs : EventArgs
	{
		private TreeListNode currNode;
		private TreeListNode prevNode;
		private TreeListColumn column;
		private string currCellVisibleText;
		private string prevCellVisibleText;
		private bool merge;

		public AllowMergeColumnCellsEventArgs(RowInfo rowInfo, int cellIndex)
		{
			currNode = rowInfo.Node;
			prevNode = rowInfo.Node.PrevNode;
			column = ((CellInfo)rowInfo.Cells[cellIndex]).Column;
			currCellVisibleText = currNode.GetDisplayText(column);
			prevCellVisibleText = prevNode.GetDisplayText(column);
			merge = true;
		}

		public TreeListNode CurrNode
		{
			get
			{
				return currNode;
			}
		}

		public TreeListNode PrevNode
		{
			get
			{
				return prevNode;
			}
		}

		public TreeListColumn Column
		{
			get
			{
				return column;
			}
		}

		public string CurrCellVisibleText
		{
			get
			{
				return currCellVisibleText;
			}
		}

		public string PrevCellVisibleText
		{
			get
			{
				return prevCellVisibleText;
			}
		}

		public bool Merge
		{
			get
			{
				return merge;
			}
			set
			{
				merge = value;
			}
		}
	}
}
