<%@ Page Title="" Language="VB" MasterPageFile="~/admintemplate/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admintemplate_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

<script src="assets/datetimepicker/jquery.js"></script>
    <script src="assets/datetimepicker/jquery.datetimepicker.full.min.js"></script>

<script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>

       <script type="text/javascript">
           $(function () {
               jQuery('#TextBox7').datetimepicker({
                   format: 'd/m/Y H:i',
                   minDate: new Date()
               });
               jQuery('#TextBox8').datetimepicker({
                   format: 'd/m/Y H:i',
                   minDate: new Date()
               });
           });
    </script>
<script type="text/javascript">

    var pageUrl = '<%=ResolveUrl("~/admintemplate/Default.aspx") %>';
    $(function () {
        jQuery("#webcam").webcam({
            width: 320,
            height: 240,
            mode: "save",
            swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
            debug: function (type, status) {
                $('#camStatus').append(type + ": " + status + '<br /><br />');
            },
            onSave: function (data) {
                $.ajax({
                    type: "POST",
                    url: pageUrl + "/GetCapturedImage",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                       // $("[id*=imgCapture]").css("visibility", "visible");
                        $("[id*=imgCapture]").attr("src", r.d);
                        
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            },
            onCapture: function () {
                webcam.save(pageUrl);
            }
        });
        
    });
    function Capture() {
        webcam.capture();
        return false;
    }
</script>

<script src="assets/js/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="assets/js/jquery.dynDateTime.min.js" type="text/javascript"></script>
<script src="assets/js/calendar-en.min.js" type="text/javascript"></script>
<link href="assets/css/calendar-blue.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-8">
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Visitor Entry</h4>
                            </div>
                            <div class="content">
                                
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Visitor Id (disabled)</label>
                                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" 
                                                    placeholder="Company" ReadOnly="True"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Visitor Name</label>
                                               <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Visitorname"></asp:TextBox>
                                                <%--<input type="text" class="form-control" placeholder="Username" value="michael23">--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Gender</label></br>
                                                <asp:RadioButton ID="RadioButton1" runat="server" value="Male" GroupName ="gender"/> Male
                                                <asp:RadioButton ID="RadioButton2" runat="server"  value="Female" GroupName="gender"/> Female
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Employee Name</label>
                                                <%--<asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="Employeename"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="DropDownList1"  class="form-control" runat="server">
                                                    </asp:DropDownList>                                           
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                 <label>Address</label>
                                                <asp:TextBox ID="TextBox4" runat="server" class="form-control" placeholder="Address"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                   <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City</label>
                                               <asp:TextBox ID="TextBox5" runat="server" class="form-control" placeholder="City"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile No</label>
                                                <asp:TextBox ID="TextBox9" runat="server" class="form-control" placeholder="Mobile No"></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Postal Code</label>
                                                <asp:TextBox ID="TextBox6" runat="server" class="form-control" placeholder="ZIP Code"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div  class="row">
                                     <div class="col-md-6">
                                      <div class="form-group">
                                     <label>In Date</label>
                                        <asp:TextBox ID="TextBox7" ClientIDMode="Static" class="form-control" runat="server"  ></asp:TextBox>

                                        </div>
                                     </div>
                                     <div class="col-md-6">
                                      <div class="form-group">
                                     <label>Out Date</label>
                                        <asp:TextBox ID="TextBox8" ClientIDMode="static" class="form-control" runat="server"></asp:TextBox>
                                         
                                     </div>
                                     </div>
                                    </div>
                                    <div class="row">
                                    <div class="col-md-6">
                                    <label></label>
                                     <div id="webcam">
                                                </div>
                                    </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                            <label>Picture</label>
                                                <asp:Image ID="imgCapture" runat="server" Style="width: 100px; height: 100px" />
                                                <asp:Image ID="imgBarCode" runat="server" Style="width: 150px; height: 150px" />  
                                                <%--<asp:PlaceHolder ID="plBarCode" runat="server"></asp:PlaceHolder>--%>
                                                 <br />
                                                 <asp:Button ID="btnCapture" Text="Capture" runat="server" class="btn btn-info btn-fill pull-right" OnClientClick="return Capture();" />
                                                 <br />
                                            <span id="camStatus"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-success btn-fill"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" Text="Out" class="btn btn-danger btn-fill "/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                    
                                <asp:Button ID="Button3" runat="server" Text="Clear" class="btn btn-info btn-fill"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="Button5" runat="server" Text="Print" class="btn btn-edit btn-fill" />
                                    <div class="clearfix"></div>
                                 </div>
                                 <div class="col-md-2"></div>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                    <div class="card">
                            <div class="header">
                                <h4 class="title">Visitor Search</h4>
                            </div>
                            <div class="content">
                                                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Visitor Id</label>
                                                <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="Company"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                       </div>
                                       <div class="row">
                                       <div class="col-md-12">
                                            <div class="form-group">
                                             <asp:Button ID="Button4" runat="server" Text="Search" class="btn btn-success btn-fill pull-right"/>                                    
                                            </div>
                                        </div>
                                       </div>
                                    
                            </div>                    
                       </div>
                </div>
            </div>
        </div>
      
    </div>
</asp:Content>

