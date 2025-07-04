// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Model.VnPay;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository) => _paymentRepository = paymentRepository;

        [HttpGet("GetALl")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await Task.Run(() => _paymentRepository.GetAllPayments());
                if (payments == null || !payments.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all payment failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all payment success",
                    Data = payments
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all payments: {ex.Message}");
            }
        }

        [HttpGet("SumAmount/{type}")]
        public IActionResult GetSumAmount(int type)
        {
            try
            {
                if (type == 1)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get all sum amount by year success",
                        Data = _paymentRepository.GetTotalAmount(type)
                    });
                }
                else if (type == 2)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get all sum amount by quarter success",
                        Data = _paymentRepository.GetTotalAmount(type)
                    });
                }
                else if (type == 3)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Get all sum amount by month success",
                        Data = _paymentRepository.GetTotalAmount(type)
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all sum amount by day now success",
                    Data = _paymentRepository.GetTotalAmount(type)
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving sum amount: {ex.Message}");

            }
        }

        [HttpGet("Response")]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _paymentRepository.PaymentExecute(Request.Query);

            return Ok(new ApiResponse {
                Success = true,
                Message = "payment success",
                Data = response

            }); 
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            try
            {
                var url = _paymentRepository.CreatePaymentUrl(model, HttpContext);
                if (url == null)
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Can't create Url",
                        Data = null
                    });
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Please redirect this link",
                    Data = url
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error create payment url: {ex.Message}");
            }
            
        }

    }
}
