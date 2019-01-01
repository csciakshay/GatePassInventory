
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session("uname")
        If Not Session("uname") IsNot Nothing Then
            Response.Redirect("~/login/login.aspx")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Session.Timeout() = 0
        Session.Clear()
        Session.Abandon()
        ' Label1.Text = Session("uname")

    End Sub
End Class
