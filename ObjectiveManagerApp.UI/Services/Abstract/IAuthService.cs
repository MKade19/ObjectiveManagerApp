﻿using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IAuthService
    {
        Task<AuthData> LoginAsync(InternalUserData user);
        Task LogoutAsync();
        Task RegisterAsync(InternalUserData user);
    }
}
