using System;
using System.Collections.Generic;

namespace StudyHub.Models.Dtos
{
    public class UserSearchResultDto
    {
        public IEnumerable<UserDisplayDto> Users;
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
    }
}
