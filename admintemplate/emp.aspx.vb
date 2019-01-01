Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data

Partial Class admintemplate_emp
    Inherits System.Web.UI.Page
    Dim con As New DataBase

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.doConnection()
        Dim gender As String
        gender = ""
        ' Dim cmd As New SqlCommand("insert into usermaster values('" + TextBox2.Text + "','" + TextBox3.Text + "','user')", con.DBConnection)
        Dim cmd As New SqlCommand()
        cmd.Connection = con.DBConnection
        cmd.CommandText = "StoredProcedure2"
        cmd.CommandType = CommandType.StoredProcedure
        If RadioButton1.Checked Then
            gender = "Male"
        End If
        If RadioButton2.Checked Then
            gender = "Female"
        End If
        cmd.Parameters.AddWithValue("empid", TextBox1.Text.ToString)
        cmd.Parameters.AddWithValue("EmployeeName", TextBox2.Text)
        ' cmd.Parameters.AddWithValue("MobileNo", Decimal.Parse(TextBox9.Text))
        cmd.Parameters.AddWithValue("Department", TextBox3.Text)
        cmd.Parameters.AddWithValue("Designation", TextBox4.Text)
        cmd.Parameters.AddWithValue("Gender", gender)


        If cmd.ExecuteNonQuery() Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Registration Success..."))
            ' Response.Redirect("~/login/login.aspx")
        End If
        con.ConnectionClose()

    End Sub
    Protected Function getEmpID() As Integer
        con.doConnection()
        Dim max As Integer
        Dim cmd As New SqlCommand("Select MAX(COALESCE(empid,0)) from empmaster", con.DBConnection)
        If IsDBNull(cmd.ExecuteScalar()) Then
            max = 0
        Else
            max = cmd.ExecuteScalar()
        End If

        con.ConnectionClose()
        Return max + 1

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Text = getEmpID()
    End Sub
End Class
