﻿using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IProjectService
    {
        IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync();
    }
}
