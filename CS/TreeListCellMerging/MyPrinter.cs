using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Printing;

namespace TreeListCellMerging
{
    public class MyPrinter : TreeListPrinter
    {

        public MyPrinter(TreeList treeList)
            : base(treeList)
        {

        }


        protected override DevExpress.XtraTreeList.Nodes.Operations.TreeListOperationPrintEachNode CreatePrintEachNodeOperation()
        {
            return new MyTreeListOperationPrintEachNode(TreeList, this, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages, TreeList.OptionsPrint.PrintCheckBoxes, TreeList.OptionsPrint.PrintAllNodes);

        }
    }
}
