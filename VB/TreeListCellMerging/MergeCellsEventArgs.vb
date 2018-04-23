' Developer Express Code Central Example:
' How to add a horizontal cells merge capability to an XtraTreeList control
' 
' This sample shows how to add a horizontal cell merge capability to an
' XtraTreeList control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2501


Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.ViewInfo

Namespace TreeListCellMerging
	Public Class AllowMergeRowCellsEventArgs
		Inherits EventArgs
		Private node_Renamed As TreeListNode
		Private currentColumn_Renamed As TreeListColumn
		Private previousColumn_Renamed As TreeListColumn
		Private currentCellDisplayText_Renamed As String
		Private previousCellDisplayText_Renamed As String
		Private merge_Renamed As Boolean

		Public Sub New(ByVal rowInfo As RowInfo, ByVal currCellIndex As Integer, ByVal prevCellIndex As Integer)
			node_Renamed = rowInfo.Node
			currentColumn_Renamed = (CType(rowInfo.Cells(currCellIndex), CellInfo)).Column
			previousColumn_Renamed = (CType(rowInfo.Cells(prevCellIndex), CellInfo)).Column
			currentCellDisplayText_Renamed = node_Renamed.GetDisplayText(currentColumn_Renamed)
			previousCellDisplayText_Renamed = node_Renamed.GetDisplayText(previousColumn_Renamed)
			merge_Renamed = True
		End Sub

		Public ReadOnly Property Node() As TreeListNode
			Get
				Return node_Renamed
			End Get
		End Property

		Public ReadOnly Property CurrentColumn() As TreeListColumn
			Get
				Return currentColumn_Renamed
			End Get
		End Property

		Public ReadOnly Property PreviousColumn() As TreeListColumn
			Get
				Return previousColumn_Renamed
			End Get
		End Property

		Public ReadOnly Property CurrentCellDisplayText() As String
			Get
				Return currentCellDisplayText_Renamed
			End Get
		End Property

		Public ReadOnly Property PreviousCellDisplayText() As String
			Get
				Return previousCellDisplayText_Renamed
			End Get
		End Property

		Public Property Merge() As Boolean
			Get
				Return merge_Renamed
			End Get
			Set(ByVal value As Boolean)
				merge_Renamed = value
			End Set
		End Property
	End Class

	Public Class AllowMergeColumnCellsEventArgs
		Inherits EventArgs
		Private currNode_Renamed As TreeListNode
		Private prevNode_Renamed As TreeListNode
		Private column_Renamed As TreeListColumn
		Private currCellVisibleText_Renamed As String
		Private prevCellVisibleText_Renamed As String
		Private merge_Renamed As Boolean

		Public Sub New(ByVal rowInfo As RowInfo, ByVal cellIndex As Integer)
			currNode_Renamed = rowInfo.Node
			prevNode_Renamed = rowInfo.Node.PrevNode
			column_Renamed = (CType(rowInfo.Cells(cellIndex), CellInfo)).Column
			currCellVisibleText_Renamed = currNode_Renamed.GetDisplayText(column_Renamed)
			prevCellVisibleText_Renamed = prevNode_Renamed.GetDisplayText(column_Renamed)
			merge_Renamed = True
		End Sub

		Public ReadOnly Property CurrNode() As TreeListNode
			Get
				Return currNode_Renamed
			End Get
		End Property

		Public ReadOnly Property PrevNode() As TreeListNode
			Get
				Return prevNode_Renamed
			End Get
		End Property

		Public ReadOnly Property Column() As TreeListColumn
			Get
				Return column_Renamed
			End Get
		End Property

		Public ReadOnly Property CurrCellVisibleText() As String
			Get
				Return currCellVisibleText_Renamed
			End Get
		End Property

		Public ReadOnly Property PrevCellVisibleText() As String
			Get
				Return prevCellVisibleText_Renamed
			End Get
		End Property

		Public Property Merge() As Boolean
			Get
				Return merge_Renamed
			End Get
			Set(ByVal value As Boolean)
				merge_Renamed = value
			End Set
		End Property
	End Class
End Namespace
