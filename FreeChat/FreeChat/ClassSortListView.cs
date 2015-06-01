using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FreeChat
{
    //ListView的排序器
    class ClassSortListView:IComparer
    {
        private int ColumnToSort; //要排序的列的index号
        private SortOrder OrderOfSort; //排序的方式(正向或反向)
        private CaseInsensitiveComparer ObjectCompare; //声明类对象

        public ClassSortListView()
        {
            ColumnToSort = 0;       //默认按第一列进行排序
            OrderOfSort = SortOrder.None;       //默认不排序
            ObjectCompare = new CaseInsensitiveComparer();
        }

        //重写ICompare接口
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX = (ListViewItem)x;
            ListViewItem listviewY = (ListViewItem)y;

            //比较
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            if (OrderOfSort == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        //获取要排序的列号
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }
        //获取排序方式
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
