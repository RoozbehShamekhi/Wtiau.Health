using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wtiau.Health.Web.Models.Domian;
using Wtiau.Health.Web.Models.ViewModels;

namespace Wtiau.Health.Web.Models.Repository
{
    public class Rep_SelectItems
    {
        private readonly HealthEntities db = new HealthEntities();

        //public IEnumerable<SelectListItem> Get_GenderList()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();

        //    foreach (var item in db.Tbl_BookCategory)
        //    {
        //        list.Add(new SelectListItem() { Value = item.BC_ID.ToString(), Text = item.BC_Name });
        //    }

        //    return list.AsEnumerable();
        //}
    }
}