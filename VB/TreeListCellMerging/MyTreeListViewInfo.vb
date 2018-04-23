' Developer Express Code Central Example:
' How to add a horizontal cells merge capability to an XtraTreeList control
' 
' This sample shows how to add a horizontal cell merge capability to an
' XtraTreeList control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2501


Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Drawing
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.ViewInfo

Namespace TreeListCellMerging
	Public Class MyTreeListViewInfo
		Inherits TreeListViewInfo
		Public Sub New(ByVal fTreeList As TreeList)
			MyBase.New(fTreeList)
		End Sub



		Protected Function GetCellDisplayText(ByVal ri As RowInfo, ByVal cellIndex As Integer) As String
			Dim col As TreeListColumn = (CType(ri.Cells(cellIndex), CellInfo)).Column
			Return ri.Node.GetDisplayText(col)
		End Function

		Protected Function GetPrevNodeCellDisplayText(ByVal ri As RowInfo, ByVal cellIndex As Integer) As String
			Dim prevNode As TreeListNode = ri.Node.PrevNode
			If prevNode Is Nothing Then
				Return ""
			End If

			Dim col As TreeListColumn = (CType(ri.Cells(cellIndex), CellInfo)).Column
			Return prevNode.GetDisplayText(col)
		End Function


		Protected Overrides Sub CalcRowCellsInfo(ByVal ri As RowInfo, ByVal viewInfoList As ArrayList)
			MyBase.CalcRowCellsInfo(ri, viewInfoList)
			If Not(CType(TreeList, MyTreeList)).OptionsView.AllowHorizontalMerge Then
				Return
			End If

			If TreeList.OptionsSelection.EnableAppearanceFocusedRow Then
				TreeList.OptionsSelection.EnableAppearanceFocusedRow = False
			End If

			For i As Integer = ri.Cells.Count - 1 To 1 Step -1
				Dim prevDisplayText As String = GetCellDisplayText(ri, i - 1)
				Dim currDisplayText As String = GetCellDisplayText(ri, i)

				If prevDisplayText = currDisplayText Then
					Dim prevCell As CellInfo = CType(ri.Cells(i - 1), CellInfo)
					Dim currCell As CellInfo = CType(ri.Cells(i), CellInfo)

					Dim args As New AllowMergeRowCellsEventArgs(ri, i, i - 1)
					CType(TreeList, MyTreeList).RaiseMergeRowCells(args)
					If (Not args.Merge) Then
						Continue For
					End If

					For j As Integer = ri.Lines.Count - 1 To 0 Step -1
						Dim lineInfo As LineInfo = CType(ri.Lines(j), LineInfo)
						If lineInfo.Appearance IsNot PaintAppearance.VertLine Then
							Continue For
						End If

						If lineInfo.Rect.X = prevCell.Bounds.X + prevCell.Bounds.Width Then
							ri.Lines.RemoveAt(j)
							Exit For
						End If
					Next j

					Dim mergedBounds As New Rectangle(prevCell.Bounds.X, prevCell.Bounds.Y, currCell.Bounds.Right - prevCell.Bounds.X, prevCell.Bounds.Height)
					mergedBounds.Inflate(-1, -1)
					prevCell.CalcViewInfo(GInfo.Graphics, Point.Empty, mergedBounds)
					prevCell.EditorViewInfo.Bounds = mergedBounds

					ri.Cells.RemoveRange(i - 1, 2)
					ri.Cells.Insert(i - 1, prevCell)
				End If
			Next i
		End Sub
	End Class
End Namespace
