<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Assignment_1.Validator" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title></title>
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
                <h2><u><center>Validation-From</center></u></h2>

            <div style="margin-left: 40px">
                <label for="txtName">&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp; Name : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </label>
                <asp:TextBox ID="txtName" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredNameValidator" runat="server" ControlToValidate="txtName" ErrorMessage="Name is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <br />
            </div>

           <div style="margin-left: 40px">
                <label for="txtFamilyName">&nbsp;&nbsp; Family Name :&nbsp;&nbsp;&nbsp; </label>
                <asp:TextBox ID="txtFamilyName" runat="server" Width="200px" BackColor="#FFFFCC" ForeColor="Black" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="error-message" ID="RequiredFamilyNameValidator" runat="server" ControlToValidate="txtFamilyName" ErrorMessage="Family Name is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:CompareValidator CssClass ="error-message" ID="CompareNameValidator" runat="server" ControlToValidate="txtName" ControlToCompare="txtFamilyName" Operator="NotEqual" ErrorMessage="Name and Family Name should not match" ForeColor="Red"></asp:CompareValidator>
                <br />
                <br />
           </div>

            <div style="margin-left: 40px; height: 99px;">
                <label for="txtAddress">&nbsp;&nbsp; Address :</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
                <asp:TextBox ID="txtAddress" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredAddressValidator" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="MinLengthAddressValidator" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address must be at least 2 characters long" ValidateValue="ValidateMinLength" ForeColor="Red"></asp:CustomValidator>
                
            </div>

            <div style="margin-left: 40px">
                <label for="txtCity">&nbsp;&nbsp; City :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; </label>
                <asp:TextBox ID="txtCity" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredCityValidator" runat="server" ControlToValidate="txtCity" ErrorMessage="City is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="MinLengthCityValidator" runat="server" ControlToValidate="txtCity" ErrorMessage="City must be at least 2 characters long" ValidateValue="ValidateMinLength" ForeColor="Red"></asp:CustomValidator>
                <br />
                <br />
                <br />
            </div>

            <div style="margin-left: 40px">
                <label for="txtZipCode">&nbsp;&nbsp; Zip Code:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; </label>
                <asp:TextBox ID="txtZipCode" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="regexZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code must be 5 digits" ValidationExpression="\d{5}" ForeColor="Red"></asp:RegularExpressionValidator>
                <br />
                <br />
                <br />
            </div>

            <div style="margin-left: 40px">
                <label for="txtPhone">&nbsp;&nbsp; Phone:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </label>
                <asp:TextBox ID="txtPhone" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone Number is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="regexPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="format (XX-XXXXXXX or XXX-XXXXXXX)" ValidationExpression="\d{10}" ForeColor="Red"></asp:RegularExpressionValidator>
                <br />
                <br />
            </div>
 
            <div style="margin-left: 40px">
                <label for="txtEmail">&nbsp;&nbsp; E-Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </label>
                <asp:TextBox ID="txtEmail" runat="server" Width="200px" BackColor="#FFFFCC" BorderStyle="None" Height="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format (xyz@xyz.xyz)" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+" ForeColor="Red"></asp:RegularExpressionValidator>
            </div>
            <div>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="Check" OnClick="Button1_Click" BackColor="#FFFF99" BorderStyle="None" Height="33px" style="margin-top: 0px" Width="89px" />
            </div>
        </form>
    </body>
</html>