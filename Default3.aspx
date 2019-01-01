<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="Default3.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="order-section-page">
		<div class="ordering-form">
			<div class="container">
    <asp:Label ID="Label1" runat="server" Text="Welcome" Font-Bold="True" Font-Italic="True" 
                    Font-Size="Medium"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" />
            <asp:ImageField DataImageUrlField="ProfilePic" HeaderText="Profile Pic">
                <ControlStyle Height="50px" Width="50px" />
            </asp:ImageField>
        </Columns>
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
                <asp:DataList ID="DataList1" runat="server" 
                    DataSourceID="SqlDataSource1"  BackColor="PowderBlue" BorderColor="#666666"

            BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"

            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal"

            Width="1075px" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" />
            <ItemTemplate>
                productName:
                <asp:Label ID="productNameLabel" runat="server" 
                    Text='<%# Eval("productName") %>' />
                <br />
                productRate:
                <asp:Label ID="productRateLabel" runat="server" 
                    Text='<%# Eval("productRate") %>' />
                <br />
                description:
                <asp:Label ID="descriptionLabel" runat="server" 
                    Text='<%# Eval("description") %>' />
                <br />
               
                <asp:Image ID="imagePathLabel" runat="server" ImageUrl='<%# Eval("imagePath") %>' Width="150px" Height="120px" style="padding-left:40px" />
                <br />
                <asp:Button ID="Button1"  text="Add" class="acount-btn" runat="server" />
<br />
            </ItemTemplate>
                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" 
                        Font-Italic="True" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                    SelectCommand="SELECT * FROM [ProdMst]"></asp:SqlDataSource>
    </div>
    </div>
    </div>
</asp:Content>

