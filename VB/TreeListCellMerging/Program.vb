' Developer Express Code Central Example:
' How to add a horizontal cells merge capability to an XtraTreeList control
' 
' This sample shows how to add a horizontal cell merge capability to an
' XtraTreeList control.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2501

Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace TreeListCellMerging
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace
