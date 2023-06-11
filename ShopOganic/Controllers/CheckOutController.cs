using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebsiteSellingSport.ModelView;
using WebsiteSellingSport.ModelView.GHNViewModel;

namespace ShopOganic.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly HttpClient _httpClient;

        public CheckOutController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("token", "4984199c-febd-11ed-8a8c-6e4795e6d902");
        }

        public IActionResult Index()
        {
            HttpResponseMessage responseProvin = _httpClient.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/province").Result;

            Provin lstprovin = new Provin();

            if (responseProvin.IsSuccessStatusCode)
            {
                string jsonData2 = responseProvin.Content.ReadAsStringAsync().Result;


                lstprovin = JsonConvert.DeserializeObject<Provin>(jsonData2);
                ViewBag.Provin = new SelectList(lstprovin.data, "ProvinceID", "ProvinceName");
            }

            return View();
        }
        public JsonResult GetListDistrict(int idProvin)
        {

            HttpResponseMessage responseDistrict = _httpClient.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id=" + idProvin).Result;

            District lstDistrict = new District();

            if (responseDistrict.IsSuccessStatusCode)
            {
                string jsonData2 = responseDistrict.Content.ReadAsStringAsync().Result;

                lstDistrict = JsonConvert.DeserializeObject<District>(jsonData2);
            }
            return Json(lstDistrict, new System.Text.Json.JsonSerializerOptions());
        }
        //Lấy địa chỉ phường xã
        public JsonResult GetListWard(int idWard)
        {


            HttpResponseMessage responseWard = _httpClient.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id=" + idWard).Result;

            Ward lstWard = new Ward();

            if (responseWard.IsSuccessStatusCode)
            {
                string jsonData2 = responseWard.Content.ReadAsStringAsync().Result;

                lstWard = JsonConvert.DeserializeObject<Ward>(jsonData2);
            }
            return Json(lstWard, new System.Text.Json.JsonSerializerOptions());
        }
        public List<CartItem> Carts()
        {


            List<CartItem> data = new List<CartItem>();


            var jsonData = Request.Cookies["Cart"];
            if (jsonData != null)
            {
                data = JsonConvert.DeserializeObject<List<CartItem>>(jsonData);
                return data;
            }

            return data;



        }
        public JsonResult GetTotalShipping([FromBody] ShippingOrder shippingOrder)
        {
            _httpClient.DefaultRequestHeaders.Add("shop_id", "3630415");

            HttpResponseMessage responseWShipping = _httpClient.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee?service_id=" + shippingOrder.service_id + "&insurance_value=" + shippingOrder.insurance_value + "&coupon=&from_district_id=" + shippingOrder.from_district_id + "&to_district_id=" + shippingOrder.to_district_id + "&to_ward_code=" + shippingOrder.to_ward_code + "&height=" + shippingOrder.height + "&length=" + shippingOrder.length + "&weight=" + shippingOrder.weight + "&width=" + shippingOrder.width + "").Result;

            Shipping shipping = new Shipping();
            if (responseWShipping.IsSuccessStatusCode)
            {
                string jsonData2 = responseWShipping.Content.ReadAsStringAsync().Result;

                shipping = JsonConvert.DeserializeObject<Shipping>(jsonData2);
                HttpContext.Session.SetInt32("shiptotal", shipping.data.total);
            }

            shipping.data.totaloder = shipping.data.total + Carts().Sum(c => c.Total);
            return Json(shipping, new System.Text.Json.JsonSerializerOptions());
        }

    }
}
