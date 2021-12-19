using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.API.DTOs.CardHistories;
using TM.Domain.Shared;

namespace TM.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserIdGlobal()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
        protected string GetUserNameGlobal()
        {
            return this.User.Claims.First(i => i.Type == "Name").Value;
        }

        protected AddCardHistoryRequest CreateAddCardHistoryRequest(HistoryActionType type)
        {
            return new AddCardHistoryRequest()
            {
                UserId = GetUserIdGlobal(),
                UserName = GetUserNameGlobal(),
                ActionType = type.ToString()
            };
        }

        protected AddCardHistoryRequest CreateUpdateHistoryRequest(HistoryActionType type)
        {
            return new AddCardHistoryRequest()
            {
                UserId = GetUserIdGlobal(),
                UserName = GetUserNameGlobal(),
                ActionType = type.ToString()
            };
        }
    }
}
