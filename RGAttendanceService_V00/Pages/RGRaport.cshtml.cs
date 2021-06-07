using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClosedXML;
using ClosedXML.Excel;
using RGAttendanceService_V00.Models;
using System.IO;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.DAL.Interfaces;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Syncfusion.Pdf;

namespace RGAttendanceService_V00.Pages
{
    public class RGRaportModel : PageModel
    {
        private ParentContext _context;
        private IAttendance _AttendanceDB;
        public List<Attendance> Attendance { get; set; }
        public List<Attendance> AllAttendances { get; set; }
        public List<Group> Groups { get; set; }

        //Binded
        [BindProperty]
        public int ChosenGroup { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        public RGRaportModel(ParentContext context, IAttendance AttendanceDB)
        {
            _context = context;
            _AttendanceDB = AttendanceDB;

            Group x = new Group { Id = 0, Name = "Wszystkie" };
            
            Groups = _context.Group.ToList();
            Groups.Insert(0, x);
        }

        public void OnGet()
        {
           
        }

        public ActionResult OnPostCreateDocument()
        {
            AllAttendances = _AttendanceDB.GetList();
            Attendance = new List<Attendance>();
            if (ChosenGroup == 0)
            {
                //Attendance = AllAttendances;
                foreach (Attendance x in AllAttendances)
                {
                    if (x.DateOfClass.CompareTo(StartDate) > 0 && x.DateOfClass.CompareTo(EndDate) < 0)
                    {
                        Attendance.Add(x);
                    }
                }
            }
            else
            {
                
                foreach (Attendance x in AllAttendances)
                {
                    if (x.GroupId == ChosenGroup && x.DateOfClass.CompareTo(StartDate) > 0 && x.DateOfClass.CompareTo(EndDate) < 0)
                    {
                        Attendance.Add(x);
                    }
                }
            }

            

            foreach (Attendance x in Attendance)
            {
                x.Checker = _context.Coach.FirstOrDefault(c => c.Id.Equals(x.CheckerId));
                x.Participant = _context.Participant.FirstOrDefault(p => p.Id.Equals(x.ParticipantId));
                x.ParticipatingGroup = _context.Group.FirstOrDefault(g => g.Id.Equals(x.GroupId));
            }

            try
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "obecnosc.xlsx";

                XLWorkbook workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.Worksheets.Add("Obecnosc");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Imie";
                worksheet.Cell(1, 3).Value = "Nazwisko";
                worksheet.Cell(1, 4).Value = "Grupa";
                worksheet.Cell(1, 5).Value = "Trener";
                worksheet.Cell(1, 6).Value = "DataZajêæ";
                worksheet.Cell(1, 7).Value = "DataSprawdzenia";
                worksheet.Cell(1, 8).Value = "StatusObecnoœci";

                for (int i = 1; i <= Attendance.Count; i++)
                {
                    worksheet.Cell(i + 1, 1).Value = Attendance[i - 1].ParticipantId;
                    worksheet.Cell(i + 1, 2).Value = Attendance[i - 1].Participant.FirstName;
                    worksheet.Cell(i + 1, 3).Value = Attendance[i - 1].Participant.LastName;
                    worksheet.Cell(i + 1, 4).Value = Attendance[i - 1].Participant.Group.Name;
                    worksheet.Cell(i + 1, 5).Value = Attendance[i - 1].Checker==null?"nie_podano":Attendance[i-1].Checker.FirstName;
                    worksheet.Cell(i + 1, 6).Value = Attendance[i - 1].DateOfClass.ToString();
                    worksheet.Cell(i + 1, 7).Value = Attendance[i - 1].DateOfCheck.ToString();
                    worksheet.Cell(i + 1, 8).Value = Attendance[i - 1].AbsenceStatus;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    System.Diagnostics.Debug.WriteLine("DownloadFile");

                    return File(content, contentType, fileName);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public void OnPost()
        {
            


        }

    }
}
