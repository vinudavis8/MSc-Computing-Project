using Helper;
using Models;
using Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WorkHiveServices.Interface;

namespace WorkHiveServices
{
    public class ReviewService : IReviewService
    {
        public async Task<bool> CreateReview(ReviewRequest review)
        {
            try
            {
               var result = await ApiHelper.PostAsync<bool>("api/Review/", review);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
