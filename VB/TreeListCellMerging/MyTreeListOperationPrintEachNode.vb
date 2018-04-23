Imports Microsoft.VisualBasic
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.ViewInfo
Imports DevExpress.XtraTreeList.Printing
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraPrinting
Imports System.Drawing

Namespace TreeListCellMerging
	Public Class MyTreeListOperationPrintEachNode
		Inherits TreeListOperationPrintEachNode

		Private treeList As TreeList
		Public Sub New(ByVal treeList As TreeList, ByVal printer As TreeListPrinter, ByVal viewInfo As TreeListViewInfo, ByVal printTree As Boolean, ByVal printImages As Boolean)
			MyBase.New(treeList, printer, viewInfo, printTree, printImages)
			Me.treeList = treeList
		End Sub

		Private prevCell As CellInfo = Nothing
		Protected Overrides Function CreateCellBrick(ByVal cell As CellInfo, ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode) As DevExpress.XtraPrinting.IBrick
			Dim brick As IVisualBrick = TryCast(MyBase.CreateCellBrick(cell, node), IVisualBrick)

			Dim lastColumnIndex As Integer = treeList.Columns.Count - 1
			Dim prevIndex As Integer = lastColumnIndex - 1

			If node.GetDisplayText(lastColumnIndex) = node.GetDisplayText(prevIndex) Then
				If cell.Column.AbsoluteIndex = lastColumnIndex Then
					brick.Sides = BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
					Dim rect As Rectangle = cell.EditorViewInfo.Bounds
					rect.X -= prevCell.EditorViewInfo.Bounds.Width
					rect.Width += prevCell.EditorViewInfo.Bounds.Width
					cell.EditorViewInfo.Bounds = rect


				ElseIf cell.Column.AbsoluteIndex = lastColumnIndex - 1 Then
					brick.Sides = BorderSide.Left Or BorderSide.Bottom Or BorderSide.Top
					TryCast(brick, TextBrick).HorzAlignment = DevExpress.Utils.HorzAlignment.Far
					prevCell = cell
				End If
			End If
			Return brick
		End Function
	End Class
End Namespace
