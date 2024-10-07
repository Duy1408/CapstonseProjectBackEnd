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
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.CommentController
{
    [Route("api/comments")]
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
        [SwaggerOperation(Summary = "Get All Comment", Description = "API này lấy tất cả các bình luận từ cơ sở dữ liệu.")]
        [SwaggerResponse(200, "Trả về danh sách các bình luận", typeof(List<CommentVM>))]
        [SwaggerResponse(500, "Nếu có lỗi từ phía máy chủ")]
        public IActionResult GetAllComment()
        {
            try
            {
                if (_cmt.GetComments() == null)
                {
                    return NotFound(new
                    {
                        message = "Comment not found."
                    });
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get comment by ID")]
        public IActionResult GetCommentByID(Guid id)
        {
            var cmt = _cmt.GetCommentById(id);

            if (cmt != null)
            {
                var responese = _mapper.Map<CommentVM>(cmt);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Comment not found."
            });

        }

        [HttpGet("property/{propertyId}")]
        [SwaggerOperation(Summary = "Get comments by property ID")]
        public IActionResult GetCommentByPropertyID(Guid propertyId)
        {
            var cmt = _cmt.GetCommentByPropertyID(propertyId);

            if (cmt != null)
            {
                var responese = cmt.Select(cmt => _mapper.Map<CommentVM>(cmt)).ToList();

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Comment not found."
            });

        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search comments by content")]
        public ActionResult<Comment> SearchCommentByContent(string searchValue)
        {
            if (_cmt.GetComments() == null)
            {
                return NotFound(new
                {
                    message = "Comment not found."
                });
            }
            var cmt = _cmt.SearchComment(searchValue);

            if (cmt == null || !cmt.Any())
            {
                return NotFound(new
                {
                    message = "Don't have this comment"
                });
            }
            var responese = cmt.Select(cmt => _mapper.Map<CommentVM>(cmt)).ToList();

            return Ok(responese);

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new comment")]
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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update comment by ID")]
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

                    return Ok(new
                    {
                        message = "Update Comment Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Comment not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete comment by ID")]
        public IActionResult DeleteComment(Guid id)
        {
            if (_cmt.GetCommentById(id) == null)
            {
                return NotFound(new
                {
                    message = "Comment not found."
                });
            }
            var cmt = _cmt.GetCommentById(id);
            if (cmt == null)
            {
                return NotFound(new
                {
                    message = "Comment not found."
                });
            }

            _cmt.ChangeStatus(cmt);

            return Ok(new
            {
                message = "Delete Comment Successfully"
            });
        }



    }
}
