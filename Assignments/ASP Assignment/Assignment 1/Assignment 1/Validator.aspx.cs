using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // if the all conditions are fulfilled... then transfer to the welcome page...
            if (Page.IsValid)
            {
                string name = txtName.Text;
                string fName = txtFamilyName.Text;
                string address = txtAddress.Text;
                string city = txtCity.Text;
                string zipCode = txtZipCode.Text;
                string phone = txtPhone.Text;
                string email = txtEmail.Text;

               
                Response.Redirect("Welcome.aspx?Name=" + name + "&FamilyName=" + fName + "&Address=" + address + "&City=" + city + "&ZipCode=" + zipCode + "&Phone=" + phone + "&Email=" + email);
            }
            else
            {
                
            }
        }
    }
}