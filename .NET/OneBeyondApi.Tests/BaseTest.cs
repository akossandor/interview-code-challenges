using OneBeyondApi.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests
{
    public abstract class BaseTest : IDisposable
    {
        private bool _isDisposed;

        internal CustomWebAppFactory AppFactory { get; private set; } = new CustomWebAppFactory();

        protected virtual HttpClient CreateHttpClient() => AppFactory.CreateClient();

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            AppFactory.Dispose();
            AppFactory = null;

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
