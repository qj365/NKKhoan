using NKKhoan.Areas.Admin.ViewModels;
using NKKhoan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace NKKhoan.Areas.Admin.DAO
{
    public class TaskDAO
    {
        static string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static List<TaskViewModel> getListTask()
        {
            var tasks = new List<TaskViewModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM NKSLK", con))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var task = new TaskViewModel();
                        task.nkslk.MaNKSLK = Convert.ToInt32(reader["MaNKSLK"]);
                        task.nkslk.GioBatDau = TimeSpan.Parse(reader["GioBatDau"].ToString());
                        task.nkslk.GioKetThuc = TimeSpan.Parse(reader["GioKetThuc"].ToString());
                        task.nkslk.NgayThucHienKhoan = DateTime.Parse(reader["NgayThucHienKhoan"].ToString());
                        task.congnhan = getListEmployeeWorkedOn(task.nkslk.MaNKSLK);
                        task.congviec = getListJobWorkedOn(task.nkslk.MaNKSLK);
                        tasks.Add(task);
                    }
                }    
            }
            return tasks;
        }

        public static List<CongNhan> getListEmployeeWorkedOn(int? MaNKSLK = null)
        {
            var employees = new List<CongNhan>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd;
                if (MaNKSLK != null)
                {
                    sqlcmd = "SELECT * FROM dbo.CongNhan JOIN dbo.ChiTietNhanCongKhoan ON ChiTietNhanCongKhoan.MaNhanCong = CongNhan.MaNhanCong WHERE ChiTietNhanCongKhoan.MaNKSLK = " + MaNKSLK;
                }
                else
                {
                    sqlcmd = "SELECT * FROM dbo.CongNhan";
                }
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var employee = new CongNhan();
                        employee.MaNhanCong = Int32.Parse(reader["MaNhanCong"].ToString());
                        employee.HoTen = reader["HoTen"].ToString();
                        employee.NgayNamSinh = DateTime.Parse(reader["NgayNamSinh"].ToString());
                        employee.QueQuan = reader["QueQuan"].ToString();
                        employee.GioiTinh = reader["GioiTinh"].ToString();
                        employee.MatKhau = reader["MatKhau"].ToString();
                        employee.LuongHopDong = Int32.Parse(reader["LuongHopDong"].ToString());
                        employee.LuongBaoHiem = Int32.Parse(reader["LuongBaoHiem"].ToString());
                        employees.Add(employee);
                    }
                }
                return employees;
            }
        }

        public static List<CongViec> getListJobWorkedOn(int? MaNKSLK = null)
        {
            var jobs = new List<CongViec>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd;
                if (MaNKSLK != null)
                {
                    sqlcmd = "SELECT * FROM dbo.CongViec JOIN dbo.ChiTietCongViec ON ChiTietCongViec.MaCongViec = CongViec.MaCongViec WHERE dbo.ChiTietCongViec.MaNKSLK = " + MaNKSLK;
                }
                else
                {
                    sqlcmd = "SELECT * FROM dbo.CongViec";
                }
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var job = new CongViec();
                        job.MaCongViec = int.Parse(reader["MaCongViec"].ToString());
                        job.TenCongViec = reader["TenCongViec"].ToString();
                        job.DinhMucKhoan = decimal.Parse(reader["DinhMucKhoan"].ToString());
                        job.HeSoKhoan = int.Parse(reader["HeSoKhoan"].ToString());
                        job.DinhMucLaoDong = int.Parse(reader["DinhMucLaoDong"].ToString());
                        job.DonGia = int.Parse(reader["DonGia"].ToString());
                        jobs.Add(job);
                    }
                }
            }
            return jobs;
        }

        public static int AssignEmployeeToTask(int MaNhanCong, int MaNKSLK)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd = "INSERT INTO dbo.ChiTietNhanCongKhoan(MaNKSLK, MaNhanCong) VALUES(" + MaNKSLK + ", " + MaNhanCong + ")";
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public static int AssignJobToTask(int MaCongViec, int MaNKSLK)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd = "INSERT INTO dbo.ChiTietCongViec(MaNKSLK, MaCongViec) VALUES(" + MaNKSLK + ", " + MaCongViec + ")";
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int FreeEmployeeFromTask(int MaNhanCong, int MaNKSLK)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd = "DELETE FROM dbo.ChiTietNhanCongKhoan WHERE MaNKSLK = " + MaNKSLK + " AND MaNhanCong = " + MaNhanCong;
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public static int FreeJobFromTask(int MaCongViec, int MaNKSLK)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd = "DELETE FROM dbo.ChiTietCongViec WHERE MaNKSLK = " + MaNKSLK + " AND MaCongViec = " + MaCongViec;
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
    }
}