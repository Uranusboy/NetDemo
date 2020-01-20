using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.UserControl
{
    public delegate void CurrentPageChangedEventHandler(Pager sender, int newPage);

    public partial class Pager : System.Web.UI.UserControl
    {
        public event CurrentPageChangedEventHandler CurrentPageChanged;
        private void OnCurrentPageChanged()
        {
            lblCurrent.Text = CurrentPageIndex.ToString();
            if (CurrentPageChanged != null) { CurrentPageChanged(this, CurrentPageIndex); }
        }
        public int CurrentPageIndex
        {
            get
            {
                int num;
                if (int.TryParse(txtGo.Text.Trim(), out num))
                {
                    if (num <= 0)
                    {
                        num = 1;
                        txtGo.Text = "1";
                    }
                    return num;
                }
                txtGo.Text = "1";
                return 1;
            }
            set
            {
                if (CurrentPageIndex != value)
                {
                    if (value < 1)
                    {
                        txtGo.Text = "1";
                    }
                    else
                    {
                        txtGo.Text = value.ToString();
                    }
                    OnCurrentPageChanged();
                }
            }
        }
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                {
                    return 0;
                }
                int i = RowCount % PageSize;
                int j = RowCount / PageSize;
                return i > 0 ? j + 1 : j;
            }
        }
        public int PageSize
        {
            get
            {
                if (ViewState["PageSize"] == null)
                {
                    ViewState["PageSize"] = 0;
                }
                return (int)ViewState["PageSize"];
            }
            set
            {
                if (value >= 0)
                {
                    ViewState["PageSize"] = value;
                    lblTotal.Text = PageCount.ToString();
                }
            }
        }
        public int RowCount
        {
            get
            {
                if (ViewState["RowCount"] == null)
                {
                    ViewState["RowCount"] = 0;
                }
                return (int)ViewState["RowCount"];
            }
            set
            {
                if (value >= 0)
                {
                    ViewState["RowCount"] = value;
                    lblTotal.Text = PageCount.ToString();
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            txtGo.TextChanged += new EventHandler(txtGo_TextChanged);
            txtGo.Text = "1";
            lblTotal.Text = "1";
            lblCurrent.Text = "1";
        }
        protected void txtGo_TextChanged(object sender, EventArgs e)
        {
            OnCurrentPageChanged();
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            //OnCurrentPageChanged();
        }

        protected void lbnFirst_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 1;
        }
        protected void lbnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
            }
        }
        protected void lbnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex < PageCount)
            {
                CurrentPageIndex++;
            }
        }
        protected void lbnLast_Click(object sender, EventArgs e)
        {
            if (PageCount > 0)
            {
                CurrentPageIndex = PageCount;
            }
        }
    }
}