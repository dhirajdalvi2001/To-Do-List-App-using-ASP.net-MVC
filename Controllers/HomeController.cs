using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TodolistApp.Models;

namespace TodolistApp.Controllers
{
    public class HomeController : Controller
    {
        // SQL Database connection
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=todolistdb;Integrated Security=True");

        // CREATE OPERATION
        [HttpPost]
        public ActionResult Index(string item, int no)
        {
            string query = "INSERT INTO List1 VALUES('" + item + "','"+no+"')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }

        // READ OPERATION
        public IActionResult Index()
        {
            // SQL Query
            string query = "SELECT * FROM List1";

            // SQLDataAdapter is a bridge between Dataset and SQL Server
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);   
            return View(dt);
        }
        // SELECT ITEM TO BE UPDATED
        public ActionResult Update(int id)
        {
            string query = "SELECT * FROM List1 WHERE id='"+id+"'";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return View(dt);
        }

        // UPDATE OPERATION
        [HttpPost]
        public ActionResult Update(int id, string item ,int no)
        {
            string query = "UPDATE List1 SET item='"+item+"','"+no+"' WHERE id='"+id+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }

        // DELETE OPERATION
        public ActionResult Delete(int id)
        {
            string query = "DELETE FROM List WHERE id='" + id + "'";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return RedirectToAction("Index");
        }
    }
}