<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="Assignment_1.Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome Form</title>
    <style>
         #form1 {
                background-color: lightblue;
                width: 648px;
                margin: 41px auto 50px auto;
                padding: 20px;
                border-radius: 20px;
                border-block-color:black;
                border-block:groove;
                border-width: 10px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2><center><u>Welcome Form</u></center></h2>
            <asp:Label ID="lblname" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblfname" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lbladdress" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblcity" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblzipCode" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblphone" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblemail" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
