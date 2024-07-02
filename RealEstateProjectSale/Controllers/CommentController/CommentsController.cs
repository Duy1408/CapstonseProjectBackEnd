using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.CommentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _cmt;

        public CommentsController(ICommentServices cmt)
        {
           _cmt = cmt;
        }

        // GET: api/Comments
        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetComments()
        {
          if (_cmt.GetComments() == null)
          {
              return NotFound();
          }
            return _cmt.GetComments().ToList();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(Guid id)
        {
          if (_cmt.GetComments() == null)
          {
              return NotFound();
          }
            var comment = _cmt.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutComment(Guid id, Comment comment)
        {
            if (_cmt.GetComments()==null)
            {
                return BadRequest();
            }


            try
            {
                _cmt.UpdateComment(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_cmt.GetComments() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Comment> PostComment(Comment comment)
        {
          if (_cmt.GetComments() == null)
          {
              return Problem("Entity set 'RealEstateProjectSaleSystemDBContext.Comments'  is null.");
          }
            _cmt.AddNew(comment);

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            if (_cmt.GetComments() == null)
            {
                return NotFound();
            }
            var comment = _cmt.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            _cmt.ChangeStatus(comment);

            return NoContent();
        }

     
    }
}
