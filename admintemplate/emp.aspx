<%@ Page Title="" Language="VB" MasterPageFile="~/admintemplate/MasterPage.master" AutoEventWireup="false" CodeFile="emp.aspx.vb" Inherits="admintemplate_emp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Employee Entry</h4>
                            </div>
                            <div class="content">
                                <form>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Visitor Id (disabled)</label>
                                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" disabled placeholder="Company"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Employee Name</label>
                                               <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Visitorname"></asp:TextBox>
                                                <%--<input type="text" class="form-control" placeholder="Username" value="michael23">--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Gender</label></br>
                                                <asp:RadioButton ID="RadioButton1" runat="server" value="Male" GroupName ="gender"/> Male
                                                <asp:RadioButton ID="RadioButton2" runat="server"  value="Female" GroupName="gender"/> Female
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Department</label>
                                                <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="Employeename"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                 <label>Designation</label>
                                                <asp:TextBox ID="TextBox4" runat="server" class="form-control" placeholder="Address"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                   <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City</label>
                                               <asp:TextBox ID="TextBox5" runat="server" class="form-control" placeholder="City"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile No</label>
                                                <asp:TextBox ID="TextBox9" runat="server" class="form-control" placeholder="Mobile No"></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Postal Code</label>
                                                <asp:TextBox ID="TextBox6" runat="server" class="form-control" placeholder="ZIP Code"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    
                                <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-info btn-fill pull-right"/>
                                
                                    <div class="clearfix"></div>
                                </form>
                            </div>
                        </div>
                    </div>
                   
                </div>
            </div>
        </div>
</asp:Content>

