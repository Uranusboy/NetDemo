﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="Web.admin.SystemUserSet.userlist" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="CoreUI - Open Source Bootstrap Admin Template">
    <meta name="author" content="luosir">
    <link rel="shortcut icon" href="../img/favicon.png">

    <title>系统用户设置</title>

    <!-- Icons -->
    <link href="../css/font-awesome.min.css" rel="stylesheet">
    <link href="../css/simple-line-icons.css" rel="stylesheet">

    <!-- Main styles for this application -->
    <link href="../css/style.css" rel="stylesheet">
</head>

<body class="app header-fixed sidebar-fixed aside-menu-fixed aside-menu-hidden">
    <header class="app-header navbar">
        <!--移动端时出-->
        <button class="navbar-toggler mobile-sidebar-toggler hidden-lg-up" type="button">☰</button>
        <!--顶部左边导向 -->
        <a class="navbar-brand" href="#"></a>
        <ul class="nav navbar-nav hidden-md-down">
            <li class="nav-item">
                <a class="nav-link navbar-toggler sidebar-toggler" href="#">☰</a>
            </li>
            <li class="nav-item px-1">
                <a class="nav-link" href="../1_tables.html">基础信息</a>
            </li>
            <li class="nav-item px-1">
                <a class="nav-link" href="#">党团学工</a>
            </li>
            <li class="nav-item px-1">
                <a class="nav-link" href="#">新闻中心</a>
            </li>
            <li class="nav-item px-1">
                <a class="nav-link" href="#">用户管理</a>
            </li>
        </ul>
        <!--顶部右边导向-->
        <ul class="nav navbar-nav ml-auto">
            <li class="nav-item dropdown">
                <!--用户头像-->
                <a class="nav-link dropdown-toggle nav-link" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">
                    <img src="../img/avatars/6.jpg" class="img-avatar" alt="admin@bootstrapmaster.com">
                    <span class="hidden-md-down">admin</span>
                </a>
                <!--下拉-->
                <div class="dropdown-menu dropdown-menu-right">
                    <!--下拉标题-->
                    <div class="dropdown-header text-center">
                        <strong>Account</strong>
                    </div>
                    <a class="dropdown-item" href="#"><i class="fa fa-bell-o"></i>Updates<span class="badge badge-info">42</span></a>
                    <a class="dropdown-item" href="#"><i class="fa fa-envelope-o"></i>Messages<span class="badge badge-success">42</span></a>
                    <!--下拉标题-->
                    <div class="dropdown-header text-center">
                        <strong>Settings</strong>
                    </div>
                    <a class="dropdown-item" href="#"><i class="fa fa-user"></i>Profile</a>
                    <a class="dropdown-item" href="#"><i class="fa fa-shield"></i>Lock Account</a>
                </div>
            </li>
            <!--最右边图片-->
            <!--<li class="nav-item hidden-md-down">
                <a class="nav-link navbar-toggler aside-menu-toggler" href="#">☰</a>
            </li>-->
        </ul>
    </header>
    <!--左边导向-->
    <div class="app-body">
        <div class="sidebar">
            <!--左边导向-->
            <nav class="sidebar-nav">
                <ul class="nav">
                    <li class="nav-title">系统用户管理
                    </li>
                    <li class="nav-item nav-dropdown">
                        <a class="nav-link nav-dropdown-toggle" href="Systemuserlist.html"><i class="icon-star"></i>系统用户管理</a>

                    </li>

                </ul>
            </nav>
        </div>

        <!-- Main content -->
        <main class="main">
            <form id="form1" runat="server">
                <!-- Breadcrumb -->
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">主页</li>
                    <li class="breadcrumb-item">
                        <a href="#">后台管理</a>
                    </li>
                    <li class="breadcrumb-item active">系统用户管理</li>

                    <!-- Breadcrumb Menu-->
                </ol>

                <div class="container-fluid">
                    <div class="animated fadeIn">
                        <!--表格-->
                        <div class="row">
                            <div class="col-lg-12">
                                <!--查询条件-->
                                <div class="card">
                                    <!--<div class="card-header">
                                    <strong>Company</strong>
                                    <small>Form</small>
                                </div>-->
                                    <div class="card-block">
                                        <div class="row">
                                            <div class="form-inline col-sm-6">
                                                姓名：
                                            <input type="text" class="form-control" id="city" placeholder="Enter your city">
                                                <asp:TextBox ID="tx_Name" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-inline col-sm-3">
                                                类型：
                                            <select class="form-control" id="ccyear">
                                                <option>管理员</option>
                                                <option>学工</option>
                                                <option>xxxx</option>
                                            </select>
                                            </div>
                                            <div class="form-inline col-sm-3">
                                                <button type="button" class="btn btn-outline-primary active">&nbsp; &nbsp; 查询&nbsp; &nbsp; </button>
                                                &nbsp;
                                            <button type="button" class="btn btn-outline-secondary active">&nbsp; &nbsp; 重置&nbsp; &nbsp; </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--查询条件end-->


                                <div class="card">
                                    <div class="card-header">
                                        <i class="fa fa-align-justify"></i>系统用户列表
                                    </div>
                                    <div class="card-block">
                                        <asp:Repeater ID="Repeater_data" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-bordered table-striped table-condensed">
                                                    <thead>
                                                        <tr>
                                                            <th>姓名</th>
                                                            <th>账号</th>
                                                            <th>角色</th>
                                                            <th width="128">操作</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("UserName")%></td>
                                                    <td><%#Eval("UserAccount")%></td>
                                                    <td><%#Eval("UserType")%></td>
                                                    <td>
                                                        <asp:LinkButton CssClass="button22" ID="LinkButton_Edit" runat="server" CommandArgument='<%#Eval("UserID") %>'
                                                            OnCommand="edit_stu_info">修改</asp:LinkButton>
                                                        <asp:LinkButton CssClass="button33" ID="LinkButton_Del" runat="server" OnClientClick='return confirm("您确定要删除吗？")'
                                                            CommandArgument='<%#Eval("UserID") %>' OnCommand="del_stu_info">删除</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                    </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <!--操作按钮-->
                                        <a href="add_systemuser.html">
                                            <button type="button" class="btn btn-outline-primary active">添加系统用户</button>
                                        </a>
                                        <!--<button type="button" class="btn btn-outline-secondary active">Secondary</button>-->
                                        <!--分页-->
                                        <nav class="float-right">
                                            <uc1:Pager ID="Pager1" runat="server" />
                                        </nav>
                                        <!--分页end-->
                                    </div>
                                </div>
                            </div>
                            <!--/.col-->
                        </div>
                        <!--/.row-->
                    </div>

                </div>
                <!-- /.conainer-fluid -->
            </form>
        </main>


    </div>

    <footer class="app-footer">
        <a href="#">LuoSir</a> © 2017 creativeLabs.
        <span class="float-right">xxxxxx <a href="#" target="_blank" title="xxxx">Luosir</a> - Collect from </a>
        </span>
    </footer>

    <!-- Bootstrap and necessary plugins -->
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <script src="../bower_components/tether/dist/js/tether.min.js"></script>
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="../bower_components/pace/pace.min.js"></script>


    <!-- Plugins and scripts required by all views -->
    <script src="../bower_components/chart.js/dist/Chart.min.js"></script>


    <!-- GenesisUI main scripts -->

    <script src="../js/app.js"></script>



</body>

</html>
