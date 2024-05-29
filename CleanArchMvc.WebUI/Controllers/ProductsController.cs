using CleanArchMvc.Application.DTO;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProducts();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.Add(product);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var productViewModel = await _productService.GetById(id);
        if (productViewModel == null) return NotFound();
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View(productViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.Update(product);

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var productViewModel = await _productService.GetById(id);
        if (productViewModel == null) return NotFound();
        return View(productViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var productViewModel = await _productService.GetById(id);
        if (productViewModel == null) return NotFound();

        var wwwroot = _webHostEnvironment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productViewModel.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productViewModel);
    }

}
