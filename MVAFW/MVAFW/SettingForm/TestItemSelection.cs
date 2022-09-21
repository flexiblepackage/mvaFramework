using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MVAFW.Common.Entity;

namespace MVAFW.SettingForm
{
    public partial class TestItemSelection : Form
    {
        public delegate void addtestitemHandler(object sender, AddtestitemEventArg e);
        public event addtestitemHandler AddedTestItems;
        public List<TreeNode> SelectNodelst = new List<TreeNode>();

        public TestItemSelection()
        {
            InitializeComponent();
            //this.tv_testItem.DoubleClick += new EventHandler(tv_testItem_DoubleClick);
            this.tv_testItem.ItemDrag += new ItemDragEventHandler(tv_testItem_ItemDrag);
            this.tv_testItem.DragEnter += new DragEventHandler(tv_testItem_DragEnter);

            displayTestItem();
        }

        private void displayTestItem()
        {
            string[] categories = new string[] { "Camera", "Android", "MISC" };

            List<eTestItemClass> testItemClasses = GetAllTestItemClasses();
            Dictionary<string, bool> dicProduct = new Dictionary<string, bool>();

            for (int categoryIndex = 0; categoryIndex < categories.Length; categoryIndex++)
            {
                string categoryName = categories[categoryIndex];
                tv_testItem.Nodes.Add(categoryName);

                foreach (eTestItemClass t in testItemClasses)
                {
                    if (t.Category == categoryName)
                    {
                        if (dicProduct.ContainsKey(t.Product) == false)
                        {
                            dicProduct[t.Product] = true;
                            tv_testItem.Nodes[categoryIndex].Nodes.Add(t.Product).Name = t.Product;
                        }
                        tv_testItem.Nodes[categoryIndex].Nodes[t.Product].Nodes.Add(t.Name).Name = t.FullClassName;
                    }
                }
            }
        }

        void tv_testItem_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void tv_testItem_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        public static List<eTestItemClass> GetAllTestItemClasses()
        {
            List<eTestItemClass> testItemClasses = new List<eTestItemClass>();

            #region MISC
            testItemClasses.Add(new eTestItemClass("MISC", "COMMON", "MessageDialog", "MVAFW.TestItemColls.COMMON.MessageDialog"));
            testItemClasses.Add(new eTestItemClass("MISC", "COMMON", "Delay", "MVAFW.TestItemColls.COMMON.Delay"));
            testItemClasses.Add(new eTestItemClass("MISC", "COMMON", "DiskRW", "MVAFW.TestItemColls.COMMON.DiskRW"));
            #endregion

            #region Camera
            testItemClasses.Add(new eTestItemClass("Camera", "Web", "OTA", "MVAFW.TestItemColls.Camera.Web.OTA"));

            testItemClasses.Add(new eTestItemClass("Camera", "Utility", "FilterLog", "MVAFW.TestItemColls.Camera.Utility.FilterLog"));
            testItemClasses.Add(new eTestItemClass("Camera", "Utility", "UartLog", "MVAFW.TestItemColls.Camera.Utility.UartLog"));
            testItemClasses.Add(new eTestItemClass("Camera", "Utility", "ESP8266", "MVAFW.TestItemColls.Camera.Utility.ESP8266"));
            #endregion

            #region Android             
            testItemClasses.Add(new eTestItemClass("Android", "App.Template", "Provision", "MVAFW.TestItemColls.Android.App.Template.Provision"));
            testItemClasses.Add(new eTestItemClass("Android", "App.Template", "Deletion", "MVAFW.TestItemColls.Android.App.Template.Deletion"));
            #endregion

            return testItemClasses;
        }

        private void addToTestItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in SelectNodelst)
            {
                if (node.Parent == null || node.Parent.Parent == null)
                    continue;
                string sSelectedNode = node.Text + ":" + node.Parent.Text;
                AddtestitemEventArg args = new AddtestitemEventArg(sSelectedNode);
                AddedTestItems(this, args);
            }

            tv_testItem.SelectedNode = null;
        }

        private void tv_testItem_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        protected void changeSelection(List<TreeNode> addedNodes, List<TreeNode> removedNodes)
        {
            foreach (TreeNode tn in addedNodes)
            {
                tn.BackColor = SystemColors.Highlight;
                tn.ForeColor = SystemColors.HighlightText;
                SelectNodelst.Add(tn);
            }
            foreach (TreeNode tn in removedNodes)
            {
                tn.BackColor = tv_testItem.BackColor;
                tn.ForeColor = tv_testItem.ForeColor;
                SelectNodelst.Remove(tn);
            }
        }

        private void tv_testItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                addNodetoList(e);
            }
        }

        private void addNodetoList(MouseEventArgs e)
        {
            TreeNode tnode = tv_testItem.GetNodeAt(e.Location);

            if (tnode == null)
                return;

            bool control = (ModifierKeys == Keys.Control);

            if (control)
            {
                List<TreeNode> addedNodes = new List<TreeNode>();
                List<TreeNode> removedNodes = new List<TreeNode>();
                if (!SelectNodelst.Contains(tnode))
                {
                    addedNodes.Add(tnode);
                }
                else
                {
                    removedNodes.Add(tnode);
                }

                changeSelection(addedNodes, removedNodes);
            }
            else  //single click
            {
                List<TreeNode> addedNodes = new List<TreeNode>();
                List<TreeNode> removedNodes = new List<TreeNode>();
                removedNodes.AddRange(SelectNodelst);

                if (removedNodes.Contains(tnode))
                    removedNodes.Remove(tnode);
                else
                    addedNodes.Add(tnode);
                changeSelection(addedNodes, removedNodes);
            }
        }

        private void tv_testItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addNodetoList(e);
            EventArgs earg = new EventArgs();
            addToTestItem_Click(sender, earg);
        }
    }

    public class AddtestitemEventArg : System.EventArgs
    {
        private string sNode;
        public AddtestitemEventArg(string sSelectNode)
        {
            this.sNode = sSelectNode;
        }

        public string SelectedNode
        {
            get
            {
                return sNode;
            }
        }
    }
}
