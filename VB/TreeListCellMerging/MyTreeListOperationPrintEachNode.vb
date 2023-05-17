﻿Imports Microsoft.VisualBasic
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Printing
Imports DevExpress.XtraTreeList.ViewInfo
Imports System.Drawing

Namespace TreeListCellMerging
	Public Class MyTreeListOperationPrintEachNode
		Inherits TreeListOperationPrintEachNode
		Private prevCell As CellInfo = Nothing
		Private printAllNodes As Boolean
		Private treeList As TreeList

		Public Sub New(ByVal treeList As TreeList, ByVal printer As TreeListPrinter, ByVal viewInfo As TreeListViewInfo, ByVal printTree As Boolean, ByVal printImages As Boolean, ByVal printCheckBoxes As Boolean, ByVal printAllNodes As Boolean)
			MyBase.New(treeList, printer, viewInfo, printTree, printImages, printCheckBoxes)
			Me.treeList = treeList
			Me.printAllNodes = printAllNodes
		End Sub

		Protected Overrides Function CreateCellBrick(ByVal cell As CellInfo, ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode) As VisualBrick
			Dim brick As VisualBrick = TryCast(MyBase.CreateCellBrick(cell, node), VisualBrick)

			Dim lastColumnIndex As Integer = treeList.Columns.Count - 1
			Dim prevIndex As Integer = lastColumnIndex - 1
			If node.GetDisplayText(lastColumnIndex) = node.GetDisplayText(prevIndex) Then
				If cell.Column.AbsoluteIndex = lastColumnIndex Then
					brick.Sides = BorderSide.Right Or BorderSide.Bottom Or BorderSide.Top
					Dim rect As Rectangle = cell.EditorViewInfo.Bounds
					rect.X -= prevCell.EditorViewInfo.Bounds.Width
					rect.Width += prevCell.EditorViewInfo.Bounds.Width
					rect.Inflate(1, 1)
					cell.SetBounds(rect, New System.Windows.Forms.Padding(0))

				ElseIf cell.Column.AbsoluteIndex = lastColumnIndex - 1 Then
					brick.Sides = BorderSide.Left Or BorderSide.Bottom Or BorderSide.Top
					TryCast(brick, TextBrick).HorzAlignment = DevExpress.Utils.HorzAlignment.Far
					prevCell = cell
				End If
			End If
			Return brick
		End Function

		Public Overrides Function NeedsVisitChildren(ByVal node As TreeListNode) As Boolean
			Return node.Expanded OrElse printAllNodes
		End Function
	End Class
End Namespace
