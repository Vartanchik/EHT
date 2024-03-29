﻿using System.Threading.Tasks;
using EHT.BLL.Services.Concrete.TreeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHT.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITreeService _treeService;

        public HomeController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        public async Task<IActionResult> Index()
        {
            var nodeList = await _treeService.GetTreeAsync();

            return View(nodeList);
        }
    }
}