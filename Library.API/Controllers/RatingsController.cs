using AutoMapper;
using Library.API.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

public class RatingsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Rating> _repo;

    public RatingsController(IMapper mapper, IGenericRepository<Rating> repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpPut("books/{id:int}/rate")]
    public async Task<ActionResult<OnlyIdResponseDTO>> BookReviewSave(int id, RatingToInsertDTO ratingToInsertDTO)
    {
        var ratingToInsert = _mapper.Map<Rating>(ratingToInsertDTO);
        ratingToInsert.BookId = id;
        var rating = await _repo.InsertAsync(ratingToInsert);
        return Ok(_mapper.Map<OnlyIdResponseDTO>(rating));
    }
}