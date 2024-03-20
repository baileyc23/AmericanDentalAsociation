using Ada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.DataAccess.Repository
{
    public interface ICourseRepository: IRepository<Course>
    {
        void Update(Course obj);
    }
}
