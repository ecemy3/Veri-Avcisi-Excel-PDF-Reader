using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using proje.Models;
using Microsoft.Office.Interop.Excel;
namespace proje.Controllers
{
    public class ManagementController : Controller
    {
        Context c = new Context();
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            return View();
        }
        public JsonResult ProductListJson()
        {
            var v = c.Products.ToList();
            return Json(new { data = v }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ExcellUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcellUpdate(int id=0)
        {
            if (Request.Files.Count > 0)
            {
                bool checkData = false;
                string dosyaUzantisi = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string yol = Server.MapPath("~/file/veri" + dosyaUzantisi);
                if (System.IO.File.Exists(yol))//Daha önce varmış.Eski dosyayı siliyoruz.
                {
                System.IO.File.Delete(yol);
                    checkData = true;
                }
                if (checkData == true)
                {
                    c.Database.ExecuteSqlCommand("DELETE From Products");
                }

                Request.Files[0].SaveAs(yol);
                Application excelApp = new Application();
                Workbook workbook = excelApp.Workbooks.Open(yol);
                Worksheet worksheet = workbook.Sheets[1]; // İlk sayfayı seçmek için
                Range range = worksheet.UsedRange;
                List<string>data = new List<string>();
                proje.Models.Product product=new proje.Models.Product();
                int sonuc = 0;
                for (int row = 2; row <= range.Rows.Count; row++)
                {
                    for (int col = 1; col <= range.Columns.Count; col++)
                    {
                        data.Add((range.Cells[row, col] as Range).Value2.ToString());
                    }
                    product.ProductName = data[0];
                    product.ProductCode = data[1];
                    product.UnitPrice = Convert.ToDecimal(data[2]);
                    product.ProductDetail = data[3];
                    data.Clear();
                    c.Products.Add(product);
                    sonuc = c.SaveChanges();
                }

                workbook.Close(false);
                excelApp.Quit();
                if(sonuc==1)
                    return Json(true);
            }
            else
            {
                return Json(false);
            }

            return View();
        }
    }
}