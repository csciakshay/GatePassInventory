﻿<%@ Page Title="" Language="VB" MasterPageFile="~/admintemplate/MasterPage.master" AutoEventWireup="false" CodeFile="empAttend.aspx.vb" Inherits="admintemplate_empAttend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Employee Attendance</h4>
                            </div>
                            <div class="content">
                                <form>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" DataKeyNames="empid" 
                                                    DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" 
                                                    BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                                    CellPadding="4" GridLines="Horizontal" Height="101px" Width="388px">
                                                    <Columns>
                                                        <asp:BoundField DataField="empid" HeaderText="empid" ReadOnly="True" 
                                                            SortExpression="empid" />
                                                        <asp:BoundField DataField="empname" HeaderText="empname" 
                                                            SortExpression="empname" />
                                                        <asp:BoundField DataField="departmentid" HeaderText="departmentid" 
                                                            SortExpression="departmentid" />
                                                        <asp:TemplateField HeaderText="Present">
                                                                <ItemTemplate>
                                                                     <asp:Button ID="Button2" Text="Present" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                     </ItemTemplate>
                                                                <ControlStyle BackColor="#6699FF" CssClass="success" />
                                                         </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                                    SelectCommand="SELECT [empid], [empname], [departmentid], [designation] FROM [empmaster]">
                                                </asp:SqlDataSource>
                                                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" 
                                                    GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
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

