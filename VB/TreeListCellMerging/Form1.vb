Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

Namespace TreeListCellMerging
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub




		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Me.departmentsTableAdapter.Fill(Me.departmentsDataSet.Departments)
			treeList1.ExpandAll()
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			treeList1.ShowPrintPreview()

		End Sub
	End Class
End Namespace