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
    [Route("api/ShoppingCart")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "VendorMachineOpenApiSpecShoppingCart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// The product controller constructor
        /// </summary>
        /// <param name="iProductRepository"></param>
        /// <param name="mapper"></param>
        public ShoppingCartController(IProductRepository iProductRepository, IMapper mapper)
        {
            _iProductRepository = iProductRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// To create product
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns>The created product</returns>
        [HttpPost]
        [Authorize(Roles = "buyer")]
        [ProducesResponseType(201, Type = typeof(ShoppingCartDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddItemToShoppingCart([FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
            {
                return BadRequest(ModelState);
            }

            var productObj = _mapper.Map<ShoppingCart>(shoppingCartDto);
            if (!_iProductRepository.AddItemToShoppingCart(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {productObj.ApplicationUserId}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetProduct", new { productId = productObj.Id }, productObj);
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="shoppingCartId">The product Id</param>
        /// <param name="productDto">The product model</param>
        /// <returns></returns>
        [HttpPut("{productId:int}", Name = "UpdateShoppingCart")]
        [Authorize(Roles = "buyer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateShoppingCart(int shoppingCartId, [FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null || shoppingCartId != shoppingCartDto.Id)
            {
                return BadRequest(ModelState);
            }

            var productObj = _mapper.Map<ShoppingCart>(shoppingCartDto);
            if (!_iProductRepository.UpdateShopingCart(productObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {productObj.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
