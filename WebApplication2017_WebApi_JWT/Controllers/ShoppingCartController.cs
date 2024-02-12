using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2017_WebApi_JWT.Models;
namespace WebApplication2017_WebApi_JWT.Controllers
{
    [RoutePrefix("api/ShoppingCart")]
    public class ShoppingCartController : ApiController
    {
        private static List<Product> _cart = new List<Product>();
        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProductToCart")]
        public IHttpActionResult AddProductToCart([FromBody] Product product)
        {
            _cart.Add(product);
            return Ok("產品已新增到購物車");
        }

        /// <summary>
        /// 由購物車移除商品
        /// </summary>
        /// <param name="productId"></param>
        /// <response code="200">OK</response> 
        /// <response code="404">Not found</response> 
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveProductFromCart/{productId}")]
        public IHttpActionResult RemoveProductFromCart(int productId)
        {
            var product = _cart.Find(p => p.ProductId == productId);
            if (product != null)
            {
                _cart.Remove(product);
                return Ok("產品已從購物車移除");
            }
            return NotFound();
        }

        [HttpPut]
        [Route("UpdateProductInCart")]
        public IHttpActionResult UpdateProductInCart([FromBody] Product product)
        {
            var existingProduct = _cart.Find(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Quantity = product.Quantity;
                return Ok("產品數量已更新");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAllProductsInCart")]
        public IHttpActionResult GetAllProductsInCart()
        {
            return Ok(_cart);
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}