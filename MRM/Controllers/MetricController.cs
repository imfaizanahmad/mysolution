using MRM.Business.Services;
using MRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRM.Controllers
{
    public class MetricController : Controller
    {
        private MetricReachServices _metricReachServices = null;
        private MetricResponseServices _metricResponseServices = null;

        public MetricController()
        {
            _metricReachServices = new MetricReachServices();
            _metricResponseServices = new MetricResponseServices();
        }

        // GET: Metric
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMetricReachList()
        {
            MetricReachViewModel metricReachModel = new MetricReachViewModel();
            metricReachModel.metricReachList = _metricReachServices.GetAllMetricReach().ToList();
            return Json(metricReachModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMetricResponseList()
        {
            MetricResponseViewModel metricResponseModel = new MetricResponseViewModel();
            metricResponseModel.metricResponseList = _metricResponseServices.GetAllMetricResponse().ToList();
            return Json(metricResponseModel, JsonRequestBehavior.AllowGet);
        }
    }
}