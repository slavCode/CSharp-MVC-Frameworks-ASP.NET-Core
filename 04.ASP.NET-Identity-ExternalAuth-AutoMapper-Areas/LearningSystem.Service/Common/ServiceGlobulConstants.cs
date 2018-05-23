namespace LearningSystem.Service.Common
{
    public static class ServiceGlobulConstants
    {
        // Only 3 for test. 
        public const int ArticlesPageSize = 3;

        public const int CoursesPageSize = 3;

        public const int UsersPageSize = 3;

        public const string Html =
            @"<div align=""center"">
                 <h1>COURSE CERTIFICATE</h1>
                    <br />
                   <small>This is certificate is issued by Learning System</small>
                    <br />
                    <h3>{3}</h3>
                     <small>has successfully completed a course</small>
                      <br />
                     <h3>{0}</h3>
                 <small>{1} - {2}</small>
                      <br/>
                    <h4>with grade <strong>{4}</strong></h4>
                     <br />
                        <div style=""float: right"">
                          Training and inspiration: <strong>{5}</strong>
                        </div>
                        <div style = ""float: left"" >
                          <em>Issue Date: <strong>{6}</strong></em>
                        </div>";
    }
}
