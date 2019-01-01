Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class DataBase
    Public DBConnection As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ApplicationServices").ConnectionString)
    Dim flag As Boolean

    Public Function doConnection() As Boolean
        DBConnection.Open()

        If DBConnection.State Then
            flag = True
        Else
            flag = False
        End If

        Return flag

    End Function

    Public Function ConnectionClose() As Boolean
        DBConnection.Close()
        Return True
    End Function
    Public Function alertMessage(ByVal message As String) As String
        'Dim message As String = "Hello! Mudassar."
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")
        sb.Append("</script>")
        Return sb.ToString()

    End Function
End Class
