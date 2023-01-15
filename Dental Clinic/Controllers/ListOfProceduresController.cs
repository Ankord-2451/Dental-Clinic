﻿using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic.Controllers
{
    public class ListOfProceduresController : Controller
    {
        private ApplicationDbContext dbContext { get; set; }

        public ListOfProceduresController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        [HttpGet("ListOfProcedures")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("ListOfProcedures/Details/{id?}")]
        public ActionResult Details(int id)
        {
            return View();
        }



        [HttpGet("ListOfProcedures/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("ListOfProcedures/Create")]
        public ActionResult Create(ProcedureModel procedure)
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("ListOfProcedures/Edit/{id?}")]
        public ActionResult Edit(int id, ProcedureModel procedure)
        {
            return RedirectToAction(nameof(Index));
        }



        [HttpDelete("ListOfProcedures/Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}