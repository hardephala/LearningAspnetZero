using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arch.Web.Areas.App.Models.ErInvoiceDatas;
using Arch.Web.Controllers;
using Arch.Authorization;
using Arch.ErInvoiceDatas;
using Arch.ErInvoiceDatas.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Arch.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_ErInvoiceDatas)]
    public class ErInvoiceDatasController : ArchControllerBase
    {
        private readonly IErInvoiceDatasAppService _erInvoiceDatasAppService;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _config;

        public ErInvoiceDatasController(IErInvoiceDatasAppService erInvoiceDatasAppService, IHostEnvironment env, IConfiguration config)
        {
            _erInvoiceDatasAppService = erInvoiceDatasAppService;
            _env = env;
            _config = config;
        }

        public ActionResult Index()
        {
            var model = new ErInvoiceDatasViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public IDbConnection Connection {
            get{
                return new SqlConnection(_config.GetConnectionString("Default"));
            }
        }

        /*[HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_ErInvoiceDatas_Upload)]
        public async Task<string> UploadFile(IFormFile excelinvoicefile)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                

                dbConnection.Open();
                var excelfile = Request.Form.Files.First();var uniqueFileName = GetUniqueFileName(excelfile.FileName);
                var dir = Path.Combine(_env.ContentRootPath, "ExcelInvoices");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var filePath = Path.Combine(dir, uniqueFileName);
                await excelfile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                //SaveImagePathToDb(input.Description, filePath);
                return uniqueFileName;
            }
            
        }*/


        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ErInvoiceDatas_Create, AppPermissions.Pages_ErInvoiceDatas_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetErInvoiceDataForEditOutput getErInvoiceDataForEditOutput;

            if (id.HasValue)
            {
                getErInvoiceDataForEditOutput = await _erInvoiceDatasAppService.GetErInvoiceDataForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getErInvoiceDataForEditOutput = new GetErInvoiceDataForEditOutput
                {
                    ErInvoiceData = new CreateOrEditErInvoiceDataDto()
                };
                getErInvoiceDataForEditOutput.ErInvoiceData.validityDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditErInvoiceDataModalViewModel()
            {
                ErInvoiceData = getErInvoiceDataForEditOutput.ErInvoiceData,
                Billofladingblno = getErInvoiceDataForEditOutput.Billofladingblno,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewErInvoiceDataModal(int id)
        {
            var getErInvoiceDataForViewDto = await _erInvoiceDatasAppService.GetErInvoiceDataForView(id);

            var model = new ErInvoiceDataViewModel()
            {
                ErInvoiceData = getErInvoiceDataForViewDto.ErInvoiceData
                ,
                Billofladingblno = getErInvoiceDataForViewDto.Billofladingblno

            };

            return PartialView("_ViewErInvoiceDataModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ErInvoiceDatas_Create, AppPermissions.Pages_ErInvoiceDatas_Edit)]
        public PartialViewResult BillofladingLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ErInvoiceDataBillofladingLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_ErInvoiceDataBillofladingLookupTableModal", viewModel);
        }

    }
}