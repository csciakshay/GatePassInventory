Imports System.Data.SqlClient
Imports DataBase
Partial Class Profile
    Inherits System.Web.UI.Page
    Dim con As New DataBase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As New SqlCommand()
        cmd.Connection = con.DBConnection
        cmd.CommandText = "select * from usermaster where UserName='" + Session("uname") + "'"
        Dim adp As New SqlDataAdapter()
        adp.SelectCommand = cmd
        Dim ds As New Data.DataSet()
        adp.Fill(ds)
        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()
    End Sub
End Class
