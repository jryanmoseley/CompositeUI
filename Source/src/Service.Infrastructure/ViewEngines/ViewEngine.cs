﻿using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure.ViewEngines
{
    public abstract class ViewEngine : RazorViewEngine
    {
        public abstract string RouteServiceValue { get; }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var searchedEngine = RouteServiceValue + "_ViewEngine";
            var notFound = new ViewEngineResult(new[] { searchedEngine });
            if (controllerContext.RouteData.DataTokens.ContainsKey(CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey))
                return (string)controllerContext.RouteData.DataTokens[CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey] == RouteServiceValue ? base.FindPartialView(controllerContext, partialViewName, useCache) : notFound;
            return notFound;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var searchedEngine = RouteServiceValue + "_ViewEngine";
            var notFound = new ViewEngineResult(new[] { searchedEngine });
            if (controllerContext.RouteData.DataTokens.ContainsKey(CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey))
                return (string)controllerContext.RouteData.DataTokens[CompositeUI.Service.Infrastructure.Consts.Consts.RouteServiceKey] == RouteServiceValue ? base.FindView(controllerContext, viewName, masterName, useCache) : notFound;
            return notFound;
        }
    }
}
