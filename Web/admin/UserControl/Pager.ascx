<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="Web.admin.UserControl.Pager" %>
第<asp:Label runat="server" ID="lblCurrent" Text="0" />页 / 共<asp:Label runat="server"
    ID="lblTotal" Text="0" />页&nbsp; &nbsp;&nbsp; &nbsp;
<asp:LinkButton ID="lbnFirst" runat="server" Text="首页" OnClick="lbnFirst_Click" />&nbsp; 
<asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" OnClick="lbnPrev_Click" />&nbsp; 
<asp:LinkButton ID="lbnNext" runat="server" Text="下一页" OnClick="lbnNext_Click" />&nbsp; 
<asp:LinkButton ID="lbnLast" runat="server" Text="末页" OnClick="lbnLast_Click" />
&nbsp; &nbsp;&nbsp; &nbsp;第:<asp:TextBox runat="server" ID="txtGo" 
    CssClass="input_textbox" Text="0"
    Width="60" Font-Bold="True" />页
<asp:Button ID="btnGo" runat="server" Text="GO" OnClick="btnGo_Click" CssClass="button_go" />