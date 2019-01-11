Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data
Imports QRCoder
Imports System.Drawing.Bitmap
Imports iTextSharp.text.Color
Imports iTextSharp.text.pdf

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
            genrateQRCode()
            
        End If
        

    End Sub
    Sub genrateQRCode()
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCode As QRCodeGenerator.QRCode = qrGenerator.CreateQrCode(TextBox1.Text, QRCodeGenerator.ECCLevel.Q)
        ' Dim imgBarCode As New System.Web.UI.WebControls.Image()
        ' imgBarCode.Height = 150
        ' imgBarCode.Width = 150
        Using bitMap As Drawing.Bitmap = qrCode.GetGraphic(20)
            Using ms As New MemoryStream()
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim byteImage As Byte() = ms.ToArray()
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage)
            End Using
           ' plBarCode.Controls.Add(imgBarCode)
        End Using
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
            genrate_PDF()
        End If
        'clear form
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
        Dim format As String = "yyyy-MM-dd"
        con.doConnection()
        Dim cmd As New SqlCommand("select a.empid,a.empname from empmaster a left join attendancemaster b on a.empid=b.empid where CONVERT(VARCHAR(25), pre_date, 126) LIKE '" + Now.Date.ToString(format) + "%'", con.DBConnection)
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
        Dim cmd As New SqlCommand("update visitormaster set outDate = '" + TextBox8.Text + "' where visitorId='" + TextBox1.Text + "'", con.DBConnection)
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
            End If
            If dt.Rows(0)("gender").Equals("Female") Then
                RadioButton1.Checked = False
                RadioButton2.Checked = True
            End If
            imgCapture.Visible = True
            imgCapture.ImageUrl = dt.Rows(0)("profilepic").ToString
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", con.alertMessage("Visitor not found..."))
        End If
        genrateQRCode()
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

    Private Function GetData(ByVal query As String) As DataTable
        ' Dim conString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim cmd As New SqlCommand(query)

        Using sda As New SqlDataAdapter()
            cmd.Connection = con.DBConnection

            sda.SelectCommand = cmd
            Using dt As New DataTable()
                sda.Fill(dt)
                Return dt
            End Using
        End Using

    End Function
    Protected Sub genrate_PDF()

        Dim dr As DataRow = GetData("SELECT *, dbo.getEmpName(empname) as employeename FROM visitormaster where visitorId = " + TextBox1.Text + "").Rows(0)

        Dim document As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 88.0F, 88.0F, 10.0F, 10.0F)
        Dim NormalFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)
        Using memoryStream As New System.IO.MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(Document, MemoryStream)
            Dim phrase As iTextSharp.text.Phrase = Nothing
            Dim cell As PdfPCell = Nothing
            Dim table As PdfPTable = Nothing
            Dim color__1 As iTextSharp.text.Color = Nothing

            document.Open()

            'Header Table
            table = New PdfPTable(2)
            table.TotalWidth = 500.0F
            table.LockedWidth = True
            table.SetWidths(New Single() {0.3F, 0.7F})

            'Company Logo
            'cell = ImageCell("~/images/6p.jpg", 30.0F, PdfPCell.ALIGN_CENTER)
            'table.AddCell(cell)
            cell = GetQRImageCell(dr("visitorId").ToString, 10.0F, PdfPCell.ALIGN_CENTER)
            table.AddCell(cell)

            'Company Name and Address
            phrase = New iTextSharp.text.Phrase()
            phrase.Add(New iTextSharp.text.Chunk("MindLab Solution Pvt. Ltd" & vbLf & vbLf, iTextSharp.text.FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.RED)))
            phrase.Add(New iTextSharp.text.Chunk("107, Park site," & vbLf, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            phrase.Add(New iTextSharp.text.Chunk("Salt Lake Road," & vbLf, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            phrase.Add(New iTextSharp.text.Chunk("Seattle, USA", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT)
            cell.VerticalAlignment = PdfCell.ALIGN_TOP
            table.AddCell(cell)

            'Separater Line
            color__1 = New iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"))
            DrawLine(writer, 25.0F, document.Top - 79.0F, document.PageSize.Width - 25.0F, document.Top - 79.0F, color__1)
            DrawLine(writer, 25.0F, document.Top - 80.0F, document.PageSize.Width - 25.0F, document.Top - 80.0F, color__1)
            document.Add(table)

            table = New PdfPTable(2)
            table.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
            table.SetWidths(New Single() {0.3F, 1.0F})
            table.SpacingBefore = 20.0F

            'Employee Details
            cell = PhraseCell(New iTextSharp.text.Phrase("Visitor Pass", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            table.AddCell(cell)
            cell = PhraseCell(New iTextSharp.text.Phrase(), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            cell.PaddingBottom = 30.0F
            table.AddCell(cell)

            'Photo
            cell = ImageCell(String.Format(dr("profilepic").ToString), 21.0F, PdfPCell.ALIGN_LEFT)
            table.AddCell(cell)
            

            'Name
            phrase = New iTextSharp.text.Phrase()
            phrase.Add(New iTextSharp.text.Chunk("(" + dr("visitorId").ToString + ")" & " " + dr("visitorname").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)))
            'phrase.Add(New Chunk("(" + "01" + ")", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            document.Add(table)

            DrawLine(writer, 160.0F, 80.0F, 160.0F, 690.0F, iTextSharp.text.Color.BLACK)
            DrawLine(writer, 115.0F, document.Top - 180.0F, document.PageSize.Width - 100.0F, document.Top - 180.0F, iTextSharp.text.Color.BLACK)

            table = New PdfPTable(2)
            table.SetWidths(New Single() {0.5F, 2.0F})
            table.TotalWidth = 340.0F
            table.LockedWidth = True
            table.SpacingBefore = 20.0F
            table.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT

            'Employee Id
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("Gender:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase(dr("gender").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            cell = PhraseCell(New iTextSharp.text.Phrase(), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            cell.PaddingBottom = 10.0F
            table.AddCell(cell)


            'Address
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("Address:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            phrase = New iTextSharp.text.Phrase(New iTextSharp.text.Chunk(dr("address").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            phrase.Add(New iTextSharp.text.Chunk(dr("city").ToString + vbLf, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            phrase.Add(New iTextSharp.text.Chunk(dr("pincode").ToString + " " + dr("visitorId").ToString + " " + dr("visitorId").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
            table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT))
            cell = PhraseCell(New iTextSharp.text.Phrase(), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            cell.PaddingBottom = 10.0F
            table.AddCell(cell)

            'Phone
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("Phone Number:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase(dr("mobileno").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            cell = PhraseCell(New iTextSharp.text.Phrase(), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            cell.PaddingBottom = 10.0F
            table.AddCell(cell)

            'Addtional Information
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("Employee Name:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase(dr("employeename").ToString, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_JUSTIFIED))

            'Date of Birth
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("In Date:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase(Convert.ToDateTime(dr("indate").ToString).ToString("dd MMMM, yyyy hh:mm"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            cell = PhraseCell(New iTextSharp.text.Phrase(), PdfPCell.ALIGN_CENTER)
            cell.Colspan = 2
            cell.PaddingBottom = 10.0F
            table.AddCell(cell)

            table.AddCell(PhraseCell(New iTextSharp.text.Phrase("Out Date:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            table.AddCell(PhraseCell(New iTextSharp.text.Phrase(Convert.ToDateTime(dr("outdate").ToString).ToString("dd MMMM, yyyy hh:mm"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)), PdfPCell.ALIGN_LEFT))
            document.Add(table)
            document.Close()
            Dim bytes As Byte() = MemoryStream.ToArray()
            memoryStream.Close()
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.BinaryWrite(bytes)
            Response.[End]()
            Response.Close()
        End Using

    End Sub

    Private Shared Sub DrawLine(ByVal writer As PdfWriter, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single, ByVal color As iTextSharp.text.Color)
        Dim contentByte As PdfContentByte = writer.DirectContent
        contentByte.SetColorStroke(color)
        contentByte.MoveTo(x1, y1)
        contentByte.LineTo(x2, y2)
        contentByte.Stroke()
    End Sub
    Private Shared Function PhraseCell(ByVal phrase As iTextSharp.text.Phrase, ByVal align As Integer) As PdfPCell
        Dim cell As New PdfPCell(phrase)
        cell.BorderColor = iTextSharp.text.Color.WHITE
        cell.VerticalAlignment = PdfCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 2.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
    Private Shared Function ImageCell(ByVal path As String, ByVal scale As Single, ByVal align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = iTextSharp.text.Color.WHITE
        cell.VerticalAlignment = PdfCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
    Private Shared Function GetQRImageCell(ByVal id As String, ByVal scale As Single, ByVal align As Integer) As PdfPCell
        Dim imageBytes As Byte()
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCode As QRCodeGenerator.QRCode = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q)
       
        Using bitMap As Drawing.Bitmap = qrCode.GetGraphic(20)
            Using ms As New MemoryStream()
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim byteImage As Byte() = ms.ToArray()
                imageBytes = byteImage
            End Using

        End Using
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageBytes)
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = iTextSharp.text.Color.WHITE
        cell.VerticalAlignment = PdfCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        genrate_PDF()
    End Sub
End Class
