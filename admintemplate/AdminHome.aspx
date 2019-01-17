<%@ Page Title="" Language="VB" MasterPageFile="~/admintemplate/AdminMaster.master" AutoEventWireup="false" CodeFile="AdminHome.aspx.vb" Inherits="admintemplate_AdminHome" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Home</h4>
                            </div>
                            <div class="content">
                               
                                    <div class="row">
                                     <div class="col-md-6">
                                        <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" 
                                            Width="486px">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="empid" YValueMembers="pre_date" 
                                                    ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <AxisY Title="Present">
                                                    </AxisY>
                                                    <AxisX Title="Employee">
                                                    </AxisX>
                                                    <Area3DStyle Enable3D="True" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Legends>
                                                <asp:Legend Name="Legend1" Title="Employee">
                                                </asp:Legend>
                                            </Legends>
                                        </asp:Chart>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                            SelectCommand="SELECT empid,count(pre_date) as pre_date FROM [attendancemaster] group by empid"></asp:SqlDataSource>
                                            </div>
                                            <div class="col-md-6">
                                            <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource2" 
                                            Width="481px">
                                            <Series>
                                                <asp:Series Name="Series1" XValueMember="visitorname" YValueMembers="total_visitor" 
                                                    ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                    <AxisY Title="Present">
                                                    </AxisY>
                                                    <AxisX Title="Visitor">
                                                    </AxisX>
                                                    <Area3DStyle Enable3D="True" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Legends>
                                                <asp:Legend Name="Legend1" Title="Visitor">
                                                </asp:Legend>
                                            </Legends>
                                        </asp:Chart>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                            SelectCommand="SELECT visitorname,count(visitorid) as total_visitor FROM [visitormaster] group by visitorname"></asp:SqlDataSource>
                                    </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                   
                </div>
            </div>
        </div>
</asp:Content>

