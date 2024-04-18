using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve data from query string
                string name = Request.QueryString["Name"];
                string familyName = Request.QueryString["FamilyName"];
                string address = Request.QueryString["Address"];
                string city = Request.QueryString["City"];
                string zipCode = Request.QueryString["ZipCode"];
                string phone = Request.QueryString["Phone"];
                string email = Request.QueryString["Email"];

                // Display data in labels
                lblname.Text = "Name: " + name;
                lblfname.Text = "Family Name: " + familyName;
                lbladdress.Text = "Address: " + address;
                lblcity.Text = "City: " + city;
                lblzipCode.Text = "Zip Code: " + zipCode;
                lblphone.Text = "Phone: " + phone;
                lblemail.Text = "Email: " + email;
            }
        }
    }
}