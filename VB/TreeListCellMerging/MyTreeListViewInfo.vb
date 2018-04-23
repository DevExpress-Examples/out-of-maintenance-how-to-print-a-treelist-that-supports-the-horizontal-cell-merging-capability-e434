Imports Microsoft.VisualBasic
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.ViewInfo
Imports System.Drawing

Namespace TreeListCellMerging
	Public Class MyTreeListViewInfo
		Inherits TreeListViewInfo
		Public Sub New(ByVal fTreeList As TreeList)
			MyBase.New(fTreeList)
		End Sub

		Protected Overrides Sub CalcRowCellsInfo(ByVal ri As RowInfo)
			MyBase.CalcRowCellsInfo(ri)
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
					Dim prevCell As CellInfo = ri.Cells(i - 1)
					Dim currCell As CellInfo = ri.Cells(i)

					Dim args As New AllowMergeRowCellsEventArgs(ri, i, i - 1)
					CType(TreeList, MyTreeList).RaiseMergeRowCells(args)
					If (Not args.Merge) Then
						Continue For
					End If

					For j As Integer = ri.Lines.Count - 1 To 0 Step -1
						Dim lineInfo As LineInfo = ri.Lines(j)
                        If lineInfo.Appearance IsNot PaintAppearance.VertLine Then
                            Continue For
                        End If

                        If lineInfo.Bounds.X = prevCell.Bounds.X + prevCell.Bounds.Width Then
							ri.Lines.RemoveAt(j)
							Exit For
						End If
					Next j
					Dim mergedBounds As New Rectangle(prevCell.Bounds.X, prevCell.Bounds.Y, currCell.Bounds.Right - prevCell.Bounds.X, prevCell.Bounds.Height)
					prevCell.SetBounds(mergedBounds, New System.Windows.Forms.Padding(0))
					mergedBounds.Inflate(-1, -1)
					prevCell.EditorViewInfo.Bounds = mergedBounds

					ri.Cells.RemoveRange(i - 1, 2)
					ri.Cells.Insert(i - 1, prevCell)
					MyBase.UpdateCellInfo(prevCell, prevCell.RowInfo.Node)
				End If
			Next i
		End Sub

		Protected Function GetCellDisplayText(ByVal ri As RowInfo, ByVal cellIndex As Integer) As String
			Dim col As TreeListColumn = ri.Cells(cellIndex).Column
			Return ri.Node.GetDisplayText(col)
		End Function

		Protected Function GetPrevNodeCellDisplayText(ByVal ri As RowInfo, ByVal cellIndex As Integer) As String
			Dim prevNode As TreeListNode = ri.Node.PrevNode
			If prevNode Is Nothing Then
				Return String.Empty
			End If

			Dim col As TreeListColumn = ri.Cells(cellIndex).Column
			Return prevNode.GetDisplayText(col)
		End Function
	End Class
End Namespace
