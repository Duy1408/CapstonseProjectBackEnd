using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.CommentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _cmt;
        private readonly IMapper _mapper;

        public CommentsController(ICommentServices cmt, IMapper mapper)
        {
            _cmt = cmt;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllComment")]
        public IActionResult GetAllComment()
        {
            try
            {
                if (_cmt.GetComments() == null)
                {
                    return NotFound();
                }
                var cmts = _cmt.GetComments();
                var response = _mapper.Map<List<CommentVM>>(cmts);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCommentByID/{id}")]
        public IActionResult GetCommentByID(Guid id)
        {
            var cmt = _cmt.GetCommentById(id);

            if (cmt != null)
            {
                var responese = _mapper.Map<CommentVM>(cmt);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet("GetCommentByPropertyID/{id}")]
        public IActionResult GetCommentByPropertyID(Guid id)
        {
            var cmt = _cmt.GetCommentByPropertyID(id);

            if (cmt != null)
            {
                var responese = cmt.Select(cmt => _mapper.Map<CommentVM>(cmt)).ToList();

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpGet]
        [Route("SearchCommentByContent")]
        public ActionResult<Comment> SearchCommentByContent(string searchValue)
        {
            if (_cmt.GetComments() == null)
            {
                return NotFound();
            }
            var cmt = _cmt.SearchComment(searchValue);

            if (cmt == null)
            {
                return NotFound("Don't have this comment ");
            }
            var responese = cmt.Select(cmt => _mapper.Map<CommentVM>(cmt)).ToList();

            return Ok(responese);

        }

        [HttpPost]
        [Route("AddNewComment")]
        public IActionResult AddNew(CommentCreateDTO cmt)
        {
            try
            {

                var newCmt = new CommentCreateDTO
                {
                    CommentId = Guid.NewGuid(),
                    Content = cmt.Content,
                    CreateTime = DateTime.Now,
                    UpdateTime = null,
                    Status = true,
                    PropertyID = cmt.PropertyID,
                    CustomerID = cmt.CustomerID
                };

                var comment = _mapper.Map<Comment>(newCmt);
                _cmt.AddNew(comment);

                return Ok("Create Comment Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateComment/{id}")]
        public IActionResult UpdateComment([FromForm] CommentUpdateDTO cmt, Guid id)
        {
            try
            {
                var existingCmt = _cmt.GetCommentById(id);
                if (existingCmt != null)
                {

                    if (!string.IsNullOrEmpty(cmt.Content))
                    {
                        existingCmt.Content = cmt.Content;
                    }
                    if (cmt.Status.HasValue)
                    {
                        existingCmt.Status = cmt.Status.Value;
                    }

                    existingCmt.UpdateTime = DateTime.Now;
                    _cmt.UpdateComment(existingCmt);

                    return Ok("Update Comment Successfully");

                }

                return NotFound("Comment not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteComment/{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            if (_cmt.GetCommentById(id) == null)
            {
                return NotFound();
            }
            var cmt = _cmt.GetCommentById(id);
            if (cmt == null)
            {
                return NotFound();
            }

            _cmt.ChangeStatus(cmt);


            return Ok("Delete Successfully");
        }



    }
}
