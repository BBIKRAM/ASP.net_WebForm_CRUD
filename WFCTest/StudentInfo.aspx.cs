﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;


namespace WFCTest
{
    public partial class StudentInfo : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public  void DataLoad()
        {
            if(Page.IsPostBack)
            {
                dgViewStudents.DataBind();

            }
        }

        public void ClearAllData()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtDOB.Text = DateTime.Today.Date.ToString();
            ddlGender.SelectedValue = ddlGender.Items[0].ToString();
            chkBoxAgree.Checked = false;
            lblMessage.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e) { }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSID.Text = dgViewStudents.SelectedRow.Cells[1].Text;
            txtName.Text = dgViewStudents.SelectedRow.Cells[2].Text;
            txtEmail.Text = dgViewStudents.SelectedRow.Cells[3].Text;
            ddlGender.Text = dgViewStudents.SelectedRow.Cells[4].Text;
            txtDOB.Text = dgViewStudents.SelectedRow.Cells[5].Text;


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text !="" && txtEmail.Text !="" && chkBoxAgree.Checked)
            {
                using(con=new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into  Student(Name,Email,Gender,BirthDate) values(@name,@email,@gender,@birthdate)", con);
                    
                    cmd.Parameters.AddWithValue("@name",txtName.Text);  
                    cmd.Parameters.AddWithValue("@email",txtEmail.Text);  
                    cmd.Parameters.AddWithValue("@gender",ddlGender.SelectedValue);  
                    cmd.Parameters.AddWithValue("@birthdate",txtDOB.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    DataLoad();
                    ClearAllData();
                }   

            }
            else
            {
                lblMessage.Text = "fill all information ";
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && chkBoxAgree.Checked)
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("update student set Name=@name ,Email=@email ,Gender =@gender,BirthDate=@birthdate where StudentID=@studentid", con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@gender", ddlGender.Text);
                    cmd.Parameters.AddWithValue("@birthdate", txtDOB.Text);
                    cmd.Parameters.AddWithValue("@studentid", lblSID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DataLoad();
                    ClearAllData();


                }

            }
            else
            {
                lblMessage.Text = "fill all information ";
            }
        }

            protected void btnDelete_Click(object sender, EventArgs e)
        {
            using(con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Delete from student where StudentID=@studentid", con);
                cmd.Parameters.AddWithValue("@studentid",lblSID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                DataLoad();
                ClearAllData();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //ClearAllData();
            ClearAllData(); 
        }
    }
}