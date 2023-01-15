using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.Constants;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Controllers.Infrastructure;
using portal.speedspot.WebUI.Helpers;
using portal.speedspot.WebUI.Infrastracture;
using portal.speedspot.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DepartmentDocumentBiz _DepartmentDocumentBiz;
        private readonly UserDepartmentsBiz _userDepartmentsBiz;
        private readonly BidBondRequestsBiz _bidBondRequestsBiz;
        private readonly BookTenderRequestsBiz _bookTenderRequestsBiz;
        private readonly TasksBiz _tasksBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly GeneralRequestsBiz _generalRequestsBiz;
        private readonly IHubNotificationHelper _hubNotificationHelper;
        private readonly PerformanceLGRequestsBiz _performanceLGRequestsBiz;
        private readonly FinalLGRequestsBiz _finalLGRequestsBiz;
        public HomeController(IMapper mapper,
            DepartmentDocumentBiz departmentDocumentBiz,
            UserDepartmentsBiz userDepartmentsBiz,
            BidBondRequestsBiz bidBondRequestsBiz,
            BookTenderRequestsBiz bookTenderRequestsBiz,
            DepartmentsBiz departmentsBiz,
            ApplicationUserManager userManager,
            LocalizationService localizationService,
            TasksBiz tasksBiz,
            GeneralRequestsBiz generalRequestsBiz,
            IHubNotificationHelper ihubNotificationHelper,
            PerformanceLGRequestsBiz performanceLGRequestsBiz,
            FinalLGRequestsBiz finalLGRequestsBiz
            ) : base(mapper, userManager, localizationService)
        {
            _DepartmentDocumentBiz = departmentDocumentBiz;
            _userDepartmentsBiz = userDepartmentsBiz;
            _hubNotificationHelper = ihubNotificationHelper;
            _bidBondRequestsBiz = bidBondRequestsBiz;
            _bookTenderRequestsBiz = bookTenderRequestsBiz;
            _tasksBiz = tasksBiz;
            _departmentsBiz = departmentsBiz;
            _generalRequestsBiz = generalRequestsBiz;
            _performanceLGRequestsBiz = performanceLGRequestsBiz;
            _finalLGRequestsBiz = finalLGRequestsBiz;
        }

        //[Authorize(Permissions.Dashboard.View)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }

        #region Requests' Functions
        public async Task<IActionResult> GetPendingRequests(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartmentIds = (await _userDepartmentsBiz.GetByUserAsync(user.Id)).Select(ud => ud.DepartmentId).ToList();
            bool isManager = await _userManager.IsInRoleAsync(user, "Manager");
            bool isAccountant = await _userManager.IsInRoleAsync(user, "Accountant");

            List<BidBondRequest> filteredBidbondTasks = new();
            List<BookTenderRequest> filteredBookTenderTasks = new();
            List<PerformanceLGRequest> filteredPerformanceLGRequests = new();
            List<FinalLGRequest> filteredFinalLGRequests = new();

            if (isAccountant)
            {
                filteredBidbondTasks.AddRange((await _bidBondRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredBookTenderTasks.AddRange((await _bookTenderRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredPerformanceLGRequests.AddRange((await _performanceLGRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredFinalLGRequests.AddRange((await _finalLGRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, user.DepartmentId)).ToList());
            }
            if (isManager)
            {
                filteredBidbondTasks.AddRange((await _bidBondRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredBookTenderTasks.AddRange((await _bookTenderRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredPerformanceLGRequests.AddRange((await _performanceLGRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, user.DepartmentId)).ToList());
                filteredFinalLGRequests.AddRange((await _finalLGRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, user.DepartmentId)).ToList());
            }

            List<GeneralRequest> filteredGeneralRequest = (await _generalRequestsBiz.GetPendingOnUserAsync(user.Id)).ToList();

            // Bidbond Tasks
            List<PendingRequestViewModel> model = new();
            List<PendingRequestViewModel> bidbondModels = _mapper.Map<List<PendingRequestViewModel>>(filteredBidbondTasks);
            model.AddRange(bidbondModels);

            // Book Tender Tasks
            List<PendingRequestViewModel> bookTenderModels = _mapper.Map<List<PendingRequestViewModel>>(filteredBookTenderTasks);
            model.AddRange(bookTenderModels);

            // Geaneral Requests
            List<PendingRequestViewModel> generalRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredGeneralRequest);
            model.AddRange(generalRequestModels);

            // Performance LG Requests
            List<PendingRequestViewModel> performanceLGRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredPerformanceLGRequests);
            model.AddRange(performanceLGRequestModels);

            // Final LG Requests
            List<PendingRequestViewModel> finalLGRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredFinalLGRequests);
            model.AddRange(finalLGRequestModels);

            model = model.OrderBy(o => o.Status == RequestStatusEnum.Rejected).ThenBy(o => o.Deadline).ThenBy(o => o.IsSent).ThenBy(o => o.RequestedDate).ToList();
            model.ForEach(x => x.RequestedDate = x.RequestedDate.AddMinutes(-offset));
            ViewData["IsAccountant"] = isAccountant;
            ViewData["IsManager"] = isManager;
            ViewData["PendingOnMeCreatedById"] = await _userManager.GetAllUsersAsync(false);
            return PartialView("_PendingOnMePartial", model);
        }

        public async Task<IActionResult> ApproveRequest(int requestId, PendingRequestSourceEnum source)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string requestDetails = "";
            string requestedById = "";
            bool result = false;
            switch (source)
            {
                case PendingRequestSourceEnum.OpportunityBidBond:
                    BidBondRequest bidbondRequest = await _bidBondRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["Bidbond"].Value + " " + bidbondRequest.BidBond.Opportunity.Name;
                    requestedById = bidbondRequest.RequestedById;

                    result = await _bidBondRequestsBiz.Approve(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.OpportunityBookTender:
                    BookTenderRequest bookTenderRequest = await _bookTenderRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["BookTender"].Value + " " + bookTenderRequest.BookTender.Opportunity.Name;
                    requestedById = bookTenderRequest.RequestedById;

                    result = await _bookTenderRequestsBiz.Approve(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.GeneralRequest:
                    GeneralRequest generalRequest = await _generalRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = generalRequest.Description;
                    requestedById = generalRequest.RequestedById;

                    result = await _generalRequestsBiz.ApproveAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.PerformanceLG:
                    PerformanceLGRequest performanceLGRequest = await _performanceLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["PerformanceLG"].Value + " " + performanceLGRequest.PerformanceLG.Quotation.Opportunity.Name;
                    requestedById = performanceLGRequest.RequestedById;

                    result = await _performanceLGRequestsBiz.Approve(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.FinalLG:
                    FinalLGRequest finalLGRequest = await _finalLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["FinalLG"].Value + " " + finalLGRequest.FinalLG.Quotation.Opportunity.Name;
                    requestedById = finalLGRequest.RequestedById;

                    result = await _finalLGRequestsBiz.Approve(requestId, user.Id);
                    break;
            }
            if (result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Request_Approved,
                    Route = "/",
                    Details = requestDetails,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = requestedById,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Json(result);
        }

        public async Task<IActionResult> RejectRequest(int requestId, PendingRequestSourceEnum source)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string requestDetails = "";
            string requestedById = "";
            bool result = false;
            switch (source)
            {
                case PendingRequestSourceEnum.OpportunityBidBond:
                    BidBondRequest bidbondRequest = await _bidBondRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["Bidbond"].Value + " " + bidbondRequest.BidBond.Opportunity.Name;
                    requestedById = bidbondRequest.RequestedById;

                    result = await _bidBondRequestsBiz.Reject(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.OpportunityBookTender:
                    BookTenderRequest bookTenderRequest = await _bookTenderRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["BookTender"].Value + " " + bookTenderRequest.BookTender.Opportunity.Name;
                    requestedById = bookTenderRequest.RequestedById;

                    result = await _bookTenderRequestsBiz.Reject(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.GeneralRequest:
                    GeneralRequest generalRequest = await _generalRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = generalRequest.Description;
                    requestedById = generalRequest.RequestedById;

                    result = await _generalRequestsBiz.RejectAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.PerformanceLG:
                    PerformanceLGRequest performanceLGRequest = await _performanceLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["PerformanceLG"].Value + " " + performanceLGRequest.PerformanceLG.Quotation.Opportunity.Name;
                    requestedById = performanceLGRequest.RequestedById;

                    result = await _performanceLGRequestsBiz.Reject(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.FinalLG:
                    FinalLGRequest finalLGRequest = await _finalLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["FinalLG"].Value + " " + finalLGRequest.FinalLG.Quotation.Opportunity.Name;
                    requestedById = finalLGRequest.RequestedById;

                    result = await _finalLGRequestsBiz.Reject(requestId, user.Id);
                    break;
            }
            if (result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Request_Rejected,
                    Route = "/",
                    Details = requestDetails,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = requestedById,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Json(result);
        }

        public async Task<IActionResult> UndoRequest(int requestId, PendingRequestSourceEnum source)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            string requestDetails = "";
            string requestedById = "";
            bool result = false;
            switch (source)
            {
                case PendingRequestSourceEnum.OpportunityBidBond:
                    BidBondRequest bidbondRequest = await _bidBondRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["Bidbond"].Value + " " + bidbondRequest.BidBond.Opportunity.Name;
                    requestedById = bidbondRequest.RequestedById;

                    result = await _bidBondRequestsBiz.Undo(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.OpportunityBookTender:
                    BookTenderRequest bookTenderRequest = await _bookTenderRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["BookTender"].Value + " " + bookTenderRequest.BookTender.Opportunity.Name;
                    requestedById = bookTenderRequest.RequestedById;

                    result = await _bookTenderRequestsBiz.Undo(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.GeneralRequest:
                    GeneralRequest generalRequest = await _generalRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = generalRequest.Description;
                    requestedById = generalRequest.RequestedById;

                    result = await _generalRequestsBiz.UndoAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.PerformanceLG:
                    PerformanceLGRequest performanceLGRequest = await _performanceLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["PerformanceLG"].Value + " " + performanceLGRequest.PerformanceLG.Quotation.Opportunity.Name;
                    requestedById = performanceLGRequest.RequestedById;

                    result = await _performanceLGRequestsBiz.Undo(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.FinalLG:
                    FinalLGRequest finalLGRequest = await _finalLGRequestsBiz.GetByIdAsync(requestId);
                    requestDetails = _localizationService["FinalLG"].Value + " " + finalLGRequest.FinalLG.Quotation.Opportunity.Name;
                    requestedById = finalLGRequest.RequestedById;

                    result = await _finalLGRequestsBiz.Undo(requestId, user.Id);
                    break;
            }
            if (result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Request_Pending,
                    Route = "/",
                    Details = requestDetails,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = requestedById,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Json(result);
        }

        public async Task<IActionResult> GetMyRequests(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<PendingRequestViewModel> model = new();

            var myRequestsBidBond = await _bidBondRequestsBiz.GetMyRequestsAsync(user.Id);
            List<PendingRequestViewModel> bidbondModels = _mapper.Map<List<PendingRequestViewModel>>(myRequestsBidBond);
            model.AddRange(bidbondModels);

            var myRequestsBookTender = await _bookTenderRequestsBiz.GetMyRequestsAsync(user.Id);
            List<PendingRequestViewModel> bookTenderModels = _mapper.Map<List<PendingRequestViewModel>>(myRequestsBookTender);
            model.AddRange(bookTenderModels);

            var myGeneralRequest = await _generalRequestsBiz.GetUserRequestsAsync(user.Id);
            List<PendingRequestViewModel> generalRequestModel = _mapper.Map<List<PendingRequestViewModel>>(myGeneralRequest);
            model.AddRange(generalRequestModel);

            var myRequestsPerformanceLG = await _performanceLGRequestsBiz.GetMyRequestsAsync(user.Id);
            List<PendingRequestViewModel> performanceLGModels = _mapper.Map<List<PendingRequestViewModel>>(myRequestsPerformanceLG);
            model.AddRange(performanceLGModels);

            var myRequestsFinalLG = await _finalLGRequestsBiz.GetMyRequestsAsync(user.Id);
            List<PendingRequestViewModel> finalLGModels = _mapper.Map<List<PendingRequestViewModel>>(myRequestsFinalLG);
            model.AddRange(finalLGModels);

            model = model.OrderBy(o => o.Status == RequestStatusEnum.Rejected).ThenBy(o => o.Deadline).ThenBy(o => o.IsSent).ThenBy(o => o.RequestedDate).ToList();
            model.ForEach(x => x.RequestedDate = x.RequestedDate.AddMinutes(-offset));

            ViewData["MyRequestsAssignedToId"] = (await _userManager.GetAllUsersAsync(false)).ToList();
            return PartialView("_MyRequestsPartial", model);
        }

        public async Task<IActionResult> DeleteRequest(int requestId, PendingRequestSourceEnum source)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = false;
            switch (source)
            {
                case PendingRequestSourceEnum.OpportunityBidBond:
                    result = await _bidBondRequestsBiz.ArchiveAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.OpportunityBookTender:
                    result = await _bookTenderRequestsBiz.ArchiveAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.GeneralRequest:
                    result = await _generalRequestsBiz.ArchiveAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.PerformanceLG:
                    result = await _performanceLGRequestsBiz.ArchiveAsync(requestId, user.Id);
                    break;
                case PendingRequestSourceEnum.FinalLG:
                    result = await _finalLGRequestsBiz.ArchiveAsync(requestId, user.Id);
                    break;
            }
            return Json(result);
        }

        public async Task<IActionResult> GetMyTeamPendingRequests(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var myEmployees = await _userManager.GetMyEmployeesAsync(user.Id);
            ViewData["MyEmployees"] = myEmployees.ToList();

            List<PendingRequestViewModel> model = new();
            foreach (var employee in myEmployees)
            {
                var userDepartmentIds = (await _userDepartmentsBiz.GetByUserAsync(employee.Id)).Select(ud => ud.DepartmentId).ToList();
                bool isManager = await _userManager.IsInRoleAsync(employee, "Manager");
                bool isAccountant = await _userManager.IsInRoleAsync(employee, "Accountant");

                List<BidBondRequest> filteredBidbondTasks = new();
                List<BookTenderRequest> filteredBookTenderTasks = new();
                List<PerformanceLGRequest> filteredPerformanceLGRequests = new();
                List<FinalLGRequest> filteredFinalLGRequests = new();

                if (isAccountant)
                {
                    filteredBidbondTasks.AddRange((await _bidBondRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredBookTenderTasks.AddRange((await _bookTenderRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredPerformanceLGRequests.AddRange((await _performanceLGRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredFinalLGRequests.AddRange((await _finalLGRequestsBiz.GetPendingOnAccountantAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                }
                if (isManager)
                {
                    filteredBidbondTasks.AddRange((await _bidBondRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredBookTenderTasks.AddRange((await _bookTenderRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredPerformanceLGRequests.AddRange((await _performanceLGRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                    filteredFinalLGRequests.AddRange((await _finalLGRequestsBiz.GetPendingOnManagerAsync(userDepartmentIds, employee.DepartmentId)).ToList());
                }

                List<GeneralRequest> filteredGeneralRequest = (await _generalRequestsBiz.GetPendingOnUserAsync(employee.Id)).ToList();

                // Bidbond Tasks
                List<PendingRequestViewModel> bidbondModels = _mapper.Map<List<PendingRequestViewModel>>(filteredBidbondTasks);
                foreach (var task in bidbondModels)
                {
                    task.PendingOnUserId = employee.Id;
                    task.PendingOnName = employee.FirstName + " " + employee.LastName;
                    task.PendingOnProfilePicture = employee.ProfilePicture;
                }
                model.AddRange(bidbondModels);

                // Book Tender Tasks
                List<PendingRequestViewModel> bookTenderModels = _mapper.Map<List<PendingRequestViewModel>>(filteredBookTenderTasks);
                foreach (var task in bookTenderModels)
                {
                    task.PendingOnUserId = employee.Id;
                    task.PendingOnName = employee.FirstName + " " + employee.LastName;
                    task.PendingOnProfilePicture = employee.ProfilePicture;
                }
                model.AddRange(bookTenderModels);

                // General Request
                List<PendingRequestViewModel> generalRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredGeneralRequest);
                foreach (var task in generalRequestModels)
                {
                    task.PendingOnUserId = employee.Id;
                    task.PendingOnName = employee.FirstName + " " + employee.LastName;
                    task.PendingOnProfilePicture = employee.ProfilePicture;
                }
                model.AddRange(generalRequestModels);

                // Performance LG Request
                List<PendingRequestViewModel> performanceLGRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredPerformanceLGRequests);
                foreach (var task in performanceLGRequestModels)
                {
                    task.PendingOnUserId = employee.Id;
                    task.PendingOnName = employee.Name;
                    task.PendingOnProfilePicture = employee.ProfilePicture;
                }

                // Final LG Requests
                List<PendingRequestViewModel> finalLGRequestModels = _mapper.Map<List<PendingRequestViewModel>>(filteredFinalLGRequests);
                foreach (var task in finalLGRequestModels)
                {
                    task.PendingOnUserId = employee.Id;
                    task.PendingOnName = employee.Name;
                    task.PendingOnProfilePicture = employee.ProfilePicture;
                }
            }

            model = model.OrderBy(o => o.Status == RequestStatusEnum.Rejected).ThenBy(o => o.Deadline).ThenBy(o => o.IsSent).ThenBy(o => o.RequestedDate).ToList();
            model.ForEach(x => x.RequestedDate = x.RequestedDate.AddMinutes(-offset));
            return PartialView("_PendingOnTeamPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsRequest(int requestId, int offset = 0)
        {
            var result = await _generalRequestsBiz.GetByIdAsync(requestId);
            if (result == null)
            {
                return NotFound();
            }
            var detailsRequest = _mapper.Map<PendingRequestViewModel>(result);
            detailsRequest.RequestedDate = detailsRequest.RequestedDate.AddMinutes(-offset);
            return PartialView("_DetailsRequestPartial", detailsRequest);
        }
        #endregion

        #region Tasks' Functions
        public async Task<IActionResult> GetAssignedTasks(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);

            var tasks = await _tasksBiz.GetAssignedToUserAsync(user.Id);

            var tasksResult = _mapper.Map<List<TaskViewModel>>(tasks);

            var myManagers = (await _userManager.GetMyManagersAsync(user.Id)).ToList();
            myManagers.Add(user);
            ViewData["MyManagers"] = myManagers;

            if (await _userManager.IsSuperAdmin(user))
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());

            tasksResult.ForEach(x => x.CreatedDate = x.CreatedDate.AddMinutes(-offset));
            return PartialView("_MyTasksPartial", tasksResult);
        }

        public async Task<IActionResult> DoneTask(int taskId)
        {
            var result = await _tasksBiz.DoneAsync(taskId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var task = await _tasksBiz.GetByIdAsync(taskId);
            if (result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Task_Done,
                    Route = "/",
                    Details = task.Title,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = task.CreatedById,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Json(result);
        }

        public async Task<IActionResult> UnDoTask(int taskId)
        {
            var result = await _tasksBiz.UnDoAsync(taskId);
            var task = await _tasksBiz.GetByIdAsync(taskId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (result)
            {
                NotificationViewModel notificationViewModel = new NotificationViewModel
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    Type = NotificationTypeEnum.Task_Pending,
                    Route = "/",
                    Details = task.Title,
                    NotificationUserVMs = new List<NotificationUserViewModel>
                    {
                        new NotificationUserViewModel
                        {
                            SendToId = task.CreatedById,
                            IsRead = false,
                        }
                    }
                };

                await _hubNotificationHelper.SendNotification(notificationViewModel);
            }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsTask(int id, int offset = 0)
        {
            var result = await _tasksBiz.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var detailsTask = _mapper.Map<TaskViewModel>(result);
            detailsTask.CreatedDate = detailsTask.CreatedDate.AddMinutes(-offset);
            return PartialView("_MyDetailsTaskPartial", detailsTask); ;
        }

        public async Task<IActionResult> GetMyTeamTasks(int offset = 0)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var myEmployees = await _userManager.GetMyEmployeesAsync(user.Id);
            var userDepartments = await _userDepartmentsBiz.GetByUserAsync(user.Id);
            ViewData["MyEmployees"] = (await _userManager.GetMyEmployeesAsync(user.Id)).ToList();
            if (await _userManager.IsSuperAdmin(user))
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedAsync();
            else
                ViewData["DepartmentId"] = await _departmentsBiz.GetAllUnarchivedByUserAsync(user.DepartmentId, userDepartments.Select(x => x.DepartmentId).ToList());
            var tasks = await _tasksBiz.GetMyTeamTasksAsync(myEmployees.Select(u => u.Id).ToList());
            var tasksResult = _mapper.Map<List<TaskViewModel>>(tasks);

            tasksResult.ForEach(x => x.CreatedDate = x.CreatedDate.AddMinutes(-offset));
            return PartialView("_MyTeamTasksPartial", tasksResult);
        }

        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = await _tasksBiz.ArchiveAsync(taskId, user.Id);
            return Json(result);
        }

        public async Task<IActionResult> EditTask(int taskId)
        {
            //For Testing
            //var user = _userManager.Users.FirstOrDefault(u => u.Id == "de6263b6-3cb0-4dc5-9324-b9472ca1eff8");

            var user = await _userManager.GetUserAsync(Request.HttpContext.User);
            var myEmployees = await _userManager.GetMyEmployeesAsync(user.Id);
            myEmployees.Add(user);
            ViewData["TaskAssignedToId"] = myEmployees.Select(u => _mapper.Map<UserViewModel>(u)).ToList();
            ViewData["PriorityLevels"] = PriorityList();
            var result = await _tasksBiz.GetByIdAsync(taskId);
            if (result == null)
            {
                return NotFound();
            }
            var detailsTask = _mapper.Map<TaskViewModel>(result);

            return PartialView("_EditDetailsTaskPartial", detailsTask);
        }
        #endregion

        public SelectList PriorityList()
        {
            try
            {
                var list = new List<SelectListItem>();
                foreach (ApplicationEnums.PriorityLevelEnum item in Enum.GetValues(typeof(ApplicationEnums.PriorityLevelEnum)))
                {
                    list.Add(new SelectListItem
                    {
                        Text = _localizationService[Enum.GetName(typeof(ApplicationEnums.PriorityLevelEnum), item)].Value,
                        Value = item.ToString()
                    });
                }
                return new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw;
            }

        }

        public async Task<IActionResult> GetAllDocumentExpiryDate()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDepartments = _mapper.Map<List<UserDepartmentViewModel>>(await _userDepartmentsBiz.GetByUserAsync(user.Id));
            List<int> departmentId = new List<int>();
            var documentsUser = new List<DepartmentDocumentViewModel>();
            foreach (var department in userDepartments)
            {
                departmentId.Add(department.DepartmentId);
            }
            foreach (var document in departmentId)
            {
                var documents = _mapper.Map<List<DepartmentDocumentViewModel>>(await _DepartmentDocumentBiz.GetAllDocumentByDepartmentIdExpiryDate(document));
                foreach (var item in documents)
                {
                    documentsUser.Add(item);
                }
            }

            return PartialView("_showExpiryDateDocuments", documentsUser);

        }
    }
}

