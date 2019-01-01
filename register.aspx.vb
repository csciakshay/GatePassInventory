Imports System.Data.SqlClient
Imports DataBase
Imports System.Data

Partial Class login
    Inherits System.Web.UI.Page
    Dim con As New DataBase
    Dim Image As String

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.doConnection()
        Dim gender As String
        gender = ""
        ' Dim cmd As New SqlCommand("insert into usermaster values('" + TextBox2.Text + "','" + TextBox3.Text + "','user')", con.DBConnection)
        Dim cmd As New SqlCommand()
        cmd.Connection = con.DBConnection
        cmd.CommandText = "CreateRegistration"
        cmd.CommandType = CommandType.StoredProcedure
        If TextBoxGender.Checked Then
            gender = "Male"
        End If
        If TextBoxGender1.Checked Then
            gender = "Female"
        End If
        cmd.Parameters.AddWithValue("UserName", TextBoxUserName.Text)
        cmd.Parameters.AddWithValue("Password", FormsAuthentication.HashPasswordForStoringInConfigFile(TextBoxPassword.Text, "sha1"))
        cmd.Parameters.AddWithValue("Role", "user")
        cmd.Parameters.AddWithValue("Name", TextBoxName.Text)
        cmd.Parameters.AddWithValue("Address", TextBoxAddress.Text)
        cmd.Parameters.AddWithValue("City", TextBoxCity.Text)
        cmd.Parameters.AddWithValue("PinCode", TextBoxPincode.Text)
        cmd.Parameters.AddWithValue("Gender", gender)
        cmd.Parameters.AddWithValue("Profilepic", Label2.Text)

        If cmd.ExecuteNonQuery() Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Registration Success..."))
            Response.Redirect("login.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (FileUpload1.HasFile) Then

            Dim str As String = FileUpload1.FileName
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + str))
            Image = "~/Upload/" + str.ToString()
            Dim name As String = TextBoxName.Text

            Label2.Text = Image
            Label1.Text = "Image Uploaded"
            Label1.ForeColor = System.Drawing.Color.ForestGreen

        Else

            Label1.Text = "Please Upload your Image"
            Label1.ForeColor = System.Drawing.Color.Red
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBoxPassword.Attributes("value") = TextBoxPassword.Text
        TextBoxConfirmpass.Attributes("value") = TextBoxConfirmpass.Text
        Label2.Attributes("value") = Label2.Text
    End Sub
End Class
