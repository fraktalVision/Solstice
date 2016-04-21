﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            txtLastName.Text = (string)Session["lastName"];
        else 
            Validate();
    }
   
    protected void btnSuggestPswdList_Click(object sender, EventArgs e)
    {
        string lastName = "";
        string birthYear = "";
        string color = "";
        string space = " ";

        Validate();
        if (IsValid)
        {
            lastName = txtLastName.Text;
            birthYear = txtBirthYear.Text;
            color = txtFavoriteColor.Text;
        }

        
        //clear the password lsit box
        if (IsPostBack)
            lstSuggestedPswdList.Items.Clear();

        // Create an array to hold 5 suggested passswords
        string[] items = new string[5] { "", "", "", "", "" };
        // Reverse birthYear
        char[] toReverse = birthYear.ToArray();
        Array.Reverse(toReverse);
        String birthYearReversed = new String(toReverse);

        // Total of lastName, birthYear, and color is more than 8
        if (lastName.Length >= 1 && color.Length > 2 && birthYear.Length == 4)
        {
            items[0] = color.Substring(0, 3) + birthYear + lastName.Substring(0, 2);
            items[1] = lastName + color.Substring(0, 3) + birthYear;
            items[2] = birthYearReversed + lastName.Substring(0, 2) + color;
            items[3] = color.Substring(0, 3) + birthYearReversed + lastName;
            items[4] = lastName + color.Substring(0, 3) + birthYearReversed;
        }
        // Total of lastName, birthYear, and color is less than 8
        else
        {
            if((lastName.Length + color.Length + birthYear.Length) < 8)
            {
                items[0] = color.Substring(0, 3) + birthYear + lastName.Substring(0, 2) + birthYearReversed.Substring(0,3);
                items[1] = lastName + color.Substring(0, 3) + birthYear + birthYearReversed.Substring(0, 3);
                items[2] = birthYearReversed + lastName.Substring(0, 2) + birthYear.Substring(0, 3) + color;
                items[3] = color.Substring(0, 3) + birthYearReversed + lastName + birthYear.Substring(0, 3);
                items[4] = birthYear.Substring(0, 3) + lastName + color.Substring(0, 3) + birthYearReversed;
            }
        }
        // Add the array values into the listbox
        for (int i = 0; i < items.Length; i++)
        {
            // If there is a space, remove it
            if (items[i].Contains(space))
                items[i] = items[i].Replace(" ", String.Empty);
            lstSuggestedPswdList.Items.Add(items[i]);
        }
    }

    protected void lstSuggestedPswdList_SelectedIndexChanged(object sender, EventArgs e)
    {
            lstSuggestedPswdList.SelectedItem.Selected = true;
       
            Session["password"] = lstSuggestedPswdList.SelectedItem.ToString();
            Session["username"] = txtLastName.Text;
            lblConfirmation.Text = "New account created for " + Session["username"] + " with" + "<br />" + "Password: " + Session["password"] + "<br />" + "Please click to log in";
            lblConfirmation.Visible = true;
            btnWelcomeLogin.Visible = true;
          
    }

    protected void btnWelcomeLogin_Click(object sender, EventArgs e)
    {
        Server.Transfer("Login.aspx");
    }
}