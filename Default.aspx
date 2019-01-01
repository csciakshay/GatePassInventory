<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Logout" />
    </h2>
    <p>
        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" 
           BackColor="LightGray" BorderColor="#666666"

            BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"

            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal"

            Width="600px">
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
               
                <asp:Image ID="imagePathLabel" runat="server" ImageUrl='<%# Eval("imagePath") %>' Width="100px" Height="120px" style="padding-left:40px" />
                <br />
                <asp:ImageButton ID="ImageButton1"  ImageUrl="~/images/icon1.jpg" Width="30px" Height="30px" runat="server" />
<br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT * FROM [ProdMst]"></asp:SqlDataSource>
    </p>
    </asp:Content>