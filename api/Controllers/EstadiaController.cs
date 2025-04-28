using api.Extensions;
using api.Interfaces;
using api.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExcelFileReader.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class EstadiaController : Controller
    {
        private readonly IEstadiasRepository _estadiasRepo;
        private readonly UserManager<AppUser> _userManager;

        public EstadiaController(UserManager<AppUser> userManager, IEstadiasRepository estadiaRepo)
        {
            _estadiasRepo = estadiaRepo;
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var AppUserId = appUser.Id;
            var estadias = await _estadiasRepo.GetAllAsync(AppUserId);
              return Ok(estadias);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ExcelFileReader(IFormFile file, int idCliente)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var AppUserId = appUser.Id;
            
           

            // Upload File
            if (file != null && file.Length > 0)
            {
                var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //Read File
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var excelData = new List<List<object>>();
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                var rowData = new List<object>();
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    rowData.Add(reader.GetValue(column));
                                  
                                }
                                excelData.Add(rowData);
                                if (rowData[0] == null)
                                {
                                    rowData[0] = "Informação_Em_Branco";
                                }
                                if (rowData[1] == null)
                                {
                                    rowData[1] = "Informação_Em_Branco";
                                }
                                if (rowData[2] == null)
                                {
                                    rowData[2] = "Informação_Em_Branco";
                                }
                                if (rowData[3] == null)
                                {
                                    rowData[3] = "Informação_Em_Branco";
                                }
                                if (rowData[4] == null)
                                {
                                    rowData[4] = "Informação_Em_Branco";
                                }
                                if (rowData[5] == null)
                                {
                                    rowData[5] = "Informação_Em_Branco";
                                }
                                if (rowData[6] == null)
                                {
                                    rowData[6] = "Informação_Em_Branco";
                                }
                                if (rowData[7] == null)
                                {
                                    rowData[7] = "Informação_Em_Branco";
                                }
                                if (rowData[8] == null)
                                {
                                    rowData[8] = "Informação_Em_Branco";
                                }
                                if (rowData[9] == null)
                                {
                                    rowData[9] = "Informação_Em_Branco";
                                }
                                if (rowData[10] == null)
                                {
                                    rowData[10] = "Informação_Em_Branco";
                                }
                                if (rowData[11] == null)
                                {
                                    rowData[11] = "Informação_Em_Branco";
                                }
                                if (rowData[12] == null)
                                {
                                    rowData[12] = "Informação_Em_Branco";
                                }

                                List<Estadias> estadias = new List<Estadias>();
                                estadias.Add(new Estadias
                                {
                                    Codigodeconfirmacao = rowData[0].ToString(),
                                    Status = rowData[1].ToString(),
                                    Nomehospede = rowData[2].ToString(),
                                    EntrarEmcontato = rowData[3].ToString(),
                                    NumeroAdultos = rowData[4].ToString(),
                                    NumeroCriancas = rowData[5].ToString(),
                                    NumeroBb = rowData[6].ToString(),
                                    DataInicio = rowData[7].ToString(),
                                    DataTermino = rowData[8].ToString(),
                                    NumeroNoites = rowData[9].ToString(),
                                    Reservado = rowData[10].ToString(),
                                    Anuncio = rowData[11].ToString(),
                                    Ganhos = rowData[12].ToString(),
                                    AppUserId = AppUserId,
                                    idCliente = idCliente

                                });
                                    await _estadiasRepo.CreateAsync(estadias);
                                
                                                            
                            }
                        } while (reader.NextResult());


                        ViewBag.ExcelData = excelData;
                    }
                }
            }

            return Ok(file);
        }
    }
}