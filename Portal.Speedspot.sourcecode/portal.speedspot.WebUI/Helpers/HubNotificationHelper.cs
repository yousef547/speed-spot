using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.ViewModels;
using portal.speedspot.WebUI.Hubs;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Helpers
{
    public class HubNotificationHelper : IHubNotificationHelper
    {
        IHubContext<NotificationHub> _hubContext { get; }
        private readonly IConnectionManager _connectionManager;
        private readonly NotificationsBiz _notificationsBiz;
        private readonly IMapper _mapper;
        private readonly LocalizationService _localizationService;
        private readonly ApplicationUserManager _applicationUserManager;
        public HubNotificationHelper(IHubContext<NotificationHub> context,
            IConnectionManager connectionManager,
            NotificationsBiz notificationsBiz,
            IMapper mapper, LocalizationService localizationService,
            ApplicationUserManager applicationUserManager)
        {
            _hubContext = context;
            _connectionManager = connectionManager;
            _notificationsBiz = notificationsBiz;
            _mapper = mapper;
            _localizationService = localizationService;
            _applicationUserManager = applicationUserManager;
        }

        public async Task SendNotificationToAll(string message)
        {
            await _hubContext.Clients.All.SendAsync("socket", message);
        }

        public async Task SendNotification(NotificationViewModel model)
        {
            string createdByName = (await _applicationUserManager.FindByIdAsync(model.CreatedById)).Name;
            foreach (var notificationUserVM in model.NotificationUserVMs)
            {
                var userConntections = _connectionManager.GetConncetions(notificationUserVM.SendToId);
                if (userConntections != null && userConntections.Count > 0)
                {
                    string typeNotification = "";
                    var refrashFunction = "";
                    switch (model.Type)
                    {
                        case NotificationTypeEnum.Task_Done:
                           typeNotification = _localizationService["Task_Done"].Value;
                            refrashFunction = "getMyTasks";
                            break;
                        case NotificationTypeEnum.Request_New:
                            typeNotification = _localizationService["Request_New"].Value;
                            refrashFunction = "refrashRequest";
                            break;
                        case NotificationTypeEnum.Request_Approved:
                            typeNotification = _localizationService["Request_Approved"].Value;
                            refrashFunction = "refrashRequest";
                            break;
                        case NotificationTypeEnum.Request_Rejected:
                            typeNotification = _localizationService["Request_Rejected"].Value;
                            refrashFunction = "refrashRequest";
                            break;
                        case NotificationTypeEnum.Request_Pending:
                            typeNotification = _localizationService["Request_Pending"].Value ;
                            refrashFunction = "refrashRequest";
                            break;
                        case NotificationTypeEnum.Task_Pending:
                            typeNotification = _localizationService["Task_pending"].Value;
                            refrashFunction = "getMyTasks";
                            break;
                        case NotificationTypeEnum.Task_New:
                            typeNotification = _localizationService["Task_New"].Value;
                            refrashFunction = "getMyTasks";
                            break;
                        case NotificationTypeEnum.Task_Reminder:
                            typeNotification = _localizationService["Task_Reminder"].Value;
                            break;
                        case NotificationTypeEnum.Request_Reminder:
                            typeNotification = _localizationService["Request_Reminder"].Value;
                            break;
                        case NotificationTypeEnum.Opportunity_New:
                            typeNotification = _localizationService["Opportunity_New"].Value;
                            break;
                    }
                    await _hubContext.Clients.Clients(userConntections).SendAsync("ReceiveNotification",
                        JsonConvert.SerializeObject(new
                        {
                            type = typeNotification,
                            createdBy = createdByName,
                            reloadFunction = refrashFunction,
                        }, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                    );
                    notificationUserVM.IsDelivered = true;
                }
                else
                {
                    notificationUserVM.IsDelivered = false;
                }
            }

            var notification = _mapper.Map<Notification>(model);

            await _notificationsBiz.AddAsync(notification);
        }

        public IEnumerable<string> GetOnlineUsers()
        {
            return _connectionManager.OnlineUsers;
        }

        public async Task<Task> SendNotificationParallel(string username)
        {
            HashSet<string> connections = _connectionManager.GetConncetions(username);

            try
            {
                if (connections != null && connections.Count > 0)
                {
                    foreach (var conn in connections)
                    {
                        try
                        {
                            await _hubContext.Clients.Clients(conn).SendAsync("socket", "hello");
                        }
                        catch
                        {
                            throw new Exception("Error: No connections found");
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch
            {
                throw new Exception("Error");
            }
        }
    }
}
