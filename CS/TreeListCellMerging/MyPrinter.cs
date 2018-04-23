using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Printing;

namespace TreeListCellMerging {
    public class MyPrinter : TreeListPrinter {

        public MyPrinter(TreeList treeList)
            : base(treeList) {

        }


        protected override DevExpress.XtraTreeList.Nodes.Operations.TreeListOperationPrintEachNode CreatePrintEachNodeOperation() {

            if (TreeList.OptionsPrint.PrintAllNodes)
                return new MyTreeListOperationPrintEachNode(TreeList, this, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages);
            return new MyTreeListOperationPrintEachNode(TreeList, this, TreeList.ViewInfo, TreeList.OptionsPrint.PrintTree, TreeList.OptionsPrint.PrintImages);
        }
    }
}
