using AutoMapper;
using Library.API.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

public class ReviewsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Review> _repo;

    public ReviewsController(IMapper mapper, IGenericRepository<Review> repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpPut("books/{id:int}/review")]
    public async Task<ActionResult<OnlyIdResponseDTO>> BookReviewSave(int id, ReviewToInsertDTO reviewToInsertDTO)
    {
        var reviewToInsert = _mapper.Map<ReviewToInsertDTO, Review>(reviewToInsertDTO);
        reviewToInsert.BookId = id;
        var review = await _repo.InsertAsync(reviewToInsert);
        return Ok(_mapper.Map<OnlyIdResponseDTO>(review));
    }
}