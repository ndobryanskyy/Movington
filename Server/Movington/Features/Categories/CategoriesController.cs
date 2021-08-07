using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movington.Database;

namespace Movington.Features.Categories
{
    [Route("categories")]
    public sealed class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriesController(
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IAsyncEnumerable<CategoryDetailsViewModel> GetAll()
        {
            return _dbContext.Categories
                .AsNoTracking()
                .ProjectTo<CategoryDetailsViewModel>(_mapper.ConfigurationProvider)
                .AsAsyncEnumerable();
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDetailsViewModel>> GetByIdAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            var foundCategory = await _dbContext.Categories
                .AsNoTracking()
                .Where(x => x.Id == categoryId)
                .ProjectTo<CategoryDetailsViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (foundCategory == null)
            {
                return NotFound();
            }

            return foundCategory;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(
            [FromBody] CategoryEditViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var newCategory = _mapper.Map<Category>(viewModel);

            _dbContext.Categories.Add(newCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetById", new { categoryId = newCategory.Id }, newCategory);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid categoryId,
            [FromBody] CategoryEditViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);

            if (existingCategory == null)
            {
                return NotFound();
            }

            var updatedCategory = _mapper.Map(viewModel, existingCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var updatedVieWModel = _mapper.Map<CategoryDetailsViewModel>(updatedCategory);
            
            return Ok(updatedVieWModel);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);

            if (existingCategory == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(existingCategory);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}