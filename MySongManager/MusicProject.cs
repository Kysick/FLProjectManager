using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongManager
{
    public class MusicProject
    {
        private string _projectName;

        private string _projectPath;

        private DateTime _date;


        public MusicProject(string projectName, string projectPath, DateTime date)
        {
            _projectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
            _projectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
            _date = date;
        }

        public string ProjectName { get { return _projectName; } set { _projectName = value; } }

        public string ProjectPath { get => _projectPath; set => _projectPath = value; }
        public DateTime Date { get => _date; set => _date = value; }
       

    }
}
