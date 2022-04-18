using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MySongManager
{
    public class MusicProject
    {
        private string _projectName;

        private string _projectPath;

        private DateTime _date;

        private IStorageFile _storageFile;


        public MusicProject(string projectName, string projectPath, DateTime date)
        {
            _projectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
            _projectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
            _date = date;
        }

        public MusicProject(string projectName, string projectPath, DateTime date, IStorageFile storageFile)
        {
            _projectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
            _projectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
            _date = date;
            StorageFile = storageFile;
        }

        public string ProjectName { get { return _projectName; } set { _projectName = value; } }

        public string ProjectPath { get => _projectPath; set => _projectPath = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public IStorageFile StorageFile { get => _storageFile; set => _storageFile = value; }
    }
}
