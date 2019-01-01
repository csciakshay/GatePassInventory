
Partial Class ContactUs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("uname") IsNot Nothing Then
            Response.Redirect("~/login/login.aspx")
        End If
    End Sub
End Class
