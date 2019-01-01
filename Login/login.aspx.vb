Imports System.Data.SqlClient
Imports DataBase
Imports System.Data

Partial Class login
    Inherits System.Web.UI.Page
    Dim con As New DataBase

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.doConnection()
        Dim cmd As New SqlCommand("select * from usermaster where username='" + TextBox1.Text + "' and password='" + FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox2.Text, "sha1") + "'", con.DBConnection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            Session("uname") = dt.Rows(0)("UserName")
            Session("role") = dt.Rows(0)("Role")
            Session("userimage") = dt.Rows(0)("Profilepic")
            Session("uname") = TextBox1.Text
            Response.Redirect("~/admintemplate/Default.aspx")
        Else
            Response.Redirect("~/login/login.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session.Clear()
        'Session.Abandon()

    End Sub
End Class
