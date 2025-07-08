// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Model;
using APIGeneCare.Model.Payment;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("IPN")]
        public IActionResult VNPayIPNCallBack()
        {
            try
            {
                var model = _paymentRepository.VNpayPaymentIPN(Request.Query);
                if (model == null || !model.Success)
                {
                    return Ok(new
                    {
                        RspCode = "99",
                        Message = "Unknow error"
                    });
                }

                if (model.TransactionStatus == "00")
                {
                    return Ok(new
                    {
                        RspCode = "00",
                        Message = "Payment success",
                        Data = model
                    });
                }
                else if (model.TransactionStatus == "01")
                {
                    return Ok(new
                    {
                        RspCode = "01",
                        Message = "Payment pending",
                        Data = model
                    });
                }
                else
                {
                    return Ok(new
                    {
                        RspCode = "02",
                        Message = "Payment failed",
                        Data = model
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    RspCode = "99",
                    Message = "Unknow error"
                });
            }
        }

        [HttpGet("Response")]
        public IActionResult PaymentCallbackVnpay()
        {
            try
            {
                var url = _paymentRepository.VNPayPaymentResponse(Request.Query);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing payment callback: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            try
            {
                var url = _paymentRepository.CreateVNPayPaymentUrl(model, HttpContext);
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
