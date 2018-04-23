Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Printing

Namespace TreeListCellMerging
	Public Class MyPrinter
		Inherits TreeListPrinter

		Public Sub New(ByVal treeList As TreeList)
			MyBase.New(treeList)

		End Sub


		Protected Overrides Function CreatePrintEachNodeOperation() As DevExpress.XtraTreeList.Nodes.Operations.TreeListOperationPrintEachNode

			If TreeList.OptionsPrint.PrintAllNodes Then
				Return New MyTreeListOperationPrintEachNode(TreeList, Me, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages)
			End If
			Return New MyTreeListOperationPrintEachNode(TreeList, Me, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages)
		End Function
	End Class
End Namespace
