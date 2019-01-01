<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 50%;
            background-color: #C0C0C0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" class="style1">
        <tr>
            <td>
                Name</td>
            <td>
                <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Gender</td>
            <td>
                <asp:RadioButton ID="TextBoxGender" runat="server" Text ="Male" GroupName="TextBoxGender"/>
                <asp:RadioButton ID="TextBoxGender1" runat="server" Text="Female" GroupName="TextBoxGender"/>
               
            </td>
        </tr>
        <tr>
            <td>
                Birth Date</td>
            <td>
                <asp:TextBox ID="TextBoxBirthDate" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Address</td>
            <td>
                <asp:TextBox ID="TextBoxAddress" runat="server" TextMode ="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                City</td>
            <td>
                <asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Pincode</td>
            <td>
                <asp:TextBox ID="TextBoxPincode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                UserName</td>
            <td>
                <asp:TextBox ID="TextBoxUserName" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Password</td>
            <td>
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Conform Password</td>
            <td>
                <asp:TextBox ID="TextBoxConfirmpass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Picture</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" accept="image/png, image/jpeg" />
               
                <asp:Button ID="Button2" runat="server" Text="Upload" />
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
               
            </td>
        </tr>
        <tr>
        <td colspan=2 align=center>
            <asp:Button ID="Button1" runat="server" Text="Login" />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </td>
        </tr>
    </table>
</asp:Content>

