
Partial Class admintemplate_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/login/login.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("uname") IsNot Nothing Then
            Response.Redirect("~/login/login.aspx")
        End If
    End Sub
End Class

