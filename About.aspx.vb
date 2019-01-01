
Partial Class About
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("uname") IsNot Nothing Then
            Label1.Text = Session("uname")
        Else
            Response.Redirect("~/login/login.aspx")
        End If
    End Sub
End Class
