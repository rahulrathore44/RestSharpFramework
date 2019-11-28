using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.JiraAPI.Request
{
    public class CreateProjectPayload
    {
        public string key { get; set; } = "EX" + new Random().Next(1000);
        public string name { get; set; } = "Example" + new Random().Next(1000);
        public string projectTypeKey { get; set; } = "business";
        public string projectTemplateKey { get; set; } = "com.atlassian.jira-core-project-templates:jira-core-project-management";
        public string description { get; set; } = "Example Project description";
        public string lead { get; set; } = "rahul";
        public string url { get; set; } = "http://atlassian.com";
        public string assigneeType { get; set; } = "PROJECT_LEAD";
        public int avatarId { get; set; } = 10324;
        //public int issueSecurityScheme { get; set; } 
        //public int permissionScheme { get; set; } 
        //public int notificationScheme { get; set; } 
        //public int categoryId { get; set; } 
    }
}
