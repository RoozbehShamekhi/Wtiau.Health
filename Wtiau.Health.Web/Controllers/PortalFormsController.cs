using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Controllers
{
    public class PortalFormsController : Controller
    {
        HealthEntities db = new HealthEntities();

        // GET: PortalForms
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowForm(string ID)
        {


            var F = db.Tbl_Form.Where(a =>  a.Form_Guid.ToString() == ID).SingleOrDefault();

            if (F != null)
            {
                Model_Form _Form = new Model_Form()
                {
                    Name = F.Form_Display,
                    ID = F.Form_ID,
                };

                _Form.Steps = new List<Model_Steps>(); 


                List<Model_Steps> _Steps = new List<Model_Steps>();

                foreach (var S_item in F.Tbl_FormStep.ToList())
                {
                    Model_Steps _Step = new Model_Steps()
                    {
                        Name = S_item.FS_Display,
                    };

                    _Step.Questions = new List<Model_Questions>();

                    List<Model_Questions> _Questions = new List<Model_Questions>();

                    foreach (var Q_item in S_item.Tbl_Question.ToList())
                    {

                        Model_Questions _Question = new Model_Questions()
                        {
                            Titel = Q_item.Question_Title,
                            type = Q_item.Question_TypeCodeID,
                        };

                        _Question.Responses = new List<Model_Responses>();

                        foreach (var R_item in Q_item.Tbl_Response.ToList())
                        {
                            _Question.Responses.Add(new Model_Responses()
                            {
                                Text = R_item.Response_Title,
                                Value = R_item.Response_Guid.ToString(),
                            });
                        }

                        _Step.Questions.Add(_Question);
                    }

                    _Form.Steps.Add(_Step);
                }

                return View(_Form);
            }
            else
            {

            }

            return View();
        }

        [HttpPost]
        public ActionResult ShowForm( FormCollection model)
        {
           var x = model;
            return View();
        }
    }
}