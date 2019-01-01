Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data

Partial Class admintemplate_empAttend
    Inherits System.Web.UI.Page
    Dim con As New DataBase

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "Select" Then
            'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            'Reference the GridView Row.
            Dim row As GridViewRow = GridView1.Rows(rowIndex)

            Dim country As String = row.Cells(0).Text
            MsgBox(country)
            con.doConnection()
            Dim cmd As New SqlCommand()
            cmd.Connection = con.DBConnection
            cmd.CommandText = "StoredProcedure3"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("EmpId", country)
            cmd.Parameters.AddWithValue("PreDate", Now.Date)
            cmd.Parameters.AddWithValue("Present", "Y")

            If cmd.ExecuteNonQuery() Then
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Registration Success..."))
                ' Response.Redirect("~/login/login.aspx")
            End If
            con.ConnectionClose()
        End If
    End Sub
End Class
