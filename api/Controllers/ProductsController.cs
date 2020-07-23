using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System.Linq;
using AutoMapper;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepo;
        public ProductsController(
            IMapper mapper,
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository) 
        {
            _mapper = mapper;
            _productRepo = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts(CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);


            return _mapper.Map<Product,ProductToReturnDTO>(product);
        }

        //[HttpGet("brands")]
        //public async Task<ActionResult<ProductBrand>> GetProductBrands()
        //{
        //    return Ok(await _repo.GetProductBrandsAsync());
        //}


        //[HttpGet("Types")]
        //public async Task<ActionResult<ProductType>> GetProductTypes()
        //{
        //    return Ok(await _repo.GetPrductTypesAsync());
        //}


    }
}