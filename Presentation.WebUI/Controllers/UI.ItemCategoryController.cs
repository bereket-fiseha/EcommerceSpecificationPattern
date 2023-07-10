using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Constant;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.WebUI.Controllers
{
    public class ItemCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<ItemCatogoryQueryDTO>> AddItemCategory(ItemCategoryCommandDTO record) {
      
            HttpResponseMessage response;
            HttpClient client=new HttpClient();
            try {
                var serializedData = JsonSerializer.Serialize(record);
                var jsonString = new StringContent(serializedData, Encoding.UTF8, "application/json");
                response = await client.PostAsync(APIRoute.ItemCategories, jsonString);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var convertedData = JsonSerializer.Deserialize<ItemCatogoryQueryDTO>(data, options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });



                    return Ok(convertedData);

                }
            }
            catch(Exception ex) {
                return StatusCode((int)HttpStatusCode.InternalServerError,new { message=ex.Message });
            }   
            return StatusCode((int)response.StatusCode, new { message=response.Content.ReadAsStringAsync()});
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetItemCategories(ItemCategoryCommandDTO record)
        {

            HttpResponseMessage response  ;
           
            using (var clt=new HttpClient()) {
                try
                {
                    response = await clt.GetAsync(APIRoute.ItemCategories);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        var convertedData = JsonSerializer.Deserialize<IEnumerable<ItemCatogoryQueryDTO>>(data);


                        return Ok(convertedData);

                    }
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
                }        }
            return StatusCode((int)response.StatusCode, new { message = response.Content.ReadAsStringAsync() });
        }

    }
}
