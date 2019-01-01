
Partial Class MainMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("uname") IsNot Nothing Then
            LinkButton1.Text = "Welcome  " + Session("uname")
        Else
            LinkButton1.Text = "Login"
        End If

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        
            Response.Redirect("login/login.aspx")

    End Sub
End Class

