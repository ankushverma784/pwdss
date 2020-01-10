using Employee.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.Controllers
{
    public class EmpController : Controller
    {
        public ActionResult create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult create(EmpModel emp)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                SqlConnection con = new SqlConnection(constr);
                string cmdTxt = @"INSERT INTO[dbo].[Employee]
                                    ([EmpID]
                                      ,[EmpName]
                                      ,[EmpDeg]
                                      ,[EmpCode]
                                      ,[EmpAge])
                                        VALUES
                                          ('{0}','{1}','{2}','{3}','{4}')";


                var inertQuery = string.Format(cmdTxt, emp.EmpID, emp.EmpName, emp.EmpDeg, emp.EmpCode, emp.EmpAge);
                SqlCommand cmd = new SqlCommand(inertQuery, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        public ActionResult index(EmpModel emp)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "EmpModel");

            ViewBag.EmpTable = ds;

            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            EmpModel emp;
               
                try
             {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
            SqlConnection con = new SqlConnection(constr);
            string cmdTxt = @"Select * from [Employee] WHERE [EmpID] = '{0}'";
                var selectQuery = string.Format(cmdTxt, id);
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                emp = new EmpModel()
                {
                    EmpID = (int)dr["empId"],
                    EmpName = dr["empName"].ToString(),
                    EmpDeg = dr["empDeg"].ToString(),
                    EmpCode = dr["empCode"].ToString(),
                    EmpAge = (int)dr["empAge"],
                };
                con.Close();
            }
            catch (Exception ex)
            {
               emp = new EmpModel();
                throw;
            }

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(EmpModel emp)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                SqlConnection con = new SqlConnection(constr);
                string cmdTxt = @"UPDATE [dbo].[Employee]
                                   SET [EmpName] = '{1}'
                                   ,[EmpDeg] = '{2}'
                                   ,[EmpCode] = '{3}'
                                    ,[EmpAge] = '{4}'
                             WHERE [EmpId] = '{0}'";

                var updateQuery = string.Format(cmdTxt, emp.EmpID, emp.EmpName, emp.EmpDeg, emp.EmpCode, emp.EmpAge);
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }


        public ActionResult Delete(string id)
        {
            EmpModel emp;
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                SqlConnection con = new SqlConnection(constr);
                string cmdTxt = @"Select * from  [dbo].[Employee] WHERE [EmpID] = '{0}'";

                var selectQuery = string.Format(cmdTxt, id);
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                emp = new EmpModel()
                {
                    EmpID = (int)dr["EmpID"],
                    EmpName = dr["empName"].ToString(),
                    EmpDeg = dr["empDeg"].ToString(),
                    EmpCode = dr["empCode"].ToString(),
                    EmpAge = (int)dr["empAge"],
                };
                con.Close();
            }
            catch (Exception ex)
            {
                emp = new EmpModel();
                throw;
            }

            return View(emp);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                SqlConnection con = new SqlConnection(constr);
                string cmdTxt = @"DELETE FROM [Employee] WHERE [EmpID] = '{0}'";

                var updateQuery = string.Format(cmdTxt, id);
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return View("DeleteConfirm");
        }

    }
}