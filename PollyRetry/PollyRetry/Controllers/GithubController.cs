using Microsoft.AspNetCore.Mvc;
using PollyRetry.Services;
using System.Threading.Tasks;

namespace PollyRetry.Controllers
{
    public class GithubController:ControllerBase
    {
        private readonly IGithubService githubService;

        public GithubController(IGithubService githubService)
        {
            this.githubService = githubService;
        }

        [HttpGet("users/userName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await githubService.GetUserByUserName(userName);
            return user != null ? (IActionResult)Ok(user) : NotFound();
        }

        [HttpGet("users/orgName")]
        public async Task<IActionResult> GetUserInOrg(string orgName)
        {
            var users = await githubService.GetUserFromOrgAsync(orgName);
            return users != null ? (IActionResult)Ok(users) : NotFound();
        }
    }
}
