Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data

Partial Class admintemplate_Default
    Inherits System.Web.UI.Page
    Dim con As New DataBase

    Private Shared Function ConvertHexToBytes(ByVal hex As String) As Byte()
        Dim bytes As Byte() = New Byte(hex.Length / 2 - 1) {}
        For i As Integer = 0 To hex.Length - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Shared Function GetCapturedImage() As String
        Dim url As String = HttpContext.Current.Session("CapturedImage").ToString()
        ' HttpContext.Current.Session("CapturedImage") = Nothing
        Return url
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Request.InputStream.Length > 0 Then
                Using reader As New StreamReader(Request.InputStream)
                    Dim hexString As String = Server.UrlEncode(reader.ReadToEnd())
                    Dim imageName As String = DateTime.Now.ToString("dd-MM-yy hh-mm-ss")
                    Dim imagePath As String = String.Format("~/Captures/{0}.png", imageName)
                    File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString))
                    Session("CapturedImage") = ResolveUrl(imagePath)
                End Using
            End If
            ' DropDownList1.SelectedIndex = 0
            TextBox1.Text = getVisitorID()
            getEmployeeList()
        End If
        

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        MsgBox(TextBox1.Text.ToString)

        con.doConnection()
        Dim gender As String
        gender = ""
        ' Dim cmd As New SqlCommand("insert into usermaster values('" + TextBox2.Text + "','" + TextBox3.Text + "','user')", con.DBConnection)
        Dim cmd As New SqlCommand()
        cmd.Connection = con.DBConnection
        cmd.CommandText = "StoredProcedure1"
        cmd.CommandType = CommandType.StoredProcedure
        If RadioButton1.Checked Then
            gender = "Male"
        End If
        If RadioButton2.Checked Then
            gender = "Female"
        End If
        cmd.Parameters.AddWithValue("VisitorId", TextBox1.Text.ToString)
        cmd.Parameters.AddWithValue("VisitorName", TextBox2.Text)
        cmd.Parameters.AddWithValue("InDate", TextBox7.Text)
        cmd.Parameters.AddWithValue("OutDate", TextBox8.Text)
        cmd.Parameters.AddWithValue("MobileNo", Decimal.Parse(TextBox9.Text))
        cmd.Parameters.AddWithValue("EmpName", DropDownList1.SelectedItem.Value)
        cmd.Parameters.AddWithValue("Address", TextBox4.Text)
        cmd.Parameters.AddWithValue("City", TextBox5.Text)
        cmd.Parameters.AddWithValue("PinCode", Integer.Parse(TextBox6.Text))
        cmd.Parameters.AddWithValue("Gender", gender)
        cmd.Parameters.AddWithValue("Profilepic", Session("CapturedImage").ToString())

        If cmd.ExecuteNonQuery() Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Registration Success..."))
            ' Response.Redirect("~/login/login.aspx")
        End If
        Me.Button3_Click(sender, e)

        con.ConnectionClose()

    End Sub

    Protected Function getVisitorID() As Integer
        con.doConnection()
        Dim max As Integer
        Dim cmd As New SqlCommand("Select MAX(COALESCE(visitorId,0)) from visitormaster", con.DBConnection)
        If IsDBNull(cmd.ExecuteScalar()) Then
            max = 0
        Else
            max = cmd.ExecuteScalar()
        End If

        con.ConnectionClose()
        Return max + 1

    End Function
    Protected Function getEmployeeList() As Boolean
        con.doConnection()
        Dim cmd As New SqlCommand("select empid,empname from empmaster", con.DBConnection)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da.Fill(dt)

        'If dt.Rows.Count > 0 Then
        DropDownList1.DataTextField = "empname"
        DropDownList1.DataValueField = "empid"
        DropDownList1.DataSource = dt
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, New ListItem("--Select Employee--", "0"))
        'End If
        con.ConnectionClose()
        Return True
    End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        con.doConnection()
        Dim cmd As New SqlCommand("update visitormaster set outDate = '" + TextBox8.Text + "' where visiterId='" + TextBox1.Text + "'", con.DBConnection)
        If cmd.ExecuteNonQuery() Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Visitor out Success..."))
            ' Response.Redirect("~/login/login.aspx")
        End If
        con.ConnectionClose()


    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        con.doConnection()

        Dim cmd As New SqlCommand("Select * from visitormaster where visitorId=" + TextBox3.Text + "", con.DBConnection)
        Dim adp As New SqlDataAdapter()
        adp.SelectCommand = cmd
        Dim dt As New Data.DataTable()
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then

            TextBox1.Text = dt.Rows(0)("visitorId").ToString
            TextBox2.Text = dt.Rows(0)("visitorname").ToString
            TextBox4.Text = dt.Rows(0)("address").ToString
            TextBox5.Text = dt.Rows(0)("city").ToString
            TextBox6.Text = dt.Rows(0)("pincode").ToString
            TextBox7.Text = dt.Rows(0)("indate").ToString
            TextBox8.Text = dt.Rows(0)("outdate").ToString
            TextBox9.Text = dt.Rows(0)("mobileno").ToString
            DropDownList1.SelectedValue = dt.Rows(0)("empname").ToString

            If dt.Rows(0)("gender").Equals("Male") Then
                RadioButton1.Checked = True
                RadioButton2.Checked = False
            Else
                RadioButton1.Checked = False
                RadioButton2.Checked = True
            End If
            imgCapture.Visible = True
            imgCapture.ImageUrl = dt.Rows(0)("profilepic").ToString
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Visitor not found..."))
        End If

        con.ConnectionClose()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = getVisitorID()
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        RadioButton1.Checked = True
        RadioButton2.Checked = False
        imgCapture.ImageUrl = ""
        DropDownList1.ClearSelection()
        DropDownList1.DataBind()
    End Sub
End Class
