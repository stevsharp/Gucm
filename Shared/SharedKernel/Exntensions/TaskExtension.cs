using System;
using System.Threading.Tasks;

namespace SharedKernel.Exntensions
{
    public static class TaskExtension
    {
        public static async void RunAndForget(this Task task, Action<Exception> onException = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }
    }
}
