// Developer Express Code Central Example:
// How to add a horizontal cells merge capability to an XtraTreeList control
// 
// This sample shows how to add a horizontal cell merge capability to an
// XtraTreeList control.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2501

using System.Collections;
using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace TreeListCellMerging
{
	public class MyTreeListViewInfo : TreeListViewInfo
	{
		public MyTreeListViewInfo(TreeList fTreeList)
			: base(fTreeList)
		{
		}

        

		protected string GetCellDisplayText(RowInfo ri, int cellIndex)
		{
			TreeListColumn col = ((CellInfo)ri.Cells[cellIndex]).Column;
			return ri.Node.GetDisplayText(col);
		}

		protected string GetPrevNodeCellDisplayText(RowInfo ri, int cellIndex)
		{
			TreeListNode prevNode = ri.Node.PrevNode;
			if ( prevNode == null )
				return "";

			TreeListColumn col = ((CellInfo)ri.Cells[cellIndex]).Column;
			return prevNode.GetDisplayText(col);
		}
        

		protected override void CalcRowCellsInfo(RowInfo ri, ArrayList viewInfoList)
		{
			base.CalcRowCellsInfo(ri, viewInfoList);
			if ( !((MyTreeList)TreeList).OptionsView.AllowHorizontalMerge )
				return;

			if ( TreeList.OptionsSelection.EnableAppearanceFocusedRow )
				TreeList.OptionsSelection.EnableAppearanceFocusedRow = false;

			for ( int i = ri.Cells.Count - 1; i > 0; i-- )
			{
				string prevDisplayText = GetCellDisplayText(ri, i - 1);
				string currDisplayText = GetCellDisplayText(ri, i);

				if ( prevDisplayText == currDisplayText )
				{
					CellInfo prevCell = (CellInfo)ri.Cells[i - 1];
					CellInfo currCell = (CellInfo)ri.Cells[i];

					AllowMergeRowCellsEventArgs args = new AllowMergeRowCellsEventArgs(ri, i, i - 1);
					((MyTreeList)TreeList).RaiseMergeRowCells(args);
					if ( !args.Merge )
						continue;

					for ( int j = ri.Lines.Count - 1; j >= 0; j-- )
					{
						LineInfo lineInfo = (LineInfo)ri.Lines[j];
						if ( lineInfo.Appearance != PaintAppearance.VertLine )
							continue;

						if ( lineInfo.Rect.X == prevCell.Bounds.X + prevCell.Bounds.Width )
						{
							ri.Lines.RemoveAt(j);
							break;
						}
					}

                    Rectangle mergedBounds = new Rectangle(prevCell.Bounds.X, prevCell.Bounds.Y, currCell.Bounds.Right - prevCell.Bounds.X, prevCell.Bounds.Height);
                    mergedBounds.Inflate(-1, -1);
                    prevCell.CalcViewInfo(GInfo.Graphics, Point.Empty, mergedBounds);
                    prevCell.EditorViewInfo.Bounds = mergedBounds;

					ri.Cells.RemoveRange(i - 1, 2);
					ri.Cells.Insert(i - 1, prevCell);
				}
			}
		}
	}
}
