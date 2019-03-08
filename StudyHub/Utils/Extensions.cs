using System;
using System.Collections.Generic;
using System.Linq;
using StudyHub.Models;

namespace StudyHub.Utils
{
    public static class Extensions
    {
        public static IEnumerable<User> Search(this IEnumerable<User> users,
                                            string searchValue)
        {
            return users.Where(x => x.NickName.Contains(searchValue)
                                || x.Email.Contains(searchValue)
                                || x.Mobile.Contains(searchValue));
        }

        public static IEnumerable<User> ApplySort(this IEnumerable<User> users,
                                                string column,
                                                string order)
        {
            bool asc = order == "ascend";
            switch (order)
            {
                case "id": return asc ? users.OrderBy(x => x.Id)
                                    : users.OrderByDescending(x => x.Id);
                case "nickName": return asc ? users.OrderBy(x => x.NickName)
                             : users.OrderByDescending(x => x.NickName);
                case "createdOn": return asc ? users.OrderBy(x => x.CreatedOn)
                         : users.OrderByDescending(x => x.CreatedOn);
                default: return asc ? users.OrderBy(x => x.Id)
                                    : users.OrderByDescending(x => x.Id);
            }
        }
    }
}
