Imports Microsoft.VisualBasic
Imports DevExpress.Utils.Controls
Imports DevExpress.XtraTreeList
Imports System.ComponentModel

Namespace TreeListCellMerging
	Public Class MyTreeListOptionsView
		Inherits TreeListOptionsView
		Private allowHorzMerge As Boolean

		Public Sub New(ByVal treeList As TreeList)
			MyBase.New(treeList)
			allowHorzMerge = False
		End Sub

		Public Overrides Sub Assign(ByVal options As BaseOptions)
			MyBase.Assign(options)
			Dim optView As MyTreeListOptionsView = TryCast(options, MyTreeListOptionsView)
			If optView Is Nothing Then
				Return
			End If

			Me.allowHorzMerge = optView.AllowHorizontalMerge
		End Sub

		<DefaultValue(False)> _
		Public Property AllowHorizontalMerge() As Boolean
			Get
				Return allowHorzMerge
			End Get
			Set(ByVal value As Boolean)
				If allowHorzMerge = value Then
					Return
				End If

				allowHorzMerge = value
			End Set
		End Property
	End Class
End Namespace
