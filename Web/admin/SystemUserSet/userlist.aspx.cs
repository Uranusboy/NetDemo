using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.SystemUserSet
{
    public partial class userlist : System.Web.UI.Page
    {
        static int pageSize = 0;//每页显示节点的数量        
        public static int ImportStudentNum = 0;
        ManageService.SystemUserService opuser = new ManageService.SystemUserService(); 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Pager1.CurrentPageChanged += new UserControl.CurrentPageChangedEventHandler(Pager1_CurrentPageChanged);
            if (!IsPostBack)
            {
                // 加载页大小
                pageSize = 10; //int.Parse(this.DropDownList_PageSize.SelectedValue);
                BindDatas(1);
            }
        }

        void Pager1_CurrentPageChanged(UserControl.Pager sender, int pageIndex)
        {
            BindDatas(pageIndex);
        }

        // 修改
        protected void edit_stu_info(object sender, CommandEventArgs e)
        {
            Response.Redirect("edit_sysuserinfo.aspx?id=" + e.CommandArgument.ToString());
        }
        // 删除
        protected void del_stu_info(object sender, CommandEventArgs e)
        {
            ManageService.Model.v_UserInfos v = new ManageService.Model.v_UserInfos();
            v.UserID = decimal.Parse(e.CommandArgument.ToString());
            opuser.del(v);
            BindDatas(1);
        }

        //---------------------------------以下为分页功能的开始------------------------------------------// 
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private ManageService.Condition.SystemuserCondition getCondition()
        {
            ManageService.Condition.SystemuserCondition v = new ManageService.Condition.SystemuserCondition();
            try
            {
                if (!string.IsNullOrEmpty(this.tx_Name.Text.Trim()))
                {
                    v.UserName = this.tx_Name.Text.Trim();
                }
                return v;
            }
            catch
            {
                return v;
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="pageIndex"></param>
        private void BindDatas(int pageIndex)
        {
            try
            {
                int recordCount = 0;
                // MsSQL数据库
                var resource = opuser.querying(getCondition(), pageIndex, pageSize, out recordCount);
                // 记录查询总数
                ImportStudentNum = recordCount;

                Pager1.RowCount = recordCount;
                Pager1.PageSize = pageSize;
                Pager1.CurrentPageIndex = pageIndex;

                if (resource.Count == 0)
                {
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "showalert('对不起!没有查询到相符的数据!');", true);
                }
                this.Repeater_data.DataSource = resource;
                this.Repeater_data.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        //---------------------------------以上为分页功能的结束------------------------------------------//   

    }
}