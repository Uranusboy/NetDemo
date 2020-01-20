using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.SystemUserSet
{
    public partial class add_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_add_Click(object sender, EventArgs e)
        {
            ManageService.Model.v_UserInfos v = new ManageService.Model.v_UserInfos();
            v.UserID = Common.IdGenerator.Instance.GenerateId();
            v.UserAccount = tx_account.Text;
            v.UserName = tx_username.Text;
            v.UserPassword = Common.Common.getMd5Hash(tx_pwd.Text);
            v.UserType = tx_usertype.Value;

            ManageService.SystemUserService opdb = new ManageService.SystemUserService();
            if(opdb.add(v))
            {
                //  提示成功

            }
            else
            {
                // 提示失败

            }
        }

        protected void bt_reset_Click(object sender, EventArgs e)
        {
            //tx_account.Text = "";
            Response.Redirect("c.html");
        }
    }
}