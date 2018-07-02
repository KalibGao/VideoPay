using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VideoPay.Models;

namespace VideoPay.Controllers
{
    [Route("files")]
    public class FilesController : Controller
    {
        private readonly IHostingEnvironment _env;
        public FilesController(IHostingEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        [HttpGet]
        public IActionResult Index()
        {
            string path = $"{_env.WebRootPath}\\packages\\";
            var files = Directory.GetFiles(path);

            var fileListViewModel = new FileListViewModel();

            if (files != null && files.Length > 0)
            {
                fileListViewModel.Files = new Dictionary<string, string>();
                foreach (var file in files)
                {
                    var newFile = EnsureFileName(file);
                    fileListViewModel.Files[newFile] = $"{Request.Scheme}://{Request.Host.Value}/files/download?filename={HttpUtility.UrlEncode(newFile)}";
                }
            }

            return View(fileListViewModel);
        }


        [HttpGet("download")]
        public IActionResult Download(string fileName)
        {
            string filePath = $"{_env.WebRootPath}\\packages\\{fileName}";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }


        private string GetPath(string fileName)
        {
            string path = $"{_env.WebRootPath}\\packages\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + fileName;
        }

        private string EnsureFileName(string fileName)
        {
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }
            return fileName;
        }
    }
}