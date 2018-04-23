' Developer Express Code Central Example:
' How to add a horizontal cells merge capability to an XtraTreeList control
' 
' This sample shows how to add a horizontal cell merge capability to an
' XtraTreeList control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2501

Imports System.ComponentModel
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.ViewInfo
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Printing

Namespace TreeListCellMerging
	Public Delegate Sub AllowMergeRowCellsEventHandler(ByVal sender As Object, ByVal e As AllowMergeRowCellsEventArgs)
	Public Delegate Sub AllowMergeColumnCellsEventHandler(ByVal sender As Object, ByVal e As AllowMergeColumnCellsEventArgs)

	Public Class MyTreeList
		Inherits TreeList

		Public Sub New()
			MyBase.New()
			AddHandler Me.FocusedColumnChanged, AddressOf MyTreeList_FocusedColumnChanged
			AddHandler Me.FocusedNodeChanged, AddressOf MyTreeList_FocusedNodeChanged
		End Sub


		Private Sub MyTreeList_FocusedNodeChanged(ByVal sender As Object, ByVal e As FocusedNodeChangedEventArgs)
			MovementUpDownInMergeColumn(e)
		End Sub

		Public _OldColumn As TreeListColumn
		Public MergedColumns As TreeListColumnCollection

		Private Sub MyTreeList_FocusedColumnChanged(ByVal sender As Object, ByVal e As FocusedColumnChangedEventArgs)
			If e.OldColumn Is Nothing OrElse e.Column Is Nothing Then
				Return
			End If
			ReturnIfBeyondRightBorder(e)

			If e.OldColumn.VisibleIndex < e.Column.VisibleIndex AndAlso Me.Columns(e.Column.VisibleIndex + 1) IsNot Nothing Then
				JumpIfInMergedCell(e, 1)
			End If
			If e.OldColumn.VisibleIndex > e.Column.VisibleIndex AndAlso Me.Columns(e.Column.VisibleIndex - 1) IsNot Nothing Then
				JumpIfInMergedCell(e, -1)
			End If
		End Sub

		Private Sub MovementUpDownInMergeColumn(ByVal e As FocusedNodeChangedEventArgs)
			If e.OldNode IsNot Nothing AndAlso (Me.FocusedColumn.VisibleIndex - 1) > 0 Then
				If Equals(e.Node.GetValue(Me.GetColumnByVisibleIndex(Me.FocusedColumn.VisibleIndex)), e.Node.GetValue(Me.GetColumnByVisibleIndex(Me.FocusedColumn.VisibleIndex - 1))) Then
					Me.FocusedColumn = Me.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex - 1)
				End If
			End If
		End Sub

		Private Sub JumpIfInMergedCell(ByVal e As FocusedColumnChangedEventArgs, ByVal [step] As Integer)
			Dim nextColumn As TreeListColumn = GetColumnByVisibleIndex(e.Column.VisibleIndex - 1)
			If Equals(Me.FocusedNode.GetValue(e.Column), Me.FocusedNode.GetValue(nextColumn)) Then
				Me.FocusedColumn = Me.GetColumnByVisibleIndex(FocusedColumn.VisibleIndex + [step])
			End If
		End Sub

		Private Sub ReturnIfBeyondRightBorder(ByVal e As FocusedColumnChangedEventArgs)
				If Me.FocusedNode.GetValue(e.Column).ToString() = Me.FocusedNode.GetValue(e.OldColumn).ToString() AndAlso Me.Columns(e.Column.VisibleIndex + 1) Is Nothing Then
					Me.FocusedColumn = e.OldColumn
				End If

		End Sub

		Public Overrides Sub ShowEditor()
			Dim ri As RowInfo = Me.FocusedRow
			Dim cell As CellInfo = ri(Me.FocusedColumn)
			If cell Is Nothing Then
				If Me.FocusedColumn.VisibleIndex = 0 Then
					Return
				End If

				Me.FocusedColumn = Me.VisibleColumns(Me.FocusedColumn.VisibleIndex - 1)
				MyBase.ShowEditor()
				Return
			End If

			MyBase.ShowEditor()
		End Sub

		Protected Overrides Function CreateViewInfo() As TreeListViewInfo
			Return New MyTreeListViewInfo(Me)
		End Function

		Protected Overrides Function CreatePrinter() As TreeListPrinter
			Return New MyPrinter(Me)
		End Function

		Public Event AllowMergeRowCells As AllowMergeRowCellsEventHandler
		Protected Friend Sub RaiseMergeRowCells(ByVal args As AllowMergeRowCellsEventArgs)
			RaiseEvent AllowMergeRowCells(Me, args)
		End Sub

		Protected Overrides Function CreateOptionsView() As TreeListOptionsView
			Return New MyTreeListOptionsView()
		End Function

		Public Shadows ReadOnly Property ViewInfo() As MyTreeListViewInfo
			Get
				Return CType(MyBase.ViewInfo, MyTreeListViewInfo)
			End Get
		End Property

		Public Overrides ReadOnly Property CanShowEditor() As Boolean
			Get
				Return MyBase.CanShowEditor
			End Get
		End Property

		<Description("Provides access to the tree list's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)>
		Public Shadows ReadOnly Property OptionsView() As MyTreeListOptionsView
			Get
				Return TryCast(MyBase.OptionsView, MyTreeListOptionsView)
			End Get
		End Property

	End Class

End Namespace