// Controllers/AuthorsController.cs
using ApiSieuThiSach.models; // Namespace chứa model Author và ApiResponse
using ApiSieuThiSach.sevice; // Namespace chứa AuthorService
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http; // Cần cho StatusCodes

namespace ApiSieuThiSach.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route sẽ là /api/Authors
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(AuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        // POST: api/Authors
        [HttpPost("web/create")]
        [ProducesResponseType(typeof(ApiResponse<Author>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] Author newAuthor)
        {
            // ASP.NET Core tự động thực hiện model binding và validation dựa trên DataAnnotations
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CreateAuthor được gọi với model state không hợp lệ.");
                return BadRequest(new ApiResponse<object>(StatusCodes.Status400BadRequest, "Dữ liệu đầu vào không hợp lệ.", ModelState));
            }

            try
            {
                // Gọi service để tạo tác giả
                var createdAuthor = await _authorService.CreateAuthorAsync(newAuthor);

                if (createdAuthor == null || string.IsNullOrEmpty(createdAuthor._idAuthors))
                {
                    // Lỗi này có thể do DisplayAuthorId đã tồn tại (nếu bạn implement kiểm tra đó trong service)
                    // hoặc một lỗi không mong muốn khác trong service.
                    _logger.LogError("Không thể tạo tác giả trong service cho DisplayAuthorId: {DisplayAuthorId}", newAuthor.DisplayAuthorId);
                    // Bạn có thể trả về một thông báo lỗi cụ thể hơn dựa trên logic trong service
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi xảy ra khi tạo tác giả.", null));
                }

                _logger.LogInformation("Tác giả đã được tạo thành công với MongoDB ID: {MongoId}", createdAuthor._idAuthors);

                var apiResponse = new ApiResponse<Author>(
                    code: StatusCodes.Status201Created,
                    message: "Tác giả đã được tạo thành công.",
                    data: null
                );

                // Trả về 201 Created cùng với thông tin tác giả đã tạo.
                // Nếu bạn có endpoint GetAuthorById, bạn có thể dùng CreatedAtAction.
                // return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor._idAuthors }, apiResponse);
                return StatusCode(StatusCodes.Status201Created, apiResponse);
            }
            catch (InvalidOperationException ex) // Bắt lỗi cụ thể nếu service ném ra (ví dụ: DisplayAuthorId đã tồn tại)
            {
                _logger.LogWarning(ex, "Lỗi nghiệp vụ khi tạo tác giả: {ErrorMessage}", ex.Message);
                return BadRequest(new ApiResponse<object>(StatusCodes.Status400BadRequest, ex.Message, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không xác định trong CreateAuthor cho DisplayAuthorId: {DisplayAuthorId}", newAuthor.DisplayAuthorId);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi máy chủ không xác định xảy ra.", null));
            }
        }

        [HttpGet("getAllAuthors")]
        [ProducesResponseType(typeof(ApiResponse<List<Author>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                _logger.LogInformation("Đang lấy danh sách tất cả tác giả...");

                var authors = await _authorService.GetAllAuthorAsync();

                if (authors == null || authors.Count == 0)
                {
                    _logger.LogInformation("Không tìm thấy tác giả nào.");
                    return Ok(new ApiResponse<List<Author>>(StatusCodes.Status200OK, "Không tìm thấy tác giả nào.", new List<Author>()));
                }

                _logger.LogInformation("Đã lấy thành công {Count} tác giả.", authors.Count);

                return Ok(new ApiResponse<List<Author>>(StatusCodes.Status200OK, "Danh sách tác giả đã được lấy thành công.", authors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tác giả");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(StatusCodes.Status500InternalServerError, "Đã có lỗi máy chủ xảy ra khi lấy danh sách tác giả.", null));
            }
        }
        
        
    }
}