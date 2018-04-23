' Developer Express Code Central Example:
' How to add a horizontal cells merge capability to an XtraTreeList control
' 
' This sample shows how to add a horizontal cell merge capability to an
' XtraTreeList control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2501

Imports System.ComponentModel
Imports DevExpress.Utils.Controls
Imports DevExpress.XtraTreeList

Namespace TreeListCellMerging
	Public Class MyTreeListOptionsView
		Inherits TreeListOptionsView

		Private allowHorzMerge As Boolean

		Public Sub New()
			MyBase.New()
			allowHorzMerge = False
		End Sub

		<DefaultValue(False)>
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

		Public Overrides Sub Assign(ByVal options As BaseOptions)
			MyBase.Assign(options)
			Dim optView As MyTreeListOptionsView = TryCast(options, MyTreeListOptionsView)
			If optView Is Nothing Then
				Return
			End If

			Me.allowHorzMerge = optView.AllowHorizontalMerge
		End Sub
	End Class
End Namespace
