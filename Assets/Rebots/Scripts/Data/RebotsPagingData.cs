using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Rebots
{
    [Serializable]
    public class RebotsPagingData<T>
    {
        public T data;

        public int page;
    }
}
