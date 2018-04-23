Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Printing

Namespace TreeListCellMerging
	Public Class MyPrinter
		Inherits TreeListPrinter

		Public Sub New(ByVal treeList As TreeList)
			MyBase.New(treeList)

		End Sub


		Protected Overrides Function CreatePrintEachNodeOperation() As DevExpress.XtraTreeList.Nodes.Operations.TreeListOperationPrintEachNode
			Return New MyTreeListOperationPrintEachNode(TreeList, Me, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages, TreeList.OptionsPrint.PrintCheckBoxes, TreeList.OptionsPrint.PrintAllNodes)

		End Function
	End Class
End Namespace
