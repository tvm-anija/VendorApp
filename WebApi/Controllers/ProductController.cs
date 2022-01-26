using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{
    /// <summary>
    /// The product controller
    /// </summary>
    [Route("api/Product")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "VendorMachineOpenApiSpecProduct")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// The product controller constructor
        /// </summary>
        /// <param name="iProductRepository"></param>
        /// <param name="mapper"></param>
        public ProductController(IProductRepository iProductRepository, IMapper mapper)
        {
            _iProductRepository = iProductRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type =typeof(List<ProductDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducts()
        {
            var objList = _iProductRepository.GetProducts();
            var objDto = new List<ProductDto>();
            foreach(var obj in objList)
            {
                objDto.Add(_mapper.Map<ProductDto>(obj));
            }
            return Ok(objList);
        }

        /// <summary>
        /// Get individual products
        /// </summary>
        /// <param name="productId">The id of the product</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{productId:int}",Name = "GetProduct")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetProduct(int productId)
        {
            var obj = _iProductRepository.GetProduct(productId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ProductDto>(obj);
            return Ok(objDto);
        }

        /// <summary>
        /// To create product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns>The created product</returns>
        [HttpPost]
        [Authorize(Roles = "seller")]
        [ProducesResponseType(201, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }

            if(_iProductRepository.ProductExists(productDto.ProductName))
            {
                ModelState.AddModelError("", "Product Exists!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productObj = _mapper.Map<Product>(productDto);
            if (!_iProductRepository.CreateProduct(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {productObj.productName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetProduct", new { productId = productObj.id },productObj);
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="productId">The product Id</param>
        /// <param name="productDto">The product model</param>
        /// <returns></returns>
        [HttpPut("{productId:int}", Name ="UpdateProduct")]
        [Authorize(Roles ="seller")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductUpdateDto productDto)
        {
            if(productDto == null || productId != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            var productObj = _mapper.Map<Product>(productDto);
            if (!_iProductRepository.UpdateProduct(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {productObj.productName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
        [HttpDelete("{productId:int}", Name = "DeleteProduct")]
        [Authorize(Roles = "seller")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_iProductRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var productObj = _iProductRepository.GetProduct(productId);
            if (!_iProductRepository.DeleteProduct(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {productObj.productName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
