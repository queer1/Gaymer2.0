﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Normal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void FillData()
    {
        GaymerLINQDataContext db = new GaymerLINQDataContext();
        LoginLib login = new LoginLib();
        var user = (from a in db.Users
                    where a.UID == login.GetUserID()
                    select new
                    {
                        Uname = a.Username,   
                        Role = a.RoleID,
                        Firstname = a.UserAbout.FirstName,
                        Lastname = a.UserAbout.LastName,
                        Avatar = "",
                        AboutMe = "",
                        Birthdate = a.UserAbout.Birthdate,
                        Sex = a.UserAbout.Gender,
                        Living = ""
                    }).FirstOrDefault();

        var rolle = (from b in db.UserRoles
                     where b.RoleID == user.Role
                     select new
                         {
                             URole = b.Role
                         }).FirstOrDefault();

        Username.Text = user.Uname;
        lblRolle.Text = rolle.URole; 

        MyAvatar.ImageUrl = "~Style/Avatar/" + user.Avatar;
        MyAvatar.AlternateText = user.Uname + " Avatar";

        AboutMeTxt.Text = user.AboutMe;

        UsersNameTxt.Text = user.Firstname + " " + user.Lastname;
        UserAgeTxt.Text = AgeYr(user.Birthdate).ToString();
        UserSexTxt.Text = user.Sex == null ? "Undefined" : user.Sex == true ? "Woman" : "Man";
        UserLivingPlaceTxt.Text = user.Living;
    }
    private int AgeYr(DateTime? Bdate)
    {
        if (Bdate == null)
        {
            return 0;
        }
        else
        {
            DateTime date = Bdate.Value;
            int arteller;
            DateTime now = DateTime.UtcNow;
            if ((int)now.Month <= date.Month)
            {
                if ((int)now.Day <= date.Day)
                    arteller = -1;
                else
                    arteller = 0;
            }
            else
                arteller = 0;

            return now.Year - date.Year + arteller;
        }
    }
}