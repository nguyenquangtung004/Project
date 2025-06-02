using ApiSieuThiSach.models;
using ApiSieuThiSach.sevice;
using Microsoft.AspNetCore.Mvc;

namespace ApiSieuThiSach.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route sẽ là /api/books
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        // POST: api/books
        [HttpPost("web/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBook([FromBody] Book newBook)
        {
            // Model binding và validation sẽ tự động được thực hiện bởi ASP.NET Core
            // dựa trên các DataAnnotations trong Book model.
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateBook called with invalid model state.");
                return BadRequest(ModelState); // Trả về lỗi validation chi tiết
            }

            try
            {
                var createdBook = await _bookService.CreateAsync(newBook);

                if (createdBook == null || string.IsNullOrEmpty(createdBook._idBooks))
                {
                    _logger.LogError("Book creation failed in service for title: {BookTitle}", newBook.Title);
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi xảy ra khi tạo sách.", null));
                }

                _logger.LogInformation("Đã thêm sách với số thứ tự: {BookId}", createdBook.DisplayBookId);
                // Trả về 201 Created cùng với thông tin sách đã tạo và URL để truy cập sách đó (nếu có endpoint GetById)
                // Hiện tại chưa có GetBookById, nên ta chỉ trả về sách đã tạo.
                // return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);

                var apiResponse = new ApiResponse<Book>(
                    code: StatusCodes.Status201Created,
                    message: "Sách đã được tạo thành công.",
                    data: null
                );

                return StatusCode(StatusCodes.Status201Created, apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in CreateBook for title: {BookTitle}", newBook.Title);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi máy chủ xảy ra.", null));
            }
        }


        [HttpGet("getAllBooks")]
        [ProducesResponseType(typeof(ApiResponse<List<Book>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Đang lấy danh sách sách...");
                var books = await _bookService.GetAllBooksAsync();
                if (books == null || books.Count == 0)
                {
                    _logger.LogInformation("Không tìm thấy sách nào.");
                    return Ok(new ApiResponse<List<Book>>(StatusCodes.Status200OK, "Không có sách nào được tìm thấy.", new List<Book>()));
                }
                _logger.LogInformation("Đã lấy thành công danh sách sách.");
                var apiResponse = new ApiResponse<List<Book>>(
                    code: StatusCodes.Status200OK,
                    message: "Danh sách sách đã được lấy thành công.",
                    data: books
                );
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách sách");
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi máy chủ xảy ra khi lấy danh sách sách.", null));
            }
        } 
    }

    
}