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
                        employee.MaNhanCong = reader["MaNhanCong"] as int? ?? default;
                        employee.HoTen = reader["HoTen"] as string;
                        employee.NgayNamSinh = reader["NgayNamSinh"] as DateTime?;
                        employee.QueQuan = reader["QueQuan"] as string;
                        employee.GioiTinh = reader["GioiTinh"] as string;
                        employee.MatKhau = reader["MatKhau"] as string;
                        employee.LuongHopDong = reader["LuongHopDong"] as int?;
                        employee.LuongBaoHiem = reader["LuongBaoHiem"] as int?;
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
                        job.MaCongViec = reader["MaCongViec"] as int? ?? default;
                        job.TenCongViec = reader["TenCongViec"] as string;
                        job.DinhMucKhoan = reader["DinhMucKhoan"] as decimal?;
                        job.HeSoKhoan = reader["HeSoKhoan"] as int? ?? default;
                        job.DinhMucLaoDong = reader["DinhMucLaoDong"] as int? ?? default;
                        job.DonGia = reader["DonGia"] as int? ?? default;
                        jobs.Add(job);
                    }
                }
            }
            return jobs;
        }

        public static int AssignEmployeeToTask(int MaNKSLK, int MaNhanCong)
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
        public static int AssignJobToTask(int MaNKSLK, int MaCongViec)
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

        public static int FreeEmployeeFromTask(int MaNKSLK, int? MaNhanCong = null)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd;
                if (MaNhanCong != null)
                {
                    sqlcmd = "DELETE FROM dbo.ChiTietNhanCongKhoan WHERE MaNKSLK = " + MaNKSLK + " AND MaNhanCong = " + MaNhanCong;
                }
                else
                {
                    sqlcmd = "DELETE FROM dbo.ChiTietNhanCongKhoan WHERE MaNKSLK = " + MaNKSLK;
                }
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public static int FreeJobFromTask(int MaNKSLK, int? MaCongViec = null)
        {
            int result;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string sqlcmd;
                if (MaCongViec != null)
                {
                    sqlcmd = "DELETE FROM dbo.ChiTietCongViec WHERE MaNKSLK = " + MaNKSLK + " AND MaCongViec = " + MaCongViec;
                }
                else
                {
                    sqlcmd = "DELETE FROM dbo.ChiTietCongViec WHERE MaNKSLK = " + MaNKSLK;
                }
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
    }
}